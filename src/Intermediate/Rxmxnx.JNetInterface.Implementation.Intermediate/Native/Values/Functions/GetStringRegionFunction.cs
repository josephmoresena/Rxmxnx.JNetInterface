namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy chars from a Java string object through JNI.
/// </summary>
/// <typeparam name="TChar">Type of character.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct GetStringRegionFunction<TChar>
	where TChar : unmanaged, IBinaryNumber<TChar>, IUnsignedNumber<TChar>
{
	/// <summary>
	/// Pointer to <c>GetStringRegion</c> function.
	/// Copies a number of characters from the string object to the given buffer.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int32, Int32, void*, void> _ptr;

	/// <summary>
	/// Pointer to <c>GetStringRegion</c> function.
	/// Copies a number of characters from the string object to the given buffer.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetStringRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 length,
		ValPtr<TChar> buffer)
		=> this._ptr(envRef, stringRef, start, length, buffer);
}