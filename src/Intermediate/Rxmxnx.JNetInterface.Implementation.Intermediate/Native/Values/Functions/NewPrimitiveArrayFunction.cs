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
internal readonly unsafe struct NewPrimitiveArrayFunction<TArrayRef> : INewPrimitiveArrayFunction
	where TArrayRef : unmanaged, IArrayReferenceType, INativePointerType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>New&lt;PrimitiveType&gt;Array</c> function.
	/// Constructs a new primitive array object.
	/// </summary>
	private readonly INewPrimitiveArrayFunction.NewPrimitiveArrayFunction _function;

	/// <summary>
	/// <c>New&lt;PrimitiveType&gt;Array</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TArrayRef NewArray(JEnvironmentRef envRef, Int32 length)
	{
		JArrayLocalRef result = SystemInfo.IsWindows ?
			this._function.Windows(envRef, length) :
			this._function.Unix(envRef, length);
		return Unsafe.As<JArrayLocalRef, TArrayRef>(ref result);
	}
}