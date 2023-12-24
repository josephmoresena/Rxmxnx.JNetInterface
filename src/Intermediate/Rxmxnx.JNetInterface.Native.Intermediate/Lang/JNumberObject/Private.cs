namespace Rxmxnx.JNetInterface.Lang;

public partial class JNumberObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JNumberObject>
	                                                          .Create(UnicodeClassNames.JNumberObjectClassName,
	                                                                  JTypeModifier.Abstract)
	                                                          .WithSignature(
		                                                          UnicodeObjectSignatures.JNumberObjectSignature)
	                                                          .Implements<JSerializableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JNumberObject.typeMetadata;
}