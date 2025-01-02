namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public abstract class IndeterminateCallTestsBase
{
	protected static void EmptyCompare(IndeterminateResult empty)
	{
		Assert.Equal(IndeterminateResult.Empty.Signature, empty.Signature);
		Assert.Equal(IndeterminateResult.Empty.Object, empty.Object);
		Assert.Equal(IndeterminateResult.Empty.BooleanValue, empty.BooleanValue);
		Assert.Equal(IndeterminateResult.Empty.ByteValue, empty.ByteValue);
		Assert.Equal(IndeterminateResult.Empty.CharValue, empty.CharValue);
		Assert.Equal(IndeterminateResult.Empty.DoubleValue, empty.DoubleValue);
		Assert.Equal(IndeterminateResult.Empty.FloatValue, empty.FloatValue);
		Assert.Equal(IndeterminateResult.Empty.IntValue, empty.IntValue);
		Assert.Equal(IndeterminateResult.Empty.LongValue, empty.LongValue);
		Assert.Equal(IndeterminateResult.Empty.ShortValue, empty.ShortValue);
	}
}