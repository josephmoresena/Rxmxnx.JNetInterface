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
		if (sizeOf <= JValue.Size) return;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new InsufficientMemoryException(resource.InvalidType);
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
		IMessageResource resource = IMessageResource.GetInstance();
		String className = ITypeInformation.GetJavaClassName(metadata);
		String message = resource.InvalidInstantiation(className);
		throw new InvalidOperationException(message);
	}
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to retrieve a void argument.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static JArgumentMetadata ThrowVoidArgument()
	{
		IMessageResource resource = IMessageResource.GetInstance();
		throw new InvalidOperationException(resource.VoidArgument);
	}
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to create a void value.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static IPrimitiveType ThrowVoidInstantiation()
	{
		IMessageResource resource = IMessageResource.GetInstance();
		throw new InvalidOperationException(resource.VoidInstantiation);
	}
#if PACKAGE
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to retrieve a void array metadata.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static JArrayTypeMetadata ThrowVoidArray()
	{
		IMessageResource resource = IMessageResource.GetInstance();
		throw new InvalidOperationException(resource.VoidArray);
	}
#endif
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to create a void value.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean ThrowVoidEquality()
	{
		IMessageResource resource = IMessageResource.GetInstance();
		throw new InvalidOperationException(resource.VoidEquality);
	}
	/// <summary>
	/// Throws an exception for an instance that cannot be cast to a <typeparamref name="T"/> value.
	/// </summary>
	/// <param name="nativeType">The invalid <see cref="JNativeType"/> value.</param>
	/// <exception cref="InvalidCastException">Always throws an exception.</exception>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ThrowInvalidCastToNativeDataType<T>(JNativeType nativeType) where T : unmanaged
	{
		Byte signature = nativeType switch
		{
			JNativeType.JBoolean => CommonNames.BooleanSignatureChar,
			JNativeType.JByte => CommonNames.ByteSignatureChar,
			JNativeType.JChar => CommonNames.CharSignatureChar,
			JNativeType.JDouble => CommonNames.DoubleSignatureChar,
			JNativeType.JFloat => CommonNames.FloatSignatureChar,
			JNativeType.JInt => CommonNames.IntSignatureChar,
			JNativeType.JLong => CommonNames.DoubleSignatureChar,
			JNativeType.JShort => CommonNames.ShortSignatureChar,
			_ => CommonNames.ObjectSignaturePrefixChar,
		};
		CommonValidationUtilities.ThrowInvalidCastToPrimitive(signature);
		return default;
	}
	/// <summary>
	/// Throws an exception for an instance that cannot be cast to a <paramref name="primitiveSignature"/> value.
	/// </summary>
	/// <param name="primitiveSignature">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <exception cref="InvalidCastException">Always throws an exception.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowInvalidCastToPrimitive(Byte primitiveSignature)
	{
		IMessageResource resource = IMessageResource.GetInstance();
		String className = ClassNameHelper.GetPrimitiveClassName(primitiveSignature);
		String message = resource.InvalidCastTo(className);
		throw new InvalidCastException(message);
	}
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
		if (value is not null)
			return (TValue)value.ToType(typeof(TValue), CultureInfo.CurrentCulture);

		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.InvalidCastTo(typeof(TValue));
		throw new InvalidCastException(message);
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
		if (allowedCast) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String className = ITypeInformation.GetJavaClassName(typeMetadata);
		String message = resource.InvalidCastTo(className);
		throw new InvalidCastException(message);
	}
	/// <summary>
	/// Throws an exception if <paramref name="signature"/> is invalid.
	/// </summary>
	/// <param name="signature">Signature.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="signature"/> is invalid.
	/// </exception>
	public static void ThrowIfInvalidObjectSignature(ReadOnlySpan<Byte> signature)
	{
		IMessageResource resource = IMessageResource.GetInstance();

		if (signature.IsEmpty) throw new ArgumentException(resource.InvalidSignatureMessage);
		if (signature.Length == 1) throw new ArgumentException(resource.SignatureNotAllowed);

		if (signature[^1] != CommonNames.ObjectSignatureSuffixChar)
			throw new ArgumentException(resource.InvalidSignatureMessage);

		Byte prefix = signature[0];
		while (prefix == CommonNames.ArraySignaturePrefixChar && signature.Length > 2)
		{
			signature = signature[1..];
			prefix = signature[0];
		}

		if (prefix != CommonNames.ObjectSignaturePrefixChar || signature.Length == 2)
			throw new ArgumentException(resource.InvalidSignatureMessage);
	}
	/// <summary>
	/// Throws an exception if <paramref name="value"/> is <see cref="ReadOnlySpan{Byte}.Empty"/>.
	/// </summary>
	/// <param name="value">A UTF-8 string.</param>
	/// <param name="paramName">The name of <paramref name="value"/>.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="value"/> is <see langword="null"/> or <see cref="CString.Empty"/>.
	/// </exception>
	public static void ValidateNotEmpty(ReadOnlySpan<Byte> value,
		[CallerArgumentExpression(nameof(value))] String paramName = "")
	{
		if (!value.IsEmpty) return;

		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.EmptyString(paramName);
		throw new InvalidOperationException(message);
	}
	/// <summary>
	/// Throws an exception if proxy flags not match.
	/// </summary>
	/// <param name="isProxyObject">Object proxy flag.</param>
	/// <param name="isMetadataProxy">Metadata proxy flag.</param>
	public static void ThrowIfNoProxyMatch(Boolean isProxyObject, Boolean isMetadataProxy)
	{
		if (isProxyObject == isMetadataProxy) return;

		IMessageResource resource = IMessageResource.GetInstance();
		String message = isProxyObject ? resource.ProxyOnNonProxyProcess : resource.NonProxyOnProxyProcess;
		throw new InvalidOperationException(message);
	}
	/// <summary>
	/// Throws an exception if CLR type doesn't match with metadata type.
	/// </summary>
	/// <param name="typeOfT">Datatype CLR type.</param>
	/// <param name="dataTypeMetadata">Datatype metadata.</param>
	public static void ThrowIfInvalidMetadata(Type typeOfT, JDataTypeMetadata dataTypeMetadata)
	{
		if (dataTypeMetadata.IsValidForType(typeOfT)) return;

		IMessageResource resource = IMessageResource.GetInstance();
		String className = ITypeInformation.GetJavaClassName(dataTypeMetadata);
		String message = resource.InvalidMetadata(className, typeOfT);
		throw new ArgumentException(message);
	}
	/// <summary>
	/// Throws an exception if <paramref name="level"/> is an invalid dimension for an array of dimension
	/// <paramref name="currentDimension"/>.
	/// </summary>
	/// <param name="currentTypeMetadata">Current <see cref="JDataTypeMetadata"/> instance.</param>
	/// <param name="currentDimension">Number of current datatype dimensions.</param>
	/// <param name="level">Level of nesting for current type.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws and exception if <paramref name="currentDimension"/> value is <see langword="null"/>.
	/// </exception>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws an exception if <paramref name="level"/> is an invalid dimension for an array of dimension
	/// <paramref name="currentDimension"/>.
	/// </exception>
	public static void ThrowIfInvalidDimension(JDataTypeMetadata currentTypeMetadata, Int32? currentDimension,
		Int32 level)
	{
		IMessageResource resource = IMessageResource.GetInstance();
		if (currentDimension is null)
			throw new InvalidOperationException(resource.MissingArrayTypeMetadata(currentTypeMetadata));
		Int32 finalDimension = currentDimension.Value + level;
		if (finalDimension is <= Byte.MinValue or >= Byte.MaxValue)
			throw new ArgumentOutOfRangeException(nameof(level), resource.InvalidArrayDimension);
	}
}