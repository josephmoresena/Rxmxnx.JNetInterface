namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utility class for argument validation.
/// </summary>
internal static class NativeValidationUtilities
{
	/// <summary>
	/// Throws an exception if the class of <paramref name="classTypeMetadata"/> is abstract.
	/// </summary>
	/// <param name="classTypeMetadata">A <see cref="JClassTypeMetadata"/> instance.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if the class of <paramref name="classTypeMetadata"/> is abstract.
	/// </exception>
	public static void ThrowIfAbstractClass(JClassTypeMetadata classTypeMetadata)
	{
		if (classTypeMetadata.Modifier == JTypeModifier.Abstract)
			throw new InvalidOperationException($"{classTypeMetadata.ClassName} is an abstract type.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="interfaceName"/> can't extend an interface whose super-interfaces are
	/// <paramref name="superInterfacesSet"/>.
	/// </summary>
	/// <param name="interfaceName">Name of implementing type.</param>
	/// <param name="interfaceType">Type of implementing type.</param>
	/// <param name="superInterfacesSet">Super-interfaces type set of super interface.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="interfaceName"/> can't extend an interface whose super-interfaces are
	/// <paramref name="superInterfacesSet"/>.
	/// </exception>
	public static void ThrowIfInvalidExtension(ReadOnlySpan<Byte> interfaceName, Type interfaceType,
		IReadOnlySet<Type> superInterfacesSet)
	{
		foreach (Type superInterfaceType in superInterfacesSet)
		{
			if (superInterfaceType == interfaceType)
				throw new InvalidOperationException(
					$"{interfaceName.ToCString()} type can't extend an interface type which extends it.");
		}
	}
	/// <summary>
	/// Throws an exception if <paramref name="classType"/> and <paramref name="className"/> are the same type.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <param name="classType">Class type.</param>
	/// <param name="baseClassType">Super class type.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="classType"/> and <paramref name="className"/> are the same type.
	/// </exception>
	public static void ThrowIfSameType(ReadOnlySpan<Byte> className, Type classType, Type baseClassType)
	{
		if (classType == baseClassType)
			throw new InvalidOperationException($"{className.ToCString()} class and super class can't be the same.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="className"/> can't extend a class whose super-classes are
	/// <paramref name="baseBaseTypes"/>.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <param name="baseTypes">Class base types.</param>
	/// <param name="baseBaseTypes">Base calss base types.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="className"/> can't extend a class whose super-classes are
	/// <paramref name="baseBaseTypes"/>.
	/// </exception>
	public static void ValidateBaseTypes(ReadOnlySpan<Byte> className, IReadOnlySet<Type> baseTypes,
		IReadOnlySet<Type> baseBaseTypes)
	{
		if (!baseTypes.IsProperSupersetOf(baseBaseTypes))
			throw new InvalidOperationException(
				$"{className.ToCString()} type can't be based on a type which is derived from it.");
	}
	/// <summary>
	/// Throws an exception if current data type is annotation.
	/// </summary>
	/// <param name="typeName">Data type name.</param>
	/// <param name="interfaceMetadata">Super interface type metadata.</param>
	/// <param name="isAnnotation">Indicates whether current data type is an annotation.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if current data type is annotation.
	/// </exception>
	public static void ThrowIfAnnotation(ReadOnlySpan<Byte> typeName, JInterfaceTypeMetadata interfaceMetadata,
		Boolean isAnnotation)
	{
		if (!isAnnotation) return;
		throw new InvalidOperationException(
			$"Unable to extend {interfaceMetadata.ClassName}. {typeName.ToCString()} is an annotation.");
	}
	/// <summary>
	/// Throws a <see cref="NotImplementedException"/> indicating current datatype is not implementing
	/// <paramref name="interfaceMetadata"/>.
	/// </summary>
	/// <param name="typeName">Name of implementing type.</param>
	/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
	/// <param name="isClass">Indicates whether implementing type is a class.</param>
	/// <exception cref="NotImplementedException">Always thrown.</exception>
	public static void ThrowInvalidImplementation(ReadOnlySpan<Byte> typeName, JInterfaceTypeMetadata interfaceMetadata,
		Boolean isClass)
	{
		String implementationType = isClass ? "implements" : "extends";
		throw new NotImplementedException(
			$"{typeName.ToCString()} type doesn't {implementationType} {interfaceMetadata.ClassName} interface.");
	}
	/// <summary>
	/// Throws a <see cref="NotImplementedException"/> if current datatype is not implementing
	/// some superinterfaces of <paramref name="interfaceMetadata"/>.
	/// </summary>
	/// <param name="typeName">Name of implementing type.</param>
	/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
	/// <param name="notContained">Names of not contained superinterfaces.</param>
	/// <param name="isClass">Indicates whether implementing type is a class.</param>
	/// <exception cref="NotImplementedException">
	/// Throws a <see cref="NotImplementedException"/> if current datatype is not implementing
	/// some superinterfaces of <paramref name="interfaceMetadata"/>.
	/// </exception>
	public static void ThrowIfInvalidImplementation(ReadOnlySpan<Byte> typeName,
		JInterfaceTypeMetadata interfaceMetadata, ISet<CString> notContained, Boolean isClass)
	{
		if (notContained.Count == 0) return;
		String implementationType = isClass ? "implements" : "extends";
		String interfacesName = notContained.Count == 1 ? "superinterface" : "superinterfaces";
		throw new NotImplementedException(
			$"{typeName.ToCString()} type doesn't {implementationType} {String.Join(", ", notContained)} {interfacesName} of {interfaceMetadata.ClassName} interface.");
	}

	/// <summary>
	/// Throws an exception if <paramref name="ordinal"/> is an invalid enum ordinal.
	/// </summary>
	/// <param name="enumTypeName">Enum type name.</param>
	/// <param name="list">A <see cref="IEnumFieldList"/> instance.</param>
	/// <param name="ordinal">Enum ordinal.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="ordinal"/> is negative.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="ordinal"/> is already defined for the current enum type.
	/// </exception>
	public static void ThrowIfInvalidOrdinal(ReadOnlySpan<Byte> enumTypeName, IEnumFieldList list, Int32 ordinal)
	{
		if (ordinal < 0)
			throw new ArgumentException($"Any ordinal for {enumTypeName.ToCString()} type must be zero or positive.");
		if (list.HasOrdinal(ordinal))
			throw new InvalidOperationException(
				$"{enumTypeName.ToCString()} has already a field with ({ordinal}) ordinal.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="hash"/> is an invalid field name hash.
	/// </summary>
	/// <param name="enumTypeName">Enum type name.</param>
	/// <param name="list">A <see cref="IEnumFieldList"/> instance.</param>
	/// <param name="hash">Enum name hash.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="hash"/> is empty.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="hash"/> is already defined for the current enum type.
	/// </exception>
	public static void ThrowIfInvalidHash(ReadOnlySpan<Byte> enumTypeName, IEnumFieldList list, String hash)
	{
		if (String.IsNullOrWhiteSpace(hash))
			throw new ArgumentException($"Any name for {enumTypeName.ToCString()} type must be non-empty.");
		if (list.HasHash(hash))
			throw new InvalidOperationException(
				$"{enumTypeName.ToCString()} has already a field with '{list[list[hash]]}' name.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="list"/> is invalid.
	/// </summary>
	/// <param name="enumTypeName">Enum type name.</param>
	/// <param name="list">A <see cref="IEnumFieldList"/> instance.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="list"/> is invalid.
	/// </exception>
	public static void ThrowIfInvalidList(ReadOnlySpan<Byte> enumTypeName, IEnumFieldList list)
	{
		IReadOnlySet<Int32> missing = list.GetMissingFields(out Int32 count, out Int32 maxOrdinal);
		if (missing.Count <= 0 && maxOrdinal == count - 1)
			return;
		String message = $"The enum field list for {enumTypeName.ToCString()} is invalid. " +
			$"Count: {count}. Maximum ordinal: {maxOrdinal}. " +
			(missing.Count > 0 ? $"Missing values: {String.Join(", ", missing)}." : "");
		throw new InvalidOperationException(message);
	}
	/// <summary>
	/// Throws an exception in illegal use of <see cref="JLocalObject.TypeMetadataBuilder"/>.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <param name="familyType">Family type name.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception in illegal use of <see cref="JLocalObject.TypeMetadataBuilder"/>.
	/// </exception>
	public static void ThrowIfInvalidTypeBuilder(ReadOnlySpan<Byte> className, Type? familyType)
	{
		if (familyType == typeof(JLocalObject)) return;

		String? expectedBuilder = default;
		if (familyType == typeof(JLocalObject.InterfaceView))
			expectedBuilder = $"{nameof(JLocalObject.InterfaceView)}.{nameof(JLocalObject.TypeMetadataBuilder)}";
		else if (familyType == typeof(JEnumTypeMetadata))
			expectedBuilder = $"{nameof(JEnumObject)}.{nameof(JLocalObject.TypeMetadataBuilder)}";
		else if (familyType == typeof(JThrowableObject))
			expectedBuilder = $"{nameof(JThrowableObject)}.{nameof(JLocalObject.TypeMetadataBuilder)}";

		throw !String.IsNullOrWhiteSpace(expectedBuilder) ?
			new InvalidOperationException($"To build {className.ToCString()} type metadata use {expectedBuilder}.") :
			new($"{className.ToCString()} is invalid reference type.");
	}

	/// <summary>
	/// Extension for <see cref="CString"/> creation.
	/// </summary>
	/// <param name="utf8Span">A UTF-8 byte span.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	private static CString ToCString(this ReadOnlySpan<Byte> utf8Span) => new(utf8Span);
}