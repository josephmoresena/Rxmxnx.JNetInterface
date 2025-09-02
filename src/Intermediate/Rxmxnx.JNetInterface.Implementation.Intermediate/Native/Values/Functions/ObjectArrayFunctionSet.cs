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
	private readonly delegate* unmanaged<IntPtr, Int32, IntPtr, IntPtr, IntPtr> _newObjectArray;
	/// <summary>
	/// Pointer to <c>GetObjectArrayElement</c> function.
	/// Returns an element of an <c>Object</c> array.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32, IntPtr> _getObjectArrayElement;
	/// <summary>
	/// Pointer to <c>SetObjectArrayElement</c> function.
	/// Sets an element of an <c>Object</c> array.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32, IntPtr, void> _setObjectArrayElement;

	/// <summary>
	/// <c>NewObjectArray</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectArrayLocalRef NewObjectArray(JEnvironmentRef envRef, Int32 length, JClassLocalRef classRef,
		JObjectLocalRef localRef)
		=> new(this._newObjectArray(envRef.Pointer, length, classRef.Pointer, localRef.Pointer));
	/// <summary>
	/// <c>GetObjectArrayElement</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef GetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef objectArrayRef,
		Int32 index)
		=> new(this._getObjectArrayElement(envRef.Pointer, objectArrayRef.Pointer, index));
	/// <summary>
	/// <c>SetObjectArrayElement</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef objectArrayRef, Int32 index,
		JObjectLocalRef localRef)
		=> this._setObjectArrayElement(envRef.Pointer, objectArrayRef.Pointer, index, localRef.Pointer);
}