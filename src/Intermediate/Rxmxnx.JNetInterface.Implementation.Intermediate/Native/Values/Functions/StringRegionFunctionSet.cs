namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to get from a Java string through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct StringRegionFunctionSet
{
	/// <summary>
	/// Pointer to <c>GetStringRegion</c> function.
	/// Copies unicode characters from given java string.
	/// </summary>
	public readonly GetStringRegionFunction<Char> Utf16;
	/// <summary>
	/// Pointer to <c>GetStringUTFRegion</c> function.
	/// Copies UTF8 characters from given java string.
	/// </summary>
	public readonly GetStringRegionFunction<Byte> Utf8;
}