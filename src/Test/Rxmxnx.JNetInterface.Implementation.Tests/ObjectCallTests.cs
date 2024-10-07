namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ObjectCallTests
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
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, ObjectCallTests.fixture.Create<String>());
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(ObjectCallTests.fixture.Create<JClassLocalRef>());
			ObjectCallTests.Test(callType, proxyEnv, jLocal);
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
			        .Returns(ObjectCallTests.fixture.Create<JClassLocalRef>());
			ObjectCallTests.Test(callType, proxyEnv, jLocal, JClassObject.GetClass<JThrowableObject>(env));
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
			        .Returns(ObjectCallTests.fixture.Create<JClassLocalRef>());
			ObjectCallTests.Test(callType, proxyEnv, default, JClassObject.GetClass<JStackTraceElementObject>(env));
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
		JFunctionDefinition<JThrowableObject> def =
			JFunctionDefinition<JThrowableObject>.Create((CString)ObjectCallTests.fixture.Create<String>(),
			                                             argsMetadata);
		IObject[] args = callType.GetArgumentsValues(proxyEnv);
		JMethodId methodId = ObjectCallTests.fixture.Create<JMethodId>();
		JDataTypeMetadata metadata = IDataType.GetMetadata<JThrowableObject>();
		using IFixedPointer.IDisposable infoDef = def.Information.GetFixedPointer();
		using IFixedPointer.IDisposable ctxClass =
			metadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)infoDef.Pointer;
		JObjectLocalRef localRef = jLocal?.Reference ?? default;
		Boolean isNonVirtual = jClass is not null && jLocal is not null;
		Boolean isStatic = jClass is not null && jLocal is null;
		Boolean isInstance = !isNonVirtual && !isStatic;
		JThrowableLocalRef result = ObjectCallTests.fixture.Create<JThrowableLocalRef>();
		JStringLocalRef strRef = ObjectCallTests.fixture.Create<JStringLocalRef>();

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		proxyEnv.GetObjectClass(result.Value).Returns(proxyEnv.ThrowableLocalRef);
		proxyEnv.CallObjectMethod(proxyEnv.ThrowableLocalRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
		                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
		proxyEnv.GetStringUtfLength(strRef).Returns(metadata.ClassName.Length);
		proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>()).Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);
		proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
		proxyEnv.GetStaticMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>())
		        .Returns(methodId);

		proxyEnv.CallObjectMethod(localRef, methodId, Arg.Is(TestUtilities.GetArgsPtr(args))).Returns(result.Value);
		proxyEnv.CallNonVirtualObjectMethod(localRef, Arg.Any<JClassLocalRef>(), methodId,
		                                    Arg.Is(TestUtilities.GetArgsPtr(args))).Returns(result.Value);
		proxyEnv.CallStaticObjectMethod(Arg.Any<JClassLocalRef>(), methodId, Arg.Is(TestUtilities.GetArgsPtr(args)))
		        .Returns(result.Value);

		using JThrowableObject? jThrowable = !isStatic ?
			JFunctionDefinition<JThrowableObject>.Invoke(def, jLocal!, jClass, isNonVirtual, args) :
			JFunctionDefinition<JThrowableObject>.StaticInvoke(def, jClass!, args);

		Assert.Equal(result.Value, jThrowable?.Reference.Value);

		proxyEnv.Received(!isStatic ? 1 : 0)
		        .GetMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());
		proxyEnv.Received(isStatic ? 1 : 0)
		        .GetStaticMethodId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());

		proxyEnv.Received(isInstance ? 1 : 0)
		        .CallObjectMethod(localRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());
		proxyEnv.Received(isNonVirtual ? 1 : 0).CallNonVirtualObjectMethod(
			localRef, Arg.Any<JClassLocalRef>(), methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());
		proxyEnv.Received(isStatic ? 1 : 0)
		        .CallStaticObjectMethod(Arg.Any<JClassLocalRef>(), methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());

		foreach (IObject obj in args)
			(obj as IDisposable)?.Dispose();

		jClass?.Dispose();
		jLocal?.Class.Dispose();
		jLocal?.Dispose();
		jThrowable?.Class.Dispose();
		nameCtx.Dispose();
	}
}