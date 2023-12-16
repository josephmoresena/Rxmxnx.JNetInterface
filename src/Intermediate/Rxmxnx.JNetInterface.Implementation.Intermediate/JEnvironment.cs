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
	/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	internal JEnvironment(IVirtualMachine vm, JEnvironmentRef envRef)
		=> this._cache = new(vm, envRef, new(this, IDataType.GetMetadata<JClassObject>(), false));

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
		IsSameObjectDelegate isSameObject = this._cache.GetDelegate<IsSameObjectDelegate>();
		Byte result = isSameObject(this._cache.Reference, jRefObj.As<JObjectLocalRef>(),
		                           jRefOther.As<JObjectLocalRef>());
		this._cache.CheckJniError();
		return result == JBoolean.TrueValue;
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
		using (LocalFrame localFrame = new(this, capacity))
		{
			this._cache.CheckJniError();
			result = func(localFrame.Environment);
			this.CreateTempGlobalRef(result, out globalRef);
		}
		this.CreateLocalRef(globalRef, result);
		return result;
	}
	TResult IEnvironment.WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
	{
		TResult? result;
		JGlobalRef globalRef;
		using (LocalFrame localFrame = new(this, capacity))
		{
			this._cache.CheckJniError();
			result = func(state);
			this.CreateTempGlobalRef(result, out globalRef);
		}
		this.CreateLocalRef(globalRef, result);
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
	/// Deletes local reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	internal void Delete(JObjectLocalRef localRef)
	{
		if (!this.JniSecure()) return;
		DeleteLocalRefDelegate deleteLocalRef = this._cache.GetDelegate<DeleteLocalRefDelegate>();
		deleteLocalRef(this.Reference, localRef);
	}
	/// <summary>
	/// Sets current object cache.
	/// </summary>
	/// <param name="localCache">A <see cref="LocalCache"/> instance.</param>
	internal void SetObjectCache(LocalCache localCache) => this._cache.SetObjectCache(localCache);

	/// <summary>
	/// Creates a new local reference frame.
	/// </summary>
	/// <param name="capacity">Frame capacity.</param>
	/// <exception cref="InvalidOperationException"/>
	/// <exception cref="JniException"/>
	private void CreateLocalFrame(Int32 capacity)
	{
		if (!this.JniSecure()) throw new InvalidOperationException();
		PushLocalFrameDelegate pushLocalFrame = this._cache.GetDelegate<PushLocalFrameDelegate>();
		JResult jResult = pushLocalFrame(this.Reference, capacity);
		if (jResult != JResult.Ok) throw new JniException(jResult);
	}
	/// <summary>
	/// Creates a new local reference for <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="TResult">Result type.</typeparam>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	/// <param name="result">A <typeparamref name="TResult"/> instance.</param>
	private void CreateLocalRef<TResult>(JGlobalRef globalRef, TResult result)
	{
		if (globalRef == default) return;
		try
		{
			NewLocalRefDelegate newLocalRef = this._cache.GetDelegate<NewLocalRefDelegate>();
			JObjectLocalRef localRef = newLocalRef(this.Reference, globalRef.Value);
			JLocalObject jLocal = this._cache.Register(result as JLocalObject)!;
			jLocal.SetValue(localRef);
		}
		finally
		{
			DeleteGlobalRefDelegate deleteGlobalRef = this._cache.GetDelegate<DeleteGlobalRefDelegate>();
			deleteGlobalRef(this.Reference, globalRef);
		}
	}
	/// <summary>
	/// Creates a new global reference to <paramref name="result"/>.
	/// </summary>
	/// <typeparam name="TResult">Result type.</typeparam>
	/// <param name="result">A <typeparamref name="TResult"/> instance.</param>
	/// <param name="globalRef">Output. A temporal <see cref="JGlobalRef"/> reference.</param>
	private void CreateTempGlobalRef<TResult>(TResult result, out JGlobalRef globalRef)
	{
		globalRef = default;
		if (result is not JLocalObject { IsDefault: false, } jLocal ||
		    (jLocal as ILocalObject).Lifetime.HasValidGlobal<JGlobal>()) return;
		NewGlobalRefDelegate newGlobalRef = this._cache.GetDelegate<NewGlobalRefDelegate>();
		globalRef = newGlobalRef(this.Reference, jLocal.As<JObjectLocalRef>());
		this._cache.CheckJniError();
	}

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>The <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.</returns>
	public static IEnvironment GetEnvironment(JEnvironmentRef reference)
	{
		IVirtualMachine vm = JEnvironmentCache.GetVirtualMachine(reference);
		return vm.GetEnvironment(reference);
	}

	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
}