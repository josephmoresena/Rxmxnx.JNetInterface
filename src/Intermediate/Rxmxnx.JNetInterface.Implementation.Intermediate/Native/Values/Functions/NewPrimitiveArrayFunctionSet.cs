namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to create Java primitive array through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct NewPrimitiveArrayFunctionSet
{
	/// <summary>
	/// Pointer to <c>NewBooleanArray</c> function.
	/// Constructs a new <c>boolean</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JBoolean, JBooleanArrayLocalRef> NewBooleanArray;
	/// <summary>
	/// Pointer to <c>NewByteArray</c> function.
	/// Constructs a new <c>byte</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JByte, JByteArrayLocalRef> NewByteArray;
	/// <summary>
	/// Pointer to <c>NewCharArray</c> function.
	/// Constructs a new <c>char</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JChar, JCharArrayLocalRef> NewCharArray;
	/// <summary>
	/// Pointer to <c>NewShortArray</c> function.
	/// Constructs a new <c>short</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JShort, JShortArrayLocalRef> NewShortArray;
	/// <summary>
	/// Pointer to <c>NewIntArray</c> function.
	/// Constructs a new <c>int</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JInt, JIntArrayLocalRef> NewIntArray;
	/// <summary>
	/// Pointer to <c>NewLongArray</c> function.
	/// Constructs a new <c>long</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JLong, JLongArrayLocalRef> NewLongArray;
	/// <summary>
	/// Pointer to <c>NewFloatArray</c> function.
	/// Constructs a new <c>float</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JFloat, JFloatArrayLocalRef> NewFloatArray;
	/// <summary>
	/// Pointer to <c>NewDoubleArray</c> function.
	/// Constructs a new <c>Double</c> array object.
	/// </summary>
	public readonly NewPrimitiveArrayFunction<JDouble, JDoubleArrayLocalRef> NewDoubleArray;
}