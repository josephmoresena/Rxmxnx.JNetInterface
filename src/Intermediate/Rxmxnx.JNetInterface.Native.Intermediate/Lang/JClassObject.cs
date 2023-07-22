namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Class&lt;?&gt;</c> instance.
/// </summary>
public sealed class JClassObject : JLocalObject, IClass, IDataType<JClassObject>
{
	/// <inheritdoc />
	public static Boolean Final => true;

	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JClassObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodeObjectSignatures.JClassObjectSignature;

	/// <inheritdoc/>
	public CString Name { get; } = default!;
	/// <inheritdoc/>
	public CString ClassSignature { get; }= default!;
	/// <summary>
	/// Indicates whether current class is final.
	/// </summary>
	public Boolean IsFinalClass { get; } = false;

	JClassLocalRef IClass.Reference => this.As<JClassLocalRef>();
	
	private JClassObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.ClassObject) { }
	internal JClassObject(IEnvironment env, JClassLocalRef jClassRef, Boolean isDummy, Boolean isNativeParameter) 
		: base(env, jClassRef.Value, isDummy, isNativeParameter, env.ClassProvider.GetClass<JClassObject>()) { }
	internal JClassObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc cref="IClass.IsValid(IEnvironment)"/>
	public Boolean IsValid() => this.IsValid(this.Environment);

	Boolean IClass.IsValid(IEnvironment env) => this.IsValid(env);
	
	private Boolean IsValid(IEnvironment env) => throw new NotImplementedException();

	/// <inheritdoc/>
	public static JClassObject? Create(JObject? jObject) => jObject is JLocalObject jLocal ? new(jLocal) : default;
}