namespace Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode;

/// <summary>
/// Unicode java primitives signatures.
/// </summary>
internal static partial class UnicodePrimitiveSignatures
{
	/// <inheritdoc cref="BooleanSignature"/>
	public const Byte BooleanSignatureChar = (Byte)PrimitiveSignatures.JBooleanSignatureChar;
	/// <inheritdoc cref="ByteSignature"/>
	public const Byte ByteSignatureChar = (Byte)PrimitiveSignatures.JByteSignatureChar;
	/// <inheritdoc cref="CharSignature"/>
	public const Byte CharSignatureChar = (Byte)PrimitiveSignatures.JCharSignatureChar;
	/// <inheritdoc cref="DoubleSignature"/>
	public const Byte DoubleSignatureChar = (Byte)PrimitiveSignatures.JDoubleSignatureChar;
	/// <inheritdoc cref="FloatSignature"/>
	public const Byte FloatSignatureChar = (Byte)PrimitiveSignatures.JFloatSignatureChar;
	/// <inheritdoc cref="IntSignature"/>
	public const Byte IntSignatureChar = (Byte)PrimitiveSignatures.JIntSignatureChar;
	/// <inheritdoc cref="LongSignature"/>
	public const Byte LongSignatureChar = (Byte)PrimitiveSignatures.JLongSignatureChar;
	/// <inheritdoc cref="ShortSignature"/>
	public const Byte ShortSignatureChar = (Byte)PrimitiveSignatures.JShortSignatureChar;

	/// <inheritdoc cref="PrimitiveSignatures.BooleanSignature"/>
	[DefaultValue(PrimitiveSignatures.BooleanSignature)]
	public static partial ReadOnlySpan<Byte> BooleanSignature();
	/// <inheritdoc cref="PrimitiveSignatures.ByteSignature"/>
	[DefaultValue(PrimitiveSignatures.ByteSignature)]
	public static partial ReadOnlySpan<Byte> ByteSignature();
	/// <inheritdoc cref="PrimitiveSignatures.CharSignature"/>
	[DefaultValue(PrimitiveSignatures.CharSignature)]
	public static partial ReadOnlySpan<Byte> CharSignature();
	/// <inheritdoc cref="PrimitiveSignatures.DoubleSignature"/>
	[DefaultValue(PrimitiveSignatures.DoubleSignature)]
	public static partial ReadOnlySpan<Byte> DoubleSignature();
	/// <inheritdoc cref="PrimitiveSignatures.FloatSignature"/>
	[DefaultValue(PrimitiveSignatures.FloatSignature)]
	public static partial ReadOnlySpan<Byte> FloatSignature();
	/// <inheritdoc cref="PrimitiveSignatures.IntSignature"/>
	[DefaultValue(PrimitiveSignatures.IntSignature)]
	public static partial ReadOnlySpan<Byte> IntSignature();
	/// <inheritdoc cref="PrimitiveSignatures.LongSignature"/>
	[DefaultValue(PrimitiveSignatures.LongSignature)]
	public static partial ReadOnlySpan<Byte> LongSignature();
	/// <inheritdoc cref="PrimitiveSignatures.ShortSignature"/>
	[DefaultValue(PrimitiveSignatures.ShortSignature)]
	public static partial ReadOnlySpan<Byte> ShortSignature();
}