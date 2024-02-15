namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JValueTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void TypeTest() => Assert.Equal(JNativeType.JValue, JValue.Type);
	[Fact]
	internal void IsDefault()
	{
		JValue def = default;
		JValue val = MemoryMarshal.Read<JValue>(JValueTests.fixture.CreateMany<Byte>(JValue.Size).ToArray());
		Assert.True(def.IsDefault);
		Assert.False(val.IsDefault);

		MethodInfo defaultMethod = Environment.Is64BitProcess ?
			typeof(JValue).GetMethod("Default", BindingFlags.Static | BindingFlags.NonPublic)! :
			typeof(JValue).GetMethod("DefaultPointer", BindingFlags.Static | BindingFlags.NonPublic)!;
		Assert.True((Boolean)defaultMethod.Invoke(default, [def,])!);
		Assert.False((Boolean)defaultMethod.Invoke(default, [val,])!);
	}

	[Fact]
	internal void CreateByteTest() => JValueTests.CreateTest<Byte>();
	[Fact]
	internal void CreateInt16Test() => JValueTests.CreateTest<Int16>();
	[Fact]
	internal void CreateInt32Test() => JValueTests.CreateTest<Int32>();
	[Fact]
	internal void CreateInt64Test() => JValueTests.CreateTest<Int64>();
	[Fact]
	internal void CreateHalfTest() => JValueTests.CreateTest<Half>();
	[Fact]
	internal void CreateSingleTest() => JValueTests.CreateTest<Single>();
	[Fact]
	internal void CreateDoubleTest() => JValueTests.CreateTest<Double>();
	[Fact]
	internal void CreateDateTimeTest() => JValueTests.CreateTest<DateTime>();
	[Fact]
	internal void CreateSByteTest() => JValueTests.CreateTest<SByte>();
	[Fact]
	internal void CreateUInt16Test() => JValueTests.CreateTest<UInt16>();
	[Fact]
	internal void CreateUInt32Test() => JValueTests.CreateTest<UInt32>();
	[Fact]
	internal void CreateUInt64Test() => JValueTests.CreateTest<UInt64>();
	[Fact]
	internal void CreateGuidTest() => JValueTests.CreateTest<UInt64>();

	private static void CreateTest<T>() where T : unmanaged
	{
		T value = JValueTests.fixture.Create<T>();
		Int32 sizeOf = NativeUtilities.SizeOf<T>();
		if (sizeOf > JValue.Size)
		{
			Assert.Throws<ArgumentException>(() => JValue.Create(value));
		}
		else
		{
			JValue jValue = JValue.Create(value);
			String textValue = Convert.ToHexString(NativeUtilities.AsBytes(jValue));
			Assert.True(value.AsBytes().SequenceEqual(jValue.AsBytes()[..NativeUtilities.SizeOf<T>()]));
			Assert.Equal($"{JNativeType.JValue.GetTypeName()}: {textValue}", jValue.ToString());
		}
	}
}