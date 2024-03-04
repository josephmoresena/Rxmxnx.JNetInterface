namespace Rxmxnx.JNetInterface.Lang.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.GenericDeclaration</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JGenericDeclarationObject : JInterfaceObject<JGenericDeclarationObject>,
	IInterfaceType<JGenericDeclarationObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JGenericDeclarationObject> typeMetadata =
		TypeMetadataBuilder<JGenericDeclarationObject>.Create(UnicodeClassNames.GenericDeclarationInterface())
		                                              .Extends<JAnnotatedElementObject>().Build();

	static JInterfaceTypeMetadata<JGenericDeclarationObject> IInterfaceType<JGenericDeclarationObject>.Metadata
		=> JGenericDeclarationObject.typeMetadata;

	/// <inheritdoc/>
	private JGenericDeclarationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JGenericDeclarationObject IInterfaceType<JGenericDeclarationObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}