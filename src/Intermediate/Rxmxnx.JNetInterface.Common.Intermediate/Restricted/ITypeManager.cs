namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a main class set.
/// </summary>
internal interface ITypeManager
{
	/// <summary>
	/// Main classes' information.
	/// </summary>
	IEnumerable<ITypeInformation> ClassesInformation { get; }

	/// <summary>
	/// Indicates whether the class hashed by <paramref name="classHash"/> is main.
	/// </summary>
	/// <param name="classHash">A class hash.</param>
	/// <returns>
	/// <see langword="true"/> if the class hashed by <paramref name="classHash"/> is main; otherwise <see langword="false"/>.
	/// </returns>
	Boolean Contains(String classHash);
	/// <summary>
	/// Retrieves <see cref="AccessCache"/> for <paramref name="classRef"/>.
	/// </summary>
	/// <param name="classRef">A global <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="AccessCache"/> instance.</returns>
	AccessCache? GetAccess(JClassLocalRef classRef);
	/// <summary>
	/// Reloads global access for <paramref name="classHash"/>.
	/// </summary>
	/// <param name="classHash">Class hash.</param>
	void ReloadAccess(String classHash);
	/// <summary>
	/// Retrieves class metadata from current <see cref="JGlobalBase"/> instance.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <see cref="ClassObjectMetadata"/> instance.</returns>
	ClassObjectMetadata? LoadMetadataGlobal(JGlobalBase jGlobal);
	/// <summary>
	/// Loads global instance in the given <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JGlobal"/> instance.</returns>
	JGlobal LoadGlobal(JClassObject jClass);
	/// <summary>
	/// Retrieves class metadata for given hash.
	/// </summary>
	/// <param name="classHash">Class hash.</param>
	/// <returns>A <see cref="ITypeInformation"/> instance.</returns>
	ITypeInformation? GetTypeInformation(String classHash);
	/// <summary>
	/// Registers native methods for given class.
	/// </summary>
	/// <param name="classHash">Class hash.</param>
	/// <param name="calls">A <see cref="JNativeCallEntry"/> array.</param>
	void RegisterNatives(String classHash, IReadOnlyList<JNativeCallEntry> calls);
	/// <summary>
	/// Unregister any native method for given class.
	/// </summary>
	/// <param name="classHash">Class hash.</param>
	void UnregisterNatives(String classHash);
}