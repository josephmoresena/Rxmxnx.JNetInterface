namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
                 Justification = CommonConstants.NonStandardTraceJustification)]
#endif
internal static partial class JTrace
{
	/// <summary>
	/// Writes a category name and the vm loading.
	/// </summary>
	/// <param name="vmRef"><see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef"><see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void MainClassesLoading(JVirtualMachineRef vmRef, JEnvironmentRef envRef,
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {vmRef} {envRef} loading.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and the vm load.
	/// </summary>
	/// <param name="vmRef"><see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef"><see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void MainClassesLoaded(JVirtualMachineRef vmRef, JEnvironmentRef envRef,
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {vmRef} {envRef} loaded.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and the main class load.
	/// </summary>
	/// <param name="classSignature">Loaded class signature.</param>
	/// <param name="globalRef">Loaded global reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void MainClassLoaded(CString classSignature, JGlobalRef globalRef,
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		String className = ClassNameHelper.GetClassName(classSignature);
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} class: {className} {globalRef} loaded.",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the main class access load.
	/// </summary>
	/// <param name="jObject">Loaded access class name.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void AccessLoaded(JReferenceObject jObject, [CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		String classText = jObject.ToTraceText();
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {classText} access loaded.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and the main class access load.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="className">Defining class name.</param>
	/// <param name="buffer">Binary class information.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void DefiningClass(JEnvironmentRef envRef, CString className, ReadOnlySpan<Byte> buffer,
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} {envRef} defining class: {className} {JTrace.GetSha256Hex(buffer)}.",
			callerMethod);
	}
	/// <summary>
	/// Writes a category name and the main class access load.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="className">Defining class name.</param>
	/// <param name="classRef">Loaded <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void DefiningClass(JEnvironmentRef envRef, CString className, JClassLocalRef classRef,
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {envRef} defined class: {className} {classRef}.",
		                callerMethod);
	}
	/// <summary>
	/// Retrieves hex string from <paramref name="bytes"/> hash.
	/// </summary>
	/// <param name="bytes">Binary class information.</param>
	/// <returns>Sha256 hex.</returns>
	private static String GetSha256Hex(ReadOnlySpan<Byte> bytes)
	{
		Span<Byte> hash = stackalloc Byte[32];
		SHA256.TryHashData(bytes, hash, out _);
		return Convert.ToHexString(hash);
	}
}