namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utility class for argument validation.
/// </summary>
internal static class NativeValidationUtilities
{
	/// <summary>
	/// Throws an exception if <typeparamref name="TObject"/> is abstract.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <typeparamref name="TObject"/> is abstract.
	/// </exception>
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	public static void ThrowIfAbstractClass<TObject>() where TObject : JReferenceObject, IReferenceType<TObject>
	{
		JDataTypeMetadata typeMetadata = IDataType.GetMetadata<TObject>();
		if (typeMetadata.Modifier == JTypeModifier.Abstract)
			throw new InvalidOperationException($"{typeMetadata.ClassName} is an abstract type.");
	}
	/// <summary>
	/// Throws an exception if <typeparamref name="TReference"/> can't extend <typeparamref name="TOtherReference"/>.
	/// </summary>
	/// <typeparam name="TReference">Type of <see cref="IReferenceType"/>.</typeparam>
	/// <typeparam name="TOtherReference">Type of <see cref="IReferenceType"/>.</typeparam>
	/// <param name="typeName">Name of implementing type.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <typeparamref name="TReference"/> can't extend <typeparamref name="TOtherReference"/>.
	/// </exception>
	[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
	public static void ThrowIfInvalidExtension<TReference, TOtherReference>(ReadOnlySpan<Byte> typeName)
		where TReference : JReferenceObject, IReferenceType<TReference>
		where TOtherReference : JReferenceObject, IReferenceType<TOtherReference>
	{
		Type derivedType = typeof(IDerivedType<TOtherReference, TReference>);
		foreach (Type interfaceType in IReferenceType<TOtherReference>.GetInterfaceTypes())
		{
			if (interfaceType == derivedType)
				throw new InvalidOperationException(
					$"{typeName.ToCString()} type can't extend an interface type which extends it.");
		}
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
	public static void ThrowIfSameType<TBase, TReference>(ReadOnlySpan<Byte> typeName)
		where TBase : JReferenceObject, IReferenceType<TBase> where TReference : TBase, IReferenceType<TReference>
	{
		if (typeof(TBase) == typeof(TReference))
			throw new InvalidOperationException($"{typeName.ToCString()} type and base type can't be the same.");
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
	public static ISet<Type> ValidateBaseTypes<TBase, TReference>(ReadOnlySpan<Byte> typeName)
		where TBase : JReferenceObject, IReferenceType<TBase> where TReference : TBase, IReferenceType<TReference>
	{
		ISet<Type> baseBaseTypes = IReferenceType<TBase>.GetBaseTypes().ToHashSet();
		ISet<Type> baseTypes = IReferenceType<TReference>.GetBaseTypes().ToHashSet();
		if (!baseTypes.IsProperSupersetOf(baseBaseTypes))
			throw new InvalidOperationException(
				$"{typeName.ToCString()} type can't be based on a type which is derived from it.");
		baseTypes.ExceptWith(baseBaseTypes);
		return baseTypes;
	}

	/// <summary>
	/// Throws a <see cref="NotImplementedException"/> indicating current datatype is not implementing
	/// <typeparamref name="TInterface"/>.
	/// </summary>
	/// <typeparam name="TInterface">Type of <see cref="IInterfaceType{TInterface}"/></typeparam>
	/// <param name="typeName">Name of implementing type.</param>
	/// <param name="isClass">Indicates whether implementing type is a class.</param>
	/// <exception cref="NotImplementedException">Always thrown.</exception>
	public static void
		ThrowInvalidImplementation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>(
			ReadOnlySpan<Byte> typeName, Boolean isClass)
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		JDataTypeMetadata interfaceMetadata = IDataType.GetMetadata<TInterface>();
		String implementationType = isClass ? "implements" : "extends";
		throw new NotImplementedException(
			$"{typeName.ToCString()} type doesn't {implementationType} {interfaceMetadata.ClassName} interface.");
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
	/// Throws an exception if <paramref name="ordinal"/> is already defined for current enum type.
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
	/// Throws an exception if <paramref name="hash"/> is already defined for current enum type.
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
	/// Extension for <see cref="CString"/> creation.
	/// </summary>
	/// <param name="utf8Span">A UTF-8 byte span.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	private static CString ToCString(this ReadOnlySpan<Byte> utf8Span) => new(utf8Span);
}