namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an interface <see cref="IDataType"/> type.
/// </summary>
public abstract record JInterfaceTypeMetadata : JReferenceTypeMetadata
{
	/// <summary>
	/// CLR interface type.
	/// </summary>
	public abstract Type InterfaceType { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Interface;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Abstract;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="interfaceName">Interface name of current type.</param>
	/// <param name="signature">JNI signature for current type.</param>
	private protected JInterfaceTypeMetadata(ReadOnlySpan<Byte> interfaceName, ReadOnlySpan<Byte> signature) : base(
		interfaceName, signature) { }

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
}

/// <summary>
/// This record stores the metadata for an interface <see cref="IDataType"/> type.
/// </summary>
/// <typeparam name="TInterface">Type of java interface type.</typeparam>
public abstract record JInterfaceTypeMetadata<TInterface> : JInterfaceTypeMetadata
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <inheritdoc/>
	private protected JInterfaceTypeMetadata(ReadOnlySpan<Byte> interfaceName, ReadOnlySpan<Byte> signature) : base(
		interfaceName, signature) { }

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
}