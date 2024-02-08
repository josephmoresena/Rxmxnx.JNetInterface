namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.GenericDeclaration</c> instance.
/// </summary>
public sealed class JGenericDeclarationObject : JInterfaceObject<JGenericDeclarationObject>,
	IInterfaceType<JGenericDeclarationObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JGenericDeclarationObject> typeMetadata =
		JTypeMetadataBuilder<JGenericDeclarationObject>.Create(UnicodeClassNames.GenericDeclarationInterface())
		                                               .Extends<JAnnotatedElementObject>().Build();

	static JInterfaceTypeMetadata<JGenericDeclarationObject> IInterfaceType<JGenericDeclarationObject>.Metadata
		=> JGenericDeclarationObject.typeMetadata;

	/// <inheritdoc/>
	private JGenericDeclarationObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JGenericDeclarationObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JGenericDeclarationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JGenericDeclarationObject IReferenceType<JGenericDeclarationObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JGenericDeclarationObject IReferenceType<JGenericDeclarationObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JGenericDeclarationObject IReferenceType<JGenericDeclarationObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}