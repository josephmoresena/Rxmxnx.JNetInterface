namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class FloatFieldTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[SkippableFact]
	internal void InstanceTest()
	{
		Skip.If(OperatingSystem.IsWindows(),
		        "Error handling floating-point numbers due to calling convention on Windows.");
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, FloatFieldTests.fixture.Create<String>());
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(FloatFieldTests.fixture.Create<JClassLocalRef>());
			FloatFieldTests.Test(proxyEnv, jLocal);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[SkippableFact]
	internal void ExtensionTest()
	{
		Skip.If(OperatingSystem.IsWindows(),
		        "Error handling floating-point numbers due to calling convention on Windows.");
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateThrowable(proxyEnv);
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(FloatFieldTests.fixture.Create<JClassLocalRef>());
			FloatFieldTests.Test(proxyEnv, jLocal, JClassObject.GetClass<JThrowableObject>(env));
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[SkippableFact]
	internal void StaticTest()
	{
		Skip.If(OperatingSystem.IsWindows(),
		        "Error handling floating-point numbers due to calling convention on Windows.");
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(FloatFieldTests.fixture.Create<JClassLocalRef>());
			FloatFieldTests.Test(proxyEnv, default, JClassObject.GetClass<JStackTraceElementObject>(env));
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}

	private static void Test(NativeInterfaceProxy proxyEnv, JLocalObject? jLocal = default,
		JClassObject? jClass = default)
	{
		JFieldDefinition<JFloat> def = new((CString)FloatFieldTests.fixture.Create<String>());
		JFieldId fieldId = FloatFieldTests.fixture.Create<JFieldId>();
		using IFixedPointer.IDisposable infoDef = def.Information.GetFixedPointer();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)infoDef.Pointer;
		JObjectLocalRef localRef = jLocal?.Reference ?? default;
		Boolean isStatic = jClass is not null && jLocal is null;
		JFloat result = FloatFieldTests.fixture.Create<Single>();
		JFloat setValue = FloatFieldTests.fixture.Create<Single>();

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		proxyEnv.GetFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(fieldId);
		proxyEnv.GetStaticFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(fieldId);

		proxyEnv.GetFloatField(localRef, fieldId).Returns(result);
		proxyEnv.GetStaticFloatField(Arg.Any<JClassLocalRef>(), fieldId).Returns(result);

		Assert.Equal(result, !isStatic ? def.Get(jLocal!, jClass) : def.StaticGet(jClass!));

		proxyEnv.Received(!isStatic ? 1 : 0)
		        .GetFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());
		proxyEnv.Received(isStatic ? 1 : 0)
		        .GetStaticFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());

		proxyEnv.Received(!isStatic ? 1 : 0).GetFloatField(localRef, fieldId);
		proxyEnv.Received(isStatic ? 1 : 0).GetStaticFloatField(Arg.Any<JClassLocalRef>(), fieldId);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		if (!isStatic)
			def.Set(jLocal!, setValue, jClass);
		else
			def.StaticSet(jClass!, setValue);

		proxyEnv.Received(!isStatic ? 1 : 0).SetFloatField(localRef, fieldId, setValue);
		proxyEnv.Received(isStatic ? 1 : 0).SetStaticFloatField(Arg.Any<JClassLocalRef>(), fieldId, setValue);

		jClass?.Dispose();
		jLocal?.Class.Dispose();
		jLocal?.Dispose();
	}
}