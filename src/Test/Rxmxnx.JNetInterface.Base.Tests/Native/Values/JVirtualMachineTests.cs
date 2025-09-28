namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JVirtualMachineTests
{
	[Theory]
	[InlineData(JResult.Ok)]
	[InlineData(JResult.DetachedThreadError)]
	[InlineData(JResult.Error)]
	[InlineData(JResult.ExitingError)]
	[InlineData(JResult.InvalidArgumentsError)]
	[InlineData(JResult.MemoryError)]
	[InlineData(JResult.VersionError)]
	internal unsafe void Test(JResult result)
	{
		Span<IntPtr> interface1 = stackalloc IntPtr[4];
		Span<IntPtr> interface2 = stackalloc IntPtr[4];

		IntPtr ptr1 = new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(interface1)));
		IntPtr ptr2 = new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(interface2)));

		DestroyDelegate destroy = Destroy;
		IntPtr destroyPtr = Marshal.GetFunctionPointerForDelegate(destroy);

		interface1[3] = destroyPtr;
		interface2[3] = destroyPtr;

		JResult destroyResult = JVirtualMachineTests.ReferenceTest(ref ptr1, ref ptr2, ref ptr2);
		Assert.Equal(result, destroyResult);
		return;
		JResult Destroy() => result;
	}

	private static unsafe JResult ReferenceTest(ref IntPtr refVal1, ref IntPtr refVal2, ref IntPtr refVal3)
	{
		JVirtualMachineRef ref1 = JVirtualMachineTests.GetReference(in refVal1);
		JVirtualMachineRef ref2 = JVirtualMachineTests.GetReference(in refVal2);
		JVirtualMachineRef ref3 = JVirtualMachineTests.GetReference(in refVal2);

		Assert.True(Unsafe.AreSame(ref refVal1, ref Unsafe.AsRef<IntPtr>(ref1.Pointer.ToPointer())));
		Assert.True(Unsafe.AreSame(ref refVal2, ref Unsafe.AsRef<IntPtr>(ref2.Pointer.ToPointer())));
		Assert.True(Unsafe.AreSame(ref refVal3, ref Unsafe.AsRef<IntPtr>(ref3.Pointer.ToPointer())));

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

		IntPtr destroyPtr1 = Unsafe.Add(ref Unsafe.AsRef<IntPtr>(ref1.InterfacePointer), 3);
		IntPtr destroyPtr2 = Unsafe.Add(ref Unsafe.AsRef<IntPtr>(ref1.InterfacePointer), 3);

		Assert.Equal(destroyPtr1, destroyPtr2);

		return Marshal.GetDelegateForFunctionPointer<DestroyDelegate>(destroyPtr1)();
	}
	private static JVirtualMachineRef GetReference(ref readonly IntPtr value)
	{
		IntPtr ptr = NativeUtilities.GetUnsafeIntPtr(in value);
		return NativeUtilities.Transform<IntPtr, JVirtualMachineRef>(ptr);
	}
	private delegate JResult DestroyDelegate();
}