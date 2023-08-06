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
	/// Throws a <see cref="NotImplementedException"/> indicating current datatype is not implementing
	/// <typeparamref name="TInterface"/>.
	/// </summary>
	/// <typeparam name="TInterface">Type of <see cref="IInterfaceType{TInterface}"/></typeparam>
	/// <param name="typeName">Name of implementing type.</param>
	/// <param name="isClass">Indicates whether implementing type is a class.</param>
	/// <exception cref="NotImplementedException">Always thrown.</exception>
	public static void ThrowInvalidImplementation<TInterface>(CString typeName, Boolean isClass)
		where TInterface : JReferenceObject, IInterfaceType<TInterface>
	{
		JDataTypeMetadata interfaceMetadata = IDataType.GetMetadata<TInterface>();
		String implementationType = isClass ? "implements" : "extends";
		throw new NotImplementedException(
			$"{typeName} type doesn't {implementationType} {interfaceMetadata.ClassName} interface.");
	}
	/// <summary>
	/// Throws an exception if <typeparamref name="TInterface"/> can't extend <typeparamref name="TOtherInterface"/>.
	/// </summary>
	/// <typeparam name="TInterface">Type of <see cref="IInterfaceType{TInterface}"/></typeparam>
	/// <typeparam name="TOtherInterface">Type of <see cref="IInterfaceType{TInterface}"/></typeparam>
	/// <param name="typeName">Name of implementing type.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <typeparamref name="TInterface"/> can't extend <typeparamref name="TOtherInterface"/>.
	/// </exception>
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	public static void ThrowIfInvalidExtension<TInterface, TOtherInterface>(CString typeName)
		where TInterface : JReferenceObject, IReferenceType<TInterface>
		where TOtherInterface : JReferenceObject, IReferenceType<TOtherInterface>
	{
		Type derivedType = typeof(IDerivedType<TOtherInterface, TInterface>);
		foreach (Type interfaceType in IReferenceType<TOtherInterface>.GetInterfaceTypes())
		{
			if (interfaceType == derivedType)
				throw new InvalidOperationException(
					$"{typeName} type can't extend an interface type which extends it.");
		}
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
		Func<JReferenceObject, Boolean> evaluator) where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TDataType>();
		if (!evaluator(jObject))
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
	public static void ThrowIfInvalidSignature(CString? signature, Boolean allowPrimitive)
	{
		if (CString.IsNullOrEmpty(signature))
			throw new ArgumentException("Invalid signature.");

		if (signature.Length == 1)
		{
			if (!allowPrimitive)
				throw new ArgumentException("Signature not allowed.");
			return;
		}

		Byte prefix = signature[0];
		Byte suffix = signature[^1];

		if (prefix == UnicodeObjectSignatures.ArraySignaturePrefix[0])
			switch (signature.Length)
			{
				case < 2:
					throw new ArgumentException("Invalid signature.");
				case > 2 when signature[1] != UnicodeObjectSignatures.ObjectSignaturePrefix[0] ||
					suffix != UnicodeObjectSignatures.ObjectSignatureSuffix[0]:
					throw new ArgumentException("Invalid signature.");
			}
		else if (prefix != UnicodeObjectSignatures.ObjectSignaturePrefix[0] ||
		         suffix != UnicodeObjectSignatures.ObjectSignatureSuffix[0])
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
	/// Throws an exception if <typeparamref name="TBase"/> and <typeparamref name="TReference"/> are the same type.
	/// </summary>
	/// <typeparam name="TBase">Base type of <typeparamref name="TReference"/>.</typeparam>
	/// <typeparam name="TReference">Type of <see cref="IReferenceType{TReference}"/>.</typeparam>
	/// <param name="typeName">Name of <see cref="IReferenceType{TReference}"/> type.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <typeparamref name="TBase"/> and <typeparamref name="TReference"/> are the same type.
	/// </exception>
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	public static void ThrowIfSameType<TBase, TReference>(CString typeName)
		where TBase : JReferenceObject, IReferenceType<TBase> where TReference : TBase, IReferenceType<TReference>
	{
		if (typeof(TBase) == typeof(TReference))
			throw new InvalidOperationException($"{typeName} type and base type can't be the same.");
	}
	/// <summary>
	/// Throws an exception if <typeparamref name="TReference"/> is not a subclass of <typeparamref name="TBase"/>.
	/// </summary>
	/// <typeparam name="TBase">Base type of <typeparamref name="TReference"/>.</typeparam>
	/// <typeparam name="TReference">Type of <see cref="IReferenceType{TReference}"/>.</typeparam>
	/// <param name="typeName">Name of <see cref="IReferenceType{TReference}"/> type.</param>
	/// <returns>The set of <typeparamref name="TReference"/> base types.</returns>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <typeparamref name="TReference"/> is not a subclass of <typeparamref name="TBase"/>.
	/// </exception>
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	public static ISet<Type> ValidateBaseTypes<TBase, TReference>(CString typeName)
		where TBase : JReferenceObject, IReferenceType<TBase> where TReference : TBase, IReferenceType<TReference>
	{
		ISet<Type> baseBaseTypes = IReferenceType<TBase>.GetBaseTypes().ToHashSet();
		ISet<Type> baseTypes = IReferenceType<TReference>.GetBaseTypes().ToHashSet();
		if (!baseTypes.IsProperSupersetOf(baseBaseTypes))
			throw new InvalidOperationException($"{typeName} type can't be based on a type which is derived from it.");
		baseTypes.ExceptWith(baseBaseTypes);
		return baseTypes;
	}
}