namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.AbstractProxyJustification)]
public abstract class ThreadProxy : EnvironmentProxy, IThread
{
	public abstract CString Name { get; }
	public abstract Boolean Attached { get; }
	public abstract Boolean Daemon { get; }

	public abstract void Dispose();

	public new static ThreadProxy CreateEnvironment(Boolean isProxy, VirtualMachineProxy? vm = default)
	{
		ThreadProxy env = Substitute.For<ThreadProxy>();
		env.Version.Returns(IVirtualMachine.MinimalVersion);
		env.VirtualMachine.Returns(vm ?? Substitute.For<VirtualMachineProxy>());
		env.NoProxy.Returns(!isProxy);
		env.AccessFeature.Returns(Substitute.For<AccessFeatureProxy>());
		env.ClassFeature.Returns(Substitute.For<ClassFeatureProxy>());
		env.ReferenceFeature.Returns(Substitute.For<ReferenceFeatureProxy>());
		env.StringFeature.Returns(Substitute.For<StringFeatureProxy>());
		env.ArrayFeature.Returns(Substitute.For<ArrayFeatureProxy>());
		env.NioFeature.Returns(Substitute.For<NioFeatureProxy>());
		env.FunctionSet.Returns(Substitute.For<NativeFunctionSet>());
		return env;
	}
	public static ThreadProxy CreateEnvironment(EnvironmentProxy proxy)
	{
		ThreadProxy thread = Substitute.For<ThreadProxy>();
		thread.Version.Returns(_ => proxy.Version);
		thread.VirtualMachine.Returns(_ => proxy.VirtualMachine);
		thread.NoProxy.Returns(_ => proxy.NoProxy);
		thread.AccessFeature.Returns(_ => proxy.AccessFeature);
		thread.ClassFeature.Returns(_ => proxy.ClassFeature);
		thread.ReferenceFeature.Returns(_ => proxy.ReferenceFeature);
		thread.StringFeature.Returns(_ => proxy.StringFeature);
		thread.ArrayFeature.Returns(_ => proxy.ArrayFeature);
		thread.NioFeature.Returns(_ => proxy.NioFeature);
		thread.FunctionSet.Returns(_ => proxy.FunctionSet);
		return thread;
	}
}