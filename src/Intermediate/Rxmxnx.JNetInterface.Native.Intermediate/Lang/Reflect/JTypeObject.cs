namespace Rxmxnx.JNetInterface.Lang.Reflect;

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
	private static readonly JInterfaceTypeMetadata<JTypeObject> typeMetadata = TypeMetadataBuilder<JTypeObject>
	                                                                           .Create(
		                                                                           UnicodeClassNames.TypeInterface())
	                                                                           .Build();

	static JInterfaceTypeMetadata<JTypeObject> IInterfaceType<JTypeObject>.Metadata => JTypeObject.typeMetadata;

	/// <inheritdoc/>
	private JTypeObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JTypeObject IInterfaceType<JTypeObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}