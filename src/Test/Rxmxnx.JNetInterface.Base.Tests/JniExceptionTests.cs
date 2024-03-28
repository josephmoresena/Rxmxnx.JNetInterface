namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JniExceptionTests
{
	[Theory]
	[InlineData(JResult.Ok)]
	[InlineData(JResult.Error)]
	[InlineData(JResult.ExitingError)]
	[InlineData(JResult.MemoryError)]
	[InlineData(JResult.DetachedThreadError)]
	[InlineData(JResult.VersionError)]
	[InlineData(JResult.InvalidArgumentsError)]
	internal void Test(JResult result)
	{
		JniException? exception = result;
		if (result is JResult.Ok)
		{
			Assert.Null(exception);
		}
		else
		{
			Assert.Equal(Enum.GetName(result), exception!.Message);
			Assert.Equal(result, exception.Result);
		}
	}
}