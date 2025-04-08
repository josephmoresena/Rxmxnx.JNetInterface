namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy values from a Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct GetPrimitiveArrayRegionFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies a region of a primitive array into a buffer.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, Int32, Int32, void*, void> _ptr;

	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies a region of a primitive array into a buffer.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Get(JEnvironmentRef envRef, TArrayRef arrayRef, Int32 start, Int32 length,
		ValPtr<TPrimitiveType> buffer)
		=> this._ptr(envRef, arrayRef.ArrayValue, start, length, buffer);
}