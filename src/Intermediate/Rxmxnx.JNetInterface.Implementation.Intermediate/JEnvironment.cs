namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JEnvironment : IEnvironment, IEquatable<IEnvironment>, IEquatable<JEnvironment>,
	IEqualityOperators<JEnvironment, JEnvironment, Boolean>, IEqualityComparer<JEnvironment>
{
	/// <summary>
	/// <see cref="JEnvironment"/> cache.
	/// </summary>
	private readonly EnvironmentCache _cache;

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
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="JEnvironment"/> reference.</param>
	private JEnvironment(EnvironmentCache cache) => this._cache = cache;

	/// <inheritdoc/>
	public Boolean NoProxy => true;
	/// <inheritdoc/>
	public JEnvironmentRef Reference => this._cache.Reference;
	/// <inheritdoc/>
	public IVirtualMachine VirtualMachine => this._cache.VirtualMachine;
	/// <inheritdoc/>
	public Int32 Version => this._cache.Version;

	Int32? IEnvironment.LocalCapacity
	{
		get => this._cache.Capacity;
		set => this._cache.EnsureLocalCapacity(value.GetValueOrDefault());
	}
	IAccessFeature IEnvironment.AccessFeature => this._cache;
	IClassFeature IEnvironment.ClassFeature => this._cache;
	IReferenceFeature IEnvironment.ReferenceFeature => this._cache;
	IStringFeature IEnvironment.StringFeature => this._cache;
	IArrayFeature IEnvironment.ArrayFeature => this._cache;
	INioFeature IEnvironment.NioFeature => this._cache;
	FunctionCache IEnvironment.Functions => InternalFunctionCache.Instance;

	JReferenceType IEnvironment.GetReferenceType(JObject jObject)
	{
		if (jObject is not JReferenceObject jRefObj || jRefObj.IsDefault || jRefObj.IsProxy)
			return JReferenceType.InvalidRefType;
		using INativeTransaction jniTransaction = this._cache.VirtualMachine.CreateTransaction(1);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JReferenceType result = this.GetReferenceType(localRef);
		if (result == JReferenceType.InvalidRefType)
		{
			if (jRefObj is JGlobalBase jGlobal) (this.VirtualMachine as JVirtualMachine)!.Remove(jGlobal);
			else this._cache.Remove(jRefObj as JLocalObject);
			jRefObj.ClearValue();
		}
		else if (this.IsSame(jRefObj.As<JObjectLocalRef>(), default))
		{
			if (jRefObj is JGlobalBase jGlobal)
				this._cache.Unload(jGlobal);
			else
				this._cache.Unload(jRefObj as JLocalObject ?? ILocalViewObject.GetObject(jRefObj as ILocalViewObject));
		}

		return result;
	}
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
	{
		if (jObject.Equals(jOther)) return true;
		if (jObject is not JReferenceObject jRefObj || jOther is not JReferenceObject jRefOther)
			return JEnvironment.EqualEquatable(jObject as IEquatable<IPrimitiveType>, jOther as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<IPrimitiveType>, jObject as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jObject as IEquatable<JPrimitiveObject>, jOther as JPrimitiveObject) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<JPrimitiveObject>, jObject as JPrimitiveObject) ??
				false;

		ValidationUtilities.ThrowIfDummy(jRefObj);
		ValidationUtilities.ThrowIfDummy(jRefOther);
		using INativeTransaction jniTransaction = this._cache.VirtualMachine.CreateTransaction(2);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JObjectLocalRef otherLocalRef = jniTransaction.Add(jRefOther);
		return this.IsSame(localRef, otherLocalRef);
	}
	/// <inheritdoc/>
	public Boolean JniSecure() => this._cache.JniSecure();
	void IEnvironment.WithFrame(Int32 capacity, Action action)
	{
		using LocalFrame localFrame = new(this, capacity);
		this._cache.CheckJniError();
		action();
	}
	void IEnvironment.WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
	{
		using LocalFrame localFrame = new(this, capacity);
		this._cache.CheckJniError();
		action(state);
	}
	TResult IEnvironment.WithFrame<TResult>(Int32 capacity, Func<TResult> func)
	{
		TResult? result;
		JGlobalRef globalRef;
		JLocalObject? localResult;
		using (LocalFrame localFrame = new(this, capacity))
		{
			this._cache.CheckJniError();
			result = func();
			localResult = localFrame.GetLocalResult(result, out globalRef);
		}
		this._cache.CreateLocalRef(globalRef, localResult);
		return result;
	}
	TResult IEnvironment.WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	{
		TResult? result;
		JGlobalRef globalRef;
		JLocalObject? localResult;
		using (LocalFrame localFrame = new(this, capacity))
		{
			this._cache.CheckJniError();
			result = func(state);
			localResult = localFrame.GetLocalResult(result, out globalRef);
		}
		this._cache.CreateLocalRef(globalRef, localResult);
		return result;
	}

	Int32 IEqualityComparer<JEnvironment>.GetHashCode(JEnvironment obj) => obj._cache.GetHashCode();
	Boolean IEqualityComparer<JEnvironment>.Equals(JEnvironment? x, JEnvironment? y) => x?.Equals(y) ?? y is null;
	Boolean IEquatable<IEnvironment>.Equals(IEnvironment? other) => this.Reference == other?.Reference;
	Boolean IEquatable<JEnvironment>.Equals(JEnvironment? other)
		=> other is not null && this._cache.Equals(other._cache);

	/// <inheritdoc/>
	public override Boolean Equals(Object? obj)
		=> (obj is JEnvironment other && this._cache.Equals(other._cache)) ||
			(obj is IEnvironment env && this.Reference == env.Reference);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._cache.GetHashCode();

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>The <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.</returns>
	public static IEnvironment GetEnvironment(JEnvironmentRef reference)
	{
		JVirtualMachine vm = (JVirtualMachine)EnvironmentCache.GetVirtualMachine(reference);
		return vm.GetEnvironment(reference);
	}

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