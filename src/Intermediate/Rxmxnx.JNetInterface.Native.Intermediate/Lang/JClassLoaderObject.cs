namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ClassLoader</c> instance.
/// </summary>
public class JClassLoaderObject : JLocalObject, IClassType<JClassLoaderObject>
{
	private static readonly JClassTypeMetadata<JClassLoaderObject> typeMetadata =
		JTypeMetadataBuilder<JClassLoaderObject>.Create(UnicodeClassNames.ClassLoaderObject()).Build();

	static JClassTypeMetadata<JClassLoaderObject> IClassType<JClassLoaderObject>.Metadata
		=> JClassLoaderObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassLoaderObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassLoaderObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassLoaderObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JClassLoaderObject IReferenceType<JClassLoaderObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassLoaderObject IReferenceType<JClassLoaderObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassLoaderObject IReferenceType<JClassLoaderObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}