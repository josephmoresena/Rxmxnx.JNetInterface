namespace Rxmxnx.JNetInterface;

/// <summary>
/// Internal <see cref="IEnvironment"/> value.
/// </summary>
internal readonly struct EnvironmentValue
{
	/// <summary>
	/// A <see cref="EnvironmentCore"/> instance.
	/// </summary>
	public readonly EnvironmentCore Core;

	/// <inheritdoc cref="IEnvironment.LocalCapacity"/>
	public Int32? LocalCapacity
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.Core.Capacity;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => this.Core.EnsureLocalCapacity(value.GetValueOrDefault());
	}
	/// <inheritdoc cref="ILocalCacheOwner.LocalCache"/>
	public LocalCache LocalCache
	{
		get => this.Core.GetLocalCache();
		set
		{
			JTrace.SetObjectCache(value.Id, value.Name);
			this.Core.SetObjectCache(value);
		}
	}
	/// <inheritdoc cref="IEnvironment.UsableStackBytes"/>
	public Int32 UsableStackBytes
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.Core.MaxStackBytes;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => this.Core.SetUsableStackBytes(value);
	}
	/// <inheritdoc cref="IEnvironment.PendingException"/>
	public ThrowableException? PendingException
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => this.GetThrown();
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => this.SetThrown(value);
	}

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	private EnvironmentValue(EnvironmentCore core) => this.Core = core;

	/// <inheritdoc cref="IEnvironment.JniSecure()"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean JniSecure() => this.Core.JniSecure();
	/// <inheritdoc cref="IEnvironment.GetReferenceType(JObject)"/>
	public JReferenceType GetReferenceType(JObject jObject)
	{
		if (jObject is not JReferenceObject jRefObj || jRefObj.IsDefault || jRefObj.IsProxy)
			return JReferenceType.InvalidRefType;
		using INativeTransaction jniTransaction = this.Core.Host.MemoryManager.CreateTransaction(1);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JReferenceType result = this.Core.GetReferenceType(localRef);
		if (result == JReferenceType.InvalidRefType)
		{
			if (jRefObj is JGlobalBase jGlobal)
				this.Core.Host.GlobalManager.Remove(jGlobal);
			else
				this.Core.Remove(jRefObj as JLocalObject);
			jRefObj.ClearValue();
		}
		else if (this.Core.IsSame(jRefObj.As<JObjectLocalRef>(), default))
		{
			if (jRefObj is JGlobalBase jGlobal)
				this.Core.Unload(jGlobal);
			else
				this.Core.Unload(jRefObj as JLocalObject ?? ILocalViewObject.GetObject(jRefObj as ILocalViewObject));
		}

		return result;
	}
	/// <inheritdoc cref="IEnvironment.IsSameObject(JObject, JObject)"/>
	public Boolean IsSameObject(JObject jObject, JObject? jOther)
	{
		if (Object.ReferenceEquals(jObject, jOther)) return true;
		if (jObject is not JReferenceObject jRefObj || jOther is not JReferenceObject jRefOther)
			return EnvironmentCore.EqualEquatable(jObject as IEquatable<IPrimitiveType>, jOther as IPrimitiveType) ??
				EnvironmentCore.EqualEquatable(jOther as IEquatable<IPrimitiveType>, jObject as IPrimitiveType) ??
				EnvironmentCore.EqualEquatable(jObject as IEquatable<JPrimitiveObject>, jOther as JPrimitiveObject) ??
				EnvironmentCore.EqualEquatable(jOther as IEquatable<JPrimitiveObject>, jObject as JPrimitiveObject) ??
				false;

		ImplementationValidationUtilities.ThrowIfProxy(jRefObj);
		ImplementationValidationUtilities.ThrowIfProxy(jRefOther);
		using INativeTransaction jniTransaction = this.Core.Host.MemoryManager.CreateTransaction(2);
		JObjectLocalRef localRef = jniTransaction.Add(jRefObj);
		JObjectLocalRef otherLocalRef = jniTransaction.Add(jRefOther);
		return this.Core.IsSame(localRef, otherLocalRef);
	}
	/// <inheritdoc cref="IMainClassLoader.GetMainClassGlobalRef(ITypeInformation)"/>
	public JGlobalRef GetMainClassGlobalRef(ITypeInformation typeInformation)
	{
		JClassLocalRef classRef = this.Core.FindMainClass(typeInformation.ClassName, typeInformation.Signature);
		return EnvironmentCore.GetMainClassGlobalRef(this.Core, typeInformation, classRef);
	}
	/// <inheritdoc cref="IMainClassLoader.GetPrimitiveMainClassGlobalRef(ClassObjectMetadata, JGlobalBase)"/>
	public JGlobalRef GetPrimitiveMainClassGlobalRef(ClassObjectMetadata classMetadata, JGlobalBase? wClassGlobal)
	{
		Byte signature = classMetadata.ClassSignature[0];
		String className = ClassNameHelper.GetPrimitiveClassName(signature);
		JClassLocalRef classRef = !JObject.IsNullOrDefault(wClassGlobal) ?
			this.Core.FindPrimitiveClass(wClassGlobal.As<JClassLocalRef>(), className) :
			this.Core.FindPrimitiveClass(signature);
		return EnvironmentCore.GetMainClassGlobalRef(this.Core, classMetadata, classRef);
	}
	/// <inheritdoc cref="ILocalCacheOwner.CreateLocalFrame(Int32)"/>
	public void CreateLocalFrame(Int32 capacity)
	{
		ref readonly NativeInterface nativeInterface =
			ref this.Core.GetNativeInterface<NativeInterface>(NativeInterface.PushLocalFrameInfo);
		JResult result = nativeInterface.ReferenceFunctions.PushLocalFrame(this.Core.Reference, capacity);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
		this.Core.CheckJniError();
	}
	/// <inheritdoc cref="ILocalCacheOwner.DeleteLocalFrame(LocalFrame, JLocalObject)"/>
	public void DeleteLocalFrame(LocalFrame frame, JLocalObject? result)
	{
		this.Core.DeleteLocalFrame(result);
		JTrace.DeleteObjectCache(frame.Id, result);
	}
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="action">An action to invoke inside created new local reference.</param>
	public void WithFrame(ILocalCacheOwner owner, Int32 capacity, Action action)
	{
		using LocalFrame _ = new(owner, capacity);
		this.Core.CheckJniError();
		action();
	}
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="action">An action to invoke inside created new local reference.</param>
	public void WithFrame<TState>(ILocalCacheOwner owner, Int32 capacity, TState state, Action<TState> action)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		using LocalFrame _ = new(owner, capacity);
		this.Core.CheckJniError();
		action(state);
	}

	/// <summary>
	/// Retrieves the <see cref="ThrowableException"/> pending exception.
	/// </summary>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? GetThrown()
	{
		ThrowableException? jniException = this.Core.Thrown as ThrowableException;
		if (jniException is not null || this.Core.Thrown is null) return jniException;
		if (!this.Core.JniSecure(JniSafetyLevels.ErrorSafe) && this.Core.HasPendingException())
			// Do not throw if not pending JNI exception.
			throw this.Core.Thrown;
		return EnvironmentCore.ParseException(this.Core, this.Core.GetPendingException());
	}
	/// <summary>
	/// Sets <paramref name="throwableException"/> as pending exception.
	/// </summary>
	/// <param name="throwableException">A <see cref="ThrowableException"/> instance.</param>
	private void SetThrown(ThrowableException? throwableException)
	{
		if (throwableException is not null && Object.ReferenceEquals(CriticalException.Instance, this.Core.Thrown) &&
		    this.Core.HasPendingException())
			// Do not throw if there is no pending JNI exception or exception in the process of being cleared.
			throw this.Core.Thrown;
		this.Core.ThrowJniException(throwableException, false);
	}

	/// <summary>
	/// Defines an implicit conversion of a given <see cref="EnvironmentCore"/> to
	/// <see cref="EnvironmentValue"/>.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentValue"/> to implicitly convert.</param>
	public static implicit operator EnvironmentValue(EnvironmentCore core) => new(core);

	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="func">A function to execute inside created new local reference.</param>
	public static TResult WithFrame<TResult>(ILocalCacheOwner owner, Int32 capacity, Func<TResult> func)
	{
		using LocalFrame localFrame = new(owner, capacity);
		TResult result = func();
		localFrame.SetResult(result);
		return result;
	}
	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="owner">A <see langword="ILocalCacheOwner"/> instance.</param>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="func">A function to execute inside created new local reference.</param>
	public static TResult WithFrame<TResult, TState>(ILocalCacheOwner owner, Int32 capacity, TState state,
		Func<TState, TResult> func)
#if NET9_0_OR_GREATER
		where TState : allows ref struct
#endif
	{
		using LocalFrame localFrame = new(owner, capacity);
		TResult result = func(state);
		localFrame.SetResult(result);
		return result;
	}
}