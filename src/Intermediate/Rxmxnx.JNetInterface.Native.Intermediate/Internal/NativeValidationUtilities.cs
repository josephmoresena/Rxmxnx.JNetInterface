namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utility class for argument validation.
/// </summary>
internal static class NativeValidationUtilities
{
	/// <summary>
	/// Throws a <see cref="NotImplementedException"/> indicating current datatype is not implementing
	/// <typeparamref name="TInterface"/>.
	/// </summary>
	/// <typeparam name="TInterface">Type of <see cref="IInterfaceType{TInterface}"/></typeparam>
	/// <param name="typeName">Name of implementing type.</param>
	/// <param name="isClass">Indicates whether implementing type is a class.</param>
	/// <exception cref="NotImplementedException">Always thrown.</exception>
	public static void ThrowInvalidImplementation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>(CString typeName, Boolean isClass)
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		JDataTypeMetadata interfaceMetadata = IDataType.GetMetadata<TInterface>();
		String implementationType = isClass ? "implements" : "extends";
		throw new NotImplementedException(
			$"{typeName} type doesn't {implementationType} {interfaceMetadata.ClassName} interface.");
	}
	
	/// <summary>
	/// Throws an exception if <paramref name="ordinal"/> is an invalid enum ordinal.
	/// </summary>
	/// <param name="list">A <see cref="IEnumFieldList"/> instance.</param>
	/// <param name="ordinal">Enum ordinal.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="ordinal"/> is negative.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="ordinal"/> is already defined for current enum type.
	/// </exception>
	public static void ThrowIfInvalidOrdinal(IEnumFieldList list, Int32 ordinal)
	{
		if (ordinal < 0)
			throw new ArgumentException($"Any ordinal for {list.TypeName} type must be zero or positive.");
		if (list.HasOrdinal(ordinal))
			throw new InvalidOperationException($"{list.TypeName} has already a field with ({ordinal}) ordinal.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="hash"/> is an invalid field name hash.
	/// </summary>
	/// <param name="list">A <see cref="IEnumFieldList"/> instance.</param>
	/// <param name="hash">Enum name hash.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="hash"/> is empty.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="hash"/> is already defined for current enum type.
	/// </exception>
	public static void ThrowIfInvalidHash(IEnumFieldList list, String hash)
	{
		if (String.IsNullOrWhiteSpace(hash))
			throw new ArgumentException($"Any name for {list.TypeName} type must be non-empty.");
		if (list.HasHash(hash))
			throw new InvalidOperationException($"{list.TypeName} has already a field with '{list[list[hash]]}' name.");
	}
	/// <summary>
	/// Throws an exception if <paramref name="list"/> is invalid.
	/// </summary>
	/// <param name="list">A <see cref="IEnumFieldList"/> instance.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="list"/> is invalid.
	/// </exception>
	public static void ThrowIfInvalidList(IEnumFieldList list)
	{
		IReadOnlySet<Int32> missing = list.GetMissingFields(out Int32 count, out Int32 maxOrdinal);
		if (missing.Count <= 0 && maxOrdinal == count - 1)
			return;
		String message = $"The enum field list for {list.TypeName} is invalid. " +
			$"Count: {count}. Maximum ordinal: {maxOrdinal}. " +
			(missing.Count > 0 ? $"Missing values: {String.Join(", ", missing)}." : "");
		throw new InvalidOperationException(message);
	}
}