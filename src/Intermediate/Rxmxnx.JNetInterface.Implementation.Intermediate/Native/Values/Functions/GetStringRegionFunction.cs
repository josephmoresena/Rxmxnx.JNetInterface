namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy chars from a Java string object through JNI.
/// </summary>
/// <typeparam name="TChar">Type of character.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct GetStringRegionFunction<TChar> : IGetStringRegionFunction
	where TChar : unmanaged, IBinaryNumber<TChar>, IUnsignedNumber<TChar>
{
	/// <summary>
	/// Pointer to <c>GetStringRegion</c> function.
	/// Copies a number of characters from the string object to the given buffer.
	/// </summary>
	private readonly IGetStringRegionFunction.GetStringRegionFunction _function;

	/// <summary>
	/// <c>GetStringRegion</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetStringRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 length,
		ValPtr<TChar> buffer)
	{
		if (SystemInfo.IsWindows)
			this._function.Windows(envRef, stringRef, start, length, buffer);
		else
			this._function.Unix(envRef, stringRef, start, length, buffer);
	}
}