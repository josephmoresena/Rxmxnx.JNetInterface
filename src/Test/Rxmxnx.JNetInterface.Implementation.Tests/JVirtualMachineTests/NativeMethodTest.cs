namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	[Fact]
	internal void NativeMethodTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JClassTypeMetadata classTypeMetadata = IClassType.GetMetadata<JTestObject>();
		using IFixedPointer.IDisposable ctx = classTypeMetadata.Information.GetFixedPointer();
		try
		{
			JClassLocalRef classRef = JVirtualMachineTests.fixture.Create<JClassLocalRef>();
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			JModifierObject.Modifiers modifiers = JModifierObject.Modifiers.Public | JModifierObject.Modifiers.Final;
			JMethodDefinition.Parameterless method1 = new("method1"u8);
			JMethodDefinition.Parameterless method2 = new("method2"u8);

			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer).Returns(classRef);
			proxyEnv.CallIntMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetModifiersMethodId,
			                       ReadOnlyValPtr<JValue>.Zero).Returns((Int32)modifiers);
			proxyEnv.CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                           ReadOnlyValPtr<JValue>.Zero).Returns(false);

			List<ObjectTracker> trackers = [];
			using (JClassObject jClass = JClassObject.GetClass<JTestObject>(env))
			{
				jClass.Register([
					TestUtilities.GetInstanceEntry(method1, out ObjectTracker tracker1),
					TestUtilities.GetStaticEntry(method2, out ObjectTracker tracker2),
				]);
				JVirtualMachineTests.CollectAndCheckAlive();
				trackers.Add(tracker1);
				trackers.Add(tracker2);
			}
			proxyEnv.Received(1).FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer);
			proxyEnv.Received(1).RegisterNatives(classRef, Arg.Any<ReadOnlyValPtr<NativeMethodValue>>(), 2);
			proxyEnv.ClearReceivedCalls();

			JVirtualMachineTests.CollectAndCheckAlive(trackers);

			while (trackers.All(d => d.WeakReference.IsAlive))
			{
				using (JClassObject jClass = JClassObject.GetClass<JTestObject>(env))
				{
					jClass.Register([
						TestUtilities.GetStaticEntry(method2, out ObjectTracker tracker2),
					]);
					trackers.Add(tracker2);
				}
				proxyEnv.Received(1).FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer);
				proxyEnv.Received(1).RegisterNatives(classRef, Arg.Any<ReadOnlyValPtr<NativeMethodValue>>(), 1);
				proxyEnv.ClearReceivedCalls();

				try
				{
					Assert.True(trackers.All(d => d.WeakReference.IsAlive ==
						                         !(d.FinalizerFlag?.Value).GetValueOrDefault()));
				}
				catch (Exception)
				{
					Assert.Contains(
						trackers, d => d.WeakReference.IsAlive == !(d.FinalizerFlag?.Value).GetValueOrDefault());
				}

				using (JClassObject jClass = JClassObject.GetClass<JTestObject>(env))
				{
					jClass.Register([
						TestUtilities.GetInstanceEntry(method1, out ObjectTracker tracker1),
					]);
					trackers.Add(tracker1);
				}
				proxyEnv.Received(1).FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer);
				proxyEnv.Received(1).RegisterNatives(classRef, Arg.Any<ReadOnlyValPtr<NativeMethodValue>>(), 1);
				proxyEnv.ClearReceivedCalls();

				JVirtualMachineTests.CollectAndCheckDead(trackers, true);

				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			Assert.True(trackers.TakeLast(2).All(d => d.WeakReference.IsAlive));
			Assert.True(trackers.TakeLast(2).All(d => !(d.FinalizerFlag?.Value).GetValueOrDefault()));
			Assert.Contains(trackers.SkipLast(2), d => !d.WeakReference.IsAlive);
			Assert.Contains(trackers.SkipLast(2), d => (d.FinalizerFlag?.Value).GetValueOrDefault());

			Int32 maxCount = Random.Shared.Next(1, trackers.Count * 10);
			for (Int32 count = 1; count <= maxCount; count++)
			{
				using (JClassObject jClass = JClassObject.GetClass<JTestObject>(env)) jClass.UnregisterNativeCalls();
				proxyEnv.Received(count).FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer);
				proxyEnv.Received(count).UnregisterNatives(classRef);
			}

			JVirtualMachineTests.CollectAndCheckDead(trackers, true);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Boolean removeResult = JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			if (Environment.Is64BitProcess)
				Assert.True(removeResult);
			proxyEnv.FinalizeProxy(true);
		}
	}

	private static void CollectAndCheckAlive(List<ObjectTracker>? trackers = default)
	{
		JVirtualMachineTests.CollectGen(2);
		if (trackers is null) return;
		try
		{
			Assert.True(trackers.All(d => d.WeakReference.IsAlive));
			Assert.True(trackers.All(d => !(d.FinalizerFlag?.Value).GetValueOrDefault()));
		}
		catch (Exception)
		{
			Assert.Contains(trackers, d => d.WeakReference.IsAlive);
			Assert.Contains(trackers, d => !(d.FinalizerFlag?.Value).GetValueOrDefault());
		}
	}
	private static void CollectAndCheckDead(List<ObjectTracker> trackers, Boolean collectGen2)
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
		if (collectGen2)
			JVirtualMachineTests.CollectGen(2);
		try
		{
			Assert.True(trackers.All(d => d.WeakReference.IsAlive == !(d.FinalizerFlag?.Value).GetValueOrDefault()));
		}
		catch (Exception)
		{
			Assert.Contains(trackers, d => d.WeakReference.IsAlive == !(d.FinalizerFlag?.Value).GetValueOrDefault());
		}
	}
	private static void CollectGen(Int32 gen)
	{
#if NET8_0_OR_GREATER
		GC.Collect(gen, GCCollectionMode.Aggressive, true);
#else
		GC.Collect(gen, GCCollectionMode.Forced, true);
#endif
		GC.WaitForPendingFinalizers();
	}
}