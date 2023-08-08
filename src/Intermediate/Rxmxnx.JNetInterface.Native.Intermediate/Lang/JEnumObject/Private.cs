namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JEnumObject>
	                                                          .Create(UnicodeClassNames.JEnumObjectClassName,
	                                                                  JTypeModifier.Final)
	                                                          .WithSignature(
		                                                          UnicodeObjectSignatures.JEnumObjectSignature)
	                                                          .AppendInterface<JSerializableObject>()
	                                                          .AppendInterface<JComparableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JEnumObject.typeMetadata;

	/// <summary>
	/// Ordinal of enum value.
	/// </summary>
	private Int32? _ordinal;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JEnumObject(JLocalObject jLocal) : base(
		jLocal.ForExternalUse(out JClassObject jClass, out JObjectMetadata metadata), jClass)
	{
		if (metadata is JEnumObjectMetadata enumMetadata)
			this._ordinal ??= enumMetadata.Ordinal;
	}
}