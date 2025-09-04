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
	private readonly IGetStringRegionFunction.GetStringRegionFunction _getStringRegion;

	/// <summary>
	/// Pointer to <c>GetStringRegion</c> function.
	/// Copies a number of characters from the string object to the given buffer.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetStringRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 length,
		ValPtr<TChar> buffer)
	{
		if (OperatingSystem.IsWindows())
			this._getStringRegion.Windows(envRef, stringRef, start, length, buffer);
		else
			this._getStringRegion.Unix(envRef, stringRef, start, length, buffer);
	}
}