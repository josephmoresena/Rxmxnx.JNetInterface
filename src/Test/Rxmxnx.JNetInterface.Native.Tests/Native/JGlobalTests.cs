namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JGlobalTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void SimpleTest(Boolean isProxy)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		env.NoProxy.Returns(!isProxy);
		vm.NoProxy.Returns(!isProxy);
		JGlobalRef globalRef0 = JGlobalTests.fixture.Create<JGlobalRef>();
		JGlobalRef globalRef1 = JGlobalTests.fixture.Create<JGlobalRef>();

		using JClassObject jClassClass = new(env);
		using JGlobal jGlobal0 = new(jClassClass, globalRef0);
		using JGlobal jGlobal1 = new(vm, jGlobal0.ObjectMetadata, globalRef0);
		using JGlobal jGlobal2 = new(jGlobal1, globalRef1);

		Assert.Equal(globalRef0, jGlobal0.Reference);
		Assert.Equal(globalRef0, jGlobal1.Reference);
		Assert.Equal(globalRef1, jGlobal2.Reference);

		Assert.Equal(isProxy, jGlobal0.IsProxy);
		Assert.Equal(isProxy, jGlobal1.IsProxy);
		Assert.Equal(isProxy, jGlobal2.IsProxy);

		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		Assert.Equal(jGlobal0, jGlobal0.GetCacheable(env));
		Assert.Equal(jGlobal1, jGlobal1.GetCacheable(env));
		Assert.Equal(jGlobal2, jGlobal2.GetCacheable(env));

		jGlobal0.Synchronize(jGlobal1);
		jGlobal1.Synchronize(jGlobal2);
		jGlobal2.Synchronize(jGlobal0);

		Assert.Equal(jGlobal0, jGlobal0.GetCacheable(env));
		Assert.Equal(jGlobal1, jGlobal1.GetCacheable(env));
		Assert.Equal(jGlobal0, jGlobal2.GetCacheable(env));
	}
}