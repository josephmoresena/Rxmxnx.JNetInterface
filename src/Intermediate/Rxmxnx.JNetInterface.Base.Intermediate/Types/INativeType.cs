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
	internal String TextValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return this switch
			{
				IFixedPointer fPtr => fPtr.Pointer.ToString(CommonConstants.IntPtrToStringFormat),
				JValue jValue => Convert.ToHexString(NativeUtilities.AsBytes(jValue)),
				JNativeInterface jNative => Convert.ToHexString(NativeUtilities.AsBytes(jNative)),
				JInvokeInterface jInvoke => Convert.ToHexString(NativeUtilities.AsBytes(jInvoke)),
				_ => this.GetType().ToString(),
			};
		}
	}

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