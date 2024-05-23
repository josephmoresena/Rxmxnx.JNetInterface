namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to get a pointer to elements Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct GetPrimitiveArrayElementsFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType<TPrimitiveType>, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;Elements</c> function.
	/// Returns the body of the primitive array.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, TArrayRef, out JBoolean, ValPtr<TPrimitiveType>> Get;
}