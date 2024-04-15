namespace Rxmxnx.JNetInterface;

/// <summary>
/// This enumeration contains the purposes for which a temporary internal thread can be requested
/// from the virtual machine.
/// </summary>
public enum ThreadPurpose : Byte
{
	/// <summary>
	/// Indicates the purpose of the thread is freeing unused global references.
	/// </summary>
	RemoveGlobalReference = 1,
	/// <summary>
	/// Indicates the purpose of the thread is freeing primitive sequences.
	/// </summary>
	ReleaseSequence = 2,
	/// <summary>
	/// Indicates the purpose of the thread is executing JNI calls under exception state.
	/// </summary>
	ExceptionExecution = 3,
	/// <summary>
	/// Indicates the purpose of the thread is executing JNI calls to get assignability.
	/// </summary>
	CheckAssignability = 4,
	/// <summary>
	/// Indicates the purpose of the thread is creating global references.
	/// </summary>
	CreateGlobalReference = 5,
	/// <summary>
	/// Indicates the purpose of the thread is checking global references.
	/// </summary>
	CheckGlobalReference = 6,
	/// <summary>
	/// Indicates the purpose of the thread is synchronizing global references.
	/// </summary>
	SynchronizeGlobalReference = 7,
	/// <summary>
	/// Indicates the purpose of the thread sending fatal error signal to VM.
	/// </summary>
	FatalError = 8,
}