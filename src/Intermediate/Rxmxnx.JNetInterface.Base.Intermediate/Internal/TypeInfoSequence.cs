namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal type information sequence.
/// </summary>
internal sealed partial class TypeInfoSequence
{
	/// <summary>
	/// JNI class name.
	/// </summary>
	public CString ClassName { get; }
	/// <summary>
	/// JNI signature for object instances of this type.
	/// </summary>
	public CString Signature { get; }
	/// <summary>
	/// JNI signature for array instances of this type.
	/// </summary>
	public CString ArraySignature { get; }
	/// <summary>
	/// Current datatype hash.
	/// </summary>
	public String Hash { get; }

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override String ToString() => this.Hash;

	//Temporal operators
	public static implicit operator CStringSequence(TypeInfoSequence info) => CStringSequence.Parse(info.Hash);
	public static implicit operator TypeInfoSequence(CStringSequence info)
		=> new(info.ToString(), info[0].Length, info[1].Length);
}