namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Result of a JNI operation.
/// </summary>
public enum JResult
{
	/// <summary>
	/// The operation was successfully completed.
	/// </summary>
	Ok = 0,
	/// <summary>
	/// The operation was unsuccessfully completed.
	/// </summary>
	Error = -1,
	/// <summary>
	/// The current thread is not attached to VM.
	/// </summary>
	DetachedThreadError = -2,
	/// <summary>
	/// The current version is invalid for a given function.
	/// </summary>
	VersionError = -3,
	/// <summary>
	/// VM memory was not enough to complete the operation.
	/// </summary>
	MemoryError = -4,
	/// <summary>
	/// Operation finalized the VM execution.
	/// </summary>
	ExitingError = -5,
	/// <summary>
	/// An operation argument was invalid.
	/// </summary>
	InvalidArgumentsError = -6,
}