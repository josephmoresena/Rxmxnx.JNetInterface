namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JEnvironment : IEnvironment, IEqualityOperators<JEnvironment, JEnvironment, Boolean>
{
	/// <summary>
	/// Thread name.
	/// </summary>
	public virtual CString Name => CString.Zero;
	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	public virtual Boolean IsDisposable => false;
	/// <summary>
	/// Indicates whether current thread is daemon.
	/// </summary>
	public virtual Boolean IsDaemon => false;
	/// <summary>
	/// Indicates whether current thread is attached to a JVM.
	/// </summary>
	public virtual Boolean IsAttached => true;
	/// <inheritdoc cref="IEnvironment.PendingException"/>
	public ThrowableException? PendingException
	{
		get => this.GetThrown();
		set => this.SetThrown(value);
	}

	/// <inheritdoc/>
	public Boolean NoProxy => true;
	/// <inheritdoc/>
	public JEnvironmentRef Reference => this._cache.Reference;
	/// <inheritdoc/>
	public IVirtualMachine VirtualMachine => this._cache.VirtualMachine;
	/// <inheritdoc/>
	public Int32 Version => this._cache.Version;
	/// <inheritdoc/>
	public Boolean JniSecure() => this._cache.JniSecure();

	void IEnvironment.WithFrame(Int32 capacity, Action action)
	{
		using LocalFrame _ = new(this, capacity);
		this._cache.CheckJniError();
		action();
	}
	void IEnvironment.WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
	{
		using LocalFrame _ = new(this, capacity);
		this._cache.CheckJniError();
		action(state);
	}
	void IEnvironment.DescribeException()
	{
		ExceptionDescribeDelegate exceptionDescribe = this._cache.GetDelegate<ExceptionDescribeDelegate>();
		exceptionDescribe(this.Reference);
	}

	/// <inheritdoc/>
	public override Boolean Equals(Object? obj)
		=> (obj is JEnvironment other && this._cache.Equals(other._cache)) ||
			(obj is IEnvironment env && this.Reference == env.Reference);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._cache.GetHashCode();

	/// <summary>
	/// Determines whether a specified <see cref="JEnvironment"/> and a <see cref="JEnvironment"/> instance
	/// have the same value.
	/// </summary>
	/// <param name="left">The <see cref="JEnvironment"/> to compare.</param>
	/// <param name="right">The <see cref="JEnvironment"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is the same as the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==(JEnvironment? left, JEnvironment? right) => left?.Equals(right) ?? right is null;
	/// <summary>
	/// Determines whether a specified <see cref="JEnvironment"/> and a <see cref="JEnvironment"/> instance
	/// have different values.
	/// </summary>
	/// <param name="left">The <see cref="JEnvironment"/> to compare.</param>
	/// <param name="right">The <see cref="JEnvironment"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is different from the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=(JEnvironment? left, JEnvironment? right) => !(left == right);
}