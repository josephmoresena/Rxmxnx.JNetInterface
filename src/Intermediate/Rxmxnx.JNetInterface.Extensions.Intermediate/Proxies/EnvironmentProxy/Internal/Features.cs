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
	
	
	/// <inheritdoc/>
	void IEnvironment.WithFrame(Int32 capacity, Action action)
	{
		Int32? oldCapacity = capacity;
		try
		{
			this.LocalCapacity = capacity;
			action();
		}
		finally
		{
			this.LocalCapacity = oldCapacity;
		}
	}
	void IEnvironment.WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
	{
		Int32? oldCapacity = capacity;
		try
		{
			this.LocalCapacity = capacity;
			action(state);
		}
		finally
		{
			this.LocalCapacity = oldCapacity;
		}
	}
	TResult IEnvironment.WithFrame<TResult>(Int32 capacity, Func<TResult> func)
	{
		Int32? oldCapacity = capacity;
		try
		{
			this.LocalCapacity = capacity;
			return func();
		}
		finally
		{
			this.LocalCapacity = oldCapacity;
		}
	}
	TResult IEnvironment.WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	{
		Int32? oldCapacity = capacity;
		try
		{
			this.LocalCapacity = capacity;
			return func(state);
		}
		finally
		{
			this.LocalCapacity = oldCapacity;
		}
	}
}