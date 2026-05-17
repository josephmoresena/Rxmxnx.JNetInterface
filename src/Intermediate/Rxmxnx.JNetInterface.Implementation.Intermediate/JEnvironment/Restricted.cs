namespace Rxmxnx.JNetInterface;

partial class JEnvironment : INativeThread<JEnvironment>, IMainClassLoader
{
	JVirtualMachineRef IMainClassLoader.VirtualMachineRef => this.VirtualMachine.Reference;
	JEnvironmentRef IMainClassLoader.EnvironmentRef => this.Reference;
	JGlobalRef IMainClassLoader.GetMainClassGlobalRef(ITypeInformation typeInformation)
		=> this._m.GetMainClassGlobalRef(typeInformation);
	JRuntimeVersion IMainClassLoader.GetVersion(JClassLocalRef systemClassRef, Boolean initializing)
	{
		if (!initializing) this._m.Core.CheckJniError();
		JMethodId getPropertyId =
			EnvironmentCore.GetStaticMethodId(this._m.Core, NativeFunctionSetImpl.GetPropertyDefinition,
			                                  systemClassRef);
		using LocalFrame? _ = !initializing ? new(this, IVirtualMachine.GetVersionCapacity) : default;
		if (getPropertyId != default)
		{
			Decimal jreVersion = this._m.Core.GetRuntimeVersion(systemClassRef, getPropertyId);
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
		this._m.Core.ClearException();
		// If it was not possible to determine the JRE version, the JNI version is assumed.
		if (JavaStandardFeature.GetRuntimeVersion() is { } jre) return jre;
		if (AndroidFeature.IsFixedAndroid) return JRuntimeVersion.J6; // Android runtime is based on JRE 1.6.
		return (JRuntimeVersion)this.Version;
	}
	JGlobalRef IMainClassLoader.GetPrimitiveMainClassGlobalRef(ClassObjectMetadata classMetadata,
		JGlobalBase? wClassGlobal)
		=> this._m.GetPrimitiveMainClassGlobalRef(classMetadata, wClassGlobal);
	IUnsafeMemoryManager INativeThread.MemoryManager => this._m.Core;
	ClassCache INativeThread.ClassCache => this.ClassCache;
	Boolean INativeThread.IsOwned => this._m.Core.IsOwned;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void INativeThread.LoadClass(JClassObject jClass) => this._m.Core.LoadClass(jClass);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void INativeThread.CheckJniError() => this._m.Core.CheckJniError();
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JClassObject INativeThread.GetReferenceTypeClass(JClassLocalRef classRef, Boolean keepReference)
		=> this.GetReferenceTypeClass(classRef, keepReference);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetFieldId(this._m.Core, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JFieldId IAccessibleManager.GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetStaticFieldId(this._m.Core, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetMethodId(this._m.Core, definition, classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JMethodId IAccessibleManager.GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetStaticMethodId(this._m.Core, definition, classRef);
	LocalCache ILocalCacheOwner.LocalCache
	{
		get => this._m.LocalCache;
		set => this._m.LocalCache = value;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ILocalCacheOwner.CreateLocalFrame(Int32 capacity) => this._m.CreateLocalFrame(capacity);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ILocalCacheOwner.DeleteLocalFrame(LocalFrame frame, JLocalObject? result)
		=> this._m.DeleteLocalFrame(frame, result);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ILocalCacheOwner.FreeReferences() => this._m.Core.FreeReferences();

	static JEnvironment INativeThread<JEnvironment>.Create(IVirtualMachineHost host, JEnvironmentRef envRef)
		=> new(host, envRef);
	static JEnvironment INativeThread<JEnvironment>.Create(IVirtualMachineHost host, JEnvironmentRef envRef,
		ThreadCreationArgs args)
		=> new JThread(host, envRef, args);
	static IThread INativeThread<JEnvironment>.Create(JEnvironment nativeThread, Boolean newThread)
		=> new JThread(nativeThread, newThread);
}