namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	internal static JClassTypeMetadata JEnumClassMetadata = JLocalObject.JTypeMetadataBuilder<JEnumObject>
	                                                                    .Create(UnicodeClassNames.JEnumObjectClassName,
		                                                                    JTypeModifier.Final)
	                                                                    .WithSignature(
		                                                                    UnicodeObjectSignatures
			                                                                    .JEnumObjectSignature)
	                                                                    .AppendInterface<JSerializableObject>()
	                                                                    .AppendInterface<JComparableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JEnumObject.JEnumClassMetadata;
}