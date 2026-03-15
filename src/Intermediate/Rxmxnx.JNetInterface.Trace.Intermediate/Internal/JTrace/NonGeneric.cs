namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
                 Justification = CommonConstants.NonStandardTraceJustification)]
#endif
internal static partial class JTrace
{
	/// <summary>
	/// Writes a category name and deleting global reference to the trace listeners.
	/// </summary>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="globalRefText">A global object reference text.</param>
	/// <param name="callerMethod">Caller member name.</param>
	private static void UnloadNonGenericGlobal(Boolean isAttached, Boolean isAlive, String globalRefText,
		String callerMethod)
	{
		if (!isAttached)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to remove {globalRefText}. Thread is not attached.",
				callerMethod);
		else if (!isAlive)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to {globalRefText}. JVM is not alive.",
				callerMethod);
		else
			Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {globalRefText} removed.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and releasing native memory to the trace listeners.
	/// </summary>
	/// <param name="isCritical">Indicates whether current VM is alive.</param>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="released">Indicates whether memory was successfully released.</param>
	/// <param name="objectRefText">Object reference text.</param>
	/// <param name="pointer">Pointer to memory.</param>
	/// <param name="callerMethod">Caller member name.</param>
	private static void ReleaseNonGenericMemory(Boolean isCritical, Boolean isAttached, Boolean isAlive,
		Boolean released, IntPtr pointer, String objectRefText, String callerMethod)
	{
		String memoryText = isCritical ? "Critical memory" : "Memory";
		if (!isAttached)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to release {memoryText.ToLower()} 0x{pointer:0x8} {objectRefText}. Thread is not attached.",
				callerMethod);
		else if (!isAlive)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to release {memoryText.ToLower()} 0x{pointer:0x8} {objectRefText}. JVM is not alive.",
				callerMethod);
		else if (!released)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Error attempting to release {memoryText} 0x{pointer:0x8} {objectRefText}.",
				callerMethod);
		else
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} {memoryText} 0x{pointer:0x8} {objectRefText} released.",
				callerMethod);
	}
	/// <summary>
	/// Writes a category name and creation of local reference to the trace listeners.
	/// </summary>
	/// <param name="objectRefText">A JNI object reference text.</param>
	/// <param name="localRef">Local JNI object reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	private static void CreateNonGenericLocalRef(String objectRefText, JObjectLocalRef localRef, String callerMethod)
	{
		Trace.WriteLine(
			localRef != default ?
				$"thread: {Environment.CurrentManagedThreadId} {objectRefText} -> {localRef}" :
				$"thread: {Environment.CurrentManagedThreadId} {objectRefText} Error.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and creation of global reference to the trace listeners.
	/// </summary>
	/// <param name="localRef">Local JNI object reference.</param>
	/// <param name="objectRefText">A JNI global object reference text.</param>
	/// <param name="callerMethod">Caller member name.</param>
	private static void CreateNonGenericGlobalRef(JObjectLocalRef localRef, String objectRefText, String callerMethod)
	{
		Trace.WriteLine(
			!String.IsNullOrEmpty(objectRefText) ?
				$"thread: {Environment.CurrentManagedThreadId} {localRef} -> {objectRefText}" :
				$"thread: {Environment.CurrentManagedThreadId} {localRef} Error.", callerMethod);
	}
}