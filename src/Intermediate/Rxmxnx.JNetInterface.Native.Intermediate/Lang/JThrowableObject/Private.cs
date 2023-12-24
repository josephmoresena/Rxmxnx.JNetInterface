namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
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
		using JStringObject message = this.Environment.Functions.GetMessage(this);
		return message.Value;
	}
	/// <summary>
	/// Provides programmatic access to the stack trace information printed by printStackTrace();
	/// </summary>
	/// <returns>Throwable stack trace.</returns>
	private static JStackTraceInfo[] GetStackTraceInfo(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		using JArrayObject<JStackTraceElementObject> stackTrace = env.Functions.GetStackTrace(jThrowable);
		JStackTraceInfo[] result = new JStackTraceInfo[stackTrace.Length];
		for (Int32 i = 0; i < result.Length; i++)
			result[i] = ((JStackTraceElementObjectMetadata)ILocalObject.CreateMetadata(stackTrace[i]!))!;
		return result;
	}
}