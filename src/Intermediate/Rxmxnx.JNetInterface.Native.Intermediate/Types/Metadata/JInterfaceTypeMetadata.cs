namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an interface <see cref="IDataType"/> type.
/// </summary>
public abstract class JInterfaceTypeMetadata : JReferenceTypeMetadata
{
	/// <summary>
	/// CLR interface type.
	/// </summary>
	public abstract Type InterfaceType { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind { get; }
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Abstract;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="interfaceName">Interface name of the current type.</param>
	/// <param name="isAnnotation">Indicates whether current type is an annotation.</param>
	private protected JInterfaceTypeMetadata(ReadOnlySpan<Byte> interfaceName, Boolean isAnnotation) :
		base(interfaceName)
		=> this.Kind = !isAnnotation ? JTypeKind.Interface : JTypeKind.Annotation;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="information">Internal sequence information.</param>
	/// <param name="isAnnotation">Indicates whether current type is an annotation.</param>
	private protected JInterfaceTypeMetadata(TypeInfoSequence information, Boolean isAnnotation) : base(information)
		=> this.Kind = !isAnnotation ? JTypeKind.Interface : JTypeKind.Annotation;

	/// <inheritdoc/>
	public override String? ToString()
		=> IVirtualMachine.TypeMetadataToStringEnabled ?
			MetadataTextUtilities.GetString(this, this.InterfaceProperties) :
			base.ToString();

	/// <summary>
	/// Sets <paramref name="proxy"/> instance as assignable to current type.
	/// </summary>
	/// <param name="proxy">A <see cref="JProxyObject"/> instance.</param>
	internal abstract void SetAssignable(JProxyObject proxy);
}

/// <summary>
/// This record stores the metadata for an interface <see cref="IDataType"/> type.
/// </summary>
/// <typeparam name="TInterface">Type of java interface type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class JInterfaceTypeMetadata<TInterface> : JInterfaceTypeMetadata
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <inheritdoc/>
	private protected JInterfaceTypeMetadata(ReadOnlySpan<Byte> interfaceName, Boolean isAnnotation) : base(
		interfaceName, isAnnotation) { }
	/// <inheritdoc/>
	private protected JInterfaceTypeMetadata(TypeInfoSequence information, Boolean isAnnotation) : base(
		information, isAnnotation) { }

	/// <inheritdoc/>
	internal override Boolean IsInstance(JReferenceObject jObject)
		=> jObject is IInterfaceObject<TInterface> || jObject.InstanceOf<TInterface>();

	/// <inheritdoc/>
	internal override void SetAssignable(JProxyObject proxy) => proxy.SetAssignableTo<TInterface>(true);
}