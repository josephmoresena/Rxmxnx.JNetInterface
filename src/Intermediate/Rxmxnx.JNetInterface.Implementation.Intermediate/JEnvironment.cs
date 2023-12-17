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
		=> this._cache = new((JVirtualMachine)vm, envRef, new(this));

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
	private Boolean IsSame(JObjectLocalRef localRef, JObjectLocalRef otherRef)
	{
		IsSameObjectDelegate isSameObject = this._cache.GetDelegate<IsSameObjectDelegate>();
		Byte result = isSameObject(this._cache.Reference, localRef, otherRef);
		this._cache.CheckJniError();
		return result == JBoolean.TrueValue;
	}

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
	/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	internal JClassObject? GetClass(JClassLocalRef classRef)
	{
		if (classRef.Value == default) return default;
		//String hash = jClass.Hash;
		return default;
	}
	/// <summary>
	/// Retrieves a global reference for given class name.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	internal JGlobalRef GetClassGlobalRef(CString className)
	{
		JClassLocalRef classRef = className.WithSafeFixed(this._cache, JEnvironmentCache.FindClass);
		if (classRef.Value == default) this._cache.CheckJniError();
		try
		{
			JGlobalRef globalRef = this._cache.CreateGlobalRef(classRef.Value);
			if (classRef.Value == default) this._cache.CheckJniError();
			return globalRef;
		}
		finally
		{
			this._cache.DeleteLocalRef(classRef.Value);
		}
	}
	/// <summary>
	/// Deletes <paramref name="globalRef"/>.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	internal void DeleteGlobalRef(JGlobalRef globalRef)
	{
		DeleteGlobalRefDelegate deleteGlobalRef = this._cache.GetDelegate<DeleteGlobalRefDelegate>();
		deleteGlobalRef(this.Reference, globalRef);
	}
	/// <summary>
	/// Deletes <paramref name="weakRef"/>.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	internal void DeleteWeakGlobalRef(JWeakRef weakRef)
	{
		DeleteWeakGlobalRefDelegate deleteWeakGlobalRef = this._cache.GetDelegate<DeleteWeakGlobalRefDelegate>();
		deleteWeakGlobalRef(this.Reference, weakRef);
	}
	/// <summary>
	/// Retrieves field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal JFieldId GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
	{
		JFieldId fieldId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetFieldId);
		if (fieldId == default) this._cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves static field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal JFieldId GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
	{
		JFieldId fieldId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetStaticFieldId);
		if (fieldId == default) this._cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	internal JMethodId GetMethodId(JMethodDefinition definition, JClassLocalRef classRef)
	{
		JMethodId methodId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetMethodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves static method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal JMethodId GetStaticMethodId(JMethodDefinition definition, JClassLocalRef classRef)
	{
		JMethodId methodId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetStaticMethodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}

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
	/// Creates a new global reference to <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JGlobalRef CreateGlobalRef(JReferenceObject jLocal)
		=> this._cache.CreateGlobalRef(jLocal.As<JObjectLocalRef>());

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

	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
	/// <summary>
	/// Retrieves field identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private static JFieldId GetFieldId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetFieldIdDelegate getFieldId = args.env._cache.GetDelegate<GetFieldIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getFieldId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
	/// <summary>
	/// Retrieves static field identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private static JFieldId GetStaticFieldId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetStaticFieldIdDelegate getStaticFieldId = args.env._cache.GetDelegate<GetStaticFieldIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getStaticFieldId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
	/// <summary>
	/// Retrieves method identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private static JMethodId GetMethodId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetMethodIdDelegate getMethodId = args.env._cache.GetDelegate<GetMethodIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getMethodId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
	/// <summary>
	/// Retrieves static method identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private static JMethodId GetStaticMethodId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetStaticMethodIdDelegate getStaticMethodId = args.env._cache.GetDelegate<GetStaticMethodIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getStaticMethodId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
}