namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// Function name of <c>java.lang.Throwable.getMessage().</c>
	/// </summary>
	private static readonly CString getMessageName = new(() => "getMessage"u8);
	/// <summary>
	/// Function name of <c>java.lang.Throwable.getMessage().</c>
	/// </summary>
	private static readonly CString getStackTraceName = new(() => "getStackTrace"u8);

	/// <inheritdoc cref="JThrowableObjectMetadata.Message"/>
	private String? _message;
	/// <inheritdoc cref="JThrowableObjectMetadata.StackTrace"/>
	private JStackTraceInfo[]? _stackTrace;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JThrowableObject(JLocalObject jLocal) : base(
		jLocal.ForExternalUse(out JClassObject jClass, out JObjectMetadata metadata), jClass)
	{
		if (metadata is not JThrowableObjectMetadata throwableMetadata)
			return;
		this._message ??= throwableMetadata.Message;
		this._stackTrace ??= throwableMetadata.StackTrace;
	}

	/// <summary>
	/// Retrieves the throwable message.
	/// </summary>
	/// <returns>Throwable message.</returns>
	private String GetMessage()
	{
		JFunctionDefinition<JStringObject> definition = new(JThrowableObject.getMessageName);
		JClassObject throwableClass = this.Environment.ClassProvider.NumberClassObject;
		using JStringObject jString = JFunctionDefinition<JStringObject>.Invoke(definition, this, throwableClass)!;
		return jString.Value;
	}
	/// <summary>
	/// Provides programmatic access to the stack trace information printed by printStackTrace();
	/// </summary>
	/// <returns>Throwable stack trace.</returns>
	private static JStackTraceInfo[] GetStackTraceInfo(JLocalObject jThrowable)
	{
		JFunctionDefinition<JArrayObject<JStackTraceElementObject>>
			definition = new(JThrowableObject.getStackTraceName);
		JClassObject throwableClass = jThrowable.Environment.ClassProvider.NumberClassObject;
		using JArrayObject<JStackTraceElementObject> jArr =
			JFunctionDefinition<JArrayObject<JStackTraceElementObject>>.Invoke(definition, jThrowable, throwableClass)!;
		JStackTraceInfo[] result = new JStackTraceInfo[jArr.Length];
		for (Int32 i = 0; i < result.Length; i++)
			result[i] = ((JStackTraceElementObjectMetadata)ILocalObject.CreateMetadata(jArr[i]!))!;
		return result;
	}
}