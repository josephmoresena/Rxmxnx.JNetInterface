namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to copy chars from a Java string object through JNI.
/// </summary>
/// <typeparam name="TChar">Type of character.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct GetStringRegionFunction<TChar>
	where TChar : unmanaged, IBinaryNumber<TChar>, IUnsignedNumber<TChar>
{
	/// <summary>
	/// Pointer to <c>GetStringRegion</c> function.
	/// Copies a number of characters from the string object to the given buffer.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int32, Int32, ReadOnlyValPtr<TChar>, void>
		GetStringRegion;
}