namespace Rxmxnx.JNetInterface.Lang;

public partial class JStackTraceElementObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JStackTraceElementObject>
	                                                          .Create(UnicodeClassNames.StackTraceElementObject(),
	                                                                  JTypeModifier.Final)
	                                                          .Implements<JSerializableObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JStackTraceElementObject.typeMetadata;

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
	
	/// <inheritdoc />
	private JStackTraceElementObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) {}
	/// <inheritdoc/>
	private JStackTraceElementObject(JLocalObject jLocal) : base(
		jLocal, jLocal.Environment.ClassFeature.GetClass<JStackTraceElementObject>())
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
		using JStringObject className = this.Environment.Functions.GetClassName(this);
		return className.Value;
	}
	/// <summary>
	/// Returns the name of the source file containing the execution point represented by this stack trace element.
	/// </summary>
	/// <returns>The name of the source file containing the execution point.</returns>
	private String GetFileName()
	{
		using JStringObject fileName = this.Environment.Functions.GetFileName(this);
		return fileName.Value;
	}
	/// <summary>
	/// Returns the name of the method containing the execution point represented by this stack trace element.
	/// </summary>
	/// <returns>The name of the method containing the execution point</returns>
	private String GetMethodName()
	{
		using JStringObject jString = this.Environment.Functions.GetMethodName(this);
		return jString.Value;
	}
}