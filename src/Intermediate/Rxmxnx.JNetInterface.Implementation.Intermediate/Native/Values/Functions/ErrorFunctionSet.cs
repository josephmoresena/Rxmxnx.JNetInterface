namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java throwable objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct ErrorFunctionSet
{
	/// <summary>
	/// Pointer to <c>Throw</c> function.
	/// Causes a <c>java.lang.Throwable</c> object to be thrown.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JThrowableLocalRef, JResult> Throw;
	/// <summary>
	/// Pointer to <c>ThrowNew</c> function.
	/// Constructs an exception object from the specified class with the message specified
	/// and causes that exception to be thrown.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, Byte*, JResult> ThrowNew;
	/// <summary>
	/// Pointer to <c>ExceptionOccurred</c> function.
	/// Determines if an exception is being thrown.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JThrowableLocalRef> ExceptionOccurred;
	/// <summary>
	/// Pointer to <c>ExceptionDescribe</c> function.
	/// Prints an exception and a backtrace of the stack to a system error-reporting channel.
	/// This is a convenience routine provided for debugging.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, void> ExceptionDescribe;
	/// <summary>
	/// Pointer to <c>ExceptionClear</c> function.
	/// Clears any exception that is currently being thrown.
	/// If no exception is currently being thrown, this routine has no effect.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, void> ExceptionClear;
	/// <summary>
	/// Pointer to <c>FatalError</c> function.
	/// Raises a fatal error and does not expect the <c>VM</c> to recover. This function does not return.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, Byte*, void> FatalError;
}