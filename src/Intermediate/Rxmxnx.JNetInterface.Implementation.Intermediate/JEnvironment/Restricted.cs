namespace Rxmxnx.JNetInterface;

partial class JEnvironment : INativeThread, IMainClassLoader
{
	JVirtualMachineRef IMainClassLoader.VirtualMachineRef => this.VirtualMachine.Reference;
	JEnvironmentRef IMainClassLoader.EnvironmentRef => this.Reference;
	JGlobalRef IMainClassLoader.GetMainClassGlobalRef(ITypeInformation typeInformation)
	{
		JClassLocalRef classRef = this._cache.FindMainClass(typeInformation.ClassName, typeInformation.Signature);
		return EnvironmentCache.GetMainClassGlobalRef(this._cache, typeInformation, classRef);
	}
	JRuntimeVersion IMainClassLoader.GetVersion(JClassLocalRef systemClassRef, Boolean initializing)
	{
		if (!initializing) this.CheckJniError();
		JMethodId getPropertyId =
			EnvironmentCache.GetStaticMethodId(this._cache, NativeFunctionSetImpl.GetPropertyDefinition,
			                                   systemClassRef);
		using LocalFrame? _ = !initializing ? new(this, IVirtualMachine.GetVersionCapacity) : default;
		if (getPropertyId != default)
		{
			Decimal jreVersion = this._cache.GetRuntimeVersion(systemClassRef, getPropertyId);
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
		this._cache.ClearException();
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
			this._cache.FindPrimitiveClass(wClassGlobal.As<JClassLocalRef>(), className) :
			this._cache.FindPrimitiveClass(signature);
		return EnvironmentCache.GetMainClassGlobalRef(this._cache, classMetadata, classRef);
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCache.GetFieldId(this._cache, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCache.GetStaticFieldId(this._cache, NativeFunctionSetImpl.PrimitiveTypeDefinition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCache.GetMethodId(this._cache, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCache.GetStaticMethodId(this._cache, definition, classRef);
	LocalCache ILocalCacheOwner.LocalCache
	{
		get => this.LocalCache;
		set => this.SetObjectCache(value);
	}
	void ILocalCacheOwner.CreateLocalFrame(Int32 capacity)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.PushLocalFrameInfo);
		JResult result = nativeInterface.ReferenceFunctions.PushLocalFrame(this.Reference, capacity);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
		this._cache.CheckJniError();
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ILocalCacheOwner.DeleteLocalFrame(LocalFrame frame, JLocalObject? result)
	{
		this._cache.DeleteLocalFrame(result);
		JTrace.DeleteObjectCache(frame.Id, result);
	}
}