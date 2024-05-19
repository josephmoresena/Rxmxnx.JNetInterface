namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public abstract class GlobalObjectTestsBase
{
	protected static readonly IFixture Fixture = new Fixture().RegisterReferences();

	private protected static ConcurrentBag<TGlobalRef> ConfigureFinalizer<TGlobalRef>(VirtualMachineProxy vm,
		ThreadProxy thread, Boolean unload) where TGlobalRef : unmanaged, IObjectGlobalReferenceType<TGlobalRef>
	{
		ConcurrentBag<TGlobalRef> result = [];

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		thread.ReferenceFeature.Unload(Arg.Any<JGlobal>()).Returns(unload);
		thread.ReferenceFeature.WhenForAnyArgs(r => r.Unload(Arg.Any<JGlobal>())).Do(c =>
		{
			try
			{
				result.Add((c[0] as JGlobalBase)!.As<TGlobalRef>());
			}
			catch (Exception)
			{
				// ignored
			}
			finally
			{
				GC.Collect();
			}
		});
		return result;
	}
	private protected static async Task FinalizerCheckTestAsync<TGlobalRef>(ThreadProxy thread,
		ConcurrentBag<TGlobalRef> references, TGlobalRef globalRef)
		where TGlobalRef : unmanaged, IObjectGlobalReferenceType<TGlobalRef>
	{
		GC.Collect();
		GC.Collect();
		GC.Collect();
		await Task.Delay(100);
		GC.Collect();
		GC.Collect();
		GC.Collect();
		thread.ReferenceFeature.Received().Unload(Arg.Any<JGlobalBase>());
		Assert.All(references, g => Assert.Equal(globalRef, g));
	}
}