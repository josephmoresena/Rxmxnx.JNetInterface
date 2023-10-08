namespace Rxmxnx.JNetInterface;

/// <summary>
/// This enumeration contains the purposes for which a temporary internal thread can be requested
/// from the virtual machine.
/// </summary>
public enum ThreadPurpose : Byte
{
	/// <summary>
	/// Indicates the purpose of the thread is release unused global references.
	/// </summary>
	RemoveGlobalReference = 1,
	/// <summary>
	/// Indicates the purpose of the thread is release primitve sequence.
	/// </summary>
	ReleaseSequence = 2,
}