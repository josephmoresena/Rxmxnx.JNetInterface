/// <summary>
/// This interface exposes an object that represents a java array type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IArrayType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Class;

	/// <summary>
	/// Retrieves the metadata for given array type.
	/// </summary>
	/// <typeparam name="TArray">Type of current java array datatype.</typeparam>
	/// <returns>The <see cref="JClassTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JArrayTypeMetadata GetMetadata<TArray>() where TArray : JReferenceObject, IArrayType<TArray>
		=> (JArrayTypeMetadata)IDataType.GetMetadata<TArray>();
}

/// <summary>
/// This interface exposes an object that represents a java array type instance.
/// </summary>
/// <typeparam name="TArray">Type of java class type.</typeparam>
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
public interface IArrayType<out TArray> : IArrayType, IReferenceType<TArray>
	where TArray : JReferenceObject, IArrayType<TArray>
{
	/// <inheritdoc cref="IDataType{TClass}.ExcludingGenericTypes"/>
	private static readonly ImmutableHashSet<Type> excludingTypes =
		ImmutableHashSet.Create(typeof(IDataType<TArray>), typeof(IReferenceType<TArray>));

	static IImmutableSet<Type> IDataType<TArray>.ExcludingGenericTypes => IArrayType<TArray>.excludingTypes;
	static Type IDataType<TArray>.SelfType => typeof(IArrayType<TArray>);
}