namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to create Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct NewPrimitiveArrayFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType<TPrimitiveType>, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>New&lt;PrimitiveType&gt;Array</c> function.
	/// Constructs a new primitive array object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, Int32, TArrayRef> NewArray;
}