namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JInterfaceTypeMetadata<JGenericDeclarationObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.GenericDeclaration</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JGenericDeclarationObject : JInterfaceObject<JGenericDeclarationObject>,
	IInterfaceType<JGenericDeclarationObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.GenericDeclarationHash, 36);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JGenericDeclarationObject>(
			JGenericDeclarationObject.typeInfo, InterfaceSet.AnnotatedElementSet);

	static TypeMetadata IInterfaceType<JGenericDeclarationObject>.Metadata => JGenericDeclarationObject.typeMetadata;

	/// <inheritdoc/>
	private JGenericDeclarationObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JGenericDeclarationObject IInterfaceType<JGenericDeclarationObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}