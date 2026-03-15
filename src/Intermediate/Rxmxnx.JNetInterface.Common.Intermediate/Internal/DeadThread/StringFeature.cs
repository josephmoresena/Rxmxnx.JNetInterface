namespace Rxmxnx.JNetInterface.Internal;

internal partial class DeadThread : IStringFeature
{
	Int32 IStringFeature.GetLength(JReferenceObject jObject)
	{
		this.GetLengthTrace(jObject);
		return 0;
	}
	Int32 IStringFeature.GetUtf8Length(JReferenceObject jObject)
	{
		this.GetUtf8LengthTrace(jObject, false);
		return 0;
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Int64? IStringFeature.GetUtf8LongLength(JReferenceObject jObject)
	{
		this.GetUtf8LengthTrace(jObject, true);
		return default;
	}
	void IStringFeature.GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex)
		=> this.ThrowInvalidResult<Byte>();
	void IStringFeature.GetUtf8Copy(JStringObject jString, Span<Byte> utf8Units, Int32 startIndex)
		=> this.ThrowInvalidResult<Byte>();
	INativeMemoryAdapter IStringFeature.GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		=> this.ThrowInvalidResult<INativeMemoryAdapter>();
	INativeMemoryAdapter IStringFeature.GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		=> this.ThrowInvalidResult<INativeMemoryAdapter>();
	INativeMemoryAdapter IStringFeature.GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		=> this.ThrowInvalidResult<INativeMemoryAdapter>();
	JStringObject IStringFeature.Create(ReadOnlySpan<Char> data, String? value)
		=> this.ThrowInvalidResult<JStringObject>();
	JStringObject IStringFeature.Create(ReadOnlySpan<Byte> utf8Data) => this.ThrowInvalidResult<JStringObject>();
	ReadOnlyValPtr<Char> IStringFeature.GetSequence(JStringLocalRef stringRef, out Boolean isCopy)
	{
		Unsafe.SkipInit(out isCopy);
		return this.ThrowInvalidResult<ReadOnlyValPtr<Char>>();
	}
	ReadOnlyValPtr<Byte> IStringFeature.GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy)
	{
		Unsafe.SkipInit(out isCopy);
		return this.ThrowInvalidResult<ReadOnlyValPtr<Byte>>();
	}
	ReadOnlyValPtr<Char> IStringFeature.GetCriticalSequence(JStringLocalRef stringRef)
		=> this.ThrowInvalidResult<ReadOnlyValPtr<Char>>();
	void IStringFeature.ReleaseSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		=> this.ReleaseSequenceTrace(stringRef, pointer);
	void IStringFeature.ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer)
		=> this.ReleaseUtf8SequenceTrace(stringRef, pointer);
	void IStringFeature.ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		=> this.ReleaseCriticalSequenceTrace(stringRef, pointer);
}