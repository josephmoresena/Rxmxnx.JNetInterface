namespace Rxmxnx.JNetInterface.Internal;

internal partial class TypeInfoSequence
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="hash">Type hash.</param>
	/// <param name="classNameLength">Class name length.</param>
	/// <param name="signatureLength">Signature length.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TypeInfoSequence(String hash, Int32 classNameLength, Int32 signatureLength) : base(hash, classNameLength)
	{
		Int32 arraySignatureOffset = classNameLength + signatureLength + 2;
		this.Signature = CString.Create<ItemState>(new(this.Hash, signatureLength, classNameLength + 1));
		this.ArraySignature = this.Hash.Length * 2 - arraySignatureOffset > 2 ?
			CString.Create<ItemState>(new(this.Hash, signatureLength + 1, arraySignatureOffset)) :
			CString.Empty;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <param name="escape">Indicates whether <paramref name="className"/> should be escaped to JNI.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TypeInfoSequence(ReadOnlySpan<Byte> className, Boolean escape = false) : base(
		TypeInfoSequence.CreateHash(className, escape, out Boolean isArray), className.Length)
	{
		Int32 signatureLength = className.Length + (isArray ? 0 : 2);
		Int32 arraySignatureOffset = className.Length + signatureLength + 2;
		this.Signature = CString.Create<ItemState>(new(this.Hash, signatureLength, className.Length + 1));
		this.ArraySignature = CString.Create<ItemState>(new(this.Hash, signatureLength + 1, arraySignatureOffset));
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="hash">Type hash.</param>
	/// <param name="classNameLength">Class name length.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TypeInfoSequence(String hash, Int32 classNameLength) : this(hash, classNameLength, classNameLength + 2) { }
}