namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to release the pointer to elements Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct ReleasePrimitiveArrayElementsFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType<TPrimitiveType>, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>Release&lt;PrimitiveType&gt;Elements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, TArrayRef, ReadOnlyValPtr<TPrimitiveType>, JReleaseMode, void>
		Release;
}