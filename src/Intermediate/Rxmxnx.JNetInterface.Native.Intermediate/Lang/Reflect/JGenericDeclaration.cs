namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JInterfaceTypeMetadata<JGenericDeclarationObject>;

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
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JGenericDeclarationObject>
	                                                    .Create("java/lang/reflect/GenericDeclaration"u8)
	                                                    .Extends<JAnnotatedElementObject>().Build();

	static TypeMetadata IInterfaceType<JGenericDeclarationObject>.Metadata => JGenericDeclarationObject.typeMetadata;

	/// <inheritdoc/>
	private JGenericDeclarationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JGenericDeclarationObject IInterfaceType<JGenericDeclarationObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}