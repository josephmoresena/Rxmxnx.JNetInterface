namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NumericHelper
{
	/// <inheritdoc cref="IBinaryNumber{T}.IsPow2(T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsPow2<T>(T value) where T : unmanaged, IBinaryNumber<T> => T.IsPow2(value);
	/// <inheritdoc cref="IBinaryNumber{T}.Log2(T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Log2<T>(T value) where T : unmanaged, IBinaryNumber<T> => T.Log2(value);
}