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
	/// <param name="signature">JNI signature for the current type.</param>
	/// <param name="isAnnotation">Indicates whether current type is an annotation.</param>
	private protected JInterfaceTypeMetadata(ReadOnlySpan<Byte> interfaceName, ReadOnlySpan<Byte> signature,
		Boolean isAnnotation) : base(interfaceName, signature)
		=> this.Kind = !isAnnotation ? JTypeKind.Interface : JTypeKind.Annotation;

	/// <inheritdoc/>
	public override String ToString() => base.ToString();

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
public abstract class JInterfaceTypeMetadata<TInterface> : JInterfaceTypeMetadata
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <inheritdoc/>
	private protected JInterfaceTypeMetadata(ReadOnlySpan<Byte> interfaceName, ReadOnlySpan<Byte> signature,
		Boolean isAnnotation) : base(interfaceName, signature, isAnnotation) { }

	/// <inheritdoc/>
	internal override Boolean IsInstance(JReferenceObject jObject)
		=> jObject is IInterfaceObject<TInterface> || jObject.InstanceOf<TInterface>();

	/// <inheritdoc/>
	internal override void SetAssignable(JProxyObject proxy) => proxy.SetAssignableTo<TInterface>(true);
}