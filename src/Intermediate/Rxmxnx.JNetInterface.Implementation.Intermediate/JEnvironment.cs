namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements the <see cref="IVirtualMachine"/> interface.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Advanced)]
public partial class JEnvironment : IEqualityOperators<JEnvironment, JEnvironment, Boolean>
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
	/// <inheritdoc cref="IEnvironment.Version"/>
	public Int32 Version => this._m.Core.Version;
	/// <summary>
	/// Indicates whether current thread is attached to a JVM.
	/// </summary>
	public virtual Boolean IsAttached => this._m.Core.Host.IsRunning;

	/// <inheritdoc cref="IEnvironment.PendingException"/>
	public ThrowableException? PendingException
	{
		get => this._m.PendingException;
		set => this._m.PendingException = value;
	}

	/// <inheritdoc/>
	public JEnvironmentRef Reference => this._m.Core.Reference;
	/// <inheritdoc/>
	public IVirtualMachine VirtualMachine => this._m.Core.Host.Value;
	/// <inheritdoc/>
	public Int32 UsedStackBytes => this._m.Core.UsedStackBytes;
	/// <inheritdoc/>
	public Int32 UsableStackBytes
	{
		get => this._m.UsableStackBytes;
		set => this._m.UsableStackBytes = value;
	}

	Int32 IEnvironment.Version
	{
		get
		{
			if (AndroidFeature.IsFixedAndroid) return (Int32)JRuntimeVersion.J6;
			if (JavaStandardFeature.GetInterfaceVersion() is { } jniVersion) return jniVersion;
			return this.Version;
		}
	}

	void IEnvironment.WithFrame(Int32 capacity, Action action) => this._m.WithFrame(this, capacity, action);
	void IEnvironment.WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
		=> this._m.WithFrame(this, capacity, state, action);

	Boolean? IEnvironment.IsVirtual(JThreadObject jThread)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jThread);
		ImplementationValidationUtilities.ThrowIfDefault(jThread);
		if (this.Version >= NativeInterface19.RequiredVersion)
		{
			ref readonly NativeInterface19 nativeInterface =
				ref this._m.Core.GetNativeInterface<NativeInterface19>(NativeInterface19.IsVirtualThreadInfo);
			using INativeTransaction jniTransaction = this._m.Core.Host.MemoryManager.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(jThread);
			return nativeInterface.IsVirtualThread(this.Reference, localRef).Value;
		}
		if (JVirtualMachine.AndroidApiLevel > 0 || this.VirtualMachine.Version < JRuntimeVersion.J21) return default;
		return this._m.Core.IsVirtual(jThread);
	}

	/// <inheritdoc/>
	public Boolean JniSecure() => this._m.JniSecure();
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void DescribeException() => EnvironmentCore.DescribeException(this._m.Core);

	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public sealed override Boolean Equals(Object? obj)
		=> (obj is JEnvironment other && this._m.Core.Equals(other._m.Core)) ||
			(obj is IEnvironment env && this.Reference == env.Reference);
	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public sealed override Int32 GetHashCode() => this._m.Core.GetHashCode();

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