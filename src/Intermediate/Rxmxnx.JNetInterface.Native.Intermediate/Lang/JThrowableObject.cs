namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Throwable</c> instance.
/// </summary>
public partial class JThrowableObject : JLocalObject, IThrowableType<JThrowableObject>,
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
	public StackTraceInfo[] StackTrace => this._stackTrace ??= this.GetStackTrace();

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
	/// <param name="throwException">Indicates whether exception should be thrown in managed code.</param>
	/// <exception cref="ThrowableException">Throws an exception if <paramref name="throwException"/> is true.</exception>
	public void Throw(Boolean throwException = true)
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
		if (throwException)
			throw env.PendingException;
	}

	/// <inheritdoc/>
	public override String ToString()
	{
		String threadInformation = this.ThreadId.HasValue ? $"[{this.ThreadId}] " : String.Empty;
		String result =
			$"{JObject.GetObjectIdentifier(this.Class.ClassSignature, this.Reference)} {threadInformation}{this.Message}";
		if (this.StackTrace.Length <= 0) return result;
		StringBuilder strBuild = new(result);
		foreach (StackTraceInfo t in this.StackTrace)
		{
			strBuild.AppendLine();
			strBuild.Append('\t');
			strBuild.Append(t.ToTraceText());
		}
		result = strBuild.ToString().Trim();
		return result;
	}
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override String ToTraceText() => JObject.GetObjectIdentifier(this.Class.ClassSignature, this.Reference);

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
	/// Throws an exception from <paramref name="jClass"/> class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="message">The message used to construct the <c>java.lang.Throwable</c> instance.</param>
	/// <param name="throwException">Indicates whether exception should be thrown in managed code.</param>
	public static void ThrowNew(JClassObject jClass, CString message, Boolean throwException = false)
		=> jClass.Environment.ClassFeature.ThrowNew(jClass, message, throwException);
	/// <summary>
	/// Throws an exception from <paramref name="jClass"/> class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="message">The message used to construct the <c>java.lang.Throwable</c> instance.</param>
	/// <param name="throwException">Indicates whether exception should be thrown in managed code.</param>
	public static void ThrowNew(JClassObject jClass, String message, Boolean throwException = false)
		=> jClass.Environment.ClassFeature.ThrowNew(jClass, message, throwException);

	/// <summary>
	/// Throws an exception from <typeparamref name="TThrowable"/> type.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="JThrowableObject"/> type.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="message">The message used to construct the <c>java.lang.Throwable</c> instance.</param>
	/// <param name="throwException">Indicates whether exception should be thrown in managed code.</param>
	public static void ThrowNew<TThrowable>(IEnvironment env, CString message, Boolean throwException = false)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> env.ClassFeature.ThrowNew<TThrowable>(message, throwException);
	/// <summary>
	/// Throws an exception from <typeparamref name="TThrowable"/> type.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="JThrowableObject"/> type.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="message">The message used to construct the <c>java.lang.Throwable</c> instance.</param>
	/// <param name="throwException">Indicates whether exception should be thrown in managed code.</param>
	public static void ThrowNew<TThrowable>(IEnvironment env, String message, Boolean throwException = false)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		=> env.ClassFeature.ThrowNew<TThrowable>(message, throwException);
}