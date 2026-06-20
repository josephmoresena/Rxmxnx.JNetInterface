namespace Rxmxnx.JNetInterface.Internal;

internal partial class AndroidEnvironment : IMainClassLoader
{
	JVirtualMachineRef IMainClassLoader.VirtualMachineRef => this._m.Core.Host.Value.Reference;
	JEnvironmentRef IMainClassLoader.EnvironmentRef => this._m.Core.Reference;
	Thread INativeThread<AndroidEnvironment>.Thread => this._m.Core.Thread;
	IAccessFeature IEnvironment.AccessFeature => this._m.Core;
	IClassFeature IEnvironment.ClassFeature => this._m.Core;
	IReferenceFeature IEnvironment.ReferenceFeature => this._m.Core;
	IStringFeature IEnvironment.StringFeature => this._m.Core;
	IArrayFeature IEnvironment.ArrayFeature => this._m.Core;
	INioFeature IEnvironment.NioFeature => this._m.Core;
	NativeFunctionSet IEnvironment.FunctionSet => NativeFunctionSetImpl.Instance;
	LocalCache ILocalCacheOwner.LocalCache
	{
		get => this._m.LocalCache;
		set => this._m.LocalCache = value;
	}
	IUnsafeMemoryManager INativeThread.MemoryManager => this._m.Core;
	ClassCache INativeThread.ClassCache => this._m.Core.GetClassCache();
	Boolean INativeThread.IsOwned => this._m.Core.IsOwned;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void INativeThread.LoadClass(JClassObject jClass) => this._m.Core.LoadClass(jClass);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void INativeThread.CheckJniError() => this._m.Core.CheckJniError();
	JClassObject IAlienObjectManager.GetReferenceTypeClass(JClassLocalRef classRef, Boolean keepReference)
		=> this._m.Core.GetClass(classRef, keepReference, JTypeKind.Undefined);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JReferenceType IAlienObjectManager.GetReferenceType(JObjectLocalRef localRef)
		=> this._m.Core.GetReferenceType(localRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JClassObject IAlienObjectManager.GetObjectClass(JObjectLocalRef localRef, out JReferenceTypeMetadata typeMetadata)
		=> EnvironmentCore.GetObjectClass(this._m.Core, localRef, out typeMetadata);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JClassObject IAlienObjectManager.GetObjectClass(ITypeInformation typeInformation, JObjectLocalRef localRef,
		out JReferenceTypeMetadata typeMetadata)
		=> EnvironmentCore.GetObjectClass(this._m.Core, typeInformation, localRef, out typeMetadata);
	Boolean? IEnvironment.IsVirtual(JThreadObject jThread) => default;
}