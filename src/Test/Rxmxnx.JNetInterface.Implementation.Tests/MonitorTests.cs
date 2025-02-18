namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class MonitorTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void NullTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Assert.True(JObject.IsNullOrDefault(env.ClassFeature.VoidObject));
			Assert.Null(env.ClassFeature.VoidObject.Synchronize());

			proxyEnv.Received(0).MonitorEnter(Arg.Any<JObjectLocalRef>());
			proxyEnv.Received(0).MonitorExit(Arg.Any<JObjectLocalRef>());
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
	internal void InvalidEnterTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, MonitorTests.fixture.Create<String>());
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(MonitorTests.fixture.Create<JClassLocalRef>());

			proxyEnv.MonitorEnter(jLocal.LocalReference).Returns(JResult.Error);
			proxyEnv.MonitorExit(jLocal.LocalReference).Returns(JResult.Error);

			Assert.Equal(JResult.Error, Assert.Throws<JniException>(() => jLocal.Synchronize()).Result);

			proxyEnv.Received(1).MonitorEnter(jLocal.LocalReference);
			proxyEnv.Received(0).MonitorExit(jLocal.LocalReference);
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
	internal void InvalidExitTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, MonitorTests.fixture.Create<String>());
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(MonitorTests.fixture.Create<JClassLocalRef>());

			proxyEnv.MonitorEnter(jLocal.LocalReference).Returns(JResult.Ok);
			proxyEnv.MonitorExit(jLocal.LocalReference).Returns(JResult.Error);

			IDisposable? disposable = jLocal.Synchronize();

			Assert.NotNull(disposable);
			Assert.Equal(JResult.Error, Assert.Throws<JniException>(() => disposable.Dispose()).Result);

			proxyEnv.Received(1).MonitorEnter(jLocal.LocalReference);
			proxyEnv.Received(1).MonitorExit(jLocal.LocalReference);

			(jLocal as IDisposable).Dispose();
			Assert.False(JObject.IsNullOrDefault(jLocal));

			proxyEnv.ClearReceivedCalls();
			proxyEnv.MonitorExit(jLocal.LocalReference).Returns(JResult.Ok);

			disposable.Dispose();

			proxyEnv.Received(1).MonitorExit(jLocal.LocalReference);
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
	internal void FullTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JGlobalRef globalRef = MonitorTests.fixture.Create<JGlobalRef>();
		JWeakRef weakRef = MonitorTests.fixture.Create<JWeakRef>();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			using JLocalObject jLocal = TestUtilities.CreateString(proxyEnv, MonitorTests.fixture.Create<String>());

			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(MonitorTests.fixture.Create<JClassLocalRef>());
			proxyEnv.NewWeakGlobalRef(jLocal.LocalReference).Returns(weakRef);
			proxyEnv.NewGlobalRef(weakRef.Value).Returns(globalRef);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);

			proxyEnv.MonitorEnter(Arg.Any<JObjectLocalRef>()).Returns(JResult.Ok);
			proxyEnv.MonitorExit(Arg.Any<JObjectLocalRef>()).Returns(JResult.Ok);

			using (IDisposable ___ = jLocal.Synchronize()!)
			{
				(jLocal as IDisposable).Dispose();

				proxyEnv.Received(0).DeleteLocalRef(jLocal.LocalReference);
				proxyEnv.Received(1).MonitorEnter(jLocal.LocalReference);
				proxyEnv.ClearReceivedCalls();

				using (JWeak jWeak = jLocal.Weak)
				{
					using (IDisposable __ = jLocal.Synchronize()!)
					{
						(jWeak as IDisposable).Dispose();

						proxyEnv.Received(0).DeleteWeakGlobalRef(jWeak.Reference);
						proxyEnv.Received(0).MonitorEnter(jLocal.LocalReference);
						proxyEnv.Received(1).MonitorEnter(jWeak.Reference.Value);
						proxyEnv.ClearReceivedCalls();

						using (JGlobal jGlobal = jLocal.Global)
						{
							using (IDisposable _ = jLocal.Synchronize()!)
							{
								(jGlobal as IDisposable).Dispose();

								proxyEnv.Received(0).DeleteGlobalRef(jGlobal.Reference);
								proxyEnv.Received(0).MonitorEnter(jLocal.LocalReference);
								proxyEnv.Received(0).MonitorEnter(jWeak.Reference.Value);
								proxyEnv.Received(1).MonitorEnter(jGlobal.Reference.Value);
								proxyEnv.ClearReceivedCalls();
							}
							proxyEnv.Received(1).MonitorExit(jGlobal.Reference.Value);
							proxyEnv.Received(0).DeleteGlobalRef(jGlobal.Reference);
						}
						proxyEnv.Received(1).DeleteGlobalRef(globalRef);
					}
					proxyEnv.Received(1).MonitorExit(jWeak.Reference.Value);
					proxyEnv.Received(0).DeleteWeakGlobalRef(jWeak.Reference);
				}
				proxyEnv.Received(1).DeleteWeakGlobalRef(weakRef);
			}

			proxyEnv.Received(1).MonitorExit(jLocal.LocalReference);
			proxyEnv.Received(0).DeleteLocalRef(jLocal.LocalReference);
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