﻿namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java objects signatures.
/// </summary>
internal static class MethodSignatures
{
	/// <summary>
	/// JNI signature for void return.
	/// </summary>
	public const Char VoidReturnSignatureChar = 'V';
	/// <summary>
	/// Prefix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	public const Char MethodParameterPrefixChar = '(';
	/// <summary>
	/// Sufix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	public const Char MethodParameterSuffixChar = ')';
}