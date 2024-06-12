namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JClassLoaderObject>;

/// <summary>
/// This class represents a local <c>java.lang.ClassLoader</c> instance.
/// </summary>
public class JClassLoaderObject : JLocalObject, IClassType<JClassLoaderObject>
{
	private static readonly TypeMetadata typeMetadata =
		TypeMetadataBuilder<JClassLoaderObject>.Create("java/lang/ClassLoader"u8).Build();

	static TypeMetadata IClassType<JClassLoaderObject>.Metadata => JClassLoaderObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassLoaderObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassLoaderObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassLoaderObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassLoaderObject IClassType<JClassLoaderObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassLoaderObject IClassType<JClassLoaderObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassLoaderObject IClassType<JClassLoaderObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}