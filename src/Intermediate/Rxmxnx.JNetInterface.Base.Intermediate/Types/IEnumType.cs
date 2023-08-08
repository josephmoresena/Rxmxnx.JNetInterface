namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java enum type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IEnumType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Enum;

	/// <summary>
	/// Retrieves the metadata for given enum type.
	/// </summary>
	/// <typeparam name="TEnum">Type of current java enum datatype.</typeparam>
	/// <returns>The <see cref="JEnumTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JEnumTypeMetadata GetMetadata<TEnum>() where TEnum : JReferenceObject, IInterfaceType<TEnum>
		=> (JEnumTypeMetadata)IDataType.GetMetadata<TEnum>();
}

/// <summary>
/// This interface exposes an object that represents a java enum type instance.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
public interface IEnumType<out TEnum> : IEnumType, IReferenceType<TEnum>
	where TEnum : JReferenceObject, IEnumType<TEnum>
{
	/// <inheritdoc cref="IDataType{TClass}.ExcludingGenericTypes"/>
	private static readonly ImmutableHashSet<Type> excludingTypes =
		ImmutableHashSet.Create(typeof(IDataType<TEnum>), typeof(IReferenceType<TEnum>));

	static IImmutableSet<Type> IDataType<TEnum>.ExcludingGenericTypes => IEnumType<TEnum>.excludingTypes;
	static Type IDataType<TEnum>.SelfType => typeof(IEnumType<TEnum>);
}