namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectReferenceType : IFixedPointer, IWrapper<JObjectLocalRef>, INativeType
{
	/// <summary>
	/// Determines whether current reference is default.
	/// </summary>
	Boolean IsDefault { get; }
}