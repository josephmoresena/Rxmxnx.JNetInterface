namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JEnvironment : IEnvironment, IEquatable<JEnvironment>, IEquatable<IEnvironment>
{
	/// <summary>
	/// <see cref="JEnvironment"/> cache.
	/// </summary>
	private readonly JEnvironmentCache _cache;

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
	private JEnvironment(JEnvironmentCache cache) => this._cache = cache;

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
	IAccessProvider IEnvironment.AccessProvider => this._cache;
	IClassProvider IEnvironment.ClassProvider => this._cache;
	IReferenceProvider IEnvironment.ReferenceProvider => this._cache;
	IStringProvider IEnvironment.StringProvider => this._cache;
	IArrayProvider IEnvironment.ArrayProvider => this._cache;

	JReferenceType IEnvironment.GetReferenceType(JObject jObject)
	{
		if (jObject is not JReferenceObject jRefObj || jRefObj.IsDefault || jRefObj.IsDummy)
			return JReferenceType.InvalidRefType;
		GetObjectRefTypeDelegate getObjectRefType = this._cache.GetDelegate<GetObjectRefTypeDelegate>();
		JReferenceType result = getObjectRefType(this._cache.Reference, jRefObj.As<JObjectLocalRef>());
		this._cache.CheckJniError();
		if (result == JReferenceType.InvalidRefType)
		{
			if (jRefObj is JGlobalBase jGlobal) (this.VirtualMachine as JVirtualMachine)!.Remove(jGlobal);
			else this._cache.Remove(jRefObj as JLocalObject);
			jRefObj.ClearValue();
		}
		else if (this.IsSame(jRefObj.As<JObjectLocalRef>(), default))
		{
			if (jRefObj is JGlobalBase jGlobal) this._cache.Unload(jGlobal);
			else this._cache.Unload(jRefObj as JLocalObject);
			jRefObj.ClearValue();
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
		return this.IsSame(jRefObj.As<JObjectLocalRef>(), jRefOther.As<JObjectLocalRef>());
	}
	/// <inheritdoc/>
	public Boolean JniSecure() => this._cache.JniSecure();
	void IEnvironment.WithFrame(Int32 capacity, Action<IEnvironment> action)
	{
		using LocalFrame localFrame = new(this, capacity);
		this._cache.CheckJniError();
		action(localFrame.Environment);
	}
	void IEnvironment.WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
	{
		using LocalFrame localFrame = new(this, capacity);
		this._cache.CheckJniError();
		action(state);
	}
	TResult IEnvironment.WithFrame<TResult>(Int32 capacity, Func<IEnvironment, TResult> func)
	{
		TResult? result;
		JGlobalRef globalRef;
		JLocalObject? localResult;
		using (LocalFrame localFrame = new(this, capacity))
		{
			this._cache.CheckJniError();
			result = func(localFrame.Environment);
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
		JVirtualMachine vm = (JVirtualMachine)JEnvironmentCache.GetVirtualMachine(reference);
		return vm.GetEnvironment(reference);
	}
}