namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.CharSequence</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JCharSequenceObject : JInterfaceObject<JCharSequenceObject>, IInterfaceType<JCharSequenceObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JCharSequenceObject> typeMetadata =
		TypeMetadataBuilder<JCharSequenceObject>.Create(UnicodeClassNames.CharSequenceInterface()).Build();

	static JInterfaceTypeMetadata<JCharSequenceObject> IInterfaceType<JCharSequenceObject>.Metadata
		=> JCharSequenceObject.typeMetadata;

	/// <inheritdoc/>
	private JCharSequenceObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCharSequenceObject IInterfaceType<JCharSequenceObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}