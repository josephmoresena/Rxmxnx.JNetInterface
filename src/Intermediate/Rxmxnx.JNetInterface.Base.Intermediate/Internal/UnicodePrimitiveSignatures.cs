namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Unicode java primitives signatures.
/// </summary>
internal static partial class UnicodePrimitiveSignatures
{
	/// <summary>
	/// JNI signature for primitive <c>boolean</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JBooleanSignature)]
	public static readonly CString JBooleanSignature;
	/// <summary>
	/// JNI signature for primitive <c>byte</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JByteSignature)]
	public static readonly CString JByteSignature;
	/// <summary>
	/// JNI signature for primitive <c>char</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JCharSignature)]
	public static readonly CString JCharSignature;
	/// <summary>
	/// JNI signature for primitive <c>double</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JDoubleSignature)]
	public static readonly CString JDoubleSignature;
	/// <summary>
	/// JNI signature for primitive <c>float</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JFloatSignature)]
	public static readonly CString JFloatSignature;
	/// <summary>
	/// JNI signature for primitive <c>int</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JIntSignature)]
	public static readonly CString JIntSignature;
	/// <summary>
	/// JNI signature for primitive <c>long</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JLongSignature)]
	public static readonly CString JLongSignature;
	/// <summary>
	/// JNI signature for primitive <c>short</c>.
	/// </summary>
	[DefaultValue(PrimitiveSignatures.JShortSignature)]
	public static readonly CString JShortSignature;
}