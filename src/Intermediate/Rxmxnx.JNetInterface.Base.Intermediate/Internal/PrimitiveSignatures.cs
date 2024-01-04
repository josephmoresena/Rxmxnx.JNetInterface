namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java primitives signatures.
/// </summary>
internal static class PrimitiveSignatures
{
	/// <inheritdoc cref="BooleanSignature"/>
	public const Char JBooleanSignatureChar = 'Z';
	/// <inheritdoc cref="ByteSignature"/>
	public const Char JByteSignatureChar = 'B';
	/// <inheritdoc cref="CharSignature"/>
	public const Char JCharSignatureChar = 'C';
	/// <inheritdoc cref="DoubleSignature"/>
	public const Char JDoubleSignatureChar = 'D';
	/// <inheritdoc cref="FloatSignature"/>
	public const Char JFloatSignatureChar = 'F';
	/// <inheritdoc cref="IntSignature"/>
	public const Char JIntSignatureChar = 'I';
	/// <inheritdoc cref="LongSignature"/>
	public const Char JLongSignatureChar = 'J';
	/// <inheritdoc cref="ShortSignature"/>
	public const Char JShortSignatureChar = 'S';

	/// <summary>
	/// JNI signature for primitive <c>boolean</c>.
	/// </summary>
	public const String BooleanSignature = "Z";
	/// <summary>
	/// JNI signature for primitive <c>byte</c>.
	/// </summary>
	public const String ByteSignature = "B";
	/// <summary>
	/// JNI signature for primitive <c>char</c>.
	/// </summary>
	public const String CharSignature = "C";
	/// <summary>
	/// JNI signature for primitive <c>double</c>.
	/// </summary>
	public const String DoubleSignature = "D";
	/// <summary>
	/// JNI signature for primitive <c>float</c>.
	/// </summary>
	public const String FloatSignature = "F";
	/// <summary>
	/// JNI signature for primitive <c>int</c>.
	/// </summary>
	public const String IntSignature = "I";
	/// <summary>
	/// JNI signature for primitive <c>long</c>.
	/// </summary>
	public const String LongSignature = "J";
	/// <summary>
	/// JNI signature for primitive <c>short</c>.
	/// </summary>
	public const String ShortSignature = "S";
}