namespace Rxmxnx.JNetInterface;

[ExcludeFromCodeCoverage]
public sealed class JniExceptionTest
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void Test()
	{
		JResult result = JniExceptionTest.fixture.Create<JResult>();
		JniException exception = new(result);
		Assert.Equal(Enum.GetName(result), exception.Message);
	}
}