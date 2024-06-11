namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy values from a Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct GetPrimitiveArrayRegionFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies a region of a primitive array into a buffer.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, TArrayRef, Int32, Int32, ValPtr<TPrimitiveType>, void> Get;
}