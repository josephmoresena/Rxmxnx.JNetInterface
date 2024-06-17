namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java native value.
/// </summary>
internal interface INativeType
{
	/// <summary>
	/// Indicates the type of native value.
	/// </summary>
	static abstract JNativeType Type { get; }

	/// <summary>
	/// Returns a <see cref="String"/> representing <paramref name="native"/>.
	/// </summary>
	/// <param name="native">A <see cref="INativeType"/> value.</param>
	/// <returns>A <see cref="String"/> representing <paramref name="native"/>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static String GetTextValue<TValue>(TValue native) where TValue : unmanaged, INativeType
		=> Convert.ToHexString(NativeUtilities.AsBytes(native));
	/// <summary>
	/// Returns a <see cref="String"/> representing <paramref name="native"/>.
	/// </summary>
	/// <param name="native">A <see cref="IFixedPointer"/> value.</param>
	/// <returns>A <see cref="String"/> representing <paramref name="native"/>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static String GetPointerText<TPointer>(TPointer native)
		where TPointer : unmanaged, IFixedPointer, INativeType
		=> $"0x{native.Pointer:x8}";
}