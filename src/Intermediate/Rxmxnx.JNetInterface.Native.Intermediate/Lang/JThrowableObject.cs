namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Throwable</c> instance.
/// </summary>
public partial class JThrowableObject : JLocalObject, IThrowableType<JThrowableObject>, ILocalObject,
	IInterfaceObject<JSerializableObject>
{
	/// <summary>
	/// JNI throwable reference.
	/// </summary>
	public new JThrowableLocalRef Reference => this.To<JThrowableLocalRef>();
	/// <summary>
	/// Throwable message.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String Message => this._message ??= this.GetMessage();
	/// <summary>
	/// Throwable stack trace.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public StackTraceInfo[] StackTrace
		=> this._stackTrace ??= this.Environment.WithFrame(5, this, JThrowableObject.GetStackTraceInfo);

	/// <inheritdoc/>
	protected JThrowableObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JThrowableObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JThrowableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <summary>
	/// Throws an exception from current instance.
	/// </summary>
	/// <exception cref="ThrowableException">Always throws an exception.</exception>
	public void Throw()
	{
		IEnvironment env = this.Environment;
		JReferenceTypeMetadata metadata = env.ClassFeature.GetTypeMetadata(this.Class);
		JGlobal jGlobal = env.ReferenceFeature.Create<JGlobal>(this);
		try
		{
			env.PendingException = metadata.CreateException(jGlobal, this.Message)!;
		}
		catch (Exception)
		{
			jGlobal.Dispose();
			throw;
		}
		throw env.PendingException;
	}

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual ThrowableObjectMetadata CreateMetadata()
		=> new(base.CreateMetadata()) { Message = this.Message, StackTrace = this._stackTrace, };

	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not ThrowableObjectMetadata throwableMetadata)
			return;
		this._message ??= throwableMetadata.Message;
		this._stackTrace ??= throwableMetadata.StackTrace;
	}

	/// <summary>
	/// Throws an exception from <typeparamref name="TThrowable"/> type.
	/// </summary>
	/// <typeparam name="TThrowable"></typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="message">
	/// The message used to construct the <c>java.lang.Throwable</c> instance.
	/// </param>
	/// <param name="throwException">
	/// Indicates whether exception should be thrown in managed code.
	/// </param>
	public static void ThrowNew<TThrowable>(IEnvironment env, CString message, Boolean throwException = false)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> env.ClassFeature.ThrowNew<TThrowable>(message, throwException);
	/// <summary>
	/// Throws an exception from <typeparamref name="TThrowable"/> type.
	/// </summary>
	/// <typeparam name="TThrowable"></typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="message">
	/// The message used to construct the <c>java.lang.Throwable</c> instance.
	/// </param>
	/// <param name="throwException">
	/// Indicates whether exception should be thrown in managed code.
	/// </param>
	public static void ThrowNew<TThrowable>(IEnvironment env, String message, Boolean throwException = false)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> env.ClassFeature.ThrowNew<TThrowable>(message, throwException);
}