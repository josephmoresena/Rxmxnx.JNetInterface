﻿namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Unicode primitive array signatures names.
/// </summary>
internal static partial class UnicodePrimitiveArraySignatures
{
	/// <summary>
	/// JNI signature for <c>boolean[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JBooleanArraySignature)]
	public static readonly CString JBooleanArraySignature;
	/// <summary>
	/// JNI signature for <c>byte[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JByteArraySignature)]
	public static readonly CString JByteArraySignature;
	/// <summary>
	/// JNI signature for <c>char[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JCharArraySignature)]
	public static readonly CString JCharArraySignature;
	/// <summary>
	/// JNI signature for <c>double[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JDoubleArraySignature)]
	public static readonly CString JDoubleArraySignature;
	/// <summary>
	/// JNI signature for <c>float[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JFloatArraySignature)]
	public static readonly CString JFloatArraySignature;
	/// <summary>
	/// JNI signature for <c>int[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JIntArraySignature)]
	public static readonly CString JIntArraySignature;
	/// <summary>
	/// JNI signature for <c>long[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JLongArraySignature)]
	public static readonly CString JLongArraySignature;
	/// <summary>
	/// JNI signature for <c>short[]</c> object.
	/// </summary>
	[DefaultValue(PrimitiveArraySignatures.JShortArraySignature)]
	public static readonly CString JShortArraySignature;
}