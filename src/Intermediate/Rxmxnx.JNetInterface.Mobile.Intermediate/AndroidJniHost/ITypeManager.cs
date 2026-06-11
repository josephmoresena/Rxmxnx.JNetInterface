namespace Rxmxnx.JNetInterface;

public sealed partial class AndroidJniHost : ITypeManager
{
	IEnumerable<ITypeInformation> ITypeManager.ClassesInformation => AndroidJniHost.MainClassesInformation;
	Boolean ITypeManager.Contains(String classHash) => AndroidJniHost.userMainClasses.ContainsKey(classHash);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	AccessCache? ITypeManager.GetAccess(JClassLocalRef classRef) => this._core.GetAccess(classRef);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ITypeManager.ReloadAccess(String classHash) => this._core.ReloadAccess(classHash);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	ClassObjectMetadata? ITypeManager.LoadMetadataGlobal(JGlobalBase jGlobal) => this._core.LoadMetadataGlobal(jGlobal);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JGlobal ITypeManager.LoadGlobal(JClassObject jClass) => this._core.LoadGlobal(jClass);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	ITypeInformation? ITypeManager.GetTypeInformation(String classHash) => this._core.GetTypeInformation(classHash);
	void ITypeManager.RegisterNatives(String classHash, IReadOnlyList<JNativeCallEntry> calls)
		=> this._core.NativesCache[classHash] = calls;
	void ITypeManager.UnregisterNatives(String classHash) => this._core.NativesCache.Clear(classHash);
}