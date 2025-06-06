namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Creates a <see cref="JWeakRef"/> from <paramref name="jObject"/>.
		/// </summary>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		/// <returns>A <see cref="JWeakRef"/> reference.</returns>
		private JWeakRef CreateWeakGlobalRef(JReferenceObject jObject)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			ImplementationValidationUtilities.ThrowIfDefault(jObject);
			using LocalFrame _ = new(this._env, IVirtualMachine.GetObjectClassCapacity);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jObject);
			return this.CreateWeakGlobalRef(localRef);
		}
		/// <summary>
		/// Creates a <see cref="JWeakRef"/> from <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="JWeakRef"/> reference.</returns>
		private JWeakRef CreateWeakGlobalRef(JClassObject jClass)
		{
			JClassLocalRef classRef = jClass.As<JClassLocalRef>();
			if (!classRef.IsDefault) return this.CreateWeakGlobalRef(classRef.Value);
			try
			{
				classRef = this.FindClass(jClass);
				JTrace.ReloadClass(classRef, jClass);
				return this.CreateWeakGlobalRef(classRef.Value);
			}
			finally
			{
				if (!classRef.IsDefault)
				{
					this._env.DeleteLocalRef(classRef.Value);
					JTrace.ClearClass(classRef, jClass);
				}
			}
		}
		/// <summary>
		/// Creates a <see cref="JWeakRef"/> from <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>A <see cref="JWeakRef"/> reference.</returns>
		private JWeakRef CreateWeakGlobalRef(JObjectLocalRef localRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewWeakGlobalRefInfo);
			JWeakRef weakRef = nativeInterface.WeakGlobalFunctions.NewWeakGlobalRef.NewRef(this.Reference, localRef);
			JTrace.CreateGlobalRef(localRef, weakRef);
			if (weakRef == default) this.CheckJniError();
			return weakRef;
		}
		/// <summary>
		/// Registers a <typeparamref name="TObject"/> in current <see cref="IEnvironment"/> instance.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jObject">A <see cref="IDataType{TObject}"/> instance.</param>
		/// <returns>Registered <see cref="IDataType{TObject}"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jObject))]
		private TObject? Register<TObject>(TObject? jObject) where TObject : IDataType<TObject>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject as JReferenceObject);
			JTrace.RegisterObject(jObject as JReferenceObject, this._objects.Id, this._objects.Name);
			this.LoadClass(jObject as JClassObject);
			if (jObject is ILocalObject jLocal && jLocal.LocalReference != default)
				this._objects[jLocal.LocalReference] = jLocal.Lifetime.GetCacheable();
			return jObject;
		}
		/// <summary>
		/// Loads <see cref="JGlobal"/> instance for <paramref name="jClass"/>
		/// </summary>
		/// <param name="jClass">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <see cref="JGlobal"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jClass))]
		private JGlobal? LoadGlobal(JClassObject? jClass)
		{
			if (jClass is null) return default;
			JGlobal result = this.VirtualMachine.LoadGlobal(jClass);
			JClassLocalRef classRef = jClass.As<JClassLocalRef>();
			switch (result.IsDefault)
			{
				case true when classRef.IsDefault:
					try
					{
						classRef = this.FindClass(jClass);
						JTrace.ReloadClass(classRef, jClass);
						this.ReloadGlobal(result, classRef.Value);
					}
					finally
					{
						if (!classRef.IsDefault)
						{
							this._env.DeleteLocalRef(classRef.Value);
							JTrace.ClearClass(classRef, jClass);
						}
					}
					break;
				case true:
					this.ReloadGlobal(result, classRef.Value);
					break;
			}
			return result;
		}
		/// <summary>
		/// Reloads <paramref name="jGlobal"/> using <paramref name="localRef"/>.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> to reload.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		private void ReloadGlobal(JGlobal jGlobal, JObjectLocalRef localRef)
		{
			JGlobalRef globalRef = this.CreateGlobalRef(localRef);
			jGlobal.SetValue(globalRef);
		}
		/// <summary>
		/// Reloads <paramref name="jGlobal"/> with a new global reference.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		private void ReloadGlobal(JGlobal jGlobal, JLocalObject jLocal, INativeTransaction jniTransaction)
		{
			if (!jGlobal.IsDefault) return;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			jGlobal.SetValue(this.CreateGlobalRef(localRef));
		}
		/// <summary>
		/// Unloads <paramref name="localRef"/>.
		/// </summary>
		/// <param name="isRegistered">
		/// Indicates whether <paramref name="localRef"/> is registered in current thread.
		/// </param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference to unload.</param>
		private void Unload(Boolean isRegistered, JObjectLocalRef localRef)
		{
			if (isRegistered && this._env.IsAttached && this.VirtualMachine.IsAlive)
				this._env.DeleteLocalRef(localRef);
			JTrace.Unload(isRegistered, this._env.IsAttached, this.VirtualMachine.IsAlive, localRef, this._objects.Id,
			              this._objects.Name);
		}
		/// <summary>
		/// Unloads <paramref name="weakRef"/>.
		/// </summary>
		/// <param name="weakRef">A <see cref="JWeakRef"/> reference to unload.</param>
		private void Unload(JWeakRef weakRef)
		{
			if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				this._env.DeleteWeakGlobalRef(weakRef);
			JTrace.UnloadGlobal(this._env.IsAttached, this.VirtualMachine.IsAlive, weakRef);
		}
		/// <summary>
		/// Unloads <paramref name="globalRef"/>.
		/// </summary>
		/// <param name="globalRef">A <see cref="JGlobalRef"/> reference to unload.</param>
		private void Unload(JGlobalRef globalRef)
		{
			if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				this._env.DeleteGlobalRef(globalRef);
			JTrace.UnloadGlobal(this._env.IsAttached, this.VirtualMachine.IsAlive, globalRef);
		}
		/// <summary>
		/// Creates a new local reference for <paramref name="result"/>.
		/// </summary>
		/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
		/// <param name="result">A <see cref="JLocalObject"/> instance.</param>
		private void CreateLocalRef<TObjectRef>(TObjectRef globalRef, JLocalObject? result)
			where TObjectRef : unmanaged, INativeType, IWrapper<JObjectLocalRef>,
			IEqualityOperators<TObjectRef, TObjectRef, Boolean>
		{
			if (globalRef == default || result is null) return;
			JObjectLocalRef localRef = this._env.CreateLocalRef(globalRef);
			if (localRef == default) this.CheckJniError();
			result.SetValue(localRef);
			this.Register(result);
		}
	}
}