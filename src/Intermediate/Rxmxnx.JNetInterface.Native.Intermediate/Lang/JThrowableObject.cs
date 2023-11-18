namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Throwable</c> instance.
/// </summary>
public partial class JThrowableObject : JLocalObject, IBaseClassType<JThrowableObject>,
	IThrowableType<JThrowableObject>, ILocalObject, IInterfaceImplementation<JThrowableObject, JSerializableObject>
{
	/// <summary>
	/// Throwable message.
	/// </summary>
	public String Message => this._message ??= this.GetMessage();
	/// <summary>
	/// Throwable stack trace.
	/// </summary>
	public JStackTraceInfo[] StackTrace => this._stackTrace ??= this.GetStackTraceInfo();

	/// <inheritdoc/>
	internal JThrowableObject(IEnvironment env, JObjectLocalRef jLocalRef, Boolean isDummy, Boolean isNativeParameter,
		JClassObject? jClass = default) : base(env, jLocalRef, isDummy, isNativeParameter,
		                                       jClass ?? env.ClassProvider.ThrowableClassObject) { }

	/// <inheritdoc/>
	protected JThrowableObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JThrowableObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass) { }

	JObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual JThrowableObjectMetadata CreateMetadata()
		=> new(base.CreateMetadata()) { Message = this.Message, StackTrace = this.StackTrace, };

	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not JThrowableObjectMetadata throwableMetadata)
			return;
		this._message ??= throwableMetadata.Message;
		this._stackTrace ??= throwableMetadata.StackTrace;
	}

	/// <inheritdoc/>
	public static JThrowableObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JThrowableObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JThrowableObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JThrowableObject>(jGlobal, env)) :
			default;
}