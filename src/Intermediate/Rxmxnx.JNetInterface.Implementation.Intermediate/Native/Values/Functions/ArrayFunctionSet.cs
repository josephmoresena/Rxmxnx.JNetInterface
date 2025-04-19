namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java arrays through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ArrayFunctionSet
{
	/// <summary>
	/// Primitive array functions.
	/// </summary>
	public enum PrimitiveFunction : Byte
	{
		NewArray = default,
		GetElements = 1,
		ReleaseElements = 2,
		GetRegion = 3,
		SetRegion = 4,
	}

	/// <summary>
	/// Pointer to <c>GetArrayLength</c> function.
	/// Returns the number of elements in the array.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, Int32> GetArrayLength;
	/// <summary>
	/// Pointers to <c>NewObjectArray</c>, <c>GetObjectArrayElement</c> and <c>SetObjectArrayElement</c> functions.
	/// </summary>
	public readonly ObjectArrayFunctionSet ObjectArrayFunctions;
	/// <summary>
	/// Pointers to <c>NewBooleanArray</c>, <c>NewByteArray</c>, <c>NewCharArray</c>,
	/// <c>NewShortArray</c>, <c>NewIntArray</c>, <c>NewLongArray</c>,
	/// <c>NewFloatArray</c> and <c>NewDoubleArray</c> functions.
	/// </summary>
	public readonly NewPrimitiveArrayFunctionSet NewPrimitiveArrayFunctions;
	/// <summary>
	/// Pointers to <c>GetBooleanArrayElements</c>, <c>GetByteArrayElements</c>, <c>GetCharArrayElements</c>,
	/// <c>GetShortArrayElements</c>, <c>GetIntArrayElements</c>, <c>GetLongArrayElements</c>,
	/// <c>GetFloatArrayElements</c> and <c>GetDoubleArrayElements</c> functions.
	/// </summary>
	public readonly GetPrimitiveArrayElementsFunctionSet GetElementsFunctions;
	/// <summary>
	/// Pointers to <c>ReleaseBooleanArrayElements</c>, <c>ReleaseByteArrayElements</c>, <c>ReleaseCharArrayElements</c>,
	/// <c>ReleaseShortArrayElements</c>, <c>ReleaseIntArrayElements</c>, <c>ReleaseLongArrayElements</c>,
	/// <c>ReleaseFloatArrayElements</c> and <c>ReleaseDoubleArrayElements</c> functions.
	/// </summary>
	public readonly ReleasePrimitiveArrayElementsFunctionSet ReleaseElementsFunctions;
	/// <summary>
	/// Pointers to <c>GetBooleanArrayRegion</c>, <c>GetByteArrayRegion</c>, <c>GetCharArrayRegion</c>,
	/// <c>GetShortArrayRegion</c>, <c>GetIntArrayRegion</c>, <c>GetLongArrayRegion</c>,
	/// <c>GetFloatArrayRegion</c>, <c>GetDoubleArrayRegion</c>, <c>SetBooleanArrayRegion</c>,
	/// <c>SetByteArrayRegion</c>, <c>SetCharArrayRegion</c>, <c>SetShortArrayRegion</c>,
	/// <c>SetIntArrayRegion</c>, <c>SetLongArrayRegion</c>, <c>SetFloatArrayRegion</c> and
	/// <c>SetDoubleArrayRegion</c> functions.
	/// </summary>
	public readonly PrimitiveArrayRegionFunctionSet RegionFunctions;
}