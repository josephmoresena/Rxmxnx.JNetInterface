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
		JEnumObject.typeInfo, JTypeModifier.Abstract, InterfaceSet.SerializableComparableSet);

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

	/// <summary>
	/// Creates a new <see cref="IEnumFieldList"/> from <paramref name="values"/>.
	/// </summary>
	/// <param name="values">Span of enum values.</param>
	/// <returns>A <see cref="IEnumFieldList"/> instance.</returns>
	private protected static IEnumFieldList CreateFieldList(ReadOnlySpan<CString> values) => new EnumFieldList(values);
	/// <summary>
	/// Creates the <see cref="JClassTypeMetadata{TClass}"/> metadata instance for built-in types.
	/// </summary>
	/// <typeparam name="TEnum"><see cref="IEnumType{TEnum}"/> type.</typeparam>
	/// <param name="information">Class type information.</param>
	/// <param name="fields">Enum fields.</param>
	/// <param name="interfaces">Class interfaces metadata set.</param>
	/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
	private protected static JEnumTypeMetadata<TEnum>
		CreateBuiltInMetadata<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TEnum>(
			TypeInfoSequence information, IEnumFieldList fields,
			ImmutableHashSet<JInterfaceTypeMetadata>? interfaces = default)
		where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
	{
		JClassTypeMetadata baseMetadata = IClassType.GetMetadata<JEnumObject>();
		interfaces ??= ImmutableHashSet<JInterfaceTypeMetadata>.Empty;
		return new EnumTypeMetadata<TEnum>(information, fields,
		                                   InterfaceSet.GetClassInterfaces(baseMetadata, interfaces));
	}

	static JEnumObject IClassType<JEnumObject>.Create(IReferenceType.ClassInitializer initializer) => new(initializer);
	static JEnumObject IClassType<JEnumObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.Instance, initializer.Class ?? initializer.Instance.Class);
	static JEnumObject IClassType<JEnumObject>.Create(IReferenceType.GlobalInitializer initializer) => new(initializer);
}