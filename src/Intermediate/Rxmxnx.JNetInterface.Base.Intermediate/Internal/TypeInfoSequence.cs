namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal type information sequence.
/// </summary>
internal sealed partial class TypeInfoSequence : InfoSequenceBase
{
	/// <summary>
	/// JNI signature for object instances of this type.
	/// </summary>
	public CString Signature { get; }
	/// <summary>
	/// JNI signature for array instances of this type.
	/// </summary>
	public CString ArraySignature { get; }

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override String ToString() => this.Hash;
}