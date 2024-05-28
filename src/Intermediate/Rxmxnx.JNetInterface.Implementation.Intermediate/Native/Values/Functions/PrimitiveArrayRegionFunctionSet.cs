namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to get/set region from/to a Java primitive array through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly struct PrimitiveArrayRegionFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetBooleanArrayRegion</c> function.
	/// Copies a region of a <c>boolean</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JBoolean, JBooleanArrayLocalRef> GetBooleanArrayRegion;
	/// <summary>
	/// Pointer to <c>GetByteArrayRegion</c> function.
	/// Copies a region of a <c>byte</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JByte, JByteArrayLocalRef> GetByteArrayRegion;
	/// <summary>
	/// Pointer to <c>GetCharArrayRegion</c> function.
	/// Copies a region of a <c>char</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JChar, JCharArrayLocalRef> GetCharArrayRegion;
	/// <summary>
	/// Pointer to <c>GetShortArrayRegion</c> function.
	/// Copies a region of a <c>short</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JShort, JShortArrayLocalRef> GetShortArrayRegion;
	/// <summary>
	/// Pointer to <c>GetIntArrayRegion</c> function.
	/// Copies a region of a <c>int</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JInt, JIntArrayLocalRef> GetIntArrayRegion;
	/// <summary>
	/// Pointer to <c>GetLongArrayRegion</c> function.
	/// Copies a region of a <c>long</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JLong, JLongArrayLocalRef> GetLongArrayRegion;
	/// <summary>
	/// Pointer to <c>GetFloatArrayRegion</c> function.
	/// Copies a region of a <c>float</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JFloat, JFloatArrayLocalRef> GetFloatArrayRegion;
	/// <summary>
	/// Pointer to <c>GetDoubleArrayRegion</c> function.
	/// Copies a region of a <c>double</c>array into a buffer.
	/// </summary>
	public readonly GetPrimitiveArrayRegionFunction<JDouble, JDoubleArrayLocalRef> GetDoubleArrayRegion;
	/// <summary>
	/// Pointer to <c>SetBooleanArrayRegion</c> function.
	/// Copies back a region of a <c>boolean</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JBoolean, JBooleanArrayLocalRef> SetBooleanArrayRegion;
	/// <summary>
	/// Pointer to <c>SetByteArrayRegion</c> function.
	/// Copies back a region of a <c>byte</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JByte, JByteArrayLocalRef> SetByteArrayRegion;
	/// <summary>
	/// Pointer to <c>SetCharArrayRegion</c> function.
	/// Copies back a region of a <c>char</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JChar, JCharArrayLocalRef> SetCharArrayRegion;
	/// <summary>
	/// Pointer to <c>SetShortArrayRegion</c> function.
	/// Copies back a region of a <c>short</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JShort, JShortArrayLocalRef> SetShortArrayRegion;
	/// <summary>
	/// Pointer to <c>SetIntArrayRegion</c> function.
	/// Copies back a region of a <c>int</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JInt, JIntArrayLocalRef> SetIntArrayRegion;
	/// <summary>
	/// Pointer to <c>SetLongArrayRegion</c> function.
	/// Copies back a region of a <c>long</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JLong, JLongArrayLocalRef> SetLongArrayRegion;
	/// <summary>
	/// Pointer to <c>SetFloatArrayRegion</c> function.
	/// Copies back a region of a <c>float</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JFloat, JFloatArrayLocalRef> SetFloatArrayRegion;
	/// <summary>
	/// Pointer to <c>SetDoubleArrayRegion</c> function.
	/// Copies back a region of a <c>double</c> array from a buffer.
	/// </summary>
	public readonly SetPrimitiveArrayRegionFunction<JDouble, JDoubleArrayLocalRef> SetDoubleArrayRegion;
}