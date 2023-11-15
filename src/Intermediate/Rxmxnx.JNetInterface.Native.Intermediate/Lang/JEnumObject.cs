namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Enum</c> instance.
/// </summary>
public partial class JEnumObject : JLocalObject, IBaseClassType<JEnumObject>, ILocalObject,
	IInterfaceImplementation<JEnumObject, JSerializableObject>, IInterfaceImplementation<JEnumObject, JComparableObject>
{
	/// <summary>
	/// Ordinal of this enumeration constant.
	/// </summary>
	/// <remarks>Its position in its enum declaration, where the initial constant is assigned an ordinal of zero</remarks>
	public Int32 Ordinal => this._ordinal ??= this.Environment.EnumProvider.GetOrdinal(this);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JEnumObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	JObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual JEnumObjectMetadata CreateMetadata()
		=> new(base.CreateMetadata()) { Ordinal = this.Ordinal, };

	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not JEnumObjectMetadata enumMetadata)
			return;
		this._ordinal ??= enumMetadata.Ordinal;
	}

	static JEnumObject? IDataType<JEnumObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JEnumObject>(jLocal)) : default;
}

/// <summary>
/// This class represents a local <c>java.lang.Enum</c> instance.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
public abstract class JEnumObject<TEnum> : JEnumObject where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	protected JEnumObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.GetClass<TEnum>()) { }
	/// <inheritdoc/>
	protected JEnumObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}