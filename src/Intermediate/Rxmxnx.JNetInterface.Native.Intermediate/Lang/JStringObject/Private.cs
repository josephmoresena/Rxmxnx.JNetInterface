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
	                                                          .Implements<JSerializableObject>()
	                                                          .Implements<JComparableObject>()
	                                                          .Implements<JCharSequenceObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JStringObject.typeMetadata;
	/// <summary>
	/// String length.
	/// </summary>
	private Int32? _length;
	/// <summary>
	/// UTF-8 string length.
	/// </summary>
	private Int32? _utf8Length;

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