namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a local array java reference.
/// </summary>
internal interface IArrayLocalRef : IWrapper<JObjectLocalRef>, IEquatable<JArrayLocalRef>
{
	/// <summary>
	/// JNI array local reference.
	/// </summary>
	JArrayLocalRef ArrayValue { get; }
}