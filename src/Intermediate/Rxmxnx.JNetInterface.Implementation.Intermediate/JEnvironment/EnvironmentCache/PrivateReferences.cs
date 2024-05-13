namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Creates a <see cref="JWeakRef"/> from <paramref name="jObject"/>.
		/// </summary>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		/// <returns>A <see cref="JWeakRef"/> reference.</returns>
		private JWeakRef CreateWeakGlobalRef(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfProxy(jObject);
			ValidationUtilities.ThrowIfDefault(jObject);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			NewWeakGlobalRefDelegate newWeakGlobalRef = this.GetDelegate<NewWeakGlobalRefDelegate>();
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jObject);
			JWeakRef weakRef = newWeakGlobalRef(this.Reference, localRef);
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
			ValidationUtilities.ThrowIfProxy(jObject as JReferenceObject);
			JTrace.RegisterObject(jObject as JReferenceObject);
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
			JObjectLocalRef localRef = jClass.As<JObjectLocalRef>();
			switch (result.IsDefault)
			{
				case true when localRef == default:
					try
					{
						localRef = this.FindClass(jClass).Value;
						this.ReloadGlobal(result, localRef);
					}
					finally
					{
						if (localRef != default) this._env.DeleteLocalRef(localRef);
					}
					break;
				case true:
					this.ReloadGlobal(result, localRef);
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
			JTrace.Unload(isRegistered, this._env.IsAttached, this.VirtualMachine.IsAlive, localRef);
		}
		/// <summary>
		/// Unloads <paramref name="weakRef"/>.
		/// </summary>
		/// <param name="weakRef">A <see cref="JWeakRef"/> reference to unload.</param>
		private void Unload(JWeakRef weakRef)
		{
			if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				this._env.DeleteWeakGlobalRef(weakRef);
			JTrace.Unload(this._env.IsAttached, this.VirtualMachine.IsAlive, weakRef);
		}
		/// <summary>
		/// Unloads <paramref name="globalRef"/>.
		/// </summary>
		/// <param name="globalRef">A <see cref="JGlobalRef"/> reference to unload.</param>
		private void Unload(JGlobalRef globalRef)
		{
			if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				this._env.DeleteGlobalRef(globalRef);
			JTrace.Unload(this._env.IsAttached, this.VirtualMachine.IsAlive, globalRef);
		}
		/// <summary>
		/// Indicates whether <paramref name="jGlobal"/> is a main global object or default.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jGlobal"/> is main global object or default;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		private Boolean IsMainOrDefault(JGlobalBase jGlobal)
			=> jGlobal.IsDefault || this.IsMainGlobal(jGlobal as JGlobal);
	}
}