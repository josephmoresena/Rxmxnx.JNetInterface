namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public abstract class EnvironmentProxy : IEnvironment
{
	public abstract JEnvironmentRef Reference { get; }
	public abstract IVirtualMachine VirtualMachine { get; }
	public abstract Int32 Version { get; }
	public abstract Int32? LocalCapacity { get; set; }
	public abstract IAccessFeature AccessFeature { get; }
	public abstract IClassFeature ClassFeature { get; }
	public abstract IReferenceFeature ReferenceFeature { get; }
	public abstract IStringFeature StringFeature { get; }
	public abstract IArrayFeature ArrayFeature { get; }
	public abstract INioFeature NioFeature { get; }
	public abstract NativeFunctionSet FunctionSet { get; }
	public abstract Boolean NoProxy { get; }

	public abstract JReferenceType GetReferenceType(JObject jObject);
	public abstract Boolean IsSameObject(JObject jObject, JObject? jOther);
	public abstract Boolean JniSecure();
	public abstract void WithFrame(Int32 capacity, Action action);
	public abstract void WithFrame<TState>(Int32 capacity, TState state, Action<TState> action);
	public abstract TResult WithFrame<TResult>(Int32 capacity, Func<TResult> func);
	public abstract TResult WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func);
}