namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <inheritdoc cref="ThrowableObjectMetadata.Message"/>
	private String? _message;
	/// <inheritdoc cref="ThrowableObjectMetadata.StackTrace"/>
	private StackTraceInfo[]? _stackTrace;

	/// <summary>
	/// Retrieves the throwable message.
	/// </summary>
	/// <returns>Throwable message.</returns>
	private String GetMessage()
	{
		using JStringObject message = this.Environment.FunctionSet.GetMessage(this);
		return message.Value;
	}

	/// <summary>
	/// Provides programmatic access to the stack trace information printed by printStackTrace();
	/// </summary>
	/// <returns>Throwable stack trace.</returns>
	private static StackTraceInfo[] GetStackTraceInfo(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		using JArrayObject<JStackTraceElementObject> stackTrace = env.FunctionSet.GetStackTrace(jThrowable);
		return JThrowableObject.GetStackTraceInfo(stackTrace!);
	}
	/// <summary>
	/// Retrieves <see cref="StackTraceInfo"/> instance from <paramref name="stackTrace"/> array.
	/// </summary>
	/// <param name="stackTrace">A <see cref="JStackTraceElementObject"/> array.</param>
	/// <returns>A <see cref="StackTraceInfo"/> array.</returns>
	private static StackTraceInfo[] GetStackTraceInfo(IReadOnlyList<JStackTraceElementObject> stackTrace)
	{
		if (stackTrace.Count == 0) return Array.Empty<StackTraceInfo>();
		StackTraceInfo[] result = new StackTraceInfo[stackTrace.Count];
		for (Int32 i = 0; i < result.Length; i++)
			result[i] = (StackTraceElementObjectMetadata)ILocalObject.CreateMetadata(stackTrace[i]);
		return result;
	}

	static JThrowableObject IClassType<JThrowableObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JThrowableObject IClassType<JThrowableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JThrowableObject IClassType<JThrowableObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}