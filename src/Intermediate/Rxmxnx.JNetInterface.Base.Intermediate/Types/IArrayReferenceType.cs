namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IArrayReferenceType : IObjectReferenceType, IEquatable<JArrayLocalRef>
{
	/// <summary>
	/// JNI array reference.
	/// </summary>
	JArrayLocalRef ArrayValue { get; }
}