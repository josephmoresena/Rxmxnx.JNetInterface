namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// JNI call safety levels.
/// </summary>
[Flags]
internal enum JniSafetyLevels : Byte
{
	/// <summary>
	/// Indicates JNI call is always unsafe.
	/// </summary>
	None = 0x0,
	/// <summary>
	/// Indicates JNI call is safe in critical mode.
	/// </summary>
	CriticalSafe = 0x1,
	/// <summary>
	/// Indicates JNI call is safe in error mode.
	/// </summary>
	ErrorSafe = 0x2,
}