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
	/// <exception cref="NotImplementedException">Always thrown.</exception>
	public static String ThrowInvalidNativeType(JNativeType nativeType)
		=> throw new InvalidEnumArgumentException(nameof(nativeType), (Int32)nativeType, typeof(JNativeType));
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
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="evaluator">Delegate to check <paramref name="jObject"/>.</param>
	/// <exception cref="InvalidCastException">
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfInvalidCast<TDataType>(JReferenceObject jObject,
		Func<JReferenceObject, Boolean> evaluator) where TDataType : IDataType
	{
		if (!evaluator(jObject))
			throw new InvalidCastException($"The current instance can't be casted to {TDataType.ClassName} type.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="signature"/> is invalid.
	/// </summary>
	/// <param name="signature">Signature.</param>
	/// <param name="allowPrimitive">Indicates whether allow primitive signatures.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="signature"/> is invalid.
	/// </exception>
	public static void ThrowIfInvalidSignature(CString? signature, Boolean allowPrimitive)
	{
		if (CString.IsNullOrEmpty(signature))
			throw new ArgumentException("Invalid signature.");
		if (signature.Length == 1)
		{
			if (!allowPrimitive)
				throw new ArgumentException("Signature not allowed.");
		}
		else if (signature[^1] != UnicodeObjectSignatures.ObjectSignatureSuffix[0])
		{
			throw new ArgumentException("Invalid signature.");
		}
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
}