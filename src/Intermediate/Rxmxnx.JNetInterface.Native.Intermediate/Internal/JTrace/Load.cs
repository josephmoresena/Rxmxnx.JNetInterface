namespace Rxmxnx.JNetInterface.Internal;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
                 Justification = CommonConstants.NonStandardTraceJustification)]
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
		if (!IVirtualMachine.TraceEnabled) return;
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
		if (!IVirtualMachine.TraceEnabled) return;
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
		if (!IVirtualMachine.TraceEnabled) return;
		String className = ClassNameHelper.GetClassName(classSignature);
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {className} {globalRef} loaded.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and the main class access load.
	/// </summary>
	/// <param name="jObject">Loaded access class name.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void AccessLoaded(JReferenceObject jObject, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
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
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} {envRef} defining {className} class {Convert.ToHexString(buffer)}.",
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
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {envRef} defined {className} class {classRef}.",
		                callerMethod);
	}
}