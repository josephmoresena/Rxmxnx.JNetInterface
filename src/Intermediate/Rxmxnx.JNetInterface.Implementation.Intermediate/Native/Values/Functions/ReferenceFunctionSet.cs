namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java object reference through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct ReferenceFunctionSet
{
	/// <summary>
	/// Pointer to <c>PushLocalFrame</c> function.
	/// Creates a new local reference frame, in which at least a given number of local references can be created.
	/// </summary>
	/// <remarks>
	/// Note that local references already created in previous local frames are still valid in the current local frame.
	/// </remarks>
	public readonly delegate* unmanaged<JEnvironmentRef, Int32, JResult> PushLocalFrame;
	/// <summary>
	/// Pointer to <c>PopLocalFrame</c> function.
	/// Pops off the current local reference frame, frees all the local references, and returns a
	/// local reference in the previous local reference frame for the given result object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef> PopLocalFrame;

	/// <summary>
	/// Pointer to <c>NewGlobalRef</c> function.
	/// Creates a new global reference to the object referred.
	/// </summary>
	/// <remarks>Global references must be explicitly disposed of by calling <c>DeleteGlobalRef()</c>.</remarks>
	public readonly NewRefFunction<JGlobalRef> NewGlobalRef;
	/// <summary>
	/// Pointer to <c>DeleteGlobalRef</c> function.
	/// Deletes the given global reference.
	/// </summary>
	public readonly DeleteRefFunction<JGlobalRef> DeleteGlobalRef;
	/// <summary>
	/// Pointer to <c>DeleteLocalRef</c> function.
	/// Deletes the given local reference.
	/// </summary>
	public readonly DeleteRefFunction<JObjectLocalRef> DeleteLocalRef;
	/// <summary>
	/// Pointer to <c>IsSameObject</c> function.
	/// Tests whether two references refer to the same Java object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef, JBoolean> IsSameObject;
	/// <summary>
	/// Pointer to <c>NewLocalRef</c> function.
	/// Creates a new local reference that refers to the given object.
	/// </summary>
	public readonly NewRefFunction<JObjectLocalRef> NewLocalRef;
	/// <summary>
	/// Pointer to <c>EnsureLocalCapacity</c> function.
	/// Ensures that at least a given number of local references can be created in the current thread.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, Int32, JResult> EnsureLocalCapacity;
}