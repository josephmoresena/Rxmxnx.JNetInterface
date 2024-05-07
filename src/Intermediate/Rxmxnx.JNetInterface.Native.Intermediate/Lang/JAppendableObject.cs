namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Appendable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JAppendableObject : JInterfaceObject<JAppendableObject>, IInterfaceType<JAppendableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JAppendableObject> typeMetadata =
		TypeMetadataBuilder<JAppendableObject>.Create(UnicodeClassNames.AppendableInterface()).Build();

	static JInterfaceTypeMetadata<JAppendableObject> IInterfaceType<JAppendableObject>.Metadata
		=> JAppendableObject.typeMetadata;

	/// <inheritdoc/>
	private JAppendableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAppendableObject IInterfaceType<JAppendableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}