namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class LocalFrameTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void MemoryErrorTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Int32 capacity = LocalFrameTests.fixture.Create<Byte>();

			proxyEnv.PushLocalFrame(capacity).Returns(JResult.MemoryError);

			JniException ex = Assert.Throws<JniException>(() =>
			{
				(env as IEnvironment).WithFrame(capacity, env, e =>
				{
					LocalCache cache = e.LocalCache;
					Assert.Equal(capacity, cache.Capacity);
				});
			});
			Assert.Equal(JResult.MemoryError, ex.Result);

			proxyEnv.Received(1).PushLocalFrame(capacity);
			proxyEnv.Received(0).PopLocalFrame(Arg.Any<JObjectLocalRef>());
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void SetCapacityTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Int32 capacity = LocalFrameTests.fixture.Create<Byte>();

			proxyEnv.PushLocalFrame(capacity).Returns(JResult.Ok);
			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();
			JLocalObject result = (env as IEnvironment).WithFrame(capacity, env, e =>
			{
				LocalCache cache = e.LocalCache;
				Assert.Equal(capacity, cache.Capacity);
				Exception ex = Assert.Throws<InvalidOperationException>(() => cache.Capacity = capacity + 1);
				Assert.Equal("Current stack frame is fixed.", ex.Message);
				return e.ClassObject;
			});
			Assert.Equal(env.ClassObject, result);
			proxyEnv.Received(1).PushLocalFrame(capacity);
			proxyEnv.Received(0).PushLocalFrame(capacity + 1);
			proxyEnv.Received(1).PopLocalFrame(default);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void QueueTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Int32 capacity = Random.Shared.Next(2, 10);

			proxyEnv.PushLocalFrame(capacity).Returns(JResult.Ok);
			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();
			(env as IEnvironment).WithFrame(capacity, proxyEnv, p =>
			{
				proxyEnv.Received(1).PushLocalFrame(capacity);

				List<JLocalObject> objects = [];
				for (Int32 i = 0; i < capacity; i++)
					objects.Add(TestUtilities.CreateString(p, LocalFrameTests.fixture.Create<String>()));
				Assert.True(objects.All(o => !JObject.IsNullOrDefault(o)));
				for (Int32 i = 0; i < capacity; i++)
					objects.Add(TestUtilities.CreateString(p, LocalFrameTests.fixture.Create<String>()));
				Assert.True(objects.Take(capacity).All(JObject.IsNullOrDefault));
				Assert.True(objects.Skip(capacity).Take(capacity).All(o => !JObject.IsNullOrDefault(o)));
				objects.Last().Dispose();
				Assert.True(JObject.IsNullOrDefault(objects.Last()));
				objects.Add(TestUtilities.CreateString(p, LocalFrameTests.fixture.Create<String>()));
				Assert.True(objects.Skip(capacity).Take(capacity - 1).All(o => !JObject.IsNullOrDefault(o)));
				Assert.False(JObject.IsNullOrDefault(objects.Last()));
			});
			proxyEnv.Received(1).PopLocalFrame(default);
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
	internal void CleaningTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Int32 capacity = Random.Shared.Next(2, 10);
			List<JLocalObject> objects = [];

			proxyEnv.PushLocalFrame(capacity).Returns(JResult.Ok);
			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();
			(env as IEnvironment).WithFrame(capacity, proxyEnv, p =>
			{
				proxyEnv.Received(1).PushLocalFrame(capacity);

				for (Int32 i = 0; i < capacity; i++)
					objects.Add(TestUtilities.CreateString(p, LocalFrameTests.fixture.Create<String>()));
				Assert.True(objects.All(o => !JObject.IsNullOrDefault(o)));
			});
			proxyEnv.Received(1).PopLocalFrame(default);
			Assert.True(objects.All(JObject.IsNullOrDefault));
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void WithFrameLocalResultTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		Int32 capacity = LocalFrameTests.fixture.Create<Byte>();
		String text = LocalFrameTests.fixture.Create<String>();
		JStringLocalRef stringRef = default;
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);

			proxyEnv.PushLocalFrame(capacity).Returns(JResult.Ok);
			proxyEnv.PopLocalFrame(Arg.Any<JObjectLocalRef>()).Returns(c =>
			{
				JObjectLocalRef p0 = (JObjectLocalRef)c[0];
				return p0 != default ? TestUtilities.InvertPointer(p0) : default;
			});
			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();
			using JStringObject result = (env as IEnvironment).WithFrame(capacity, env, _ =>
			{
				proxyEnv.Received(1).PushLocalFrame(capacity);
				JStringObject result = TestUtilities.CreateString(proxyEnv, text);
				stringRef = result.Reference;
				return result;
			});
			Assert.Equal(TestUtilities.InvertPointer(stringRef), result.Reference);
			proxyEnv.Received(1).PopLocalFrame(stringRef.Value);
		}
		finally
		{
			proxyEnv.Received(1).DeleteLocalRef(TestUtilities.InvertPointer(stringRef).Value);

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void WithFrameNoLocalResultTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		Int32 capacity = LocalFrameTests.fixture.Create<Byte>();
		String text = LocalFrameTests.fixture.Create<String>();
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			JStringObject jString = TestUtilities.CreateString(proxyEnv, text);
			JStringLocalRef stringRef = jString.Reference;

			proxyEnv.PushLocalFrame(capacity).Returns(JResult.Ok);
			proxyEnv.PopLocalFrame(Arg.Any<JObjectLocalRef>()).Returns(c =>
			{
				JObjectLocalRef p0 = (JObjectLocalRef)c[0];
				return p0 != default ? TestUtilities.InvertPointer(p0) : default;
			});
			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			using JStringObject result = (env as IEnvironment).WithFrame(capacity, jString, _ =>
			{
				proxyEnv.Received(1).PushLocalFrame(capacity);
				return jString;
			});
			Assert.Equal(stringRef, result.Reference);
			Assert.True(Object.ReferenceEquals(jString, result));
			proxyEnv.Received(1).PopLocalFrame(default);
		}
		finally
		{
			GC.Collect();
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void WithFrameNoStateTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		Int32 capacity = LocalFrameTests.fixture.Create<Byte>();
		String text = LocalFrameTests.fixture.Create<String>();
		JStringLocalRef stringRef = default;
		try
		{
			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);

			proxyEnv.PushLocalFrame(capacity).Returns(JResult.Ok);
			proxyEnv.PopLocalFrame(Arg.Any<JObjectLocalRef>()).Returns(c =>
			{
				JObjectLocalRef p0 = (JObjectLocalRef)c[0];
				return p0 != default ? TestUtilities.InvertPointer(p0) : default;
			});
			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();
			(env as IEnvironment).WithFrame(
				capacity,
				() =>
				{
					Assert.Equal(Environment.CurrentManagedThreadId,
					             (env as IEnvironment).WithFrame(capacity, () => Environment.CurrentManagedThreadId));
				});
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}