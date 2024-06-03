namespace Rxmxnx.JNetInterface.Lang;

public partial class JStackTraceElementObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JStackTraceElementObject> typeMetadata =
		TypeMetadataBuilder<JStackTraceElementObject>
			.Create(UnicodeClassNames.StackTraceElementObject(), JTypeModifier.Final).Implements<JSerializableObject>()
			.Build();

	static JClassTypeMetadata<JStackTraceElementObject> IClassType<JStackTraceElementObject>.Metadata
		=> JStackTraceElementObject.typeMetadata;

	/// <inheritdoc cref="JStackTraceElementObject.ClassName"/>
	private String? _className;
	/// <inheritdoc cref="JStackTraceElementObject.FileName"/>
	private String? _fileName;
	/// <inheritdoc cref="JStackTraceElementObject.LineNumber"/>
	private Int32? _lineNumber;
	/// <inheritdoc cref="JStackTraceElementObject.MethodName"/>
	private String? _methodName;
	/// <inheritdoc cref="JStackTraceElementObject.NativeMethod"/>
	private Boolean? _nativeMethod;

	/// <inheritdoc/>
	private JStackTraceElementObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JStackTraceElementObject(JLocalObject jLocal) : base(
		jLocal, jLocal.Environment.ClassFeature.StackTraceElementObject)
	{
		JStackTraceElementObject? traceElement = jLocal as JStackTraceElementObject;
		this._className ??= traceElement?._className;
		this._fileName ??= traceElement?._fileName;
		this._lineNumber ??= traceElement?._lineNumber;
		this._methodName ??= traceElement?._methodName;
		this._nativeMethod ??= traceElement?._nativeMethod;
	}
	/// <inheritdoc/>
	private JStackTraceElementObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <summary>
	/// Returns the fully qualified name of the class containing the execution point represented
	/// by this stack trace element.
	/// </summary>
	/// <returns>The fully qualified name of the class containing the execution point.</returns>
	private String GetClassName()
	{
		using JStringObject className = this.Environment.FunctionSet.GetClassName(this);
		return className.Value;
	}
	/// <summary>
	/// Returns the name of the source file containing the execution point represented by this stack trace element.
	/// </summary>
	/// <returns>The name of the source file containing the execution point.</returns>
	private String? GetFileName()
	{
		using JStringObject? fileName = this.Environment.FunctionSet.GetFileName(this);
		return fileName?.Value;
	}
	/// <summary>
	/// Returns the name of the method containing the execution point represented by this stack trace element.
	/// </summary>
	/// <returns>The name of the method containing the execution point</returns>
	private String GetMethodName()
	{
		using JStringObject jString = this.Environment.FunctionSet.GetMethodName(this);
		return jString.Value;
	}

	static JStackTraceElementObject IClassType<JStackTraceElementObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JStackTraceElementObject IClassType<JStackTraceElementObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer.Instance);
	static JStackTraceElementObject IClassType<JStackTraceElementObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer.Environment, initializer.Global);
}