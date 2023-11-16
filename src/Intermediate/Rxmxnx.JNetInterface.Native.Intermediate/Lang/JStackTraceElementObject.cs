namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.StackTraceElement</c> instance.
/// </summary>
public sealed partial class JStackTraceElementObject : JLocalObject, IClassType<JStackTraceElementObject>,
	IInterfaceImplementation<JStackTraceElementObject, JSerializableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JStackTraceElementObject>
	                                                          .Create(UnicodeClassNames.JStackTraceElementClassName,
	                                                                  JTypeModifier.Final)
	                                                          .WithSignature(
		                                                          UnicodeObjectSignatures
			                                                          .JStackTraceElementObjectSignature)
	                                                          .Implements<JSerializableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JStackTraceElementObject.typeMetadata;

	/// <summary>
	/// The fully qualified name of the class containing the execution point.
	/// </summary>
	public String ClassName => this._className ??= this.GetClassName();
	/// <summary>
	/// The name of the source file containing the execution point
	/// </summary>
	public String FileName => this._fileName ??= this.GetFileName();
	/// <summary>
	/// The line number of the source line containing the execution point
	/// </summary>
	public Int32 LineNumber => this._lineNumber ??= this.GetLineNumber();
	/// <summary>
	/// The name of the method containing the execution point.
	/// </summary>
	public String MethodName => this._methodName ??= this.GetMethodName();
	/// <summary>
	/// Indicates whether the method containing the execution point is a native method.
	/// </summary>
	public Boolean NativeMethod => this._nativeMethod ??= this.IsNativeMethod();

	/// <inheritdoc/>
	public JStackTraceElementObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JStackTraceElementObjectMetadata(base.CreateMetadata())
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
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not JStackTraceElementObjectMetadata traceElementMetadata)
			return;
		this._className = traceElementMetadata.Information?.ClassName;
		this._fileName = traceElementMetadata.Information?.FileName;
		this._lineNumber = traceElementMetadata.Information?.LineNumber;
		this._methodName = traceElementMetadata.Information?.MethodName;
		this._nativeMethod = traceElementMetadata.Information?.NativeMethod;
	}

	/// <inheritdoc/>
	public static JStackTraceElementObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JStackTraceElementObject>(jLocal)) : default;
}