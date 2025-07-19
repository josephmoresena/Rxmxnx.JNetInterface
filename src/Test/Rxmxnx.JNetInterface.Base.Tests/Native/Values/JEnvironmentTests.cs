namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JEnvironmentTests
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

		JEnvironmentValue val1 = JEnvironmentTests.NativeInterfaceTest(jniVersion, bytes1);
		JEnvironmentValue val2 = JEnvironmentTests.NativeInterfaceTest(jniVersion, bytes2);

		JEnvironmentTests.ValueTest(bytes1, bytes2, val1, val2);
		JEnvironmentTests.ReferenceTest(ref val1, ref val2, ref val2);
	}

	private static void ReferenceTest(ref JEnvironmentValue refVal1, ref JEnvironmentValue refVal2,
		ref JEnvironmentValue refVal3)
	{
		JEnvironmentRef ref1 = JEnvironmentTests.GetReference(in refVal1);
		JEnvironmentRef ref2 = JEnvironmentTests.GetReference(in refVal2);
		JEnvironmentRef ref3 = JEnvironmentTests.GetReference(in refVal2);

		Assert.True(Unsafe.AreSame(in refVal1, in ref1.Reference));
		Assert.True(Unsafe.AreSame(in refVal2, in ref2.Reference));
		Assert.True(Unsafe.AreSame(in refVal3, in ref3.Reference));

		Assert.True(Unsafe.AreSame(in refVal1, in (ref1 as IReadOnlyReferenceable<JEnvironmentValue>).Reference));
		Assert.True(Unsafe.AreSame(in refVal2, in (ref2 as IReadOnlyReferenceable<JEnvironmentValue>).Reference));
		Assert.True(Unsafe.AreSame(in refVal3, in (ref3 as IReadOnlyReferenceable<JEnvironmentValue>).Reference));

		Assert.Equal(NativeUtilities.GetUnsafeIntPtr(in refVal1), ref1.Pointer);
		Assert.Equal(ref2.Pointer.GetHashCode(), ref2.GetHashCode());

		Assert.NotEqual(ref1, ref2);
		Assert.Equal(ref2, ref3);

		Assert.False(ref1 == ref3);
		Assert.True(ref2 == ref3);
		Assert.True(ref1 != ref3);
		Assert.False(ref2 != ref3);

		Assert.True(ref1.Equals((Object)JEnvironmentTests.GetReference(in refVal1)));
		Assert.False(ref2.Equals((Object)JEnvironmentTests.GetReference(in refVal1)));

		Assert.Equal(IntPtr.Zero, new JEnvironmentRef().Pointer);
	}
	private static void ValueTest(Span<Byte> bytes1, Span<Byte> bytes2, JEnvironmentValue val1, JEnvironmentValue val2)
	{
		Span<Byte> bytes3 = stackalloc Byte[bytes1.Length];
		JEnvironmentValue val3 = JEnvironmentTests.GetValue(bytes3);

		bytes2.CopyTo(bytes3);
		Assert.NotEqual(val1, val2);
		Assert.Equal(val1, JEnvironmentTests.GetValue(bytes1));
		Assert.Equal(val2, JEnvironmentTests.GetValue(bytes2));

		Assert.NotEqual(val2, val3);

		Assert.Equal(val1.GetHashCode(), JEnvironmentTests.GetValue(bytes1).Pointer.GetHashCode());
		Assert.NotEqual(val2.GetHashCode(), val3.Pointer.GetHashCode());

		Assert.False(val1 == val2);
		Assert.True(val2 == JEnvironmentTests.GetValue(bytes2));
		Assert.True(val1 != val2);
		Assert.False(val2 != JEnvironmentTests.GetValue(bytes2));

		Assert.True(val1.Equals((Object)JEnvironmentTests.GetValue(bytes1)));
		Assert.False(val2.Equals((Object)val3));

		Assert.Equal(IntPtr.Zero, new JEnvironmentValue().Pointer);
	}
	private static JEnvironmentValue NativeInterfaceTest(Int32 jniVersion, Span<Byte> bytes)
	{
		JEnvironmentTests.fixture.CreateMany<Byte>(bytes.Length).ToArray().CopyTo(bytes);
		ref JNativeInterface jni = ref MemoryMarshal.Cast<Byte, JNativeInterface>(bytes)[0];
		IntPtr ptr = jni.GetUnsafeIntPtr();
		JEnvironmentValue value = NativeUtilities.Transform<IntPtr, JEnvironmentValue>(in ptr);
		IntPtr[] pointers = new IntPtr[228];
		for (Int32 i = 0; i < pointers.Length; i++)
			pointers[i] = value.Reference[i];
		Assert.True(Unsafe.AreSame(in jni, in value.Reference));
		Assert.Equal(ptr, value.Pointer);
		Assert.Equal(bytes.AsValues<Byte, IntPtr>()[4], value.Reference.GetVersionPointer);
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
	private static JEnvironmentRef GetReference(ref readonly JEnvironmentValue value)
	{
		IntPtr ptr = NativeUtilities.GetUnsafeIntPtr(in value);
		return NativeUtilities.Transform<IntPtr, JEnvironmentRef>(ptr);
	}
}