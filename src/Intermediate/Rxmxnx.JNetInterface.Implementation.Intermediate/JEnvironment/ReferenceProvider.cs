namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IReferenceProvider
	{
		public JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			IEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			return metadata.Signature[0] switch
			{
				0x90 => //Z
					this.Register(JBooleanObject.Create(env, Unsafe.As<TPrimitive, JBoolean>(ref primitive))),
				0x66 => //B
					this.Register(JByteObject.Create(env, Unsafe.As<TPrimitive, JByte>(ref primitive))),
				0x67 => //C
					this.Register(JCharacterObject.Create(env, Unsafe.As<TPrimitive, JChar>(ref primitive))),
				0x68 => //D
					this.Register(JDoubleObject.Create(env, Unsafe.As<TPrimitive, JDouble>(ref primitive))),
				0x70 => //F
					this.Register(JFloatObject.Create(env, Unsafe.As<TPrimitive, JFloat>(ref primitive))),
				0x73 => //I
					this.Register(JIntegerObject.Create(env, Unsafe.As<TPrimitive, JInt>(ref primitive))),
				0x74 => //J
					this.Register(JLongObject.Create(env, Unsafe.As<TPrimitive, JLong>(ref primitive))),
				0x83 => //S
					this.Register(JShortObject.Create(env, Unsafe.As<TPrimitive, JShort>(ref primitive))),
				_ => throw new InvalidOperationException("Object is not primitive."),
			};
		}
		public TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			if (typeof(TGlobal) == typeof(JWeak))
			{
				NewWeakGlobalRefDelegate newWeakGlobalRef = this.GetDelegate<NewWeakGlobalRefDelegate>();
				JWeakRef weakRef = newWeakGlobalRef(this.Reference, jLocal.As<JObjectLocalRef>());
				if (weakRef == default) this.CheckJniError();
				JWeak jWeak = this.VirtualMachine.Register(new JWeak(jLocal, weakRef));
				return (jWeak as TGlobal)!;
			}
			if (this.LoadGlobal(jLocal as JClassObject) is TGlobal result) return result;
			JObjectMetadata metadata = ILocalObject.CreateMetadata(jLocal);
			if (metadata.ObjectClassName.AsSpan().SequenceEqual(UnicodeClassNames.JClassObjectClassName))
			{
				JClassObject jClass = this.GetClass(UnicodeClassNames.JClassObjectClassName);
				result = (TGlobal)(Object)this.LoadGlobal(jClass);
			}
			else
			{
				JGlobal jGlobal = this.VirtualMachine.Register(
					new JGlobal(this.VirtualMachine, metadata, false,
					            this.CreateGlobalRef(jLocal.As<JObjectLocalRef>())));
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
			try
			{
				this.DeleteLocalRef(localRef);
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
			JEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			try
			{
				if (jGlobal is JGlobal)
					env.DeleteGlobalRef(jGlobal.As<JGlobalRef>());
				else
					env.DeleteWeakGlobalRef(jGlobal.As<JWeakRef>());
			}
			finally
			{
				jGlobal.ClearValue();
				this.VirtualMachine.Remove(jGlobal);
			}
			return true;
		}
		public Boolean IsParameter(JLocalObject jLocal) => throw new NotImplementedException();

		/// <summary>
		/// Registers a <typeparamref name="TObject"/> in current <see cref="IEnvironment"/> instance.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jObject">A <see cref="IDataType{TObject}"/> instance.</param>
		/// <returns>Registered <see cref="IDataType{TObject}"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jObject))]
		public TObject? Register<TObject>(TObject? jObject) where TObject : IDataType<TObject>
		{
			if (jObject is JClassObject jClass)
			{
				this._classes[jClass.Hash] = jClass;
				this.VirtualMachine.LoadGlobal(jClass);
			}
			JLocalObject? jLocal = jObject as JLocalObject;
			ValidationUtilities.ThrowIfDummy(jLocal);
			if (!JObject.IsNullOrDefault(jLocal))
				this._objects[jLocal.As<JObjectLocalRef>()] = (jLocal as ILocalObject).Lifetime.GetCacheable();
			return jObject;
		}

		/// <summary>
		/// Applies cast from <see cref="JLocalObject"/> to <typeparamref name="TObject"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <typeparamref name="TObject"/> instance.</returns>
		private TObject? Cast<TObject>(JLocalObject? jLocal) where TObject : IDataType<TObject>
		{
			if (jLocal is null || jLocal.IsDefault) return default;
			if (typeof(TObject) == typeof(JLocalObject))
				return this.Register((TObject)(Object)jLocal);
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)IDataType.GetMetadata<TObject>();
			TObject result = (TObject)(Object)metadata.ParseInstance(jLocal);
			jLocal.Dispose();
			return this.Register(result);
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
			switch (result.IsDefault)
			{
				case true when localRef == default:
					try
					{
						localRef = jClass.Name.WithSafeFixed(this, JEnvironmentCache.FindClass).Value;
						this.ReloadGlobal(result, localRef);
					}
					finally
					{
						if (localRef != default) this.DeleteLocalRef(localRef);
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