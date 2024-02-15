namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class TypeInformationTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void GetSegmentLengthTest()
	{
		CString[] texts = TypeInformationTests.fixture.CreateMany<String>(10).Select(s => (CString)s).ToArray();
		CStringSequence sequence = new(texts);
		ReadOnlySpan<Byte> span = sequence.ToString().AsSpan().AsBytes();
		Int32 offset = 0;
		Int32 count = 0;
		while (offset < span.Length - 1)
		{
			Int32 length = ITypeInformation.GetSegmentLength(span, offset);
			Assert.Equal(sequence[count].Length, length);
			count++;
			offset += length + 1;
		}
	}
}