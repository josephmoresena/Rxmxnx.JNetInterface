namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record JEnvironmentCache : IReferenceFeature
	{
		public Boolean RealEnvironment => true;

		public IDisposable GetSynchronizer(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			ValidationUtilities.ThrowIfDefault(jObject);
			JEnvironment env = this._mainClasses.Environment;
			return this.VirtualMachine.CreateSynchronized(env, jObject);
		}

		public ObjectLifetime GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
		{
			IEnvironment env = this._mainClasses.Environment;
			ObjectLifetime? result = this._objects.GetLifetime(initializer.LocalReference);
			if (result is null)
				return new(env, jLocal, initializer.LocalReference)
				{
					Class = initializer.Class,
					IsRealClass = initializer.Class is not null && initializer.Class.IsFinal.GetValueOrDefault(),
				};
			result.Load(jLocal);
			if (!result.IsRealClass && initializer.OverrideClass && initializer.Class is not null)
				result.SetClass(initializer.Class);
			return result;
		}
		public JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			JClassObject jClass;
			JObjectLocalRef localRef;
			JLocalObject result;
			switch (metadata.Signature[0])
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					jClass = this.GetClass<JBooleanObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.BooleanConstructor, primitive);
					result = new JBooleanObject(jClass, localRef,
					                            NativeUtilities.Transform<TPrimitive, JBoolean>(in primitive));
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					jClass = this.GetClass<JByteObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.ByteConstructor, primitive);
					result = new JByteObject(jClass, localRef,
					                         NativeUtilities.Transform<TPrimitive, JByte>(in primitive));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					jClass = this.GetClass<JCharacterObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.CharacterConstructor, primitive);
					result = new JCharacterObject(jClass, localRef,
					                              NativeUtilities.Transform<TPrimitive, JChar>(in primitive));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					jClass = this.GetClass<JDoubleObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.DoubleConstructor, primitive);
					result = new JDoubleObject(jClass, localRef,
					                           NativeUtilities.Transform<TPrimitive, JDouble>(in primitive));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					jClass = this.GetClass<JFloatObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.FloatConstructor, primitive);
					result = new JFloatObject(jClass, localRef,
					                          NativeUtilities.Transform<TPrimitive, JFloat>(in primitive));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					jClass = this.GetClass<JIntegerObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.IntegerConstructor, primitive);
					result = new JIntegerObject(jClass, localRef,
					                            NativeUtilities.Transform<TPrimitive, JInt>(in primitive));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					jClass = this.GetClass<JLongObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.LongConstructor, primitive);
					result = new JLongObject(jClass, localRef,
					                         NativeUtilities.Transform<TPrimitive, JLong>(in primitive));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar: //S
					jClass = this.GetClass<JShortObject>();
					localRef = this.NewObject(jClass, InternalFunctionCache.ShortConstructor, primitive);
					result = new JShortObject(jClass, localRef,
					                          NativeUtilities.Transform<TPrimitive, JShort>(in primitive));
					break;
				default:
					throw new InvalidOperationException("Object is not primitive.");
			}
			return this.Register(result);
		}
		public TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			if (typeof(TGlobal) == typeof(JWeak))
			{
				NewWeakGlobalRefDelegate newWeakGlobalRef = this.GetDelegate<NewWeakGlobalRefDelegate>();
				JObjectLocalRef localRef = jniTransaction.Add(jLocal);
				JWeakRef weakRef = newWeakGlobalRef(this.Reference, localRef);
				if (weakRef == default) this.CheckJniError();
				JWeak jWeak = this.VirtualMachine.Register(new JWeak(jLocal, weakRef));
				return (jWeak as TGlobal)!;
			}
			if (this.LoadGlobal(jLocal as JClassObject) is TGlobal result) return result;
			ObjectMetadata metadata = ILocalObject.CreateMetadata(jLocal);
			if (metadata.ObjectClassName.AsSpan().SequenceEqual(UnicodeClassNames.ClassObject))
			{
				JClassObject jClass = this.GetClass(UnicodeClassNames.ClassObject);
				result = (TGlobal)(Object)this.LoadGlobal(jClass);
			}
			else
			{
				JObjectLocalRef localRef = jniTransaction.Add(jLocal);
				JGlobal jGlobal = this.VirtualMachine.Register(
					new JGlobal(this.VirtualMachine, metadata, false, this.CreateGlobalRef(localRef)));
				result = (TGlobal)(Object)jGlobal;
			}
			return result;
		}
		public Boolean Unload(JLocalObject? jLocal)
		{
			if (jLocal is null) return false;
			ValidationUtilities.ThrowIfDummy(jLocal);
			Boolean isClass = jLocal is JClassObject;
			JObjectLocalRef localRef = jLocal.InternalReference;
			JEnvironment env = this._mainClasses.Environment;
			if (!this.VirtualMachine.SecureRemove(localRef)) return false;
			try
			{
				env.DeleteLocalRef(localRef);
			}
			finally
			{
				this.Remove(jLocal);
				jLocal.ClearValue();
			}
			return !isClass;
		}
		public Boolean Unload(JGlobalBase jGlobal)
		{
			ValidationUtilities.ThrowIfDummy(jGlobal);
			if (jGlobal.IsDefault || this._mainClasses.IsMainGlobal(jGlobal as JGlobal)) return false;
			JEnvironment env = this._mainClasses.Environment;
			try
			{
				if (jGlobal is JGlobal)
				{
					JGlobalRef globalRef = jGlobal.As<JGlobalRef>();
					if (!this.VirtualMachine.SecureRemove(globalRef)) return false;
					env.DeleteGlobalRef(globalRef);
				}
				else
				{
					JWeakRef weakRef = jGlobal.As<JWeakRef>();
					if (!this.VirtualMachine.SecureRemove(weakRef)) return false;
					env.DeleteWeakGlobalRef(weakRef);
				}
			}
			finally
			{
				jGlobal.ClearValue();
				this.VirtualMachine.Remove(jGlobal);
			}
			return true;
		}
		public Boolean IsParameter(JLocalObject jLocal) => this._objects.IsParameter(jLocal.InternalReference);

		/// <summary>
		/// Registers a <typeparamref name="TObject"/> in current <see cref="IEnvironment"/> instance.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jObject">A <see cref="IDataType{TObject}"/> instance.</param>
		/// <returns>Registered <see cref="IDataType{TObject}"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jObject))]
		public TObject? Register<TObject>(TObject? jObject) where TObject : IDataType<TObject>
		{
			ValidationUtilities.ThrowIfDummy(jObject as JReferenceObject);
			this.LoadClass(jObject as JClassObject);
			JLocalObject? jLocal = jObject as JLocalObject;
			if (!JObject.IsNullOrDefault(jLocal))
				this._objects[jLocal.As<JObjectLocalRef>()] = jLocal.Lifetime.GetCacheable();
			return jObject;
		}

		/// <summary>
		/// Applies cast from <see cref="JLocalObject"/> to <typeparamref name="TObject"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="register">Indicates whether object must be registered.</param>
		/// <returns>A <typeparamref name="TObject"/> instance.</returns>
		private TObject? Cast<TObject>(JLocalObject? jLocal, Boolean register = true) where TObject : IDataType<TObject>
		{
			if (jLocal is null || jLocal.IsDefault) return default;
			if (typeof(TObject) == typeof(JLocalObject))
				return register ? this.Register((TObject)(Object)jLocal) : (TObject)(Object)jLocal;
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TObject>();
			TObject result = (TObject)(Object)metadata.ParseInstance(jLocal);
			jLocal.Dispose();
			return register ? this.Register(result) : result;
		}
		/// <summary>
		/// Loads <see cref="JGlobal"/> instance for <paramref name="jClass"/>
		/// </summary>
		/// <param name="jClass">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <see cref="JGlobal"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jClass))]
		private JGlobal? LoadGlobal(JClassObject? jClass)
		{
			if (jClass is null) return default;
			JGlobal result = this.VirtualMachine.LoadGlobal(jClass);
			JObjectLocalRef localRef = jClass.As<JObjectLocalRef>();
			JEnvironment env = this._mainClasses.Environment;
			switch (result.IsDefault)
			{
				case true when localRef == default:
					try
					{
						localRef = this.FindClass(jClass).Value;
						this.ReloadGlobal(result, localRef);
					}
					finally
					{
						if (localRef != default) env.DeleteLocalRef(localRef);
					}
					break;
				case true:
					this.ReloadGlobal(result, localRef);
					break;
			}
			return result;
		}
		/// <summary>
		/// Reloads <paramref name="jGlobal"/> using <paramref name="localRef"/>.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> to reload.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		private void ReloadGlobal(JGlobal jGlobal, JObjectLocalRef localRef)
		{
			JGlobalRef globalRef = this.CreateGlobalRef(localRef);
			jGlobal.SetValue(globalRef);
		}
	}
}