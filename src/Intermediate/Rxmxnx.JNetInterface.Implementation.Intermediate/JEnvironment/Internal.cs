namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
partial class JEnvironment
{
	/// <summary>
	/// Class cache.
	/// </summary>
	internal ClassCache ClassCache => this._m.Core.GetClassCache();
	/// <summary>
	/// Local cache.
	/// </summary>
	internal LocalCache LocalCache => this._m.LocalCache;
	/// <inheritdoc cref="IClassFeature.ClassObject"/>
	internal JClassObject ClassObject => this._m.Core.ClassObject;

	/// <summary>
	/// Sets current object cache.
	/// </summary>
	/// <param name="localCache">A <see cref="LocalCache"/> instance.</param>
	internal void SetObjectCache(LocalCache localCache) => this._m.LocalCache = localCache;
	/// <summary>
	/// Deletes <paramref name="globalRef"/>.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void DeleteGlobalRef(JGlobalRef globalRef) => this._m.Core.DeleteGlobalRef(globalRef);
	/// <summary>
	/// Deletes <paramref name="weakRef"/>.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void DeleteWeakGlobalRef(JWeakRef weakRef) => this._m.Core.DeleteWeakGlobalRef(weakRef);
	/// <summary>
	/// Retrieves type of given reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JReferenceType"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal JReferenceType GetReferenceType(JObjectLocalRef localRef) => this._m.Core.GetReferenceType(localRef);
	/// <summary>
	/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	internal JClassObject GetReferenceTypeClass(JClassLocalRef classRef, Boolean keepReference = false)
		=> this._m.Core.GetClass(classRef, keepReference, JTypeKind.Undefined);
	/// <summary>
	/// Loads in current cache given class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void LoadClass(JClassObject jClass) => this._m.Core.LoadClass(jClass);
	/// <summary>
	/// Reloads current class object.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>Current <see cref="JClassLocalRef"/> reference.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void ReloadClass(JClassObject jClass) => this._m.Core.ReloadClass(jClass);
	/// <summary>
	/// Sends JNI fatal error signal to VM.
	/// </summary>
	/// <param name="errorMessage">Error message.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void FatalError(ReadOnlySpan<Byte> errorMessage) => EnvironmentCore.FatalError(this._m.Core, errorMessage);
	/// <summary>
	/// Checks JNI occurred error.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void CheckJniError() => this._m.Core.CheckJniError();
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal JClassObject GetObjectClass(JObjectLocalRef localRef, out JReferenceTypeMetadata typeMetadata)
		=> EnvironmentCore.GetObjectClass(this._m.Core, localRef, out typeMetadata);

	/// <summary>
	/// Retrieves object class reference.
	/// </summary>
	/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
	/// <param name="localRef">Object instance to get class.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	internal static JClassObject GetObjectClass(JEnvironment env, JObjectLocalRef localRef)
	{
		using LocalFrame frame = new(env, IVirtualMachine.GetObjectClassCapacity);
		JClassLocalRef classRef = EnvironmentCore.GetObjectClass(env._m.Core, localRef);
		JClassObject jClass = env.GetReferenceTypeClass(classRef);
		env._m.Core.LoadClass(frame, classRef, jClass); // Runtime class loading.
		return jClass;
	}
	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>The <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.</returns>
	internal static JEnvironment GetEnvironment(JEnvironmentRef reference)
	{
		JVirtualMachineRef vmRef = EnvironmentCore.GetVirtualMachineRef(reference);
		JVirtualMachine vm = (JVirtualMachine)JVirtualMachine.GetVirtualMachine(vmRef);
		return vm.GetEnvironment(reference);
	}
}