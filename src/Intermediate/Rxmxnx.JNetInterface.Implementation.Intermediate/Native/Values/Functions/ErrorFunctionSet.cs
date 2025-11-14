namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java throwable objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ErrorFunctionSet
{
	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>Throw</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult Throw(JEnvironmentRef envRef, JThrowableLocalRef throwableRef)
		=> SystemInfo.IsWindows ? this._windows.Throw(envRef, throwableRef) : this._unix.Throw(envRef, throwableRef);
	/// <summary>
	/// <c>ThrowNew</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult ThrowNew(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* messagePtr)
		=> SystemInfo.IsWindows ?
			this._windows.ThrowNew(envRef, classRef, messagePtr) :
			this._unix.ThrowNew(envRef, classRef, messagePtr);
	/// <summary>
	/// <c>ExceptionOccurred</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JThrowableLocalRef ExceptionOccurred(JEnvironmentRef envRef)
		=> SystemInfo.IsWindows ? this._windows.ExceptionOccurred(envRef) : this._unix.ExceptionOccurred(envRef);
	/// <summary>
	/// <c>ExceptionDescribe</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ExceptionDescribe(JEnvironmentRef envRef)
	{
		if (SystemInfo.IsWindows)
			this._windows.ExceptionDescribe(envRef);
		else
			this._unix.ExceptionDescribe(envRef);
	}
	/// <summary>
	/// <c>ExceptionClear</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ExceptionClear(JEnvironmentRef envRef)
	{
		if (SystemInfo.IsWindows)
			this._windows.ExceptionClear(envRef);
		else
			this._unix.ExceptionClear(envRef);
	}
	/// <summary>
	/// <c>FatalError</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void FatalError(JEnvironmentRef envRef, Byte* messagePtr)
	{
		if (SystemInfo.IsWindows)
			this._windows.FatalError(envRef, messagePtr);
		else
			this._unix.FatalError(envRef, messagePtr);
	}

	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Pointer to <c>Throw</c> function.
		/// Causes a <c>java.lang.Throwable</c> object to be thrown.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JThrowableLocalRef, JResult> Throw;
		/// <summary>
		/// Pointer to <c>ThrowNew</c> function.
		/// Constructs an exception object from the specified class with the message specified
		/// and causes that exception to be thrown.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JClassLocalRef, Byte*, JResult> ThrowNew;
		/// <summary>
		/// Pointer to <c>ExceptionOccurred</c> function.
		/// Determines if an exception is being thrown.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JThrowableLocalRef> ExceptionOccurred;
		/// <summary>
		/// Pointer to <c>ExceptionDescribe</c> function.
		/// Prints an exception and a backtrace of the stack to a system error-reporting channel.
		/// This is a convenience routine provided for debugging.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, void> ExceptionDescribe;
		/// <summary>
		/// Pointer to <c>ExceptionClear</c> function.
		/// Clears any exception that is currently being thrown.
		/// If no exception is currently being thrown, this routine has no effect.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, void> ExceptionClear;
		/// <summary>
		/// Pointer to <c>FatalError</c> function.
		/// Raises a fatal error and does not expect the <c>VM</c> to recover. This function does not return.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, Byte*, void> FatalError;
	}

	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Pointer to <c>Throw</c> function.
		/// Causes a <c>java.lang.Throwable</c> object to be thrown.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JThrowableLocalRef, JResult> Throw;
		/// <summary>
		/// Pointer to <c>ThrowNew</c> function.
		/// Constructs an exception object from the specified class with the message specified
		/// and causes that exception to be thrown.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JClassLocalRef, Byte*, JResult> ThrowNew;
		/// <summary>
		/// Pointer to <c>ExceptionOccurred</c> function.
		/// Determines if an exception is being thrown.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JThrowableLocalRef> ExceptionOccurred;
		/// <summary>
		/// Pointer to <c>ExceptionDescribe</c> function.
		/// Prints an exception and a backtrace of the stack to a system error-reporting channel.
		/// This is a convenience routine provided for debugging.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, void> ExceptionDescribe;
		/// <summary>
		/// Pointer to <c>ExceptionClear</c> function.
		/// Clears any exception that is currently being thrown.
		/// If no exception is currently being thrown, this routine has no effect.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, void> ExceptionClear;
		/// <summary>
		/// Pointer to <c>FatalError</c> function.
		/// Raises a fatal error and does not expect the <c>VM</c> to recover. This function does not return.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, Byte*, void> FatalError;
	}
}