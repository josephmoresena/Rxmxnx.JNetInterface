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
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	internal JStringObject(IEnvironment env, JStringLocalRef jStrRef, Boolean isDummy, Boolean isNativeParameter) :
		base(env, jStrRef.Value, isDummy, isNativeParameter, env.ClassProvider.StringClassObject) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JStringObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JStringObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.StringClassObject) { }

	/// <inheritdoc/>
	public override String ToString() => this._value;
	
	/// <inheritdoc/>
	public static JStringObject? Create(JObject? jObject) 
		=> jObject is JLocalObject jLocal && jLocal.Environment.ClassProvider.IsAssignableTo<JClassObject>(jLocal) ? 
			new(jLocal) : default;
}