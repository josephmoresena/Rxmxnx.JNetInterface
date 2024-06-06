namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JThrowableObject>;

public partial class JThrowableObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JThrowableObject>
	                                                    .Create("java/lang/Throwable"u8)
	                                                    .Implements<JSerializableObject>().Build();

	static TypeMetadata IThrowableType<JThrowableObject>.Metadata => JThrowableObject.typeMetadata;
	static Type IDataType.FamilyType => typeof(JThrowableObject);
}