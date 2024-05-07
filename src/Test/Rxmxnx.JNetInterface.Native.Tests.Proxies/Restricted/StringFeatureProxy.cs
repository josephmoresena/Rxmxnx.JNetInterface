namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract partial class StringFeatureProxy : IStringFeature
{
	public abstract Int32 GetLength(JReferenceObject jObject);
	public abstract Int32 GetUtf8Length(JReferenceObject jObject);
	public abstract void GetCopyUtf8(JStringObject jString, Memory<Byte> utf8Units, Int32 startIndex = 0);
	public abstract INativeMemoryAdapter GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	public abstract INativeMemoryAdapter GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	public abstract INativeMemoryAdapter GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind);
	public abstract ReadOnlyValPtr<Char> GetSequence(JStringLocalRef stringRef, out Boolean isCopy);
	public abstract ReadOnlyValPtr<Byte> GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy);
	public abstract ReadOnlyValPtr<Char> GetCriticalSequence(JStringLocalRef stringRef);
	public abstract void ReleaseSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer);
	public abstract void ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer);
	public abstract void ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer);
	public abstract void GetCopy(JStringObject jString, IFixedMemory<Char> chars, Int32 startIndex = 0);
	public abstract JStringObject Create(String data);
	public abstract JStringObject Create(CString utf8Data);
}