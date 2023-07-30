namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Reference metadata.
	/// </summary>
	private static readonly JReferenceMetadata metadata = JMetadataBuilder
	                                                      .Create<JLocalObject>(
		                                                      UnicodeClassNames.JStringObjectClassName,
		                                                      JTypeModifier.Final)
	                                                      .WithSignature(UnicodeObjectSignatures.JStringObjectSignature)
	                                                      .Build();

	static JDataTypeMetadata IDataType.Metadata => JStringObject.metadata;

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
		if (jLocal is not IWrapper<String> wString)
			return;
		this._value = wString.Value;
	}
}