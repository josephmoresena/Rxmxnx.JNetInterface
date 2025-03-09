namespace Rxmxnx.JNetInterface.Native;

public partial class JNativeCallEntry
{
	/// <summary>
	/// Represents a parameterless instance method delegate.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public delegate void ParameterlessInstanceMethodDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);

	/// <summary>
	/// Represents a parameterless static method delegate.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public delegate void ParameterlessStaticMethodDelegate(JEnvironmentRef envRef, JClassLocalRef classRef);
}