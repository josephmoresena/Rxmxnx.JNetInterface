namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stored information for Thread creation.
/// </summary>
internal sealed class ThreadCreationArgs
{
	/// <summary>
	/// Dictionary of threads name.
	/// </summary>
	private static readonly Dictionary<ThreadPurpose, CString> threadNames = new()
	{
		{ ThreadPurpose.ExceptionExecution, new(() => "ExceptionExecution"u8) },
		{ ThreadPurpose.ReleaseSequence, new(() => "ReleaseSequence"u8) },
		{ ThreadPurpose.RemoveGlobalReference, new(() => "RemoveGlobalReference"u8) },
	};

	/// <summary>
	/// Thread Name.
	/// </summary>
	public CString? Name { get; init; }
	/// <summary>
	/// Thread group.
	/// </summary>
	public JGlobalBase? ThreadGroup { get; init; }
	/// <summary>
	/// Indicates whether created thread is a daemon.
	/// </summary>
	public Boolean IsDaemon { get; init; }
	/// <summary>
	/// JNI Version.
	/// </summary>
	public Int32 Version { get; init; } = IVirtualMachine.MinimalVersion;

	/// <summary>
	/// Creates a <see cref="ThreadCreationArgs"/> instance from <paramref name="purpose"/>.
	/// </summary>
	/// <param name="purpose">Thread purpose.</param>
	/// <returns>A <see cref="ThreadCreationArgs"/> instance.</returns>
	public static ThreadCreationArgs Create(ThreadPurpose purpose)
		=> new() { Name = ThreadCreationArgs.GetThreadName(purpose), };

	/// <summary>
	/// Retrieves the name for purposed thread.
	/// </summary>
	/// <param name="purpose">A <see cref="ThreadPurpose"/> value.</param>
	/// <returns>A <see cref="CString"/> containing thread name.</returns>
	private static CString GetThreadName(ThreadPurpose purpose)
	{
		CString prefix = ThreadCreationArgs.threadNames.GetValueOrDefault(purpose) ?? CString.Zero;
		return CString.Concat(prefix, (CString)Environment.CurrentManagedThreadId.ToString());
	}
}