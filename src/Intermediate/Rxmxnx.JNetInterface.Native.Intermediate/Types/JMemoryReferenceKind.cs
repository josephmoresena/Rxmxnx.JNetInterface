namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// Kinds of native java memory.
/// </summary>
public enum JMemoryReferenceKind : Byte
{
	/// <summary>
	/// The memory is only usable in current thread using a local reference.
	/// </summary>
	Local = 0,
	/// <summary>
	/// The memory is usable in another threads using a global weak reference.
	/// </summary>
	ThreadIndependent = 1,
	/// <summary>
	/// The memory is usable in another threads using a global reference.
	/// </summary>
	ThreadUnrestricted = 2,
}