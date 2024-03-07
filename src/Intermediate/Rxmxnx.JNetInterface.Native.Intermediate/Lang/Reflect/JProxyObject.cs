namespace Rxmxnx.JNetInterface.Lang.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Proxy</c> instance.
/// </summary>
public class JProxyObject : JLocalObject, IClassType<JProxyObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	internal static readonly JClassTypeMetadata<JProxyObject> ProxyTypeMetadata = TypeMetadataBuilder<JProxyObject>
		.Create(UnicodeClassNames.ProxyObject()).Build();

	static JClassTypeMetadata<JProxyObject> IClassType<JProxyObject>.Metadata => JProxyObject.ProxyTypeMetadata;

	/// <inheritdoc/>
	protected JProxyObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JProxyObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JProxyObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JProxyObject IClassType<JProxyObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JProxyObject IClassType<JProxyObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JProxyObject IClassType<JProxyObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}