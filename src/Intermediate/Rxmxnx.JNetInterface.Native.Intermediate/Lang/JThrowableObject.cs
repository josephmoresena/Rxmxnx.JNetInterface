namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Throwable</c> instance.
/// </summary>
public partial class JThrowableObject : JLocalObject, IBaseClassType<JThrowableObject>,
	IThrowableType<JThrowableObject>, ILocalObject, IInterfaceObject<JSerializableObject>
{
	/// <summary>
	/// Throwable message.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String Message => this._message ??= this.GetMessage();
	/// <summary>
	/// Throwable stack trace.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JStackTraceInfo[] StackTrace
		=> this._stackTrace ??= this.Environment.WithFrame(5, this, JThrowableObject.GetStackTraceInfo);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	private JThrowableObject(JClassObject jClass, JThrowableLocalRef throwableRef) :
		base(jClass, throwableRef.Value) { }

	/// <inheritdoc/>
	protected JThrowableObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JThrowableObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JThrowableObject(IReferenceType.ObjectInitializer initializer) : base(
		initializer.Instance.ForExternalUse(out JClassObject jClass, out ObjectMetadata metadata), jClass)
	{
		if (metadata is not ThrowableObjectMetadata throwableMetadata)
			return;
		this._message ??= throwableMetadata.Message;
		this._stackTrace ??= throwableMetadata.StackTrace;
	}

	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

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
}