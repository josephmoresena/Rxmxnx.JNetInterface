using System.Runtime.CompilerServices;

namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JEnvironmentValueTests
{
	private static readonly IFixture fixture = new Fixture();

	[Theory]
	[InlineData(0x00010001)]
	[InlineData(0x00010002)]
	[InlineData(0x00010004)]
	[InlineData(0x00010006)]
	[InlineData(0x00010008)]
	[InlineData(0x00090000)]
	[InlineData(0x000a0000)]
	[InlineData(0x00130000)]
	[InlineData(0x00140000)]
	[InlineData(0x00150000)]
	internal void Test(Int32 jniVersion)
	{
		Int32 binarySize = NativeUtilities.SizeOf<JNativeInterface>() + 2 * IntPtr.Size;
		Span<Byte> bytes1 = stackalloc Byte[binarySize];
		Span<Byte> bytes2 = stackalloc Byte[binarySize];
		Span<Byte> bytes3 = stackalloc Byte[binarySize];
		JEnvironmentValue val1 = JEnvironmentValueTests.NativeInterfaceTest(jniVersion, bytes1);
		JEnvironmentValue val2 = JEnvironmentValueTests.NativeInterfaceTest(jniVersion, bytes2);
		JEnvironmentValue val3 = JEnvironmentValueTests.GetValue(bytes3);

		bytes2.CopyTo(bytes3);
		Assert.NotEqual(val1, val2);
		Assert.Equal(val1, JEnvironmentValueTests.GetValue(bytes1));
		Assert.Equal(val2, JEnvironmentValueTests.GetValue(bytes2));

		Assert.Equal(bytes1.SequenceEqual(bytes2), val1.Reference.Equals(val2.Reference));
		Assert.Equal(val2.Reference, val3.Reference);
		Assert.NotEqual(val2, val3);

		Assert.Equal(val1.GetHashCode(), JEnvironmentValueTests.GetValue(bytes1).Pointer.GetHashCode());
		Assert.NotEqual(val2.GetHashCode(), val3.Pointer.GetHashCode());

		Assert.False(val1 == val2);
		Assert.True(val2 == JEnvironmentValueTests.GetValue(bytes2));
		Assert.True(val1 != val2);
		Assert.False(val2 != JEnvironmentValueTests.GetValue(bytes2));

		Assert.True(val1.Equals((Object)JEnvironmentValueTests.GetValue(bytes1)));
		Assert.False(val2.Equals((Object)val3));

		Assert.Equal(IntPtr.Zero, new JEnvironmentValue().Pointer);
	}

	private static JEnvironmentValue NativeInterfaceTest(Int32 jniVersion, Span<Byte> bytes)
	{
		JEnvironmentValueTests.fixture.CreateMany<Byte>(bytes.Length).ToArray().CopyTo(bytes);
		ref JNativeInterface jni = ref MemoryMarshal.Cast<Byte, JNativeInterface>(bytes)[0];
		IntPtr ptr = jni.GetUnsafeIntPtr();
		JEnvironmentValue value = NativeUtilities.Transform<IntPtr, JEnvironmentValue>(in ptr);
		IntPtr[] pointers = new IntPtr[228];
		for (Int32 i = 0; i < pointers.Length; i++)
			pointers[i] = value.Reference[i];
		Assert.True(Unsafe.AreSame(in jni, in value.Reference));
		Assert.Equal(ptr, value.Pointer);
		Assert.Equal(bytes.AsValues<Byte, IntPtr>()[5..233].ToArray(), pointers);

		Int32 size = jniVersion switch
		{
			>= 0x00090000 and < 0x00130000 => 1,
			>= 0x00130000 => 2,
			_ => 0,
		};
		ReadOnlySpan<IntPtr> additionalFunctions = value.GetAdditionalPointers(jniVersion);
		Assert.Equal(size, additionalFunctions.Length);
		Span<IntPtr> additionalFunctionsT =
			bytes[NativeUtilities.SizeOf<JNativeInterface>()..].AsValues<Byte, IntPtr>();
		for (Int32 i = 0; i < size; i++)
			Assert.True(Unsafe.AreSame(in additionalFunctionsT[i], in additionalFunctions[i]));
		return value;
	}
	private static JEnvironmentValue GetValue(Span<Byte> bytes)
	{
		ref JNativeInterface jni = ref MemoryMarshal.Cast<Byte, JNativeInterface>(bytes)[0];
		IntPtr ptr = jni.GetUnsafeIntPtr();
		return NativeUtilities.Transform<IntPtr, JEnvironmentValue>(in ptr);
	}
}