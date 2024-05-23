namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy values to a Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct SetPrimitiveArrayRegionFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType<TPrimitiveType>, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>Set&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies back a region of a primitive array from a buffer.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, TArrayRef, Int32, Int32, ReadOnlyValPtr<TPrimitiveType>, void>
		SetArrayRegion;
}