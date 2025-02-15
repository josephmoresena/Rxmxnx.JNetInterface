namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy values to a Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct SetPrimitiveArrayRegionFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType
{
	/// <summary>
	/// Pointer to <c>Set&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies back a region of a primitive array from a buffer.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, Int32, Int32, void*, void> _ptr;

	/// <summary>
	/// Pointer to <c>Set&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies back a region of a primitive array from a buffer.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Set(JEnvironmentRef envRef, TArrayRef arrayRef, Int32 start, Int32 length,
		ReadOnlyValPtr<TPrimitiveType> buffer)
		=> this._ptr(envRef, arrayRef.ArrayValue, start, length, buffer);
}