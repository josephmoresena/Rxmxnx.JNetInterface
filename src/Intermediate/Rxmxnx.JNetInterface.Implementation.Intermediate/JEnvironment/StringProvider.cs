namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IStringProvider
	{
		public String ToString(JReferenceObject jObject) => throw new NotImplementedException();
		public JStringObject Create(ReadOnlySpan<Char> data) => throw new NotImplementedException();
		public JStringObject Create(ReadOnlySpan<Byte> utf8Data) => throw new NotImplementedException();
		public Int32 GetLength(JStringObject jString) => throw new NotImplementedException();
		public Int32 GetUtf8Length(JStringObject jString) => throw new NotImplementedException();
		public IntPtr GetSequence(JStringObject jString, out Boolean isCopy) => throw new NotImplementedException();
		public IntPtr GetUtf8Sequence(JStringObject jString, out Boolean isCopy) => throw new NotImplementedException();
		public IntPtr GetCriticalSequence(JStringObject jString) => throw new NotImplementedException();
		public void ReleaseSequence(JStringObject jString, IntPtr pointer) { throw new NotImplementedException(); }
		public void ReleaseUtf8Sequence(JStringObject jString, IntPtr pointer) { throw new NotImplementedException(); }
		public void ReleaseCriticalSequence(JStringObject jString, IntPtr pointer)
		{
			throw new NotImplementedException();
		}
		public void GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex = 0)
		{
			throw new NotImplementedException();
		}
		public void GetCopyUtf8(JStringObject jString, Span<Byte> utf8Units, Int32 startIndex = 0)
		{
			throw new NotImplementedException();
		}
	}
}