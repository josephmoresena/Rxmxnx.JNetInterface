namespace Rxmxnx.JNetInterface.Tests;

public partial class JNativeCallAdapterTests
{
	[Fact]
	internal void StaticNoClassParameterlessTestsClass()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JStringObject>();
		using IFixedPointer.IDisposable ctx =
			classTypeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		try
		{
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.GetStringUtfLength(strRef).Returns(classTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			Exception ex = Assert.Throws<ArgumentException>(() => JNativeCallAdapter
			                                                      .Create(proxyEnv.Reference,
			                                                              JClassLocalRef.FromReference(in localRef),
			                                                              out _).Build().FinalizeCall());
			Assert.Equal(
				IMessageResource.GetInstance()
				                .NotTypeObject(classTypeMetadata.ClassName,
				                               IClassType.GetMetadata<JClassObject>().ClassName), ex.Message);

			proxyEnv.Received(1).GetObjectRefType(localRef);
			proxyEnv.Received(1).GetObjectClass(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(1).GetStringUtfLength(strRef);
			proxyEnv.Received(1).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(0).GetSuperclass(Arg.Any<JClassLocalRef>());
			proxyEnv.Received(0).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
		}
		finally
		{
			nameCtx.Dispose();
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			proxyEnv.FinalizeProxy(true);
		}
	}
}