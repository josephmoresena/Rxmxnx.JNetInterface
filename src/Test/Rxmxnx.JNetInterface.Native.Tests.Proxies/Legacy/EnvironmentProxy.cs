namespace Rxmxnx.JNetInterface.Tests;

public partial class EnvironmentProxy
{
	IVirtualMachine IEnvironment.VirtualMachine => this.VirtualMachine;
	IAccessFeature IEnvironment.AccessFeature => this.AccessFeature;
	IClassFeature IEnvironment.ClassFeature => this.ClassFeature;
	IReferenceFeature IEnvironment.ReferenceFeature => this.ReferenceFeature;
	IStringFeature IEnvironment.StringFeature => this.StringFeature;
	IArrayFeature IEnvironment.ArrayFeature => this.ArrayFeature;
	INioFeature IEnvironment.NioFeature => this.NioFeature;
}