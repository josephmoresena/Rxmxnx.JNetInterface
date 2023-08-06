namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java data type.
/// </summary>
public interface IDataType
{
	/// <inheritdoc cref="IDataType.ExcludingTypes"/>
	internal static readonly ImmutableHashSet<Type> ExcludingBasicTypes = ImmutableHashSet.Create(
		typeof(IDisposable), typeof(IObject), typeof(IEquatable<JObject>), typeof(IDataType), typeof(IPrimitiveType),
		typeof(IReferenceType), typeof(IInterfaceType), typeof(IClassType), typeof(JReferenceObject));

	/// <summary>
	/// Excluding CLR types.
	/// </summary>
	internal static virtual IImmutableSet<Type> ExcludingTypes => IDataType.ExcludingBasicTypes;
	/// <summary>
	/// Datatype kind.
	/// </summary>
	internal static virtual JTypeKind Kind => JTypeKind.Undefined;
	/// <summary>
	/// Datatype family type.
	/// </summary>
	internal static abstract Type? FamilyType { get; }

	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected static virtual JDataTypeMetadata Metadata
		=> ValidationUtilities.ThrowInvalidInterface<JDataTypeMetadata>(nameof(IDataType));

	/// <summary>
	/// Retrieves the metadata for given type.
	/// </summary>
	/// <typeparam name="TDataType">Type of current java datatype.</typeparam>
	/// <returns>The <see cref="JDataTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JDataTypeMetadata GetMetadata<TDataType>() where TDataType : IDataType<TDataType>
		=> TDataType.Metadata;
}

/// <summary>
/// This interface exposes a java data type.
/// </summary>
/// <typeparam name="TDataType">Type of current Java datatype.</typeparam>
public interface IDataType<out TDataType> : IDataType where TDataType : IDataType<TDataType>
{
	/// <summary>
	/// Self CLR type.
	/// </summary>
	internal static virtual Type SelfType => typeof(IDataType<TDataType>);
	/// <summary>
	/// Excluding generic CLR types.
	/// </summary>
	internal static virtual IImmutableSet<Type> ExcludingGenericTypes
		=> ValidationUtilities.ThrowInvalidInterface<IImmutableSet<Type>>(nameof(IDataType));

	static JDataTypeMetadata IDataType.Metadata
		=> ValidationUtilities.ThrowInvalidInterface<JDataTypeMetadata>(nameof(IDataType));

	/// <summary>
	/// Creates a <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>A <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.</returns>
	static abstract TDataType? Create(JObject? jObject);
}