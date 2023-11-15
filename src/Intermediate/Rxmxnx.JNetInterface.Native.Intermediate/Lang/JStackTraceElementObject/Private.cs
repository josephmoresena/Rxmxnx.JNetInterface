namespace Rxmxnx.JNetInterface.Lang;

public partial class JStackTraceElementObject
{
	/// <summary>
	/// Function name of <c>java.lang.StackTraceElement.getClassName().</c>
	/// </summary>
	private static readonly CString getClassNameName = new(() => "getClassName"u8);
	/// <summary>
	/// Function name of <c>java.lang.StackTraceElement.getFileName().</c>
	/// </summary>
	private static readonly CString getFileNameName = new(() => "getFileName"u8);
	/// <summary>
	/// Function name of <c>java.lang.StackTraceElement.getLineNumber().</c>
	/// </summary>
	private static readonly CString getLineNumberName = new(() => "getLineNumber"u8);
	/// <summary>
	/// Function name of <c>java.lang.StackTraceElement.getMethodName().</c>
	/// </summary>
	private static readonly CString getMethodNameName = new(() => "getMethodName"u8);
	/// <summary>
	/// Function name of <c>java.lang.StackTraceElement.isNativeMethod().</c>
	/// </summary>
	private static readonly CString isNativeMethodName = new(() => "isNativeMethod"u8);

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
	private JStackTraceElementObject(JLocalObject jLocal) : base(
		jLocal, jLocal.Environment.ClassProvider.GetClass<JStackTraceElementObject>())
	{
		JStackTraceElementObject? traceElement = jLocal as JStackTraceElementObject;
		this._className ??= traceElement?._className;
		this._fileName ??= traceElement?._fileName;
		this._lineNumber ??= traceElement?._lineNumber;
		this._methodName ??= traceElement?._methodName;
		this._nativeMethod ??= traceElement?._nativeMethod;
	}

	/// <summary>
	/// Returns the fully qualified name of the class containing the execution point represented
	/// by this stack trace element.
	/// </summary>
	/// <returns>The fully qualified name of the class containing the execution point.</returns>
	private String GetClassName()
	{
		JFunctionDefinition<JStringObject> definition = new(JStackTraceElementObject.getClassNameName);
		using JStringObject jString = JFunctionDefinition<JStringObject>.Invoke(definition, this)!;
		return jString.Value;
	}
	/// <summary>
	/// Returns the name of the source file containing the execution point represented by this stack trace element.
	/// </summary>
	/// <returns>The name of the source file containing the execution point.</returns>
	private String GetFileName()
	{
		JFunctionDefinition<JStringObject> definition = new(JStackTraceElementObject.getFileNameName);
		using JStringObject jString = JFunctionDefinition<JStringObject>.Invoke(definition, this)!;
		return jString.Value;
	}
	/// <summary>
	/// Returns the line number of the source line containing the execution point represented by this stack trace element.
	/// </summary>
	/// <returns>The line number of the source line containing the execution point</returns>
	private Int32 GetLineNumber()
	{
		JPrimitiveFunctionDefinition definition =
			JPrimitiveFunctionDefinition.CreateIntDefinition(JStackTraceElementObject.getLineNumberName);
		return JPrimitiveFunctionDefinition.Invoke<Int32>(definition, this);
	}
	/// <summary>
	/// Returns the name of the method containing the execution point represented by this stack trace element.
	/// </summary>
	/// <returns>The name of the method containing the execution point</returns>
	private String GetMethodName()
	{
		JFunctionDefinition<JStringObject> definition = new(JStackTraceElementObject.getMethodNameName);
		using JStringObject jString = JFunctionDefinition<JStringObject>.Invoke(definition, this)!;
		return jString.Value;
	}
	/// <summary>
	/// Returns <see langword="true"/> if the method containing the execution point represented by this stack trace element is
	/// a native method.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if the method containing the execution point is a native method; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	private Boolean IsNativeMethod()
	{
		JPrimitiveFunctionDefinition definition =
			JPrimitiveFunctionDefinition.CreateBooleanDefinition(JStackTraceElementObject.isNativeMethodName);
		return JPrimitiveFunctionDefinition.Invoke<Byte>(definition, this) == 0x1;
	}
}