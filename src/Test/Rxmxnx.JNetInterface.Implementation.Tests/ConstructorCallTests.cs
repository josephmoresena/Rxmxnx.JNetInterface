namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ConstructorCallTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(CallType.Parameterless)]
	[InlineData(CallType.SingleObject)]
	[InlineData(CallType.SingleValue)]
	[InlineData(CallType.Values)]
	[InlineData(CallType.Objects)]
	[InlineData(CallType.Mixed)]
	internal void Test(CallType callType)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JArgumentMetadata[] argsMetadata = callType.GetArgumentsMetadata();
			JConstructorDefinition def = JConstructorDefinition.Create(argsMetadata);
			IObject[] args = callType.GetArgumentsValues(proxyEnv);
			JMethodId methodId = ConstructorCallTests.fixture.Create<JMethodId>();
			JDataTypeMetadata metadata = IDataType.GetMetadata<JThrowableObject>();
			using IFixedPointer.IDisposable infoDef = def.Information.GetFixedPointer();
			using IFixedPointer.IDisposable ctxClass = metadata.Information.GetFixedPointer();
			ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)infoDef.Pointer;
			JThrowableLocalRef result = ConstructorCallTests.fixture.Create<JThrowableLocalRef>();
			JClassLocalRef classRef = JClassLocalRef.FromReference(proxyEnv.VirtualMachine.ThrowableGlobalRef);
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			proxyEnv.GetMethodId(classRef, namePtr, Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
			proxyEnv.NewObject(classRef, methodId, Arg.Is(TestUtilities.GetArgsPtr(args))).Returns(result.Value);

			using JThrowableObject jThrowable =
				JConstructorDefinition.New<JThrowableObject>(def, env.ClassFeature.ThrowableObject, args);

			Assert.Equal(result.Value, jThrowable.Reference.Value);

			proxyEnv.Received(1).GetMethodId(classRef, namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetStaticMethodId(classRef, namePtr, Arg.Any<ReadOnlyValPtr<Byte>>());

			proxyEnv.Received(1).NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());

			foreach (IObject obj in args)
				(obj as IDisposable)?.Dispose();
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
}