namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
[Browsable(false)]
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
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		=> (JInterfaceTypeMetadata)IDataType.GetMetadata<TInterface>();
}

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
/// <typeparam name="TInterface">Type of java interface type.</typeparam>
public interface IInterfaceType<TInterface> : IInterfaceType, IReferenceType<TInterface>, IInterfaceObject<TInterface>
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JInterfaceTypeMetadata<TInterface> Metadata { get; }

	static JDataTypeMetadata IDataType<TInterface>.Metadata => TInterface.Metadata;
}