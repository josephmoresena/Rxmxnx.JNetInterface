namespace Rxmxnx.JNetInterface.Tests;

public partial class JNativeCallAdapterTests
{
	[Theory]
	[InlineData((Int32)JRuntimeVersion.SEd2)]
	[InlineData((Int32)JRuntimeVersion.SEd3)]
	[InlineData((Int32)JRuntimeVersion.SEd4)]
	[InlineData((Int32)JRuntimeVersion.J5)]
	[InlineData((Int32)JRuntimeVersion.J6)]
	[InlineData((Int32)JRuntimeVersion.J9)]
	[InlineData((Int32)JRuntimeVersion.J19)]
	[InlineData((Int32)JRuntimeVersion.J20)]
	[InlineData((Int32)JRuntimeVersion.J21)]
	[InlineData((Int32)JRuntimeVersion.J24)]
	internal void InstanceNoLocalParameterlessCall(Int32 version)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JObjectLocalRef localRef = JNativeCallAdapterTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JNativeCallAdapterTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef strRef = JNativeCallAdapterTests.fixture.Create<JStringLocalRef>();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JStringObject>();
		Boolean throwsException = version >= IVirtualMachine.MinimalVersion;
		using IFixedPointer.IDisposable ctx =
			classTypeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		try
		{
			proxyEnv.GetVersion().Returns(version);
			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.InvalidRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.GetStringUtfLength(strRef).Returns(classTypeMetadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			if (throwsException)
			{
				Exception ex =
					Assert.Throws<ArgumentException>(() => JNativeCallAdapter
					                                       .Create(proxyEnv.Reference, localRef, out _).Build()
					                                       .FinalizeCall());
				Assert.Equal(IMessageResource.GetInstance().OnlyLocalReferencesAllowed, ex.Message);
			}
			else
			{
				JNativeCallAdapter.Create(proxyEnv.Reference, localRef, out _).Build().FinalizeCall();
			}

			proxyEnv.Received(throwsException ? 1 : 0).GetObjectRefType(localRef);
			proxyEnv.Received(!throwsException ? 1 : 0).GetObjectClass(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(!throwsException ? 1 : 0).GetStringUtfLength(strRef);
			proxyEnv.Received(!throwsException ? 1 : 0).GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(!throwsException ? 1 : 0).CallObjectMethod(classRef.Value,
			                                                             proxyEnv.VirtualMachine.ClassGetNameMethodId,
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