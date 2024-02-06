namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JniExceptionTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void Test()
	{
		JResult result = JniExceptionTests.fixture.Create<JResult>();
		JniException exception = new(result);
		Assert.Equal(Enum.GetName(result), exception.Message);
		Assert.Equal(result, exception.Result);
	}
}