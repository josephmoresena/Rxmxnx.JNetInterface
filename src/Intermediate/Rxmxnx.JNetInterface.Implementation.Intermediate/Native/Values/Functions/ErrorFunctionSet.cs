namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java throwable objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ErrorFunctionSet
{
	/// <summary>
	/// Pointer to <c>Throw</c> function.
	/// Causes a <c>java.lang.Throwable</c> object to be thrown.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Int32> _throw;
	/// <summary>
	/// Pointer to <c>ThrowNew</c> function.
	/// Constructs an exception object from the specified class with the message specified
	/// and causes that exception to be thrown.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Byte*, Int32> _throwNew;
	/// <summary>
	/// Pointer to <c>ExceptionOccurred</c> function.
	/// Determines if an exception is being thrown.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr> _exceptionOccurred;
	/// <summary>
	/// Pointer to <c>ExceptionDescribe</c> function.
	/// Prints an exception and a backtrace of the stack to a system error-reporting channel.
	/// This is a convenience routine provided for debugging.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, void> _exceptionDescribe;
	/// <summary>
	/// Pointer to <c>ExceptionClear</c> function.
	/// Clears any exception that is currently being thrown.
	/// If no exception is currently being thrown, this routine has no effect.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, void> _exceptionClear;
	/// <summary>
	/// Pointer to <c>FatalError</c> function.
	/// Raises a fatal error and does not expect the <c>VM</c> to recover. This function does not return.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, Byte*, void> _fatalError;

	/// <summary>
	/// <c>Throw</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult Throw(JEnvironmentRef envRef, JThrowableLocalRef throwableRef)
		=> (JResult)this._throw(envRef.Pointer, throwableRef.Pointer);
	/// <summary>
	/// <c>ThrowNew</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult ThrowNew(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* messagePtr)
		=> (JResult)this._throwNew(envRef.Pointer, classRef.Pointer, messagePtr);
	/// <summary>
	/// <c>ExceptionOccurred</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JThrowableLocalRef ExceptionOccurred(JEnvironmentRef envRef) => new(this._exceptionOccurred(envRef.Pointer));
	/// <summary>
	/// <c>ExceptionDescribe</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ExceptionDescribe(JEnvironmentRef envRef) => this._exceptionDescribe(envRef.Pointer);
	/// <summary>
	/// <c>ExceptionClear</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ExceptionClear(JEnvironmentRef envRef) => this._exceptionClear(envRef.Pointer);
	/// <summary>
	/// <c>FatalError</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void FatalError(JEnvironmentRef envRef, Byte* messagePtr) => this._fatalError(envRef.Pointer, messagePtr);
}