namespace Rxmxnx.JNetInterface;

partial class JEnvironment : INativeThread, IMainClassLoader
{
	JVirtualMachineRef IMainClassLoader.VirtualMachineRef => this.VirtualMachine.Reference;
	JEnvironmentRef IMainClassLoader.EnvironmentRef => this.Reference;
	JGlobalRef IMainClassLoader.GetMainClassGlobalRef(ITypeInformation typeInformation)
	{
		JClassLocalRef classRef = this._core.FindMainClass(typeInformation.ClassName, typeInformation.Signature);
		return EnvironmentCore.GetMainClassGlobalRef(this._core, typeInformation, classRef);
	}
	JRuntimeVersion IMainClassLoader.GetVersion(JClassLocalRef systemClassRef, Boolean initializing)
	{
		if (!initializing) this.CheckJniError();
		JMethodId getPropertyId =
			EnvironmentCore.GetStaticMethodId(this._core, NativeFunctionSetImpl.GetPropertyDefinition, systemClassRef);
		using LocalFrame? _ = !initializing ? new(this, IVirtualMachine.GetVersionCapacity) : default;
		if (getPropertyId != default)
		{
			Decimal jreVersion = this._core.GetRuntimeVersion(systemClassRef, getPropertyId);
			switch (jreVersion)
			{
				case < 1:
					break;
				case < 2:
					return JRuntimeVersion.SEd0 + (Int32)(10 * (jreVersion - 1.0m));
				default:
					return (JRuntimeVersion)((Int32)JRuntimeVersion.SEd0 * jreVersion);
			}
		}
		this._core.ClearException();
		// If it was not possible to determine the JRE version, the JNI version is assumed.
		if (JavaStandardFeature.GetRuntimeVersion() is { } jre) return jre;
		if (AndroidFeature.IsFixedAndroid) return JRuntimeVersion.J6; // Android runtime is based on JRE 1.6.
		return (JRuntimeVersion)this.Version;
	}
	JGlobalRef IMainClassLoader.GetPrimitiveMainClassGlobalRef(ClassObjectMetadata classMetadata,
		JGlobalBase? wClassGlobal)
	{
		Byte signature = classMetadata.ClassSignature[0];
		String className = ClassNameHelper.GetPrimitiveClassName(signature);
		JClassLocalRef classRef = !JObject.IsNullOrDefault(wClassGlobal) ?
			this._core.FindPrimitiveClass(wClassGlobal.As<JClassLocalRef>(), className) :
			this._core.FindPrimitiveClass(signature);
		return EnvironmentCore.GetMainClassGlobalRef(this._core, classMetadata, classRef);
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetFieldId(this._core, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetStaticFieldId(this._core, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetMethodId(this._core, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetStaticMethodId(this._core, definition, classRef);
	LocalCache ILocalCacheOwner.LocalCache
	{
		get => this.LocalCache;
		set => this.SetObjectCache(value);
	}
	void ILocalCacheOwner.CreateLocalFrame(Int32 capacity)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._core.GetNativeInterface<NativeInterface>(NativeInterface.PushLocalFrameInfo);
		JResult result = nativeInterface.ReferenceFunctions.PushLocalFrame(this.Reference, capacity);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
		this._core.CheckJniError();
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ILocalCacheOwner.DeleteLocalFrame(LocalFrame frame, JLocalObject? result)
	{
		this._core.DeleteLocalFrame(result);
		JTrace.DeleteObjectCache(frame.Id, result);
	}
}