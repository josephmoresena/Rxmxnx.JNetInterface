namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stored information for Thread creation.
/// </summary>
internal readonly struct ThreadCreationArgs
{
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
	public Int32 Version { get; init; }

	/// <summary>
	/// Creates a <see cref="ThreadCreationArgs"/> instance from <paramref name="purpose"/>.
	/// </summary>
	/// <param name="purpose">Thread purpose.</param>
	/// <returns>A <see cref="ThreadCreationArgs"/> instance.</returns>
	public static ThreadCreationArgs Create(ThreadPurpose purpose)
		=> new() { Name = ThreadCreationArgs.GetThreadName(purpose), Version = IVirtualMachine.MinimalVersion, };

	/// <summary>
	/// Retrieves the name for purposed thread.
	/// </summary>
	/// <param name="purpose">A <see cref="ThreadPurpose"/> value.</param>
	/// <returns>A <see cref="CString"/> containing thread name.</returns>
	private static CString GetThreadName(ThreadPurpose purpose)
	{
		ReadOnlySpan<Byte> prefix = ThreadCreationArgs.GetPurposePrefix(purpose);
		return CString.Concat(prefix, "-"u8, (CString)Environment.CurrentManagedThreadId.ToString());
	}
	/// <summary>
	/// Retrieves the <paramref name="purpose"/> UTF-8 text prefix for thread name.
	/// </summary>
	/// <param name="purpose">A <see cref="ThreadPurpose"/> value.</param>
	/// <returns>UTF-8 thread name prefix.</returns>
	private static ReadOnlySpan<Byte> GetPurposePrefix(ThreadPurpose purpose)
		=> purpose switch
		{
			ThreadPurpose.ExceptionExecution => "ExceptionExecution"u8,
			ThreadPurpose.CheckAssignability => "CheckAssignability"u8,
			ThreadPurpose.ReleaseSequence => "ReleaseSequence"u8,
			ThreadPurpose.RemoveGlobalReference => "RemoveGlobalReference"u8,
			ThreadPurpose.FatalError => "FatalError"u8,
			ThreadPurpose.CheckGlobalReference => "CheckGlobalReference"u8,
			ThreadPurpose.SynchronizeGlobalReference => "SynchronizeGlobalReference"u8,
			_ => "CreateGlobalReference"u8,
		};
}