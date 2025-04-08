namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements the <see cref="IVirtualMachine"/> interface.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Advanced)]
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
	public JEnvironmentRef Reference => this._cache.Reference;
	/// <inheritdoc/>
	public IVirtualMachine VirtualMachine => this._cache.VirtualMachine;
	/// <inheritdoc/>
	public Int32 Version => this._cache.Version;
	/// <inheritdoc/>
	public Int32 UsedStackBytes => this._cache.UsedStackBytes;
	/// <inheritdoc/>
	public Int32 UsableStackBytes
	{
		get => this._cache.MaxStackBytes;
		set => this._cache.SetUsableStackBytes(value);
	}

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
	unsafe Boolean? IEnvironment.IsVirtual(JThreadObject jThread)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jThread);
		ImplementationValidationUtilities.ThrowIfDefault(jThread);
		if (this.Version < NativeInterface19.RequiredVersion) return default;
		ref readonly NativeInterface19 nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface19>(NativeInterface19.IsVirtualThreadInfo);
		using INativeTransaction jniTransaction = this._cache.VirtualMachine.CreateTransaction(1);
		JObjectLocalRef localRef = jniTransaction.Add(jThread);
		return nativeInterface.IsVirtualThread(this.Reference, localRef).Value;
	}

	/// <inheritdoc/>
	public Boolean JniSecure() => this._cache.JniSecure();
	/// <inheritdoc/>
	public unsafe void DescribeException()
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionDescribeInfo);
		nativeInterface.ErrorFunctions.ExceptionDescribe(this.Reference);
	}

	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public override Boolean Equals(Object? obj)
		=> (obj is JEnvironment other && this._cache.Equals(other._cache)) ||
			(obj is IEnvironment env && this.Reference == env.Reference);
	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
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
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
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
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=(JEnvironment? left, JEnvironment? right) => !(left == right);
}