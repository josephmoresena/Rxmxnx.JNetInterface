namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to create Java primitive array through JNI.
/// </summary>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NewPrimitiveArrayFunction<TArrayRef>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType
{
	/// <summary>
	/// Pointer to <c>New&lt;PrimitiveType&gt;Array</c> function.
	/// Constructs a new primitive array object.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, Int32, IntPtr> _ptr;

	/// <summary>
	/// Pointer to <c>New&lt;PrimitiveType&gt;Array</c> function.
	/// Constructs a new primitive array object.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TArrayRef NewArray(JEnvironmentRef envRef, Int32 length)
	{
		TArrayRef result = default;
		Unsafe.As<TArrayRef, IntPtr>(ref result) = this._ptr(envRef, length);
		return result;
	}
}