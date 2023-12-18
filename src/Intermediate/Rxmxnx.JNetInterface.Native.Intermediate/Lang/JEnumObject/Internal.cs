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

	/// <summary>
	/// Returns the name of current instance.
	/// </summary>
	/// <returns>Returns the name of current instance.</returns>
	internal virtual String GetName()
	{
		JFunctionDefinition<JStringObject> definition = new(JEnumObject.nameName);
		JClassObject enumClass = this.Environment.ClassProvider.EnumClassObject;
		using JStringObject jString = JFunctionDefinition<JStringObject>.Invoke(definition, this, enumClass)!;
		return jString.Value;
	}
}