namespace Rxmxnx.JNetInterface.Lang;

public partial class JNumberObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JNumberObject>
	                                                          .Create("java/lang/Number"u8, JTypeModifier.Abstract)
	                                                          .Implements<JSerializableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JNumberObject.typeMetadata;
}