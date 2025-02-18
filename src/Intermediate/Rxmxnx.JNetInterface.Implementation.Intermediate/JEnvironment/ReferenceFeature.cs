namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache : IReferenceFeature
	{
		public IDisposable GetSynchronizer(JReferenceObject jObject)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			ImplementationValidationUtilities.ThrowIfDefault(jObject);
			return this.VirtualMachine.CreateSynchronized(this._env, jObject);
		}
		public ObjectLifetime GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
		{
			ObjectLifetime? result = this._objects.GetLifetime(initializer.LocalReference);
			if (result is null)
				return new(this._env, jLocal, initializer.LocalReference)
				{
					Class = initializer.Class,
					IsRealClass = initializer.Class is not null &&
						(initializer.Class.IsFinal || initializer.OverrideClass),
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
			NativeFunctionSetImpl.SingleObjectBuffer buffer = new();
			Span<IObject?> span = NativeFunctionSetImpl.SingleObjectBuffer.GetSpan(ref buffer);
			span[0] = primitive;
			switch (metadata.Signature[0])
			{
				case CommonNames.BooleanSignatureChar:
					jClass = this.GetClass<JBooleanObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.BooleanConstructor, span);
					result = new JBooleanObject(jClass, localRef,
					                            NativeUtilities.Transform<TPrimitive, JBoolean>(in primitive));
					break;
				case CommonNames.ByteSignatureChar:
					jClass = this.GetClass<JByteObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.ByteConstructor, span);
					result = new JByteObject(jClass, localRef,
					                         NativeUtilities.Transform<TPrimitive, JByte>(in primitive));
					break;
				case CommonNames.CharSignatureChar:
					jClass = this.GetClass<JCharacterObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.CharacterConstructor, span);
					result = new JCharacterObject(jClass, localRef,
					                              NativeUtilities.Transform<TPrimitive, JChar>(in primitive));
					break;
				case CommonNames.DoubleSignatureChar:
					jClass = this.GetClass<JDoubleObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.DoubleConstructor, span);
					result = new JDoubleObject(jClass, localRef,
					                           NativeUtilities.Transform<TPrimitive, JDouble>(in primitive));
					break;
				case CommonNames.FloatSignatureChar:
					jClass = this.GetClass<JFloatObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.FloatConstructor, span);
					result = new JFloatObject(jClass, localRef,
					                          NativeUtilities.Transform<TPrimitive, JFloat>(in primitive));
					break;
				case CommonNames.IntSignatureChar:
					jClass = this.GetClass<JIntegerObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.IntegerConstructor, span);
					result = new JIntegerObject(jClass, localRef,
					                            NativeUtilities.Transform<TPrimitive, JInt>(in primitive));
					break;
				case CommonNames.LongSignatureChar:
					jClass = this.GetClass<JLongObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.LongConstructor, span);
					result = new JLongObject(jClass, localRef,
					                         NativeUtilities.Transform<TPrimitive, JLong>(in primitive));
					break;
				case CommonNames.ShortSignatureChar: //S
					jClass = this.GetClass<JShortObject>();
					localRef = this.NewObject(jClass, NativeFunctionSetImpl.ShortConstructor, span);
					result = new JShortObject(jClass, localRef,
					                          NativeUtilities.Transform<TPrimitive, JShort>(in primitive));
					break;
				default:
					IMessageResource resource = IMessageResource.GetInstance();
					throw new InvalidOperationException(resource.NotPrimitiveObject);
			}
			return this.Register(result);
		}
		public TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase
		{
			JClassObject? jClass = jLocal as JClassObject;
			if (typeof(TGlobal) == typeof(JWeak))
			{
				JWeakRef weakRef = jClass is not null ?
					this.CreateWeakGlobalRef(jClass) :
					this.CreateWeakGlobalRef(jLocal);
				return (TGlobal)(Object)this.VirtualMachine.Register(new JWeak(jLocal, weakRef));
			}

			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JGlobal? jGlobal;

			if (jClass is not null)
			{
				jGlobal = this.LoadGlobal(jClass);
			}
			else if (jLocal.InstanceOf<JClassObject>())
			{
				jGlobal = this.LoadGlobal(this.AsClassObjectUnchecked(jLocal));
			}
			else
			{
				JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
				jGlobal = new(jLocal, this.CreateGlobalRef(localRef));
			}

			this.ReloadGlobal(jGlobal, jLocal, jniTransaction);
			return (TGlobal)(Object)this.VirtualMachine.Register(jGlobal);
		}
		public JWeak CreateWeak(JGlobalBase jGlobal)
		{
			JWeakRef weakRef = this.CreateWeakGlobalRef(jGlobal);
			return this.VirtualMachine.Register(new JWeak(jGlobal, weakRef));
		}
		public void LocalLoad(JGlobalBase jGlobal, JLocalObject jLocal)
		{
			if (jLocal.LocalReference != default) return;
			if (jGlobal is JGlobal)
				this.CreateLocalRef(jGlobal.As<JGlobalRef>(), jLocal);
			else
				this.CreateLocalRef(jGlobal.As<JWeakRef>(), jLocal);
		}
		public Boolean Unload(JLocalObject? jLocal)
		{
			if (jLocal is null || jLocal.LocalReference == default) return false;
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);

			Boolean isClass = jLocal is JClassObject;
			JObjectLocalRef localRef = jLocal.LocalReference;
			LocalCache objects = this._objects;
			Boolean isRegistered = this._objects.IsRegistered(localRef);
			if (!this.VirtualMachine.SecureRemove(localRef)) return false;
			if (JLocalObject.FinalizerExecution && isRegistered && objects.IsFromLocalFrame(localRef))
				// Required to avoid finalizer calls JNI when object is at local frame.
				return false;
			try
			{
				this.Unload(isRegistered, localRef);
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
			ImplementationValidationUtilities.ThrowIfProxy(jGlobal);
			Boolean keepReference = false;
			if (EnvironmentCache.IsMainOrDefault(jGlobal)) return false;
			try
			{
				if (jGlobal is JGlobal)
				{
					JGlobalRef globalRef = jGlobal.As<JGlobalRef>();
					if (!this.VirtualMachine.SecureRemove(globalRef))
					{
						keepReference = true;
						return false;
					}
					this.Unload(globalRef);
				}
				else
				{
					JWeakRef weakRef = jGlobal.As<JWeakRef>();
					if (!this.VirtualMachine.SecureRemove(weakRef))
					{
						keepReference = true;
						return false;
					}
					this.Unload(weakRef);
				}
			}
			finally
			{
				if (!keepReference)
				{
					jGlobal.ClearValue();
					this.VirtualMachine.Remove(jGlobal);
				}
			}
			return true;
		}
		public Boolean IsParameter(JLocalObject jLocal) => this._objects.IsParameter(jLocal.LocalReference);
		public unsafe void MonitorEnter(JObjectLocalRef localRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.MonitorEnterInfo);
			JResult result = nativeInterface.MonitorFunctions.MonitorEnter(this.Reference, localRef);
			ImplementationValidationUtilities.ThrowIfInvalidResult(result);
		}
		public unsafe void MonitorExit(JObjectLocalRef localRef)
		{
			JResult result = JResult.Ok;
			if (this._env.IsAttached && this.VirtualMachine.IsAlive)
			{
				ref readonly NativeInterface nativeInterface =
					ref this.GetNativeInterface<NativeInterface>(NativeInterface.MonitorExitInfo);
				result = nativeInterface.MonitorFunctions.MonitorExit(this.Reference, localRef);
			}
			JTrace.MonitorExit(this._env.IsAttached, this.VirtualMachine.IsAlive, result == JResult.Ok, localRef);
			this.CheckJniError();
			ImplementationValidationUtilities.ThrowIfInvalidResult(result);
		}
	}
}