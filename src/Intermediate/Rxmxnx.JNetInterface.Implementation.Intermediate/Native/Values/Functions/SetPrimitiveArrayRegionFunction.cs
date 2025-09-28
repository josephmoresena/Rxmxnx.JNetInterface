namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy values to a Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct
	SetPrimitiveArrayRegionFunction<TPrimitiveType, TArrayRef> : ISetPrimitiveArrayRegionFunction
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, INativePointerType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>Set&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies back a region of a primitive array from a buffer.
	/// </summary>
	private readonly ISetPrimitiveArrayRegionFunction.SetPrimitiveArrayRegionFunction _function;

	/// <summary>
	/// <c>Set&lt;PrimitiveType&gt;ArrayRegion</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Set(JEnvironmentRef envRef, TArrayRef arrayRef, Int32 start, Int32 length,
		ReadOnlyValPtr<TPrimitiveType> buffer)
	{
		if (OperatingSystem.IsWindows())
			this._function.Windows(envRef, arrayRef.ArrayValue, start, length, buffer);
		else
			this._function.Unix(envRef, arrayRef.ArrayValue, start, length, buffer);
	}
}