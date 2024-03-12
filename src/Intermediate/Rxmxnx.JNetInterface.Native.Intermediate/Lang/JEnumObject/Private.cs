namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JEnumObject> enumClassMetadata = JLocalObject
		.TypeMetadataBuilder<JEnumObject>.Create(UnicodeClassNames.EnumObject(), JTypeModifier.Abstract)
		.Implements<JSerializableObject>().Implements<JComparableObject>().Build();

	static JClassTypeMetadata<JEnumObject> IClassType<JEnumObject>.Metadata => JEnumObject.enumClassMetadata;

	/// <summary>
	/// String of enum value.
	/// </summary>
	private String? _name;
	/// <summary>
	/// Ordinal of enum value.
	/// </summary>
	private Int32? _ordinal;

	/// <inheritdoc/>
	private JEnumObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass) { }

	static JEnumObject IClassType<JEnumObject>.Create(IReferenceType.ClassInitializer initializer) => new(initializer);
	static JEnumObject IClassType<JEnumObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.Instance, initializer.Class ?? initializer.Instance.Class);
	static JEnumObject IClassType<JEnumObject>.Create(IReferenceType.GlobalInitializer initializer) => new(initializer);
}