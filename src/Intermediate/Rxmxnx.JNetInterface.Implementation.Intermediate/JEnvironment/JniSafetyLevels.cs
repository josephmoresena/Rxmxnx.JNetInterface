namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// JNI call safety levels.
	/// </summary>
	[Flags]
	private enum JniSafetyLevels : Byte
	{
		/// <summary>
		/// Indicates JNI call is always unsafe.
		/// </summary>
		Unsafe = 0x0,
		/// <summary>
		/// Indicates JNI call is safe in critical mode.
		/// </summary>
		CriticalSafe = 0x1,
		/// <summary>
		/// Indicates JNI call is safe in error mode.
		/// </summary>
		ErrorSafe = 0x2,
	}
}