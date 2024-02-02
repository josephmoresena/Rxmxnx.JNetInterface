namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// This record stores cache for a <see cref="JEnvironment"/> instance.
	/// </summary>
	private sealed partial record EnvironmentCache : LocalMainClasses
	{
		/// <inheritdoc cref="JEnvironment.Reference"/>
		public readonly JEnvironmentRef Reference;
		/// <summary>
		/// Managed thread.
		/// </summary>
		public readonly Thread Thread;
		/// <inheritdoc cref="IEnvironment.Version"/>
		public readonly Int32 Version;

		/// <inheritdoc cref="JEnvironment.VirtualMachine"/>
		public readonly JVirtualMachine VirtualMachine;

		/// <summary>
		/// Ensured capacity.
		/// </summary>
		public Int32? Capacity => this._objects.Capacity;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> instance.</param>
		public EnvironmentCache(JVirtualMachine vm, JEnvironment env, JEnvironmentRef envRef) : base(env)
		{
			this._env = env;

			this.VirtualMachine = vm;
			this.Reference = envRef;
			this.Version = EnvironmentCache.GetVersion(envRef);
			this.Thread = Thread.CurrentThread;

			Task.Factory.StartNew(EnvironmentCache.FinalizeCache, this, this._cancellation.Token);
			this.LoadMainClasses();
		}

		/// <summary>
		/// Retrieves a <typeparamref name="TDelegate"/> instance for <typeparamref name="TDelegate"/>.
		/// </summary>
		/// <typeparam name="TDelegate">Type of method delegate.</typeparam>
		/// <returns>A <typeparamref name="TDelegate"/> instance.</returns>
		public TDelegate GetDelegate<TDelegate>() where TDelegate : Delegate
		{
			ValidationUtilities.ThrowIfDifferentThread(this.Thread);
			Type typeOfT = typeof(TDelegate);
			Int32 index = EnvironmentCache.delegateIndex[typeOfT];
			IntPtr ptr = this.GetPointer(index);
			return this._delegateCache.GetDelegate<TDelegate>(ptr);
		}
		/// <summary>
		/// Ensure local capacity to <paramref name="capacity"/>.
		/// </summary>
		/// <param name="capacity">Top of local references.</param>
		/// <exception cref="JniException"/>
		public void EnsureLocalCapacity(Int32 capacity)
		{
			if (capacity <= 0) return;
			ValidationUtilities.ThrowIfDifferentThread(this.Thread);
			EnsureLocalCapacityDelegate ensureLocalCapacity = this.GetDelegate<EnsureLocalCapacityDelegate>();
			JResult result = ensureLocalCapacity(this.Reference, capacity);
			if (result != JResult.Ok)
				throw new JniException(result);
			this._objects.Capacity = capacity;
		}
		/// <summary>
		/// Checks JNI occurred error.
		/// </summary>
		public void CheckJniError()
		{
			ExceptionOccurredDelegate exceptionOccurred = this.GetDelegate<ExceptionOccurredDelegate>();
			JThrowableLocalRef throwableRef = exceptionOccurred(this.Reference);
			if (throwableRef.Value == default) return;
			try
			{
				ExceptionClearDelegate exceptionClear = this.GetDelegate<ExceptionClearDelegate>();
				exceptionClear(this.Reference);
				using LocalFrame frame = new(this._env, 10);
				JClassLocalRef classRef = this._env.GetObjectClass(throwableRef.Value);
				JClassObject jClass = this.AsClassObject(classRef);
				String message = EnvironmentCache.GetThrowableMessage(jClass, throwableRef);
				ThrowableObjectMetadata objectMetadata = new(jClass, message);
				JThrowableTypeMetadata throwableMetadata =
					MetadataHelper.GetMetadata(jClass.Hash) as JThrowableTypeMetadata ??
					(JThrowableTypeMetadata)MetadataHelper.GetMetadata<JThrowableObject>();
				JGlobalRef globalRef = this.CreateGlobalRef(throwableRef.Value);
				JGlobal jGlobalThrowable = new(this.VirtualMachine, objectMetadata, false, globalRef);
				throw throwableMetadata.CreateException(jGlobalThrowable, message);
			}
			finally
			{
				ThrowDelegate jThrow = this.GetDelegate<ThrowDelegate>();
				jThrow(this.Reference, throwableRef);
			}
		}
		/// <summary>
		/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference)
		{
			using JStringObject jString = JClassObject.GetClassName(this._env, classRef, out Boolean isPrimitive);
			using JNativeMemory<Byte> utf8Text = jString.GetNativeUtf8Chars();
			JClassObject jClass = isPrimitive ?
				this.GetPrimitiveClass(utf8Text.Values) :
				this.GetClass(utf8Text.Values, keepReference ? classRef : default);
			if (keepReference && jClass.InternalReference == default) jClass.SetValue(classRef);
			return jClass;
		}
		/// <inheritdoc cref="IEnvironment.JniSecure"/>
		public Boolean JniSecure() => this.Thread.ManagedThreadId == Environment.CurrentManagedThreadId;
		/// <inheritdoc cref="JEnvironment.SetObjectCache(LocalCache?)"/>
		public void SetObjectCache(LocalCache localCache) { this._objects = localCache; }
		/// <summary>
		/// Retrieves local cache.
		/// </summary>
		public LocalCache GetLocalCache() => this._objects;
		/// <summary>
		/// Class cache cache.
		/// </summary>
		public ClassCache GetClassCache() => this._classes;
		/// <summary>
		/// Release all references.
		/// </summary>
		public void FreeReferences()
		{
			this._objects.ClearCache(this._env, true);
			this._cancellation.Cancel();
		}
		/// <summary>
		/// Creates a new local reference for <paramref name="result"/>.
		/// </summary>
		/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
		/// <param name="result">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="deleteGlobal">Indicates whether global reference must be deleted.</param>
		public void CreateLocalRef(JGlobalRef globalRef, JLocalObject? result, Boolean deleteGlobal = true)
		{
			if (globalRef == default || result is not null) return;
			try
			{
				JObjectLocalRef localRef = this._env.CreateLocalRef(globalRef.Value);
				JLocalObject jLocal = this.Register(result)!;
				jLocal.SetValue(localRef);
			}
			finally
			{
				if (deleteGlobal) this._env.DeleteGlobalRef(globalRef);
			}
		}
		/// <summary>
		/// Creates a global reference from <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
		public JGlobalRef CreateGlobalRef(JObjectLocalRef localRef)
		{
			NewGlobalRefDelegate newGlobalRef = this.GetDelegate<NewGlobalRefDelegate>();
			JGlobalRef globalRef = newGlobalRef(this.Reference, localRef);
			if (globalRef == default) this.CheckJniError();
			return globalRef;
		}
		/// <summary>
		/// Removes <paramref name="jLocal"/> from current thread.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		public void Remove(JLocalObject? jLocal)
		{
			if (jLocal is null) return;
			this._objects.Remove(jLocal.InternalReference);
			if (jLocal is JClassObject)
				this._classes.Unload(
					NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(jLocal.InternalReference));
		}
		/// <inheritdoc cref="JEnvironment.LoadClass(JClassObject?)"/>
		public void LoadClass(JClassObject? jClass)
		{
			if (jClass is null) return;
			this._classes[jClass.Hash] = jClass;
			this.VirtualMachine.LoadGlobal(jClass);
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> reference for primitive class.
		/// </summary>
		/// <param name="signature">Primitive signature.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public JClassLocalRef FindPrimitiveClass(Byte signature)
		{
			using JClassObject wrapperClass = signature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => this.GetClass<JBooleanObject>(),
				UnicodePrimitiveSignatures.ByteSignatureChar => this.GetClass<JByteObject>(),
				UnicodePrimitiveSignatures.CharSignatureChar => this.GetClass<JCharacterObject>(),
				UnicodePrimitiveSignatures.DoubleSignatureChar => this.GetClass<JDoubleObject>(),
				UnicodePrimitiveSignatures.FloatSignatureChar => this.GetClass<JFloatObject>(),
				UnicodePrimitiveSignatures.IntSignatureChar => this.GetClass<JIntegerObject>(),
				UnicodePrimitiveSignatures.LongSignatureChar => this.GetClass<JLongObject>(),
				UnicodePrimitiveSignatures.ShortSignatureChar => this.GetClass<JShortObject>(),
				_ => this.GetClass<JVoidObject>(),
			};
			JObjectLocalRef localRef =
				this.GetStaticObjectField(wrapperClass, InternalFunctionCache.PrimitiveTypeDefinition);
			return NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(in localRef);
		}

		/// <summary>
		/// Retrieves a <see cref="JVirtualMachine"/> from given <paramref name="jEnv"/>.
		/// </summary>
		/// <param name="jEnv">A <see cref="JEnvironmentRef"/> reference.</param>
		/// <returns>A <see cref="IVirtualMachine"/> instance.</returns>
		public static IVirtualMachine GetVirtualMachine(JEnvironmentRef jEnv)
		{
			Int32 index = EnvironmentCache.delegateIndex[typeof(GetVirtualMachineDelegate)];
			GetVirtualMachineDelegate getVirtualMachine =
				jEnv.Reference.Reference[index].GetUnsafeDelegate<GetVirtualMachineDelegate>()!;
			JResult result = getVirtualMachine(jEnv, out JVirtualMachineRef vmRef);
			if (result == JResult.Ok)
				return JVirtualMachine.GetVirtualMachine(vmRef);
			throw new JniException(result);
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="classNameCtx"/> as class name.
		/// </summary>
		/// <param name="classNameCtx">A <see cref="IReadOnlyFixedMemory"/> instance.</param>
		/// <param name="cache">Current <see cref="EnvironmentCache"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public static JClassLocalRef FindClass(in IReadOnlyFixedMemory classNameCtx, EnvironmentCache cache)
		{
			FindClassDelegate findClass = cache.GetDelegate<FindClassDelegate>();
			JClassLocalRef result = findClass(cache.Reference, (ReadOnlyValPtr<Byte>)classNameCtx.Pointer);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}
	}
}