namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public sealed class JArgumentMetadataTests
{
	[Fact]
	internal void Test()
	{
		JArgumentMetadata argMetadata = JArgumentMetadata.Create("Lpackage/ClassName;"u8);
		Assert.Equal(IntPtr.Size, argMetadata.Size);
		Assert.Equal("Lpackage/ClassName;", argMetadata.Signature.ToString());

		Assert.Throws<ArgumentException>(() => JArgumentMetadata.Create(ReadOnlySpan<Byte>.Empty));
		Assert.Throws<ArgumentException>(() => JArgumentMetadata.Create("L"u8));
		Assert.Throws<ArgumentException>(() => JArgumentMetadata.Create("[I"u8));
		Assert.Throws<ArgumentException>(() => JArgumentMetadata.Create("[L"u8));
		Assert.Throws<ArgumentException>(() => JArgumentMetadata.Create("[L;"u8));
		Assert.Throws<ArgumentException>(() => JArgumentMetadata.Create("[Lpackage/ClassName"u8));
	}
}