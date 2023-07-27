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
	/// Instance value.
	/// </summary>
	private readonly String _value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jStrRef">Local string reference.</param>
	/// <param name="value">Internal value.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	internal JStringObject(IEnvironment env, JStringLocalRef jStrRef, String value, Boolean isDummy,
		Boolean isNativeParameter) : base(env, jStrRef.Value, isDummy, isNativeParameter,
		                                  env.ClassProvider.StringClassObject)
		=> this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JStringObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) => this._value ??= default!;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="value">Internal value.</param>
	private JStringObject(JLocalObject jLocal, String value) : base(
		jLocal, jLocal.Environment.ClassProvider.StringClassObject)
		=> this._value = value;

	/// <inheritdoc/>
	public override String ToString() => this._value;

	/// <inheritdoc cref="IDataType{TDataType}.Create(JObject)"/>
	private static JStringObject? Create(JLocalObject jLocal)
	{
		String? value = jLocal.Environment.StringProvider.GetStringValue(jLocal);
		return value is not null ? new JStringObject(jLocal!, value) : default;
	}

	/// <inheritdoc/>
	public static JStringObject? Create(JObject? jObject)
		=> jObject is JLocalObject jLocal && jLocal.Environment.ClassProvider.IsAssignableTo<JClassObject>(jLocal) ?
			JStringObject.Create(jLocal) :
			default;
}