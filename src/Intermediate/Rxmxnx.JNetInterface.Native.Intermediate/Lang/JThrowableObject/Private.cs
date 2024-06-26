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
	private StackTraceInfo[] GetStackTrace()
	{
		IEnvironment env = this.Environment;
		using JArrayObject<JStackTraceElementObject> stackTrace = env.FunctionSet.GetStackTrace(this);
		return this.Environment.WithFrame(IVirtualMachine.GetStackTraceCapacity, stackTrace,
		                                  JThrowableObject.GetStackTraceInfo!);
	}

	/// <summary>
	/// Retrieves <see cref="StackTraceInfo"/> instance from <paramref name="stackTrace"/> array.
	/// </summary>
	/// <param name="stackTrace">A <see cref="JStackTraceElementObject"/> array.</param>
	/// <returns>A <see cref="StackTraceInfo"/> array.</returns>
	/// <remarks>This method runs within a local frame. Any local references created will be deleted.</remarks>
	private static StackTraceInfo[] GetStackTraceInfo(IReadOnlyList<JStackTraceElementObject> stackTrace)
	{
		if (stackTrace.Count == 0) return [];
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