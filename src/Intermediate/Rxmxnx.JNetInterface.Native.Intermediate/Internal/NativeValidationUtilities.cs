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
		if (classTypeMetadata.Modifier != JTypeModifier.Abstract) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.AbstractClass(classTypeMetadata.ClassName);
		throw new InvalidOperationException(message);
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
		using IEnumerator<Type> enumerator = superInterfacesSet.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current != interfaceType) continue;
			IMessageResource resource = IMessageResource.GetInstance();
			String message = resource.InvalidInterfaceExtension(interfaceName.GetString());
			throw new InvalidOperationException(message);
		}
	}
	/// <summary>
	/// Throws an exception if <paramref name="baseType"/> and <paramref name="typeName"/> are the same type.
	/// </summary>
	/// <param name="typeName">Class name.</param>
	/// <param name="baseType">Class type.</param>
	/// <param name="baseClassType">Super class type.</param>
	/// <param name="isInterface">Indicates whether current type is an interface.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="baseType"/> and <paramref name="typeName"/> are the same type.
	/// </exception>
	public static void ThrowIfSameType(ReadOnlySpan<Byte> typeName, Type baseType, Type baseClassType,
		Boolean isInterface = false)
	{
		if (baseType != baseClassType) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String typeNameString = typeName.GetString();
		String message = isInterface ?
			resource.SameInterfaceExtension(typeNameString) :
			resource.SameClassExtension(typeNameString);
		throw new InvalidOperationException(message);
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
		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.AnnotationType(interfaceMetadata.ClassName, typeName.GetString());
		throw new InvalidOperationException(message);
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
		IMessageResource resource = IMessageResource.GetInstance();
		String typeNameString = typeName.GetString();
		String message = isClass ?
			resource.InvalidImplementation(interfaceMetadata.ClassName, typeNameString) :
			resource.InvalidExtension(interfaceMetadata.ClassName, typeNameString);
		throw new NotImplementedException(message);
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
		JInterfaceTypeMetadata interfaceMetadata, IReadOnlySet<CString> notContained, Boolean isClass)
	{
		if (notContained.Count == 0) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String typeNameString = typeName.GetString();
		String message = isClass switch
		{
			true when notContained.Count == 1 => resource.InvalidImplementation(
				interfaceMetadata.ClassName, typeNameString, notContained.First()),
			true => resource.InvalidImplementation(interfaceMetadata.ClassName, typeNameString, notContained),
			false when notContained.Count == 1 => resource.InvalidExtension(
				interfaceMetadata.ClassName, typeNameString, notContained.First()),
			_ => resource.InvalidExtension(interfaceMetadata.ClassName, typeNameString, notContained),
		};
		throw new NotImplementedException(message);
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
		IMessageResource resource = IMessageResource.GetInstance();
		if (ordinal < 0)
			throw new ArgumentException(resource.InvalidOrdinal(enumTypeName.GetString()));
		if (list.HasOrdinal(ordinal))
			throw new InvalidOperationException(resource.DuplicateOrdinal(enumTypeName.GetString(), ordinal));
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
		IMessageResource resource = IMessageResource.GetInstance();
		if (String.IsNullOrWhiteSpace(hash))
			throw new ArgumentException(resource.InvalidValueName(enumTypeName.GetString()));
		if (list.HasHash(hash))
			throw new InvalidOperationException(
				resource.DuplicateValueName(enumTypeName.GetString(), list[list[hash]]));
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
		IMessageResource resource = IMessageResource.GetInstance();
		String enumTypeNameString = enumTypeName.GetString();
		String message = missing.Count > 0 ?
			resource.InvalidValueList(enumTypeNameString, count, maxOrdinal, missing) :
			resource.InvalidValueList(enumTypeNameString, count, maxOrdinal);
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

		IMessageResource resource = IMessageResource.GetInstance();
		String typeName = className.GetString();
		String message = !String.IsNullOrWhiteSpace(expectedBuilder) ?
			resource.InvalidBuilderType(typeName, expectedBuilder) :
			resource.InvalidReferenceType(typeName);
		throw new InvalidOperationException(message);
	}
	/// <summary>
	/// Throws an exception if <paramref name="callDefinition"/> is not a <see cref="JConstructorDefinition"/> instance.
	/// </summary>
	/// <param name="callDefinition">A <see cref="JCallDefinition"/> instance.</param>
	/// <returns><paramref name="callDefinition"/> as <see cref="JConstructorDefinition"/> instance.</returns>
	/// <exception cref="NotImplementedException"></exception>
	public static JConstructorDefinition ThrowIfNotConstructor(JCallDefinition callDefinition)
	{
		if (callDefinition is JConstructorDefinition definition) return definition;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new InvalidOperationException(resource.NotConstructorDefinition);
	}

	/// <summary>
	/// Extension for <see cref="CString"/> creation.
	/// </summary>
	/// <param name="utf8Span">A UTF-8 byte span.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static String GetString(this ReadOnlySpan<Byte> utf8Span) => Encoding.UTF8.GetString(utf8Span);
}