namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <inheritdoc cref="ThrowableObjectMetadata.Message"/>
	private String? _message;
	/// <inheritdoc cref="ThrowableObjectMetadata.StackTrace"/>
	private JStackTraceInfo[]? _stackTrace;

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
		JStackTraceInfo[] result = stackTrace.Length > 0 ?
			new JStackTraceInfo[stackTrace.Length] :
			Array.Empty<JStackTraceInfo>();
		for (Int32 i = 0; i < result.Length; i++)
			result[i] = ((StackTraceElementObjectMetadata)ILocalObject.CreateMetadata(stackTrace[i]!))!;
		return result;
	}

	static JThrowableObject IReferenceType<JThrowableObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JThrowableObject IReferenceType<JThrowableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JThrowableObject IReferenceType<JThrowableObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}