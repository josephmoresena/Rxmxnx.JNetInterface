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
		String message = resource.InvalidInstantiation(metadata.ClassName);
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
	[ExcludeFromCodeCoverage]
	public static Boolean ThrowVoidEquality()
	{
		IMessageResource resource = IMessageResource.GetInstance();
		throw new InvalidOperationException(resource.VoidEquality);
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
		String message = resource.InvalidCastTo(typeMetadata.ClassName);
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

		Byte prefix = signature[0];
		Byte suffix = signature[^1];

		if (prefix == CommonNames.ArraySignaturePrefixChar)
			switch (signature.Length)
			{
				case <= 3:
				case > 3 when signature[1] != CommonNames.ObjectSignaturePrefixChar ||
					suffix != CommonNames.ObjectSignatureSuffixChar:
					throw new ArgumentException(resource.InvalidSignatureMessage);
			}
		else if (prefix != CommonNames.ObjectSignaturePrefixChar || suffix != CommonNames.ObjectSignatureSuffixChar)
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
		String message = resource.InvalidMetadata(dataTypeMetadata.ClassName, typeOfT);
		throw new ArgumentException(message);
	}
}