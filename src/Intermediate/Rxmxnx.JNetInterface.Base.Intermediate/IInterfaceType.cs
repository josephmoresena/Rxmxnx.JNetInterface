namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
public interface IInterfaceType : IReferenceType
{
	/// <summary>
	/// Retrieves the metadata for given interface type.
	/// </summary>
	/// <typeparam name="TInterface">Type of current java interface datatype.</typeparam>
	/// <returns>The <see cref="JInterfaceMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JInterfaceMetadata GetMetadata<TInterface>() where TInterface : IClassType
		=> (JInterfaceMetadata)IDataType.GetMetadata<TInterface>();
}

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
/// <typeparam name="TInterface">Type of java interface type.</typeparam>
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
public interface IInterfaceType<out TInterface> : IInterfaceType, IReferenceType<TInterface>
	where TInterface : JReferenceObject, IInterfaceType<TInterface>
{
	/// <inheritdoc cref="IReferenceType{TClass}.ExcludingGenericTypes"/>
	private static readonly ImmutableHashSet<Type> excludingTypes =
		ImmutableHashSet.Create(typeof(IDataType<TInterface>), typeof(IReferenceType<TInterface>));
	static Type IReferenceType<TInterface>.SelfType => typeof(IInterfaceType<TInterface>);
}