namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to get elements from a Java primitive array through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly struct GetPrimitiveArrayElementsFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetBooleanArrayElements</c> function.
	/// Returns the body of the <c>boolean</c> array.
	/// </summary>
	/// <remarks>ReleaseBooleanArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JBoolean, JBooleanArrayLocalRef> GetBooleanArrayElements;
	/// <summary>
	/// Pointer to <c>GetByteArrayElements</c> function.
	/// Returns the body of the <c>byte</c> array.
	/// </summary>
	/// <remarks>ReleaseByteArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JByte, JByteArrayLocalRef> GetByteArrayElements;
	/// <summary>
	/// Pointer to <c>GetCharArrayElements</c> function.
	/// Returns the body of the <c>char</c> array.
	/// </summary>
	/// <remarks>ReleaseCharArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JChar, JCharArrayLocalRef> GetCharArrayElements;
	/// <summary>
	/// Pointer to <c>GetShortArrayElements</c> function.
	/// Returns the body of the <c>short</c> array.
	/// </summary>
	/// <remarks>ReleaseShortArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JShort, JShortArrayLocalRef> GetShortArrayElements;
	/// <summary>
	/// Pointer to <c>GetIntArrayElements</c> function.
	/// Returns the body of the <c>int</c> array.
	/// </summary>
	/// <remarks>ReleaseIntArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JInt, JIntArrayLocalRef> GetIntArrayElements;
	/// <summary>
	/// Pointer to <c>GetLongArrayElements</c> function.
	/// Returns the body of the <c>long</c> array.
	/// </summary>
	/// <remarks>ReleaseLongArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JLong, JLongArrayLocalRef> GetLongArrayElements;
	/// <summary>
	/// Pointer to <c>GetFloatArrayElements</c> function.
	/// Returns the body of the <c>float</c> array.
	/// </summary>
	/// <remarks>ReleaseFloatArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JFloat, JFloatArrayLocalRef> GetFloatArrayElements;
	/// <summary>
	/// Pointer to <c>GetDoubleArrayElements</c> function.
	/// Returns the body of the <c>double</c> array.
	/// </summary>
	/// <remarks>ReleaseDoubleArrayElements() function is called. </remarks>
	public readonly GetPrimitiveArrayElementsFunction<JDouble, JDoubleArrayLocalRef> GetDoubleArrayElements;
}