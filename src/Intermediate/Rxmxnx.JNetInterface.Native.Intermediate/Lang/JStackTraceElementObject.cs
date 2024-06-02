namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.StackTraceElement</c> instance.
/// </summary>
public sealed partial class JStackTraceElementObject : JLocalObject, IClassType<JStackTraceElementObject>,
	IInterfaceObject<JSerializableObject>
{
	/// <summary>
	/// The fully qualified name of the class containing the execution point.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String ClassName => this._className ??= this.GetClassName();
	/// <summary>
	/// The name of the source file containing the execution point
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String? FileName => this._fileName ??= this.GetFileName();
	/// <summary>
	/// The line number of the source line containing the execution point
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int32 LineNumber => this._lineNumber ??= this.Environment.FunctionSet.GetLineNumber(this);
	/// <summary>
	/// The name of the method containing the execution point.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String MethodName => this._methodName ??= this.GetMethodName();
	/// <summary>
	/// Indicates whether the method containing the execution point is a native method.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Boolean NativeMethod => this._nativeMethod ??= this.Environment.FunctionSet.IsNativeMethod(this);

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new StackTraceElementObjectMetadata(base.CreateMetadata())
		{
			Information = new()
			{
				ClassName = this.ClassName,
				FileName = this.FileName,
				LineNumber = this.LineNumber,
				MethodName = this.MethodName,
				NativeMethod = this.NativeMethod,
			},
		};
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not StackTraceElementObjectMetadata traceElementMetadata)
			return;
		this._className = traceElementMetadata.Information?.ClassName;
		this._fileName = traceElementMetadata.Information?.FileName;
		this._lineNumber = traceElementMetadata.Information?.LineNumber;
		this._methodName = traceElementMetadata.Information?.MethodName;
		this._nativeMethod = traceElementMetadata.Information?.NativeMethod;
	}
}