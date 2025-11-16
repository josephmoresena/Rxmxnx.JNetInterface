namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	[Theory]
	[InlineData(JRuntimeVersion.SEd2)]
	[InlineData(JRuntimeVersion.SEd3)]
	[InlineData(JRuntimeVersion.SEd4)]
	[InlineData(JRuntimeVersion.J5)]
	[InlineData(JRuntimeVersion.J6)]
	[InlineData(JRuntimeVersion.J7)]
	[InlineData(JRuntimeVersion.J8)]
	[InlineData(JRuntimeVersion.J9)]
	[InlineData(JRuntimeVersion.J10)]
	[InlineData(JRuntimeVersion.J11)]
	[InlineData(JRuntimeVersion.J12)]
	[InlineData(JRuntimeVersion.J13)]
	[InlineData(JRuntimeVersion.J14)]
	[InlineData(JRuntimeVersion.J15)]
	[InlineData(JRuntimeVersion.J16)]
	[InlineData(JRuntimeVersion.J17)]
	[InlineData(JRuntimeVersion.J18)]
	[InlineData(JRuntimeVersion.J19)]
	[InlineData(JRuntimeVersion.J20)]
	[InlineData(JRuntimeVersion.J21)]
	[InlineData(JRuntimeVersion.J22)]
	[InlineData(JRuntimeVersion.J23)]
	[InlineData(JRuntimeVersion.J24)]
	[InlineData(JRuntimeVersion.J25)]
	[InlineData(JRuntimeVersion.SEd2, true)]
	[InlineData(JRuntimeVersion.SEd3, true)]
	[InlineData(JRuntimeVersion.SEd4, true)]
	[InlineData(JRuntimeVersion.J5, true)]
	[InlineData(JRuntimeVersion.J6, true)]
	[InlineData(JRuntimeVersion.J7, true)]
	[InlineData(JRuntimeVersion.J8, true)]
	[InlineData(JRuntimeVersion.J9, true)]
	[InlineData(JRuntimeVersion.J10, true)]
	[InlineData(JRuntimeVersion.J11, true)]
	[InlineData(JRuntimeVersion.J12, true)]
	[InlineData(JRuntimeVersion.J13, true)]
	[InlineData(JRuntimeVersion.J14, true)]
	[InlineData(JRuntimeVersion.J15, true)]
	[InlineData(JRuntimeVersion.J16, true)]
	[InlineData(JRuntimeVersion.J17, true)]
	[InlineData(JRuntimeVersion.J18, true)]
	[InlineData(JRuntimeVersion.J19, true)]
	[InlineData(JRuntimeVersion.J20, true)]
	[InlineData(JRuntimeVersion.J21, true)]
	[InlineData(JRuntimeVersion.J22, true)]
	[InlineData(JRuntimeVersion.J23, true)]
	[InlineData(JRuntimeVersion.J24, true)]
	[InlineData(JRuntimeVersion.J25, true)]
	internal void GetVersionTest(JRuntimeVersion version, Boolean invalidResult = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		Decimal jreVersion = version switch
		{
			JRuntimeVersion.SEd2 => 1.2m,
			JRuntimeVersion.SEd3 => 1.3m,
			JRuntimeVersion.SEd4 => 1.4m,
			JRuntimeVersion.J5 => 1.5m,
			JRuntimeVersion.J6 => 1.6m,
			JRuntimeVersion.J7 => 1.7m,
			JRuntimeVersion.J8 => 1.8m,
			_ => (Decimal)version / (Int32)JRuntimeVersion.SEd0,
		};
		JStringLocalRef stringRef = JVirtualMachineTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef stringRef1 = !invalidResult || Random.Shared.Next(0, 10) < 8 ?
			JVirtualMachineTests.fixture.Create<JStringLocalRef>() :
			default;
		String result = !invalidResult ?
			jreVersion.ToString(CultureInfo.InvariantCulture) :
			JVirtualMachineTests.fixture.Create<String>();
		using MemoryHandle _ = NativeFunctionSetImpl.GetPropertyDefinition.Hash.AsMemory().Pin();
		JClassLocalRef systemClassRef = new(proxyEnv.VirtualMachine.SystemGlobalRef.Value);
		CString resultUtf8 = (CString)result;

		proxyEnv.GetStringLength(stringRef1).Returns(result.Length);
		proxyEnv.When(e => e.GetStringRegion(stringRef1, 0, result.Length, Arg.Any<ValPtr<Char>>())).Do(c =>
		{
			Span<Char> chars = ((ValPtr<Char>)c[3]).Pointer.GetUnsafeSpan<Char>(result.Length);
			result.CopyTo(chars);
		});
		proxyEnv.GetStringUtfLength(stringRef1).Returns(resultUtf8.Length);
		proxyEnv.When(e => e.GetStringUtfRegion(stringRef1, 0, resultUtf8.Length, Arg.Any<ValPtr<Byte>>())).Do(c =>
		{
			Span<Byte> utf8Chars = ((ValPtr<Byte>)c[3]).Pointer.GetUnsafeSpan<Byte>(resultUtf8.Length);
			resultUtf8.AsSpan().CopyTo(utf8Chars);
		});
		proxyEnv.CallStaticObjectMethod(systemClassRef, proxyEnv.VirtualMachine.SystemGetPropertyMethodId,
		                                Arg.Is<ReadOnlyValPtr<JValueWrapper>>(a => Marshal.ReadIntPtr(a.Pointer) ==
			                                                                      stringRef.Pointer))
		        .Returns(stringRef1.Value);
		proxyEnv.NewStringUtf(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(stringRef);
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Int32 jVersion = (Int32)env.VirtualMachine.Version;
			Assert.Equal(invalidResult ? env.Version : (Int32)version, jVersion);
			Assert.Equal(invalidResult ? (JRuntimeVersion)env.Version : version, env.VirtualMachine.Version);

			proxyEnv.Received().ExceptionCheck();
			proxyEnv.Received(1).PushLocalFrame(IVirtualMachine.GetVersionCapacity);
			proxyEnv.Received(1).CallStaticObjectMethod(systemClassRef,
			                                            proxyEnv.VirtualMachine.SystemGetPropertyMethodId,
			                                            Arg.Any<ReadOnlyValPtr<JValueWrapper>>());
			proxyEnv.Received(invalidResult ? 1 : 0).ExceptionClear();
			proxyEnv.Received(1).PopLocalFrame(default);

			if (stringRef1 == default)
			{
				proxyEnv.Received(0).GetStringLength(stringRef1);
				proxyEnv.Received(0).GetStringUtfLength(stringRef1);
				proxyEnv.Received(0).GetStringRegion(stringRef1, 0, result.Length, Arg.Any<ValPtr<Char>>());
				proxyEnv.Received(0).GetStringUtfRegion(stringRef1, 0, resultUtf8.Length, Arg.Any<ValPtr<Byte>>());
			}
			else
			{
#if !NET8_0_OR_GREATER
				const Int32 utf8Count = 0;
				const Int32 count = 1;
#else
				const Int32 utf8Count = 1;
				const Int32 count = 0;
#endif
				proxyEnv.Received(count).GetStringLength(stringRef1);
				proxyEnv.Received(utf8Count).GetStringUtfLength(stringRef1);
				proxyEnv.Received(count).GetStringRegion(stringRef1, 0, result.Length, Arg.Any<ValPtr<Char>>());
				proxyEnv.Received(utf8Count)
				        .GetStringUtfRegion(stringRef1, 0, resultUtf8.Length, Arg.Any<ValPtr<Byte>>());
			}
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			proxyEnv.FinalizeProxy(true);
		}
	}
}