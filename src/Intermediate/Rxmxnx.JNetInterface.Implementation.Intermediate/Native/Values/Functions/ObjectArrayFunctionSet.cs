namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java object array through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ObjectArrayFunctionSet
{
	/// <summary>
	/// Pointer to <c>NewObjectArray</c> function.
	/// Constructs a new array holding objects in given class.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, Int32, JClassLocalRef, JObjectLocalRef, JObjectArrayLocalRef>
		NewObjectArray;
	/// <summary>
	/// Pointer to <c>GetObjectArrayElement</c> function.
	/// Returns an element of an <c>Object</c> array.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef>
		GetObjectArrayElement;
	/// <summary>
	/// Pointer to <c>SetObjectArrayElement</c> function.
	/// Sets an element of an <c>Object</c> array.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef, void>
		SetObjectArrayElement;
}