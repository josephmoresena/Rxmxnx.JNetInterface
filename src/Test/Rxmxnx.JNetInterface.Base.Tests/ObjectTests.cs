namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ObjectTests
{
	private static readonly IFixture fixture = new Fixture();

	[Theory]
	[InlineData(0)]
	[InlineData(10)]
	[InlineData(100)]
	internal void CopyTest(Int32 length)
	{
		ObjectProxy jObject = Substitute.For<ObjectProxy>();
		Byte[] array = ObjectTests.fixture.CreateMany<Byte>(length).ToArray();
		(jObject as IObject).CopyTo(array);
		jObject.Received(1).CopyTo(Arg.Is<Byte[]>(a => a.SequenceEqual(array)),
		                           Arg.Is<IMutableReference<Int32>>(r => r.Value == 0));
	}
	[Theory]
	[InlineData(0)]
	[InlineData(10)]
	[InlineData(100)]
#pragma warning disable CA1859
	internal void ViewByteCopyTest(Int32 length)
	{
		ObjectProxy jObject = Substitute.For<ObjectProxy>();
		IViewObject view = new ViewObjectProxy(jObject);
		Byte[] array = ObjectTests.fixture.CreateMany<Byte>(length).ToArray();
		Int32 offset = Random.Shared.Next(0, array.Length);
		view.CopyTo(array, ref offset);
		jObject.Received(1).CopyTo(Arg.Is<Byte[]>(a => a.SequenceEqual(array)),
		                           Arg.Is<IMutableReference<Int32>>(r => r.Value == offset));
	}
	[Theory]
	[InlineData(0)]
	[InlineData(10)]
	[InlineData(100)]
	internal void ViewValueCopyTest(Int32 length)
	{
		ObjectProxy jObject = Substitute.For<ObjectProxy>();
		IViewObject view = new ViewObjectProxy(jObject);
		ValueProxy[] array = MemoryMarshal.Cast<Byte, ValueProxy>(
			ObjectTests.fixture.CreateMany<Byte>(length * JValue.Size).ToArray()).ToArray();
		Int32 index = Random.Shared.Next(0, array.Length);
		view.CopyTo(MemoryMarshal.Cast<ValueProxy, JValue>(array), index);
		jObject.Received(1).CopyTo(Arg.Is<ValueProxy[]>(a => a.SequenceEqual(array)), index);
	}
	[Fact]
	internal void ViewInformationTest()
	{
		CString className = (CString)ObjectTests.fixture.Create<String>();
		CString signature = (CString)ObjectTests.fixture.Create<String>();
		ObjectProxy jObject = Substitute.For<ObjectProxy>();
		IViewObject view = new ViewObjectProxy(jObject);

		jObject.ObjectClassName.Returns(className);
		jObject.ObjectSignature.Returns(signature);

		Assert.Equal(jObject, view.Object);
		Assert.Equal(jObject, view.Value);
		Assert.Equal(className, view.ObjectClassName);
		Assert.Equal(signature, view.ObjectSignature);
	}
#pragma warning restore CA1859
}