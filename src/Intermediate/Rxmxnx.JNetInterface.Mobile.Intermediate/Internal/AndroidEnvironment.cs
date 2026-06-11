namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal implementation of <see cref="IEnvironment"/>.
/// </summary>
internal partial class AndroidEnvironment : INativeThread<AndroidEnvironment>
{
	/// <summary>
	/// <see cref="EnvironmentValue"/> instance.
	/// </summary>
	private readonly EnvironmentValue _m;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	private AndroidEnvironment(IVirtualMachineHost host, JEnvironmentRef envRef)
		=> this._m = new EnvironmentCore(host, this, envRef);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instancia.</param>
	private AndroidEnvironment(EnvironmentCore core) => this._m = core;

	JRuntimeVersion IMainClassLoader.GetVersion(JClassLocalRef systemClassRef, Boolean initializing)
		=> JRuntimeVersion.J6;
	JGlobalRef IMainClassLoader.GetPrimitiveMainClassGlobalRef(ClassObjectMetadata classMetadata,
		JGlobalBase? wClassGlobal)
		=> this._m.GetPrimitiveMainClassGlobalRef(classMetadata, wClassGlobal);
	JGlobalRef IMainClassLoader.GetMainClassGlobalRef(ITypeInformation typeInformation)
		=> this._m.GetMainClassGlobalRef(typeInformation);

	JFieldId IAccessibleManager.GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetFieldId(this._m.Core, definition, classRef);
	JFieldId IAccessibleManager.GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetStaticFieldId(this._m.Core, definition, classRef);
	JMethodId IAccessibleManager.GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetMethodId(this._m.Core, definition, classRef);
	JMethodId IAccessibleManager.GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
		=> EnvironmentCore.GetStaticMethodId(this._m.Core, definition, classRef);
	void ILocalCacheOwner.CreateLocalFrame(Int32 capacity) => this._m.CreateLocalFrame(capacity);
	void ILocalCacheOwner.DeleteLocalFrame(LocalFrame frame, JLocalObject? result)
		=> this._m.DeleteLocalFrame(frame, result);
	void ILocalCacheOwner.FreeReferences() => this._m.Core.FreeReferences();
	void ILocalCacheOwner.ReloadClass(JClassObject jClass) => this._m.Core.ReloadClass(jClass);
	static AndroidEnvironment INativeThread<AndroidEnvironment>.Create(IVirtualMachineHost host, JEnvironmentRef envRef)
		=> new(host, envRef);
	static AndroidEnvironment INativeThread<AndroidEnvironment>.Create(IVirtualMachineHost host, JEnvironmentRef envRef,
		ThreadCreationArgs args)
		=> new AndroidThread(host, envRef, args);
	static IThread INativeThread<AndroidEnvironment>.Create(AndroidEnvironment nativeThread, Boolean newThread)
		=> new AndroidThread(nativeThread, newThread);
}