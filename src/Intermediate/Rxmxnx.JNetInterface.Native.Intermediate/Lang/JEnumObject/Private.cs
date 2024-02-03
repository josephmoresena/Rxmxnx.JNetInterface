namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata enumClassMetadata = JLocalObject.JTypeMetadataBuilder<JEnumObject>
	                                                                           .Create(UnicodeClassNames.EnumObject(),
		                                                                           JTypeModifier.Final)
	                                                                           .Implements<JSerializableObject>()
	                                                                           .Implements<JComparableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JEnumObject.enumClassMetadata;
	static Type IDataType.FamilyType => typeof(JLocalObject);
	static JClassTypeMetadata IBaseClassType<JEnumObject>.SuperClassMetadata => JEnumObject.enumClassMetadata;

	/// <summary>
	/// String of enum value.
	/// </summary>
	private String? _name;
	/// <summary>
	/// Ordinal of enum value.
	/// </summary>
	private Int32? _ordinal;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JEnumObject(JLocalObject jLocal) : this(
		jLocal.ForExternalUse(out JClassObject jClass, out ObjectMetadata metadata), jClass)
	{
		if (metadata is not EnumObjectMetadata enumMetadata) return;
		this._ordinal ??= enumMetadata.Ordinal;
		this._name ??= enumMetadata.Name;
	}
	/// <inheritdoc/>
	private JEnumObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass) { }

	static JEnumObject IReferenceType<JEnumObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer.ToInternal());
	static JEnumObject IReferenceType<JEnumObject>.Create(IReferenceType.ObjectInitializer initializer)
	{
		JClassObject? jClass = initializer.Class ?? initializer.Instance.Lifetime.Class;
		if (jClass is null || !jClass.IsFinal.GetValueOrDefault())
			return new(initializer.Instance);
		return new(initializer.Instance, jClass);
	}
	static JEnumObject IReferenceType<JEnumObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer.ToInternal());
}