namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ObjectTest
{
	[Fact]
	internal void CopyTest()
	{
		ObjectDummy jObject = Substitute.For<ObjectDummy>();
		Span<Byte> span = Span<Byte>.Empty;
		Int32 length = span.Length;
		(jObject as IObject).CopyTo(span);

		jObject.Received(1).CopyTo(Arg.Is<Byte[]>(a => a.Length == length),
		                           Arg.Is<IMutableReference<Int32>>(r => r.Value == 0));
	}
}