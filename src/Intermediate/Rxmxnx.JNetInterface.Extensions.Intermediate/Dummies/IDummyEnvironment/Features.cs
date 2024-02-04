namespace Rxmxnx.JNetInterface.Native.Dummies;

public partial interface IDummyEnvironment : IAccessFeature, IClassFeature, IReferenceFeature, IStringFeature,
	IArrayFeature, INioFeature
{
	IAccessFeature IEnvironment.AccessFeature => this;
	IClassFeature IEnvironment.ClassFeature => this;
	IReferenceFeature IEnvironment.ReferenceFeature => this;
	IStringFeature IEnvironment.StringFeature => this;
	IArrayFeature IEnvironment.ArrayFeature => this;
	INioFeature IEnvironment.NioFeature => this;
	FunctionCache IEnvironment.Functions => InternalFunctionCache.Instance;
}