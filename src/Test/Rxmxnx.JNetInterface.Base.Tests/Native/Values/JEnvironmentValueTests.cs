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
		Span<Byte> bytes = stackalloc Byte[NativeUtilities.SizeOf<JNativeInterface>() + 2 * IntPtr.Size];
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
		for (Int32 i = 0; i < size; i++)
			Assert.True(Unsafe.AreSame(
				            in bytes[NativeUtilities.SizeOf<JNativeInterface>()..].AsValues<Byte, IntPtr>()[i],
				            in additionalFunctions[i]));
	}
}