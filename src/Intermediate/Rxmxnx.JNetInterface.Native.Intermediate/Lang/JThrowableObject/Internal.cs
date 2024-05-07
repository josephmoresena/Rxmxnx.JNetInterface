namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JThrowableObject> typeMetadata =
		TypeMetadataBuilder<JThrowableObject>.Create(UnicodeClassNames.ThrowableObject())
		                                     .Implements<JSerializableObject>().Build();

	static JThrowableTypeMetadata<JThrowableObject> IThrowableType<JThrowableObject>.Metadata
		=> JThrowableObject.typeMetadata;
	static Type IDataType.FamilyType => typeof(JThrowableObject);
}