namespace Rxmxnx.JNetInterface.Native.Dummies;

/// <summary>
/// This interface exposes a JNI dummy instance.
/// </summary>
public interface IDummyEnvironment : IEnvironment, IAccessor, IClassProvider
{
	IAccessor IEnvironment.Accessor => this;
	IClassProvider IEnvironment.ClassProvider => this;
}