namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java native value.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INativeType
{
	/// <summary>
	/// Indicates the type of native value.
	/// </summary>
	static abstract JNativeType Type { get; }

	/// <summary>
	/// Current instance text value.
	/// </summary>
	internal String TextValue => INativeType.GetTextValue(this);

	/// <summary>
	/// Current value as <see cref="String"/>.
	/// </summary>
	/// <returns>Current instance as <see cref="String"/>.</returns>
	String AsString();

	/// <summary>
	/// <paramref name="nativeType"/> as <see cref="String"/>.
	/// </summary>
	/// <typeparam name="TNative">Type of <see cref="INativeType"/></typeparam>
	/// <param name="nativeType"><see cref="INativeType"/> instance.</param>
	/// <returns><see cref="INativeType"/> instance as <see cref="String"/>.</returns>
	internal static String ToString<TNative>(TNative nativeType) where TNative : unmanaged, INativeType<TNative>
		=> nativeType.AsString();

	/// <summary>
	/// Returns a <see cref="String"/> representing <paramref name="native"/>.
	/// </summary>
	/// <param name="native">A <see cref="INativeType"/> value.</param>
	/// <returns>A <see cref="String"/> representing <paramref name="native"/>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static String GetTextValue(INativeType native)
		=> native switch
		{
			IFixedPointer fPtr => $"0x{fPtr.Pointer:x8}",
			JValue jValue => Convert.ToHexString(NativeUtilities.AsBytes(jValue)),
			JNativeInterface jNative => Convert.ToHexString(NativeUtilities.AsBytes(jNative)),
			JInvokeInterface jInvoke => Convert.ToHexString(NativeUtilities.AsBytes(jInvoke)),
			_ => native.GetType().ToString(),
		};
}

/// <summary>
/// This interface exposes a java native value.
/// </summary>
/// <typeparam name="TNative">Type of <see cref="INativeType"/></typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INativeType<TNative> : INativeType where TNative : unmanaged, INativeType<TNative>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	String INativeType.AsString() => $"{TNative.Type.GetTypeName()}: {this.TextValue}";
}