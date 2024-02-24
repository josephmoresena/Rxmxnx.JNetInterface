namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.AnnotatedElement</c> instance.
/// </summary>
public sealed class JAnnotatedElementObject : JInterfaceObject<JAnnotatedElementObject>,
	IInterfaceType<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JAnnotatedElementObject> typeMetadata =
		TypeMetadataBuilder<JAnnotatedElementObject>.Create(UnicodeClassNames.AnnotatedElementInterface()).Build();

	static JInterfaceTypeMetadata<JAnnotatedElementObject> IInterfaceType<JAnnotatedElementObject>.Metadata
		=> JAnnotatedElementObject.typeMetadata;

	/// <inheritdoc/>
	private JAnnotatedElementObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JAnnotatedElementObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JAnnotatedElementObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAnnotatedElementObject IReferenceType<JAnnotatedElementObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JAnnotatedElementObject IReferenceType<JAnnotatedElementObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JAnnotatedElementObject IReferenceType<JAnnotatedElementObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}