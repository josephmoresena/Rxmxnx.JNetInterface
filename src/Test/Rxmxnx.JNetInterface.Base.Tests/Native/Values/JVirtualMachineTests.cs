namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JVirtualMachineTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void Test()
	{
		Int32 binarySize = NativeUtilities.SizeOf<JInvokeInterface>();
		Span<Byte> bytes1 = stackalloc Byte[binarySize];
		Span<Byte> bytes2 = stackalloc Byte[binarySize];

		JVirtualMachineValue val1 = JVirtualMachineTests.InvokeInterfaceTest(bytes1);
		JVirtualMachineValue val2 = JVirtualMachineTests.InvokeInterfaceTest(bytes2);

		JVirtualMachineTests.ValueTest(bytes1, bytes2, val1, val2);
		JVirtualMachineTests.ReferenceTest(ref val1, ref val2, ref val2);
	}

	private static void ReferenceTest(ref JVirtualMachineValue refVal1, ref JVirtualMachineValue refVal2,
		ref JVirtualMachineValue refVal3)
	{
		JVirtualMachineRef ref1 = JVirtualMachineTests.GetReference(in refVal1);
		JVirtualMachineRef ref2 = JVirtualMachineTests.GetReference(in refVal2);
		JVirtualMachineRef ref3 = JVirtualMachineTests.GetReference(in refVal2);

		Assert.True(Unsafe.AreSame(in refVal1, in ref1.Reference));
		Assert.True(Unsafe.AreSame(in refVal2, in ref2.Reference));
		Assert.True(Unsafe.AreSame(in refVal3, in ref3.Reference));

		Assert.True(Unsafe.AreSame(in refVal1, in (ref1 as IReadOnlyReferenceable<JVirtualMachineValue>).Reference));
		Assert.True(Unsafe.AreSame(in refVal2, in (ref2 as IReadOnlyReferenceable<JVirtualMachineValue>).Reference));
		Assert.True(Unsafe.AreSame(in refVal3, in (ref3 as IReadOnlyReferenceable<JVirtualMachineValue>).Reference));

		Assert.Equal(NativeUtilities.GetUnsafeIntPtr(in refVal1), ref1.Pointer);
		Assert.Equal(ref2.Pointer.GetHashCode(), ref2.GetHashCode());

		Assert.NotEqual(ref1, ref2);
		Assert.Equal(ref2, ref3);

		Assert.False(ref1 == ref3);
		Assert.True(ref2 == ref3);
		Assert.True(ref1 != ref3);
		Assert.False(ref2 != ref3);

		Assert.True(ref1.Equals((Object)JVirtualMachineTests.GetReference(in refVal1)));
		Assert.False(ref2.Equals((Object)JVirtualMachineTests.GetReference(in refVal1)));

		Assert.Equal(IntPtr.Zero, new JVirtualMachineRef().Pointer);
	}
	private static void ValueTest(Span<Byte> bytes1, Span<Byte> bytes2, JVirtualMachineValue val1,
		JVirtualMachineValue val2)
	{
		Span<Byte> bytes3 = stackalloc Byte[bytes1.Length];
		JVirtualMachineValue val3 = JVirtualMachineTests.GetValue(bytes3);

		bytes2.CopyTo(bytes3);
		Assert.NotEqual(val1, val2);
		Assert.Equal(val1, JVirtualMachineTests.GetValue(bytes1));
		Assert.Equal(val2, JVirtualMachineTests.GetValue(bytes2));

		//TODO: Check equality
		//Assert.Equal(bytes1.SequenceEqual(bytes2), val1.Reference.Equals(val2.Reference));
		//Assert.Equal(val2.Reference, val3.Reference);
		Assert.NotEqual(val2, val3);

		Assert.Equal(val1.GetHashCode(), JVirtualMachineTests.GetValue(bytes1).Pointer.GetHashCode());
		Assert.NotEqual(val2.GetHashCode(), val3.Pointer.GetHashCode());

		Assert.False(val1 == val2);
		Assert.True(val2 == JVirtualMachineTests.GetValue(bytes2));
		Assert.True(val1 != val2);
		Assert.False(val2 != JVirtualMachineTests.GetValue(bytes2));

		Assert.True(val1.Equals((Object)JVirtualMachineTests.GetValue(bytes1)));
		Assert.False(val2.Equals((Object)val3));

		Assert.Equal(IntPtr.Zero, new JVirtualMachineValue().Pointer);
	}
	private static JVirtualMachineValue InvokeInterfaceTest(Span<Byte> bytes)
	{
		JVirtualMachineTests.fixture.CreateMany<Byte>(bytes.Length).ToArray().CopyTo(bytes);
		ref JInvokeInterface jii = ref MemoryMarshal.Cast<Byte, JInvokeInterface>(bytes)[0];
		IntPtr ptr = jii.GetUnsafeIntPtr();
		JVirtualMachineValue value = NativeUtilities.Transform<IntPtr, JVirtualMachineValue>(in ptr);
		ReadOnlySpan<IntPtr> functionsPointer = bytes.AsValues<Byte, IntPtr>()[3..];
		Assert.Equal(jii.DestroyJavaVmPointer, functionsPointer[0]);
		Assert.Equal(jii.AttachCurrentThreadPointer, functionsPointer[1]);
		Assert.Equal(jii.DetachCurrentThreadPointer, functionsPointer[2]);
		Assert.Equal(jii.GetEnvPointer, functionsPointer[3]);
		Assert.Equal(jii.AttachCurrentThreadAsDaemonPointer, functionsPointer[4]);
		return value;
	}
	private static JVirtualMachineValue GetValue(Span<Byte> bytes)
	{
		ref JInvokeInterface jni = ref MemoryMarshal.Cast<Byte, JInvokeInterface>(bytes)[0];
		IntPtr ptr = jni.GetUnsafeIntPtr();
		return NativeUtilities.Transform<IntPtr, JVirtualMachineValue>(in ptr);
	}
	private static JVirtualMachineRef GetReference(ref readonly JVirtualMachineValue value)
	{
		IntPtr ptr = NativeUtilities.GetUnsafeIntPtr(in value);
		return NativeUtilities.Transform<IntPtr, JVirtualMachineRef>(ptr);
	}
}