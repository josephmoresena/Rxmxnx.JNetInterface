namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JCharSequenceObject>;

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
	private static readonly TypeMetadata typeMetadata =
		TypeMetadataBuilder<JCharSequenceObject>.Create("java/lang/CharSequence"u8).Build();

	static TypeMetadata IInterfaceType<JCharSequenceObject>.Metadata => JCharSequenceObject.typeMetadata;

	/// <inheritdoc/>
	private JCharSequenceObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCharSequenceObject IInterfaceType<JCharSequenceObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}