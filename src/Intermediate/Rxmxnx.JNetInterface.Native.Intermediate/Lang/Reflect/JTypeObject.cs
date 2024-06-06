namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JInterfaceTypeMetadata<JTypeObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Type</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JTypeObject : JInterfaceObject<JTypeObject>, IInterfaceType<JTypeObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JTypeObject>
	                                                    .Create("java/lang/reflect/Type"u8).Build();

	static TypeMetadata IInterfaceType<JTypeObject>.Metadata => JTypeObject.typeMetadata;

	/// <inheritdoc/>
	private JTypeObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JTypeObject IInterfaceType<JTypeObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}