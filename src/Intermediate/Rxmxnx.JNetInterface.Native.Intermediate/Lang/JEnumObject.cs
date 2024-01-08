namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Enum</c> instance.
/// </summary>
public partial class JEnumObject : JLocalObject, IBaseClassType<JEnumObject>, ILocalObject,
	IInterfaceObject<JSerializableObject>, IInterfaceObject<JComparableObject>
{
	/// <summary>
	/// Ordinal of this enumeration constant.
	/// </summary>
	/// <remarks>Its position in its enum declaration, where the initial constant is assigned an ordinal of zero</remarks>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int32 Ordinal => this._ordinal ??= this.Environment.Functions.GetOrdinal(this);
	/// <summary>
	/// Name of this enum constant, exactly as declared in its enum declaration.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String Name => this._name ??= this.GetName();

	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual EnumObjectMetadata CreateMetadata()
		=> new(base.CreateMetadata()) { Ordinal = this.Ordinal, Name = this.Name, };

	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not EnumObjectMetadata enumMetadata)
			return;
		this._ordinal ??= enumMetadata.Ordinal;
	}

	/// <inheritdoc/>
	public static JEnumObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JEnumObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JEnumObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JEnumObject>(jGlobal, env)) : default;
}

/// <summary>
/// This class represents a local <c>java.lang.Enum</c> instance.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
public abstract class JEnumObject<TEnum> : JEnumObject, IDataType where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
{
	static Type IDataType.FamilyType => typeof(JEnumObject);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	protected JEnumObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassFeature.GetClass<TEnum>()) { }
	/// <inheritdoc/>
	protected JEnumObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <inheritdoc/>
	internal override String GetName()
	{
		JEnumTypeMetadata metadata = IEnumType.GetMetadata<TEnum>();
		return metadata.Fields.HasOrdinal(this.Ordinal) ? metadata.Fields[this.Ordinal].ToString() : base.GetName();
	}
}