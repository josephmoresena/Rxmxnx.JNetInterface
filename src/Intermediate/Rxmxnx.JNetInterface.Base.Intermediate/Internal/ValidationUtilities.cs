namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utility class for argument validation.
/// </summary>
internal static class ValidationUtilities
{
	/// <summary>
	/// Validates if <see langword="unmanaged"/> value can be safely stored into a <see cref="JValue"/>.
	/// </summary>
	/// <typeparam name="TValue"><see langword="unmanaged"/> type.</typeparam>
	/// <exception cref="InsufficientMemoryException">
	/// Thrown if the size of <see langword="unmanaged"/> type exceeds to <see cref="JValue.Size"/>.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfInvalidType<TValue>() where TValue : unmanaged
	{
		if (NativeUtilities.SizeOf<TValue>() > JValue.Size)
			throw new InsufficientMemoryException("The requested value can't be contained in a JValue.");
	}
	/// <summary>
	/// Throws a <see cref="NotImplementedException"/> for invalid <see cref="INumberBase{TSelf}"/> implementations.
	/// </summary>
	/// <typeparam name="TNumber">The type parameter of <see cref="INumberBase{TValue}"/>.</typeparam>
	/// <param name="result">The output value.</param>
	/// <returns>Always throws an exception.</returns>
	/// <exception cref="NotImplementedException">Always thrown.</exception>
	public static Boolean ThrowInvalidNumberBaseImplementation<TNumber>(out TNumber result)
		where TNumber : INumberBase<TNumber>
		=> throw new NotImplementedException("Protected methods on INumberBase<> could not be implemented.");
	/// <summary>
	/// Throws a <see cref="NotImplementedException"/> with a specified message in accordance with
	/// the <paramref name="interfaceName"/>.
	/// </summary>
	/// <typeparam name="TResult">The type of the requested result.</typeparam>
	/// <param name="interfaceName">The name of the interface that has the missing implementation.</param>
	/// <returns>Always throws an exception.</returns>
	/// <exception cref="NotImplementedException">Always thrown.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult ThrowInvalidInterface<TResult>(String interfaceName)
	{
		String messagePrefix = $"The {interfaceName} interface can't be implemented by itself.";
		String recommendation = interfaceName switch
		{
			"INative" =>
				"Please use primitive types such as JBoolean, JByte, JChar, JDouble, JFloat, JInt, JLong, JShort, or extend the JLocalObject class.",
			"IPrimitive" =>
				"Please use primitive types such as JBoolean, JByte, JChar, JDouble, JFloat, JInt, JLong, JShort.",
			_ => String.Empty,
		};
		String message = $"{messagePrefix} {recommendation}";
		throw new NotImplementedException(message);
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
		=> throw new InvalidOperationException("A void value can't be an argument.");
	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/> attempting to create a void value.
	/// </summary>
	/// <exception cref="InvalidOperationException">Always throws an exception.</exception>
	public static IPrimitiveType ThrowVoidInstantiation()
		=> throw new InvalidOperationException("A void value can't be created.");
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
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </summary>
	/// <typeparam name="TDataType"><see langword="IDatatype"/> type.</typeparam>
	/// <param name="allowedCast">Indicates whether current cast is allowed.</param>
	/// <exception cref="InvalidCastException">
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfInvalidCast<TDataType>(Boolean allowedCast) where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TDataType>();
		if (!allowedCast)
			throw new InvalidCastException($"The current instance can't be casted to {metadata.ClassName} type.");
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
		if (signature.IsEmpty) throw new ArgumentException("Invalid signature.");

		if (signature.Length == 1)
		{
			if (!allowPrimitive)
				throw new ArgumentException("Signature not allowed.");
			return;
		}

		Byte prefix = signature[0];
		Byte suffix = signature[^1];

		if (prefix == UnicodeObjectSignatures.ArraySignaturePrefixChar)
			switch (signature.Length)
			{
				case < 2:
					throw new ArgumentException("Invalid signature.");
				case > 2 when signature[1] != UnicodeObjectSignatures.ObjectSignaturePrefixChar ||
					suffix != UnicodeObjectSignatures.ObjectSignatureSuffixChar:
					throw new ArgumentException("Invalid signature.");
			}
		else if (prefix != UnicodeObjectSignatures.ObjectSignaturePrefixChar ||
		         suffix != UnicodeObjectSignatures.ObjectSignatureSuffixChar)
			throw new ArgumentException("Invalid signature.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="value"/> named <paramref name="nameofValue"/> is not null-terminated
	/// UTF-8 string.
	/// </summary>
	/// <param name="value">A UTF-8 string.</param>
	/// <param name="nameofValue">The name of <paramref name="value"/>.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="value"/> named <paramref name="nameofValue"/> is not null-terminated
	/// UTF-8 string.
	/// </exception>
	public static void ThrowIfNotNullTerminatedCString(CString value,
		[CallerArgumentExpression(nameof(value))] String nameofValue = "")
	{
		if (!value.IsNullTerminated)
			throw new InvalidOperationException($"{nameofValue} must be null-terminated UTF-8 string.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="value"/> named <paramref name="paramName"/> is not null-terminated
	/// UTF-8 string.
	/// </summary>
	/// <param name="value">A UTF-8 string.</param>
	/// <param name="paramName">The name of <paramref name="value"/>.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="value"/> named <paramref name="paramName"/> is not null-terminated
	/// UTF-8 string.
	/// </exception>
	public static CString ValidateNullTermination(CString? value,
		[CallerArgumentExpression(nameof(value))] String paramName = "")
	{
		ArgumentNullException.ThrowIfNull(value, paramName);
		if (!value.IsNullTerminated)
			throw new InvalidOperationException($"{paramName} must be null-terminated UTF-8 string.");
		return value;
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
	public static CString ValidateNotEmpty(CString? value,
		[CallerArgumentExpression(nameof(value))] String paramName = "")
	{
		if (CString.IsNullOrEmpty(value))
			throw new InvalidOperationException($"{paramName} must be non-empty string");
		return value;
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
	/// Throws an exception if current sequence is not valid.
	/// </summary>
	/// <param name="isInvalid">Indicates whether current instance is invalid valid.</param>
	/// <exception cref="InvalidOperationException">Throws an exception if current sequence is not valid.</exception>
	public static void ThrowIfInvalidSequence(IWrapper<Boolean> isInvalid)
	{
		if (isInvalid.Value)
			throw new InvalidOperationException("The sequence is no longer valid.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="jObject"/> is dummy.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="message">Exception message.</param>
	/// <exception cref="ArgumentException">Throws an exception if <paramref name="jObject"/> is dummy.</exception>
	public static void ThrowIfDummy(JReferenceObject? jObject, String? message = default)
	{
		if (jObject is not null && jObject.IsProxy)
			throw new ArgumentException(message ?? "Invalid JReferenceObject.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="definition"/> doesn't match with <paramref name="otherDefinition"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <param name="otherDefinition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="definition"/> doesn't match with <paramref name="otherDefinition"/>.
	/// </exception>
	public static void ThrowIfNotMatchDefinition(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
	{
		if (definition.Information.ToString() != otherDefinition.Information.ToString())
			throw new ArgumentException($"[{definition}] Expected: [{otherDefinition}].");
	}
	/// <summary>
	/// Throws an exception if <paramref name="jObject"/> is default.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="message">Exception message.</param>
	/// <exception cref="InvalidOperationException">Throws an exception if <paramref name="jObject"/> is default.</exception>
	public static void ThrowIfDefault(JReferenceObject jObject, String? message = default)
	{
		if (jObject.IsDefault)
			throw new ArgumentException(message ?? "Disposed JReferenceObject.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="thread"/> is different to current thread.
	/// </summary>
	/// <param name="thread">A <see cref="Thread"/>.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="thread"/> is different to current
	/// thread.
	/// </exception>
	public static void ThrowIfDifferentThread(Thread thread)
	{
		if (thread != Thread.CurrentThread)
			throw new InvalidOperationException("JNI Environment is assigned to another thread.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="length"/> is invalid.
	/// </summary>
	/// <param name="length">Array length.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="length"/> is invalid.
	/// </exception>
	public static void ThrowIfInvalidArrayLength(Int32 length)
	{
		if (length < 0)
			throw new ArgumentException("Array length must be zero or positive.", nameof(length));
	}
	/// <summary>
	/// Throws an exception if <paramref name="result"/> is not <see cref="JResult.Ok"/>.
	/// </summary>
	/// <param name="result">A <see cref="JResult"/> value.</param>
	/// <exception cref="JniException">
	/// Throws an exception if <paramref name="result"/> is not <see cref="JResult.Ok"/>.
	/// </exception>
	public static void ThrowIfInvalidResult(JResult result)
	{
		if (result != JResult.Ok)
			throw new JniException(result);
	}
	/// <summary>
	/// Throws an exception if <paramref name="version"/> is invalid.
	/// </summary>
	/// <param name="version">JNI version.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="version"/> is invalid.
	/// </exception>
	public static Int32 ThrowIfInvalidVersion(Int32 version)
		=> version > 0 ? version : throw new InvalidOperationException();
}