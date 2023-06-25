namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Types of JNI object reference.
/// </summary>
public enum JReferenceType
{
	/// <summary>
	/// Invalid JNI object reference (<c>InvalidRefType</c>).
	/// </summary>
	InvalidRefType = 0,
	/// <summary>
	/// Local JNI object reference (<c>LocalRefType</c>).
	/// </summary>
	LocalRefType = 1,
	/// <summary>
	/// Global JNI object reference (<c>GlobalRefType</c>).
	/// </summary>
	GlobalRefType = 2,
	/// <summary>
	/// Weak global JNI object reference (<c>WeakGlobalRefType</c>).
	/// </summary>
	WeakGlobalRefType = 3,
}