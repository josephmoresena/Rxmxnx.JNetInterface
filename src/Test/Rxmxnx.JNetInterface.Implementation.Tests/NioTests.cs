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
}