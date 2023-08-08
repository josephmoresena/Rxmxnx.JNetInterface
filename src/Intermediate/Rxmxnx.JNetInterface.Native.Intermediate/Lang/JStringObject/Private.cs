namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JStringObject>
	                                                          .Create(UnicodeClassNames.JStringObjectClassName,
	                                                                  JTypeModifier.Final)
	                                                          .WithSignature(
		                                                          UnicodeObjectSignatures.JStringObjectSignature)
	                                                          .AppendInterface<JSerializableObject>()
	                                                          .AppendInterface<JComparableObject>()
	                                                          .AppendInterface<JCharSequenceObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JStringObject.typeMetadata;

	/// <summary>
	/// Instance value.
	/// </summary>
	private String? _value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JStringObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.StringClassObject)
	{
		if (jLocal is not JStringObject jString)
			return;
		this._value = jString.Value;
	}
}