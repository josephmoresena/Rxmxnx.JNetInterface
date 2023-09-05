namespace Rxmxnx.JNetInterface.Native.Dummies;

/// <summary>
/// This interface exposes a JNI dummy instance.
/// </summary>
public interface IDummyEnvironment : IEnvironment, IAccessProvider, IClassProvider, IReferenceProvider, IStringProvider,
	IEnumProvider, IArrayProvider
{
	IAccessProvider IEnvironment.AccessProvider => this;
	IClassProvider IEnvironment.ClassProvider => this;
	IReferenceProvider IEnvironment.ReferenceProvider => this;
	IStringProvider IEnvironment.StringProvider => this;
	IEnumProvider IEnvironment.EnumProvider => this;
	IArrayProvider IEnvironment.ArrayProvider => this;
}