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
internal readonly unsafe struct
	GetPrimitiveArrayRegionFunction<TPrimitiveType, TArrayRef> : IGetPrimitiveArrayRegionFunction
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, INativePointerType<TArrayRef>
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;ArrayRegion</c> function.
	/// Copies a region of a primitive array into a buffer.
	/// </summary>
	private readonly IGetPrimitiveArrayRegionFunction.GetPrimitiveArrayRegionFunction _function;

	/// <summary>
	/// <c>Get&lt;PrimitiveType&gt;ArrayRegion</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Get(JEnvironmentRef envRef, TArrayRef arrayRef, Int32 start, Int32 length,
		ValPtr<TPrimitiveType> buffer)
	{
		if (SystemInfo.IsWindows)
			this._function.Windows(envRef, arrayRef.ArrayValue, start, length, buffer);
		else
			this._function.Unix(envRef, arrayRef.ArrayValue, start, length, buffer);
	}
}