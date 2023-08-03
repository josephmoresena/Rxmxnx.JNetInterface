namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IInterfaceType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Interface;

	/// <summary>
	/// Retrieves the metadata for given interface type.
	/// </summary>
	/// <typeparam name="TInterface">Type of current java interface datatype.</typeparam>
	/// <returns>The <see cref="JInterfaceTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JInterfaceTypeMetadata GetMetadata<TInterface>()
		where TInterface : JReferenceObject, IInterfaceType<TInterface>
		=> (JInterfaceTypeMetadata)IDataType.GetMetadata<TInterface>();
}

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
/// <typeparam name="TInterface">Type of java interface type.</typeparam>
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
public interface IInterfaceType<out TInterface> : IInterfaceType, IReferenceType<TInterface>
	where TInterface : JReferenceObject, IInterfaceType<TInterface>
{
	/// <inheritdoc cref="IDataType{TClass}.ExcludingGenericTypes"/>
	private static readonly ImmutableHashSet<Type> excludingTypes =
		ImmutableHashSet.Create(typeof(IDataType<TInterface>), typeof(IReferenceType<TInterface>));

	static IImmutableSet<Type> IDataType<TInterface>.ExcludingGenericTypes => IInterfaceType<TInterface>.excludingTypes;
	static Type IDataType<TInterface>.SelfType => typeof(IInterfaceType<TInterface>);
}