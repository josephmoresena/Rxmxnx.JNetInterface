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
		thread.Version.Returns(c => proxy.Version);
		thread.VirtualMachine.Returns(c => proxy.VirtualMachine);
		thread.NoProxy.Returns(c => proxy.NoProxy);
		thread.AccessFeature.Returns(c => proxy.AccessFeature);
		thread.ClassFeature.Returns(c => proxy.ClassFeature);
		thread.ReferenceFeature.Returns(c => proxy.ReferenceFeature);
		thread.StringFeature.Returns(c => proxy.StringFeature);
		thread.ArrayFeature.Returns(c => proxy.ArrayFeature);
		thread.NioFeature.Returns(c => proxy.NioFeature);
		thread.FunctionSet.Returns(c => proxy.FunctionSet);
		return thread;
	}
}