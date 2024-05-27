namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to release elements from a Java primitive array through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct ReleasePrimitiveArrayElementsFunctionSet
{
	/// <summary>
	/// Pointer to <c>ReleaseBooleanArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JBoolean, JBooleanArrayLocalRef> ReleaseBooleanArrayElements;
	/// <summary>
	/// Pointer to <c>ReleaseByteArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JByte, JByteArrayLocalRef> ReleaseByteArrayElements;
	/// <summary>
	/// Pointer to <c>ReleaseCharArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JChar, JCharArrayLocalRef> ReleaseCharArrayElements;
	/// <summary>
	/// Pointer to <c>ReleaseShortArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JShort, JShortArrayLocalRef> ReleaseShortArrayElements;
	/// <summary>
	/// Pointer to <c>ReleaseIntArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JInt, JIntArrayLocalRef> ReleaseIntArrayElements;
	/// <summary>
	/// Pointer to <c>ReleaseLongArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JLong, JLongArrayLocalRef> ReleaseLongArrayElements;
	/// <summary>
	/// Pointer to <c>ReleaseFloatArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JFloat, JFloatArrayLocalRef> ReleaseFloatArrayElements;
	/// <summary>
	/// Pointer to <c>ReleaseDoubleArrayElements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunction<JDouble, JDoubleArrayLocalRef> ReleaseDoubleArrayElements;
}