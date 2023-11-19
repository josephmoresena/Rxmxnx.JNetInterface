namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Result of a JNI operation.
/// </summary>
public enum JResult
{
	/// <summary>
	/// Operation was successfully completed.
	/// </summary>
	Ok = 0,
	/// <summary>
	/// Operation was unsuccessfully completed.
	/// </summary>
	Error = -1,
	/// <summary>
	/// Current thread is not attached to VM.
	/// </summary>
	DetachedThreadError = -2,
	/// <summary>
	/// Current version is invalid for given function.
	/// </summary>
	VersionError = -3,
	/// <summary>
	/// VM memory was no enough to complete the operation.
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