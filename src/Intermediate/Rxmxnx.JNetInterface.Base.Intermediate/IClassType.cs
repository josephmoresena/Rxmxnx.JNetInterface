namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a java class type instance.
/// </summary>
public interface IClassType : IReferenceType
{
	/// <summary>
	/// Retrieves the metadata for given class type.
	/// </summary>
	/// <typeparam name="TClass">Type of current java class datatype.</typeparam>
	/// <returns>The <see cref="JClassMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JClassMetadata GetMetadata<TClass>() where TClass : IClassType
		=> (JClassMetadata)IDataType.GetMetadata<TClass>();
}

/// <summary>
/// This interface exposes an object that represents a java class type instance.
/// </summary>
/// <typeparam name="TClass">Type of java class type.</typeparam>
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
public interface IClassType<out TClass> : IClassType, IReferenceType<TClass>
	where TClass : JReferenceObject, IClassType<TClass>
{
	/// <inheritdoc cref="IReferenceType{TClass}.ExcludingGenericTypes"/>
	private static readonly ImmutableHashSet<Type> excludingTypes =
		ImmutableHashSet.Create(typeof(IDataType<TClass>), typeof(IReferenceType<TClass>), typeof(IClassType<TClass>));

	static IImmutableSet<Type> IReferenceType<TClass>.ExcludingGenericTypes => IClassType<TClass>.excludingTypes;
}