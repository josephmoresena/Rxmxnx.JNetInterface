namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class TypeInformationTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void GetSegmentTest()
	{
		CString[] texts = TypeInformationTests.fixture.CreateMany<String>(10).Select(s => (CString)s).ToArray();
		CStringSequence sequence = new(texts);
		ReadOnlySpan<Byte> span = sequence.ToString().AsSpan().AsBytes();
		Int32 count = 0;
		while (span.Length > 0)
		{
			ReadOnlySpan<Byte> segment = ITypeInformation.GetSegment(ref span);
			Assert.True(segment.SequenceEqual(texts[count]));
			count++;
		}
	}
}