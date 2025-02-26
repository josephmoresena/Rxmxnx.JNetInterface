namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy : NativeFunctionSet, IEnvironment, IAccessFeature, IClassFeature,
	IReferenceFeature, IStringFeature, IArrayFeature, INioFeature
{
	Boolean IEnvironment.NoProxy => false;
	IVirtualMachine IEnvironment.VirtualMachine => this.VirtualMachine;

	IAccessFeature IEnvironment.AccessFeature => this;
	IClassFeature IEnvironment.ClassFeature => this;
	IReferenceFeature IEnvironment.ReferenceFeature => this;
	IStringFeature IEnvironment.StringFeature => this;
	IArrayFeature IEnvironment.ArrayFeature => this;
	INioFeature IEnvironment.NioFeature => this;
	NativeFunctionSet IEnvironment.FunctionSet => this;
}