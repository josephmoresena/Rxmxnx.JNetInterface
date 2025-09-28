namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JEnvironmentTests
{
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
	internal unsafe void Test(Int32 jniVersion)
	{
		Span<IntPtr> interface1 = stackalloc IntPtr[5];
		Span<IntPtr> interface2 = stackalloc IntPtr[5];

		IntPtr ptr1 = new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(interface1)));
		IntPtr ptr2 = new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(interface2)));

		GetVersionDelegate getVersion = GetVersion;
		IntPtr getVersionPtr = Marshal.GetFunctionPointerForDelegate(getVersion);

		interface1[4] = getVersionPtr;
		interface2[4] = getVersionPtr;

		Int32 version = JEnvironmentTests.ReferenceTest(ref ptr1, ref ptr2, ref ptr2);
		Assert.Equal(jniVersion, version);
		return;
		Int32 GetVersion() => jniVersion;
	}

	private static unsafe Int32 ReferenceTest(ref IntPtr refVal1, ref IntPtr refVal2, ref IntPtr refVal3)
	{
		JEnvironmentRef ref1 = JEnvironmentTests.GetReference(in refVal1);
		JEnvironmentRef ref2 = JEnvironmentTests.GetReference(in refVal2);
		JEnvironmentRef ref3 = JEnvironmentTests.GetReference(in refVal2);

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

		Assert.True(ref1.Equals((Object)JEnvironmentTests.GetReference(in refVal1)));
		Assert.False(ref2.Equals((Object)JEnvironmentTests.GetReference(in refVal1)));

		Assert.Equal(IntPtr.Zero, new JEnvironmentRef().Pointer);

		IntPtr getVersionPtr1 = Unsafe.Add(ref Unsafe.AsRef<IntPtr>(ref1.InterfacePointer), 4);
		IntPtr getVersionPtr2 = Unsafe.Add(ref Unsafe.AsRef<IntPtr>(ref2.InterfacePointer), 4);

		Assert.Equal(getVersionPtr1, getVersionPtr2);

		return Marshal.GetDelegateForFunctionPointer<GetVersionDelegate>(getVersionPtr1)();
	}
	private static JEnvironmentRef GetReference(ref readonly IntPtr value)
	{
		IntPtr ptr = NativeUtilities.GetUnsafeIntPtr(in value);
		return NativeUtilities.Transform<IntPtr, JEnvironmentRef>(ptr);
	}
	private delegate Int32 GetVersionDelegate();
}