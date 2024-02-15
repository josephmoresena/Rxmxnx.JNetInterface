namespace Rxmxnx.JNetInterface.Native.Dummies;

public abstract partial class EnvironmentProxy : IEnvironment, IAccessFeature, IClassFeature, IReferenceFeature,
	IStringFeature, IArrayFeature, INioFeature
{
	Boolean IEnvironment.NoProxy => false;
	IVirtualMachine IEnvironment.VirtualMachine => this.VirtualMachine;

	IAccessFeature IEnvironment.AccessFeature => this;
	IClassFeature IEnvironment.ClassFeature => this;
	IReferenceFeature IEnvironment.ReferenceFeature => this;
	IStringFeature IEnvironment.StringFeature => this;
	IArrayFeature IEnvironment.ArrayFeature => this;
	INioFeature IEnvironment.NioFeature => this;
	FunctionCache IEnvironment.Functions => InternalFunctionCache.Instance;
}