namespace Rxmxnx.JNetInterface.Tests;

public partial class JNativeCallAdapterTests
{
	[Theory]
	[InlineData(0x00010002)]
	[InlineData(0x00010003)]
	[InlineData(0x00010004)]
	[InlineData(0x00010005)]
	[InlineData(0x00010006)]
	[InlineData(0x00090000)]
	[InlineData(0x00130000)]
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
				Exception ex = Assert.Throws<ArgumentException>(
					() => JNativeCallAdapter.Create(proxyEnv.Reference, localRef, out _).Build().FinalizeCall());
				Assert.Equal("JNI call only allow local references.", ex.Message);
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