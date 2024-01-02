namespace Rxmxnx.JNetInterface.Lang;

public partial class JNumberObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JNumberObject>
	                                                          .Create(UnicodeClassNames.NumberObject,
	                                                                  JTypeModifier.Abstract)
	                                                          .Implements<JSerializableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JNumberObject.typeMetadata;
}