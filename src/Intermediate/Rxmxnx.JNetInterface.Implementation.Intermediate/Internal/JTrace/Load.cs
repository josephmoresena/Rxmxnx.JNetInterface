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
	/// Writes a category name and the virtual machine load.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="firstTime">Indicates whether the virtual machine was loaded for first time.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void VirtualMachineLoad(JVirtualMachineRef vmRef, Boolean firstTime,
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {vmRef} loaded, new: {firstTime}.",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the environment load.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="firstTime">Indicates whether the environment was loaded for first time.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void EnvironmentLoad(JEnvironmentRef envRef, Boolean firstTime,
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {envRef} loaded, new: {firstTime}.",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the retrieving java.specification.version property value.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="jniVersion">JNI version.</param>
	/// <param name="specificationVersion">java.specification.version property value.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetRuntimeVersion(JEnvironmentRef envRef, Int32 jniVersion,
#if !NET8_0_OR_GREATER
		ReadOnlySpan<Char> specificationVersion,
#else
		ReadOnlySpan<Byte> specificationVersion,
#endif
		[CallerMemberName] String callerMethod = "")
	{
		if (!JVirtualMachine.TraceEnabled) return;
#if !NET8_0_OR_GREATER
		String jVersion = new(specificationVersion);
#else
		String jVersion = Encoding.UTF8.GetString(specificationVersion);
#endif
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} {envRef} (0x{jniVersion:x8}) java.specification.version: {jVersion}.",
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