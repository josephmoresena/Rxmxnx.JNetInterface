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
		ThreadProxy env = Substitute.For<ThreadProxy>();
		env.VirtualMachine.Returns(proxy.VirtualMachine);
		env.NoProxy.Returns(!proxy.NoProxy);
		env.AccessFeature.Returns(proxy.AccessFeature);
		env.ClassFeature.Returns(proxy.ClassFeature);
		env.ReferenceFeature.Returns(proxy.ReferenceFeature);
		env.StringFeature.Returns(proxy.StringFeature);
		env.ArrayFeature.Returns(proxy.ArrayFeature);
		env.NioFeature.Returns(proxy.NioFeature);
		env.FunctionSet.Returns(proxy.FunctionSet);
		return env;
	}
}