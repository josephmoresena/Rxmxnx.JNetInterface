namespace Rxmxnx.JNetInterface.Internal;

internal partial class AndroidEnvironment
{
	/// <inheritdoc/>
	public JReferenceType GetReferenceType(JObject jObject) => this._m.GetReferenceType(jObject);
	/// <inheritdoc/>
	public Boolean IsSameObject(JObject jObject, JObject? jOther) => this._m.IsSameObject(jObject, jOther);
	/// <inheritdoc/>
	public Boolean JniSecure() => this._m.JniSecure();
	/// <inheritdoc/>
	public void WithFrame(Int32 capacity, Action action) => this._m.WithFrame(this, capacity, action);
	/// <inheritdoc/>
	public void WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
		=> this._m.WithFrame(this, capacity, state, action);
	/// <inheritdoc/>
	public TResult WithFrame<TResult>(Int32 capacity, Func<TResult> func)
		=> EnvironmentValue.WithFrame(this, capacity, func);
	/// <inheritdoc/>
	public TResult WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
		=> EnvironmentValue.WithFrame(this, capacity, state, func);
	/// <inheritdoc/>
	public void DescribeException() => EnvironmentCore.DescribeException(this._m.Core);
	/// <inheritdoc/>
	public Boolean IsValidationAvoidable(JGlobalBase jGlobal)
		=> EnvironmentCore.IsValidationAvoidable(this._m.Core, jGlobal);
}