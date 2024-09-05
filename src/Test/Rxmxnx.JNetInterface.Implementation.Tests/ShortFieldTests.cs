namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ShortFieldTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void InstanceTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, ShortFieldTests.fixture.Create<String>());
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(ShortFieldTests.fixture.Create<JClassLocalRef>());
			ShortFieldTests.Test(proxyEnv, jLocal);
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
	[Fact]
	internal void ExtensionTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateThrowable(proxyEnv);
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(ShortFieldTests.fixture.Create<JClassLocalRef>());
			ShortFieldTests.Test(proxyEnv, jLocal, JClassObject.GetClass<JThrowableObject>(env));
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
	[Fact]
	internal void StaticTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(ShortFieldTests.fixture.Create<JClassLocalRef>());
			ShortFieldTests.Test(proxyEnv, default, JClassObject.GetClass<JStackTraceElementObject>(env));
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
		JFieldDefinition<JShort> def = new((CString)ShortFieldTests.fixture.Create<String>());
		JFieldId fieldId = ShortFieldTests.fixture.Create<JFieldId>();
		using IFixedPointer.IDisposable infoDef = def.Information.GetFixedPointer();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)infoDef.Pointer;
		JObjectLocalRef localRef = jLocal?.Reference ?? default;
		Boolean isStatic = jClass is not null && jLocal is null;
		JShort result = ShortFieldTests.fixture.Create<Int16>();
		JShort setValue = ShortFieldTests.fixture.Create<Int16>();

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		proxyEnv.GetFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(fieldId);
		proxyEnv.GetStaticFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(fieldId);

		proxyEnv.GetShortField(localRef, fieldId).Returns(result);
		proxyEnv.GetStaticShortField(Arg.Any<JClassLocalRef>(), fieldId).Returns(result);

		Assert.Equal(result, !isStatic ? def.Get(jLocal!, jClass) : def.StaticGet(jClass!));

		proxyEnv.Received(!isStatic ? 1 : 0)
		        .GetFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());
		proxyEnv.Received(isStatic ? 1 : 0)
		        .GetStaticFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());

		proxyEnv.Received(!isStatic ? 1 : 0).GetShortField(localRef, fieldId);
		proxyEnv.Received(isStatic ? 1 : 0).GetStaticShortField(Arg.Any<JClassLocalRef>(), fieldId);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		if (!isStatic)
			def.Set(jLocal!, setValue, jClass);
		else
			def.StaticSet(jClass!, setValue);

		proxyEnv.Received(!isStatic ? 1 : 0).SetShortField(localRef, fieldId, setValue);
		proxyEnv.Received(isStatic ? 1 : 0).SetStaticShortField(Arg.Any<JClassLocalRef>(), fieldId, setValue);

		jClass?.Dispose();
		jLocal?.Class.Dispose();
		jLocal?.Dispose();
	}
}