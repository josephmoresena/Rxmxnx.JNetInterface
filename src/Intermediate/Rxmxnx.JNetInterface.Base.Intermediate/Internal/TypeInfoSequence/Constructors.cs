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
	public TypeInfoSequence(String hash, Int32 classNameLength, Int32 signatureLength)
	{
		Int32 arraySignatureOffset = classNameLength + signatureLength + 2;
		this.Hash = hash;
		this.ClassName = CString.Create<State>(new(this.Hash, classNameLength));
		this.Signature = CString.Create<State>(new(this.Hash, signatureLength, classNameLength + 1));
		this.ArraySignature = arraySignatureOffset < this.Hash.Length * 2 ?
			CString.Create<State>(new(this.Hash, signatureLength + 1, arraySignatureOffset)) :
			CString.Empty;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="hash">Type hash.</param>
	/// <param name="classNameLength">Class name length.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TypeInfoSequence(String hash, Int32 classNameLength) : this(hash, classNameLength, classNameLength + 2) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <param name="escape">Indicates whether <paramref name="className"/> should be escaped to JNI.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe TypeInfoSequence(ReadOnlySpan<Byte> className, Boolean escape = false)
	{
		// To create TypeInfoSequence instance we use JNI class name.
		ReadOnlySpan<Byte> jniClassName = escape ?
			className :
			TypeInfoSequence.JniEscapeClassName(stackalloc Byte[className.Length], className);
		// Buffer length should hold at least 3 times the class name, 3 null-characters and 1 array prefix char.
		Int32 bufferLength = 3 * className.Length + 4;
		Boolean isArray = className[0] == CommonNames.ArraySignaturePrefixChar;
		// If class is not an array, signature and class name are different, so we need hold signature
		// prefix and suffix 2 times.
		if (!isArray) bufferLength += 4;
		fixed (Byte* char0 = &MemoryMarshal.GetReference(jniClassName))
		{
			SpanState state = new(char0, jniClassName.Length, isArray);
			Int32 stringLength = bufferLength / sizeof(Char) + bufferLength % sizeof(Char);
			this.Hash = String.Create(stringLength, state, TypeInfoSequence.CreateInfoSequence);
		}
		this.ClassName = CString.Create<State>(new(this.Hash, className.Length));
		this.Signature =
			CString.Create<State>(new(this.Hash, className.Length + (isArray ? 0 : 2), this.ClassName.Length + 1));
		this.ArraySignature = CString.Create<State>(new(this.Hash, this.Signature.Length + 1,
		                                                this.ClassName.Length + this.Signature.Length + 2));
	}
}