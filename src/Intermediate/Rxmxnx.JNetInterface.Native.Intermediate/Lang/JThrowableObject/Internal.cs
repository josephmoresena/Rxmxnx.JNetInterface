namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	internal static readonly JThrowableTypeMetadata JThrowableClassMetadata = JTypeMetadataBuilder<JThrowableObject>
	                                                                          .Create("java/lang/Throwable"u8)
	                                                                          .Implements<JSerializableObject>()
	                                                                          .Build();

	static JClassTypeMetadata IBaseClassType<JThrowableObject>.SuperClassMetadata
		=> JThrowableObject.JThrowableClassMetadata;
	static JDataTypeMetadata IDataType.Metadata => JThrowableObject.JThrowableClassMetadata;
}