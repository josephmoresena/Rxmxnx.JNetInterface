namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Enum</c> instance.
/// </summary>
public partial class JEnumObject : JLocalObject, IClassType<JEnumObject>, ILocalObject,
	IInterfaceObject<JSerializableObject>, IInterfaceObject<JComparableObject>
{
	/// <summary>
	/// Ordinal of this enumeration constant.
	/// </summary>
	/// <remarks>Its position in its enum declaration, where the initial constant is assigned an ordinal of zero</remarks>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int32 Ordinal => this._ordinal ??= this.Environment.FunctionSet.GetOrdinal(this);
	/// <summary>
	/// Name of this enum constant, exactly as declared in its enum declaration.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String Name => this._name ??= this.GetName();

	/// <inheritdoc/>
	private protected JEnumObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JEnumObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JEnumObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <summary>
	/// Returns the name of current instance.
	/// </summary>
	/// <returns>Returns the name of current instance.</returns>
	private protected virtual String GetName()
	{
		using JStringObject enumName = this.Environment.FunctionSet.GetName(this);
		return enumName.Value;
	}

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual EnumObjectMetadata CreateMetadata()
		=> new(base.CreateMetadata()) { Ordinal = this.Ordinal, Name = this.Name, };

	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not EnumObjectMetadata enumMetadata)
			return;
		this._name ??= enumMetadata.Name;
		this._ordinal ??= enumMetadata.Ordinal;
	}
}

/// <summary>
/// This class represents a local <c>java.lang.Enum</c> instance.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
public abstract class JEnumObject<TEnum> : JEnumObject, IDataType where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
{
	static JTypeKind IDataType.Kind => JTypeKind.Enum;
	static Type IDataType.FamilyType => typeof(JEnumObject);

	/// <inheritdoc/>
	protected JEnumObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JEnumObject(IReferenceType.ObjectInitializer initializer) : base(initializer.WithClass<TEnum>()) { }
	/// <inheritdoc/>
	protected JEnumObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }

	/// <inheritdoc/>
	private protected override String GetName()
	{
		JEnumTypeMetadata metadata = IEnumType.GetMetadata<TEnum>();
		return metadata.Fields.HasOrdinal(this.Ordinal) ? metadata.Fields[this.Ordinal].ToString() : base.GetName();
	}
}