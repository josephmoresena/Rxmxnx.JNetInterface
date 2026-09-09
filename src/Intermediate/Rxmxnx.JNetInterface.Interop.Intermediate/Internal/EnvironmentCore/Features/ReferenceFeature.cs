namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCore : IReferenceFeature
{
	public IDisposable GetSynchronizer(JReferenceObject jObject)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jObject);
		ImplementationValidationUtilities.ThrowIfDefault(jObject);
		return this.Host.MemoryManager.CreateSynchronized(this._env, jObject);
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
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	public unsafe JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, INativeDataType<TPrimitive>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TPrimitive>();
		JClassObject jClass;
		JConstructorDefinition definition;
		delegate*<JClassObject, JObjectLocalRef, void*, JLocalObject> constructor;
		switch (metadata.Signature[0])
		{
			case CommonNames.BooleanSignatureChar:
				jClass = this.GetClass<JBooleanObject>();
				definition = NativeFunctionSetImpl.BooleanConstructor;
				constructor = &BooleanConstructor;
				break;
			case CommonNames.ByteSignatureChar:
				jClass = this.GetClass<JByteObject>();
				definition = NativeFunctionSetImpl.ByteConstructor;
				constructor = &ByteConstructor;
				break;
			case CommonNames.CharSignatureChar:
				jClass = this.GetClass<JCharacterObject>();
				definition = NativeFunctionSetImpl.CharacterConstructor;
				constructor = &CharacterConstructor;
				break;
			case CommonNames.DoubleSignatureChar:
				jClass = this.GetClass<JDoubleObject>();
				definition = NativeFunctionSetImpl.DoubleConstructor;
				constructor = &DoubleConstructor;
				break;
			case CommonNames.FloatSignatureChar:
				jClass = this.GetClass<JFloatObject>();
				definition = NativeFunctionSetImpl.FloatConstructor;
				constructor = &FloatConstructor;
				break;
			case CommonNames.IntSignatureChar:
				jClass = this.GetClass<JIntegerObject>();
				definition = NativeFunctionSetImpl.IntegerConstructor;
				constructor = &IntegerConstructor;
				break;
			case CommonNames.LongSignatureChar:
				jClass = this.GetClass<JLongObject>();
				definition = NativeFunctionSetImpl.LongConstructor;
				constructor = &LongConstructor;
				break;
			case CommonNames.ShortSignatureChar: //S
				jClass = this.GetClass<JShortObject>();
				definition = NativeFunctionSetImpl.ShortConstructor;
				constructor = &ShortConstructor;
				break;
			default:
				IMessageResource resource = IMessageResource.GetInstance();
				throw new InvalidOperationException(resource.NotPrimitiveObject);
		}
		ref WrapperConstructorArg<TPrimitive> refArgs =
			ref Unsafe.As<TPrimitive, WrapperConstructorArg<TPrimitive>>(ref primitive);
		JObjectLocalRef localRef = this.NewObject(jClass, definition, in refArgs);
		JLocalObject result = constructor(jClass, localRef, Unsafe.AsPointer(ref primitive));
		return this.Register(result);

		#region StaticConstructors
		static JLocalObject BooleanConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JBooleanObject(jClass, localRef, (Byte)Unsafe.AsRef<TPrimitive>(pointer) == JBoolean.TrueValue);
		static JLocalObject ByteConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JByteObject(jClass, localRef, (SByte)Unsafe.AsRef<TPrimitive>(pointer));
		static JLocalObject CharacterConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JCharacterObject(jClass, localRef, (Char)Unsafe.AsRef<TPrimitive>(pointer));
		static JLocalObject DoubleConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JDoubleObject(jClass, localRef, (Double)Unsafe.AsRef<TPrimitive>(pointer));
		static JLocalObject FloatConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JFloatObject(jClass, localRef, (Single)Unsafe.AsRef<TPrimitive>(pointer));
		static JLocalObject IntegerConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JIntegerObject(jClass, localRef, (Int32)Unsafe.AsRef<TPrimitive>(pointer));
		static JLocalObject LongConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JLongObject(jClass, localRef, (Int64)Unsafe.AsRef<TPrimitive>(pointer));
		static JLocalObject ShortConstructor(JClassObject jClass, JObjectLocalRef localRef, void* pointer)
			=> new JShortObject(jClass, localRef, (Int16)Unsafe.AsRef<TPrimitive>(pointer));
		#endregion
	}
	public TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase
	{
		JClassObject? jClass = jLocal as JClassObject;
		if (typeof(TGlobal) == typeof(JWeak))
		{
			JWeakRef weakRef = jClass is not null ? this.CreateWeakGlobalRef(jClass) : this.CreateWeakGlobalRef(jLocal);
			return (TGlobal)(Object)this.Host.GlobalManager.Register(new JWeak(jLocal, weakRef));
		}

		ImplementationValidationUtilities.ThrowIfProxy(jLocal);
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(1);
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
		return (TGlobal)(Object)this.Host.GlobalManager.Register(jGlobal);
	}
	public JWeak CreateWeak(JGlobalBase jGlobal)
	{
		JWeakRef weakRef = this.CreateWeakGlobalRef(jGlobal);
		return this.Host.GlobalManager.Register(new JWeak(jGlobal, weakRef));
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
		if (!this.Host.MemoryManager.SecureRemove(localRef)) return false;
		if (JLocalObject.FinalizerExecution && isRegistered && objects.IsFromLocalFrame(localRef))
			// Required to avoid finalizer calls JNI when object is at local frame.
			return false;
		// Only registered references are unloaded and removed.
		try
		{
			this.Unload(isRegistered, localRef);
		}
		finally
		{
			this.Remove(isRegistered, jLocal);
			jLocal.ClearValue();
		}
		return !isClass;
	}
	public Boolean Unload(JGlobalBase jGlobal)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jGlobal);
		Boolean keepReference = false;
		if (EnvironmentCore.IsMainOrDefault(jGlobal)) return false;
		try
		{
			if (jGlobal is JGlobal)
			{
				JGlobalRef globalRef = jGlobal.As<JGlobalRef>();
				if (!this.Host.MemoryManager.SecureRemove(globalRef))
				{
					keepReference = true;
					return false;
				}
				this.Unload(globalRef);
			}
			else
			{
				JWeakRef weakRef = jGlobal.As<JWeakRef>();
				if (!this.Host.MemoryManager.SecureRemove(weakRef))
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
				this.Host.GlobalManager.Remove(jGlobal);
				jGlobal.ClearValue();
			}
		}
		return true;
	}
	public Boolean IsParameter(JLocalObject jLocal) => this._objects.IsParameter(jLocal.LocalReference);
	public void MonitorEnter(JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.MonitorEnterInfo);
		JResult result = nativeInterface.MonitorFunctions.MonitorEnter(this.Reference, localRef);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
	}
	public void MonitorExit(JObjectLocalRef localRef)
	{
		JResult result = JResult.Ok;
		if (this._env.IsAttached && this.Host.IsRunning)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.MonitorExitInfo);
			result = nativeInterface.MonitorFunctions.MonitorExit(this.Reference, localRef);
		}
		JTrace.MonitorExit(this._env.IsAttached, this.Host.IsRunning, result == JResult.Ok, localRef);
		this.CheckJniError();
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
	}
}