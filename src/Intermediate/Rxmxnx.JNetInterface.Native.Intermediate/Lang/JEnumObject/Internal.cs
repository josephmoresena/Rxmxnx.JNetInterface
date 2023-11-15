namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	internal static readonly JClassTypeMetadata JEnumClassMetadata = JLocalObject.JTypeMetadataBuilder<JEnumObject>
		.Create(UnicodeClassNames.JEnumObjectClassName, JTypeModifier.Final)
		.WithSignature(UnicodeObjectSignatures.JEnumObjectSignature).Implements<JSerializableObject>()
		.Implements<JComparableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JEnumObject.JEnumClassMetadata;
	static JClassTypeMetadata IBaseClassType<JEnumObject>.SuperClassMetadata => JEnumObject.JEnumClassMetadata;
}