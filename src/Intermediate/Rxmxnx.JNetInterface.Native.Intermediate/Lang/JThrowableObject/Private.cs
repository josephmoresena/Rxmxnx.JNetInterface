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
		if (env.Version < (Int32)JRuntimeVersion.SEd4) return [];
		using JArrayObject<JStackTraceElementObject> stackTrace = env.FunctionSet.GetStackTrace(this);
#pragma warning disable CS8620
		StackTraceFunction stackTraceFunction = new(stackTrace);
#pragma warning restore CS8620
		stackTraceFunction.WithFrame(env, IVirtualMachine.GetStackTraceCapacity, out StackTraceInfo[] result);
		return result;
	}

	static JThrowableObject IClassType<JThrowableObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JThrowableObject IClassType<JThrowableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JThrowableObject IClassType<JThrowableObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);

	/// <summary>
	/// Frame function that reads a throwable stack trace.
	/// </summary>
	/// <param name="stackTrace">A <see cref="JStackTraceElementObject"/> array.</param>
	private readonly struct StackTraceFunction(IReadOnlyList<JStackTraceElementObject> stackTrace)
		: IFrameFunction<StackTraceInfo[]>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		StackTraceInfo[] IFrameFunction<StackTraceInfo[]>.Apply(IEnvironment env,
			ReadOnlySpan<JReferenceObject?> objects)
		{
			if (stackTrace.Count == 0) return [];
			StackTraceInfo[] result = new StackTraceInfo[stackTrace.Count];
			for (Int32 i = 0; i < result.Length; i++)
				result[i] = (StackTraceElementObjectMetadata)ILocalObject.CreateMetadata(stackTrace[i]);
			return result;
		}
	}
}