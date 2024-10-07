namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ObjectFieldTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void InstanceTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, ObjectFieldTests.fixture.Create<String>());
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(ObjectFieldTests.fixture.Create<JClassLocalRef>());
			ObjectFieldTests.Test(proxyEnv, jLocal);
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
			        .Returns(ObjectFieldTests.fixture.Create<JClassLocalRef>());
			ObjectFieldTests.Test(proxyEnv, jLocal, JClassObject.GetClass<JThrowableObject>(env));
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
			        .Returns(ObjectFieldTests.fixture.Create<JClassLocalRef>());
			ObjectFieldTests.Test(proxyEnv, default, JClassObject.GetClass<JStackTraceElementObject>(env));
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
		JFieldDefinition<JThrowableObject> def = new((CString)ObjectFieldTests.fixture.Create<String>());
		JFieldId fieldId = ObjectFieldTests.fixture.Create<JFieldId>();
		JDataTypeMetadata metadata = IDataType.GetMetadata<JThrowableObject>();
		using IFixedPointer.IDisposable infoDef = def.Information.GetFixedPointer();
		using IFixedPointer.IDisposable ctxClass = metadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)infoDef.Pointer;
		JObjectLocalRef localRef = jLocal?.Reference ?? default;
		Boolean isStatic = jClass is not null && jLocal is null;
		JThrowableLocalRef result = ObjectFieldTests.fixture.Create<JThrowableLocalRef>();
		JStringLocalRef strRef = ObjectFieldTests.fixture.Create<JStringLocalRef>();
		using JThrowableObject setObject = TestUtilities.CreateThrowable(proxyEnv);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		proxyEnv.GetObjectClass(result.Value).Returns(proxyEnv.ThrowableLocalRef);
		proxyEnv.CallObjectMethod(proxyEnv.ThrowableLocalRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
		                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
		proxyEnv.GetStringUtfLength(strRef).Returns(metadata.ClassName.Length);
		proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>()).Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);
		proxyEnv.GetFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(fieldId);
		proxyEnv.GetStaticFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(fieldId);

		proxyEnv.GetObjectField(localRef, fieldId).Returns(result.Value);
		proxyEnv.GetStaticObjectField(Arg.Any<JClassLocalRef>(), fieldId).Returns(result.Value);

		using JThrowableObject? jThrowable = !isStatic ? def.Get(jLocal!, jClass) : def.StaticGet(jClass!);

		Assert.Equal(result.Value, jThrowable?.Reference.Value);

		proxyEnv.Received(!isStatic ? 1 : 0)
		        .GetFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());
		proxyEnv.Received(isStatic ? 1 : 0)
		        .GetStaticFieldId(Arg.Any<JClassLocalRef>(), namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());

		proxyEnv.Received(!isStatic ? 1 : 0).GetObjectField(localRef, fieldId);
		proxyEnv.Received(isStatic ? 1 : 0).GetStaticObjectField(Arg.Any<JClassLocalRef>(), fieldId);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		if (!isStatic)
			def.Set(jLocal!, setObject, jClass);
		else
			def.StaticSet(jClass!, setObject);

		proxyEnv.Received(!isStatic ? 1 : 0).SetObjectField(localRef, fieldId, setObject.Reference.Value);
		proxyEnv.Received(isStatic ? 1 : 0)
		        .SetStaticObjectField(Arg.Any<JClassLocalRef>(), fieldId, setObject.Reference.Value);

		jClass?.Dispose();
		jLocal?.Class.Dispose();
		jLocal?.Dispose();
		jThrowable?.Class.Dispose();
		setObject.Class.Dispose();
		nameCtx.Dispose();
	}
}