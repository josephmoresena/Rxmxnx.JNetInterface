namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Registers a <typeparamref name="TObject"/> in current <see cref="IEnvironment"/> instance.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jObject">A <see cref="IDataType{TObject}"/> instance.</param>
		/// <returns>Registered <see cref="IDataType{TObject}"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jObject))]
		private TObject? Register<TObject>(TObject? jObject) where TObject : IDataType<TObject>
		{
			ValidationUtilities.ThrowIfDummy(jObject as JReferenceObject);
			this.LoadClass(jObject as JClassObject);
			JLocalObject? jLocal = jObject as JLocalObject;
			if (!JObject.IsNullOrDefault(jLocal))
				this._objects[jLocal.As<JObjectLocalRef>()] = jLocal.Lifetime.GetCacheable();
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
	}
}