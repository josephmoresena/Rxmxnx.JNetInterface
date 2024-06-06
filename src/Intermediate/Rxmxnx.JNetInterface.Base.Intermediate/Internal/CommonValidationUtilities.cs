namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utility class for argument validation.
/// </summary>
internal static class CommonValidationUtilities
{
	/// <summary>
	/// Validates if <see langword="unmanaged"/> value can be safely stored into a <see cref="JValue"/>.
	/// </summary>
	/// <param name="sizeOf">Size of <see langword="unmanaged"/> type.</param>
	/// <exception cref="InsufficientMemoryException">
	/// Thrown if the size of <see langword="unmanaged"/> type exceeds to <see cref="JValue.Size"/>.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfInvalidType(Int32 sizeOf)
	{
		if (sizeOf > JValue.Size)
			throw new InsufficientMemoryException("The requested value can't be contained in a JValue.");
	}
	/// <summary>
	/// Throws an <see cref="InvalidEnumArgumentException"/> for the specified <paramref name="nativeType"/>.
	/// </summary>
	/// <param name="nativeType">The invalid <see cref="JNativeType"/> value.</param>
	/// <returns>Always throws an exception.</returns>
	/// <exception cref="InvalidEnumArgumentException">Always thrown.</exception>
	public static String ThrowInvalidNativeType(JNativeType nativeType)
		=> throw new InvalidEnumArgumentException(nameof(nativeType), (Int32)nativeType, typeof(JNativeType));
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> for the specified <typeparamref name="TReference"/>.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IDataType{TReference}"/> type.</typeparam>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static TReference ThrowInvalidInstantiation<TReference>()
		where TReference : JReferenceObject, IDataType<TReference>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TReference>();
		throw new InvalidOperationException($"{metadata.ClassName} not is an instantiable type.");
	}
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to retrieve a void argument.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static JArgumentMetadata ThrowVoidArgument()
		=> throw new InvalidOperationException("The void type can't be an instance argument.");
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to create a void value.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static IPrimitiveType ThrowVoidInstantiation()
		=> throw new InvalidOperationException("A void value can't be created.");
#if PACKAGE
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to retrieve a void array metadata.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static JArrayTypeMetadata ThrowVoidArray()
		=> throw new InvalidOperationException("The void type can't be element type of an array.");
#endif
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to create a void value.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static Boolean ThrowVoidEquality()
		=> throw new InvalidOperationException("A Void instance can't be equatable.");
	/// <summary>
	/// Throws an exception if <paramref name="value"/> cannot be cast to <typeparamref name="TValue"/>.
	/// </summary>
	/// <param name="value">Convertible value.</param>
	/// <typeparam name="TValue">Type of the result value.</typeparam>
	/// <returns>
	/// The resulting <typeparamref name="TValue"/> instance from casting <paramref name="value"/>.
	/// </returns>
	/// <exception cref="InvalidCastException">
	/// Thrown if <paramref name="value"/> is <see langword="null"/> or cannot be cast to <typeparamref name="TValue"/>.
	/// </exception>
	public static TValue ThrowIfInvalidCast<TValue>(IConvertible? value) where TValue : unmanaged
	{
		if (value is null)
			throw new InvalidCastException($"Invalid cast to {typeof(TValue)}");
		return (TValue)value.ToType(typeof(TValue), CultureInfo.CurrentCulture);
	}
	/// <summary>
	/// Throws an exception if the instance cannot be cast to <paramref name="typeMetadata"/> instance.
	/// </summary>
	/// <param name="typeMetadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <param name="allowedCast">Indicates whether current cast is allowed.</param>
	/// <exception cref="InvalidCastException">
	/// Throws an exception if the instance cannot be cast to <paramref name="typeMetadata"/> instance.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfInvalidCast(JDataTypeMetadata typeMetadata, Boolean allowedCast)
	{
		if (!allowedCast)
			throw new InvalidCastException($"The current instance can't be casted to {typeMetadata.ClassName} type.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="signature"/> is invalid.
	/// </summary>
	/// <param name="signature">Signature.</param>
	/// <param name="allowPrimitive">Indicates whether allow primitive signatures.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="signature"/> is invalid.
	/// </exception>
	public static void ThrowIfInvalidSignature(ReadOnlySpan<Byte> signature, Boolean allowPrimitive)
	{
		if (signature.IsEmpty) throw new ArgumentException(CommonConstants.InvalidSignatureMessage);

		if (signature.Length == 1)
		{
			if (!allowPrimitive)
				throw new ArgumentException("Signature not allowed.");
			CommonValidationUtilities.ThrowIfInvalidPrimitiveSignature(signature[0]);
		}

		Byte prefix = signature[0];
		Byte suffix = signature[^1];

		if (prefix == CommonNames.ArraySignaturePrefixChar)
			switch (signature.Length)
			{
				case 2:
					if (!allowPrimitive)
						throw new ArgumentException("Array signature not allowed.");
					CommonValidationUtilities.ThrowIfInvalidPrimitiveSignature(signature[1]);
					break;
				case <= 3:
					throw new ArgumentException(CommonConstants.InvalidSignatureMessage);
				case > 3 when signature[1] != CommonNames.ObjectSignaturePrefixChar ||
					suffix != CommonNames.ObjectSignatureSuffixChar:
					throw new ArgumentException(CommonConstants.InvalidSignatureMessage);
			}
		else if (prefix != CommonNames.ObjectSignaturePrefixChar || suffix != CommonNames.ObjectSignatureSuffixChar)
			throw new ArgumentException(CommonConstants.InvalidSignatureMessage);
	}
	/// <summary>
	/// Throws an exception if <paramref name="value"/> is <see langword="null"/> or <see cref="CString.Empty"/>.
	/// </summary>
	/// <param name="value">A UTF-8 string.</param>
	/// <param name="paramName">The name of <paramref name="value"/>.</param>
	/// <returns>A non-empty <see cref="CString"/> instance.</returns>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="value"/> is <see langword="null"/> or <see cref="CString.Empty"/>.
	/// </exception>
	public static ReadOnlySpan<Byte> ValidateNotEmpty(ReadOnlySpan<Byte> value,
		[CallerArgumentExpression(nameof(value))] String paramName = "")
	{
		if (value.IsEmpty)
			throw new InvalidOperationException($"{paramName} must be non-empty string");
		return value;
	}

	/// <summary>
	/// Throws an exception if <paramref name="signature"/> is not valid primitive signature.
	/// </summary>
	/// <param name="signature">A signature char.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="signature"/> is not valid primitive signature.
	/// </exception>
	private static void ThrowIfInvalidPrimitiveSignature(Byte signature)
	{
		switch (signature)
		{
			case CommonNames.BooleanSignatureChar:
			case CommonNames.ByteSignatureChar:
			case CommonNames.CharSignatureChar:
			case CommonNames.DoubleSignatureChar:
			case CommonNames.FloatSignatureChar:
			case CommonNames.IntSignatureChar:
			case CommonNames.LongSignatureChar:
			case CommonNames.ShortSignatureChar:
				return;
		}
		throw new ArgumentException("Invalid primitive signature.");
	}
}