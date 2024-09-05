namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class CharCallTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(CallType.Parameterless)]
	[InlineData(CallType.SingleObject)]
	[InlineData(CallType.SingleValue)]
	[InlineData(CallType.Values)]
	[InlineData(CallType.Objects)]
	[InlineData(CallType.Mixed)]
	internal void InstanceTest(CallType callType)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, CharCallTests.fixture.Create<String>());
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(CharCallTests.fixture.Create<JClassLocalRef>());
			CharCallTests.Test(callType, proxyEnv, jLocal);
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
	[Theory]
	[InlineData(CallType.Parameterless)]
	[InlineData(CallType.SingleObject)]
	[InlineData(CallType.SingleValue)]
	[InlineData(CallType.Values)]
	[InlineData(CallType.Objects)]
	[InlineData(CallType.Mixed)]
	internal void NonVirtualTest(CallType callType)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateThrowable(proxyEnv);
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(CharCallTests.fixture.Create<JClassLocalRef>());
			CharCallTests.Test(callType, proxyEnv, jLocal, JClassObject.GetClass<JThrowableObject>(env));
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
	[Theory]
	[InlineData(CallType.Parameterless)]
	[InlineData(CallType.SingleObject)]
	[InlineData(CallType.SingleValue)]
	[InlineData(CallType.Values)]
	[InlineData(CallType.Objects)]
	[InlineData(CallType.Mixed)]
	internal void StaticTest(CallType callType)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(CharCallTests.fixture.Create<JClassLocalRef>());
			CharCallTests.Test(callType, proxyEnv, default, JClassObject.GetClass<JStackTraceElementObject>(env));
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

	private static void Test(CallType callType, NativeInterfaceProxy proxyEnv, JLocalObject? jLocal = default,
		JClassObject? jClass = default)
	{
		JArgumentMetadata[] argsMetadata = callType.GetArgumentsMetadata();
		JFunctionDefinition<JChar> def =
			JFunctionDefinition<JChar>.Create((CString)CharCallTests.fixture.Create<String>(), argsMetadata);
		IObject[] args = callType.GetArgumentsValues(proxyEnv);
		JMethodId methodId = CharCallTests.fixture.Create<JMethodId>();
		using IFixedPointer.IDisposable infoDef = def.Information.GetFixedPointer();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)infoDef.Pointer;
		JObjectLocalRef localRef = jLocal?.Reference ?? default;
		Boolean isNonVirtual = jClass is not null && jLocal is not null;
		Boolean isStatic = jClass is not null && jLocal is null;
		Boolean isInstance = !isNonVirtual && !isStatic;
		JChar result = CharCallTests.fixture.Create<Char>();

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
		proxyEnv.GetStaticMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>())
		        .Returns(methodId);

		proxyEnv.CallCharMethod(localRef, methodId, Arg.Is(TestUtilities.GetArgsPtr(args))).Returns(result);
		proxyEnv.CallNonVirtualCharMethod(localRef, Arg.Any<JClassLocalRef>(), methodId,
		                                  Arg.Is(TestUtilities.GetArgsPtr(args))).Returns(result);
		proxyEnv.CallStaticCharMethod(Arg.Any<JClassLocalRef>(), methodId, Arg.Is(TestUtilities.GetArgsPtr(args)))
		        .Returns(result);

		Assert.Equal(
			result,
			!isStatic ?
				JFunctionDefinition<JChar>.Invoke(def, jLocal!, jClass, isNonVirtual, args) :
				JFunctionDefinition<JChar>.StaticInvoke(def, jClass!, args));

		proxyEnv.Received(!isStatic ? 1 : 0)
		        .GetMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());
		proxyEnv.Received(isStatic ? 1 : 0)
		        .GetStaticMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());

		proxyEnv.Received(isInstance ? 1 : 0)
		        .CallCharMethod(localRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());
		proxyEnv.Received(isNonVirtual ? 1 : 0).CallNonVirtualCharMethod(
			localRef, Arg.Any<JClassLocalRef>(), methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());
		proxyEnv.Received(isStatic ? 1 : 0)
		        .CallStaticCharMethod(Arg.Any<JClassLocalRef>(), methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());

		foreach (IObject obj in args)
			(obj as IDisposable)?.Dispose();

		jClass?.Dispose();
		jLocal?.Class.Dispose();
		jLocal?.Dispose();
	}
}