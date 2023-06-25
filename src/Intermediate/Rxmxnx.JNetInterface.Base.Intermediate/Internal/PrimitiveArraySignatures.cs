namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Java primitive array signatures.
/// </summary>
internal static class PrimitiveArraySignatures
{
	/// <summary>
	/// JNI signature for <c>boolean[]</c> object.
	/// </summary>
	public const String JBooleanArraySignature =
		ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JBooleanSignature;
	/// <summary>
	/// JNI signature for <c>byte[]</c> object.
	/// </summary>
	public const String JByteArraySignature =
		ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JByteSignature;
	/// <summary>
	/// JNI signature for <c>char[]</c> object.
	/// </summary>
	public const String JCharArraySignature =
		ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JCharSignature;
	/// <summary>
	/// JNI signature for <c>double[]</c> object.
	/// </summary>
	public const String JDoubleArraySignature =
		ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JDoubleSignature;
	/// <summary>
	/// JNI signature for <c>float[]</c> object.
	/// </summary>
	public const String JFloatArraySignature =
		ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JFloatSignature;
	/// <summary>
	/// JNI signature for <c>int[]</c> object.
	/// </summary>
	public const String JIntArraySignature = ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JIntSignature;
	/// <summary>
	/// JNI signature for <c>long[]</c> object.
	/// </summary>
	public const String JLongArraySignature =
		ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JLongSignature;
	/// <summary>
	/// JNI signature for <c>short[]</c> object.
	/// </summary>
	public const String JShortArraySignature =
		ObjectSignatures.ArraySignaturePrefix + PrimitiveSignatures.JShortSignature;
}