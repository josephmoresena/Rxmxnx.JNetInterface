namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public abstract class IndeterminateAccessTestsBase
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
	protected static void Compare(IndeterminateResult reference, IndeterminateResult result)
	{
		Assert.Equal(reference.Signature, result.Signature);
		Assert.Equal(reference.Object, result.Object);
		Assert.Equal(reference.BooleanValue, result.BooleanValue);
		Assert.Equal(reference.ByteValue, result.ByteValue);
		Assert.Equal(reference.CharValue, result.CharValue);
		Assert.Equal(reference.DoubleValue, result.DoubleValue);
		Assert.Equal(reference.FloatValue, result.FloatValue);
		Assert.Equal(reference.IntValue, result.IntValue);
		Assert.Equal(reference.LongValue, result.LongValue);
		Assert.Equal(reference.ShortValue, result.ShortValue);
	}
}