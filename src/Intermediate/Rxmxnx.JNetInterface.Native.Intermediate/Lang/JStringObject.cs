namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.String</c> instance.
/// </summary>
public sealed partial class JStringObject : JLocalObject, IDataType<JStringObject>
{
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JStringObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodeObjectSignatures.JStringObjectSignature;
	/// <inheritdoc/>
	public static JTypeModifier Modifier => JTypeModifier.Final;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JStringObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
		=> this._value ??= env.StringProvider.ToString(jGlobal);

	/// <inheritdoc/>
	public override String ToString()
	{
		this._value ??= this.Environment.StringProvider.ToString(this);
		return this._value;
	}
	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JStringMetadata(base.CreateMetadata()) { Value = this.ToString(), };
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata metadata)
	{
		base.ProcessMetadata(metadata);
		if (metadata is not JStringMetadata stringMetadata)
			return;
		this._value = stringMetadata.Value;
	}

	/// <inheritdoc/>
	public static JStringObject? Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JClassObject>(jLocal)) : default;
}