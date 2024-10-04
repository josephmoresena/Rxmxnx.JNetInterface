namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JEnumObject>;

public partial class JEnumObject
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.EnumHash, 14);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JEnumObject>(
		JEnumObject.typeInfo, JTypeModifier.Final, InterfaceSet.SerializableComparableSet);

	static TypeMetadata IClassType<JEnumObject>.Metadata => JEnumObject.typeMetadata;

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