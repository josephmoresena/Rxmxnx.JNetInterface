namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JIdentifierTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void FieldTest() => JIdentifierTests.Test<JFieldId>();
	[Fact]
	internal void MethodTest() => JIdentifierTests.Test<JMethodId>();

	private static void Test<TIdentifier>()
		where TIdentifier : unmanaged, IAccessibleIdentifierType, IEquatable<TIdentifier>,
		IEqualityOperators<TIdentifier, TIdentifier, Boolean>
	{
		IntPtr ptr1 = JIdentifierTests.fixture.CreateMany<Byte>(IntPtr.Size).ToArray().AsSpan().AsValue<IntPtr>();
		IntPtr ptr2 = JIdentifierTests.fixture.CreateMany<Byte>(IntPtr.Size).ToArray().AsSpan().AsValue<IntPtr>();
		IntPtr ptr3 = ptr1.AsBytes().AsValue<IntPtr>();

		TIdentifier id1 = NativeUtilities.Transform<IntPtr, TIdentifier>(in ptr1);
		TIdentifier id2 = NativeUtilities.Transform<IntPtr, TIdentifier>(in ptr2);
		TIdentifier id3 = NativeUtilities.Transform<IntPtr, TIdentifier>(in ptr3);

		Assert.Equal(id1.GetHashCode(), id1.Pointer.GetHashCode());
		Assert.NotEqual(id1, id2);

		Assert.False(id1.Equals(id2));
		Assert.True(id1.Equals(id3));
		Assert.False(id1.Equals((Object)id2));
		Assert.True(id1.Equals((Object)id3));

		Assert.False(id1 == id2);
		Assert.True(id1 == id3);
		Assert.True(id1 != id2);
		Assert.False(id1 != id3);

		Assert.Equal(IntPtr.Zero, new TIdentifier().Pointer);
	}
}