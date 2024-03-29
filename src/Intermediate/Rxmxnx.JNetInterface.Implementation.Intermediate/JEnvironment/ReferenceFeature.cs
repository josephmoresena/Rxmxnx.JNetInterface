namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache : IReferenceFeature
	{
		public IDisposable GetSynchronizer(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			ValidationUtilities.ThrowIfDefault(jObject);
			return this.VirtualMachine.CreateSynchronized(this._env, jObject);
		}
		public ObjectLifetime GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
		{
			ObjectLifetime? result = this._objects.GetLifetime(initializer.LocalReference);
			if (result is null)
				return new(this._env, jLocal, initializer.LocalReference)
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
				JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
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
				JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
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
			if (!this.VirtualMachine.SecureRemove(localRef)) return false;
			try
			{
				this._env.DeleteLocalRef(localRef);
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
			if (jGlobal.IsDefault || this.IsMainGlobal(jGlobal as JGlobal)) return false;
			try
			{
				if (jGlobal is JGlobal)
				{
					JGlobalRef globalRef = jGlobal.As<JGlobalRef>();
					if (!this.VirtualMachine.SecureRemove(globalRef)) return false;
					this._env.DeleteGlobalRef(globalRef);
				}
				else
				{
					JWeakRef weakRef = jGlobal.As<JWeakRef>();
					if (!this.VirtualMachine.SecureRemove(weakRef)) return false;
					this._env.DeleteWeakGlobalRef(weakRef);
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
		public void MonitorEnter(JObjectLocalRef localRef)
		{
			MonitorEnterDelegate monitorEnter = this.GetDelegate<MonitorEnterDelegate>();
			ValidationUtilities.ThrowIfInvalidResult(monitorEnter(this.Reference, localRef));
		}
		public void MonitorExit(JObjectLocalRef localRef)
		{
			MonitorExitDelegate monitorExit = this.GetDelegate<MonitorExitDelegate>();
			ValidationUtilities.ThrowIfInvalidResult(monitorExit(this.Reference, localRef));
		}
	}
}