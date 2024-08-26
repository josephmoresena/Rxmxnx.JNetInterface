namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class NioTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void AllocTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		HashSet<JObjectLocalRef> bufferRefs = [];
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			INioFeature feature = env.NioFeature;

			Assert.Equal(128, env.UsableStackBytes);
			Assert.Throws<ArgumentOutOfRangeException>(() => env.UsableStackBytes = 0);

			Assert.Equal(128, env.UsableStackBytes);
			Assert.Equal(0, env.UsedStackBytes);

			env.UsableStackBytes = 512;
			Assert.Equal(512, env.UsableStackBytes);
			Assert.Equal(0, env.UsedStackBytes);

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			proxyEnv.NewDirectByteBuffer(Arg.Any<IntPtr>(), Arg.Any<Int64>()).Returns(c =>
			{
				JObjectLocalRef localRef = NioTests.fixture.Create<JObjectLocalRef>();
				bufferRefs.Add(localRef);
				return localRef;
			});

			feature.WithDirectByteBuffer<JBufferObject>(128, b0 =>
			{
				Assert.Equal(128, b0.Capacity);
				Assert.Equal(b0.Capacity, b0.Environment.UsedStackBytes);
				Assert.Equal(b0.Capacity + 128,
				             feature.WithDirectByteBuffer<JBufferObject, Int64>(
					             128, b1 => b1.Environment.UsedStackBytes));

				feature.WithDirectByteBuffer<JBufferObject, Int64>(128, b0.Capacity, (b1, c0) =>
				{
					Assert.Equal(c0 + b1.Capacity, env.UsedStackBytes);
					Assert.Equal(c0 + b1.Capacity, feature.WithDirectByteBuffer<JBufferObject, Int64, Int64>(
						             512, env.UsedStackBytes, (b2, c1) =>
						             {
							             Assert.Equal(512, b2.Capacity);
							             Assert.Equal(c1, b2.Environment.UsedStackBytes);
							             return b2.Environment.UsedStackBytes;
						             }));

					Assert.Throws<ArgumentOutOfRangeException>(() => env.UsableStackBytes = 128);
					Assert.Equal(512, env.UsableStackBytes);
				});
			});
		}
		finally
		{
			proxyEnv.Received().DeleteLocalRef(Arg.Is<JObjectLocalRef>(j => bufferRefs.Contains(j)));

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void UnknownTest(Boolean isDirect)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JObjectLocalRef localRef = NioTests.fixture.Create<JObjectLocalRef>();
			JClassLocalRef classRef = NioTests.fixture.Create<JClassLocalRef>();
			JStringLocalRef stringRef = NioTests.fixture.Create<JStringLocalRef>();
			JMethodId getCapacityId = NioTests.fixture.Create<JMethodId>();
			JMethodId isDirectId = NioTests.fixture.Create<JMethodId>();
			JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JBufferObject>();
			using IFixedPointer.IDisposable fCtx = typeMetadata.Information.GetFixedPointer();
			using IFixedPointer.IDisposable isDirectCtx =
				NativeFunctionSetImpl.IsDirectBufferDefinition.Information.GetFixedPointer();
			using IFixedPointer.IDisposable getCapacityCtx =
				NativeFunctionSetImpl.BufferCapacityDefinition.Information.GetFixedPointer();
			Int64 capacity = NioTests.fixture.Create<Int64>();
			IntPtr? address = isDirect ? NioTests.fixture.Create<JObjectLocalRef>().Pointer : null;

			proxyEnv.GetObjectRefType(localRef).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(stringRef.Value);
			proxyEnv.GetStringUtfLength(stringRef).Returns(typeMetadata.ClassName.Length);
			proxyEnv.GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)fCtx.Pointer);

			proxyEnv.GetDirectBufferAddress(localRef).Returns(address.GetValueOrDefault());
			proxyEnv.GetDirectBufferCapacity(localRef).Returns(capacity);

			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)fCtx.Pointer).Returns(classRef);
			proxyEnv.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)isDirectCtx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>())
			        .Returns(isDirectId);
			proxyEnv.CallBooleanMethod(localRef, isDirectId, ReadOnlyValPtr<JValueWrapper>.Zero).Returns(isDirect);
			proxyEnv.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)getCapacityCtx.Pointer,
			                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(getCapacityId);
			proxyEnv.CallLongMethod(localRef, getCapacityId, ReadOnlyValPtr<JValueWrapper>.Zero).Returns(capacity);

			JNativeCallAdapter call = JNativeCallAdapter.Create(proxyEnv.Reference, localRef, out JBufferObject buffer)
			                                            .Build();

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			Assert.Equal(capacity, buffer.Capacity);
			Assert.Equal(address, buffer.Address);

			proxyEnv.Received(1).GetMethodId(classRef, (ReadOnlyValPtr<Byte>)isDirectCtx.Pointer,
			                                 Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).CallBooleanMethod(localRef, isDirectId, ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(isDirect ? 0 : 1).GetMethodId(classRef, (ReadOnlyValPtr<Byte>)getCapacityCtx.Pointer,
			                                                Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(isDirect ? 0 : 1)
			        .CallLongMethod(localRef, getCapacityId, ReadOnlyValPtr<JValueWrapper>.Zero);

			proxyEnv.Received(isDirect ? 1 : 0).GetDirectBufferAddress(localRef);
			proxyEnv.Received(isDirect ? 1 : 0).GetDirectBufferCapacity(localRef);

			call.FinalizeCall();
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}