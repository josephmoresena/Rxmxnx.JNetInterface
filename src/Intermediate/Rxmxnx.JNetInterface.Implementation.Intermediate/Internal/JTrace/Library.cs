namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
                 Justification = CommonConstants.NonStandardTraceJustification)]
#endif
internal static partial class JTrace
{
	/// <summary>
	/// Writes a category name and loading <paramref name="libraryPath"/> library to the trace listeners.
	/// </summary>
	/// <param name="libraryPath">JVM library path.</param>
	/// <param name="handle">Loaded handle.</param>
	public static void LoadLibrary(String libraryPath, IntPtr? handle)
	{
		if (!JVirtualMachine.TraceEnabled) return;
		String handleText = handle.HasValue ? $"0x{handle.Value:x8}" : "null";
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} library: {libraryPath} loaded with {handleText} handle.");
	}
	/// <summary>
	/// Writes a category name and retrieving <paramref name="name"/> symbol from <paramref name="handle"/>
	/// to the trace listeners.
	/// </summary>
	/// <param name="handle">Loaded library handle.</param>
	/// <param name="name">Symbol name.</param>
	/// <param name="found">Indicates whether symbol was found.</param>
	/// <param name="address">Symbol address.</param>
	public static void GetJniExport(IntPtr handle, String name, Boolean found, IntPtr address)
	{
		if (!JVirtualMachine.TraceEnabled) return;
		String addressText = found ? $"0x{address:x8}" : "not found";
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} library handle: {handle} symbol: {addressText} {addressText}.");
	}
}