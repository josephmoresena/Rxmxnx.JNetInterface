namespace Rxmxnx.JNetInterface;

public readonly ref partial struct JniCall
{
	/// <summary>
	/// Current <see cref="IEnvironment"/> instance.
	/// </summary>
	private readonly JEnvironment _env;
	/// <summary>
	/// Current <see cref="CallFrame"/> instance.
	/// </summary>
	private readonly CallFrame _cache;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	private JniCall(JEnvironmentRef envRef)
	{
		this._env = (JEnvironment)JEnvironment.GetEnvironment(envRef);
		this._cache = new(this._env);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	private JniCall(JVirtualMachine vm, JEnvironmentRef envRef)
	{
		this._env = vm.GetEnvironment(envRef);
		this._cache = new(this._env);
	}
	
	public readonly ref partial struct Builder
	{
		/// <summary>
		/// <see cref="JniCall"/> instance.
		/// </summary>
		private readonly JniCall _call;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="call">Current <see cref="JniCall"/> instance.</param>
		private Builder(JniCall call) => this._call = call;
		
		/// <inheritdoc cref="JEnvironment.GetObjectClass(JObjectLocalRef)"/>
		private JClassObject GetObjectClass(JObjectLocalRef localRef)
		{
			JEnvironment env = this._call._env;
			JClassLocalRef classRef = env.GetObjectClass(localRef);
			try
			{
				return env.GetClass(classRef);
			}
			finally
			{
				env.DeleteLocalRef(classRef.Value);
			}
		}
		/// <summary>
		/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.</returns>
		private JLocalObject CreateInitialObject(JObjectLocalRef localRef)
		{
			JEnvironment env = this._call._env;
			JClassObject jClass = this.GetObjectClass(localRef);
			if (!jClass.Name.AsSpan().SequenceEqual(env.ClassObject.Name))
			{
				JLocalObject result = new(env, localRef, false, jClass);
				this._call._cache[localRef] = result.Lifetime;
				return result;
			}
			JClassLocalRef classRef = NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(in localRef);
			return this.CreateInitialClass(classRef);
		}
		/// <summary>
		/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>Initial <see cref="JClassObject"/> instance for <paramref name="classRef"/>.</returns>
		private JClassObject CreateInitialClass(JClassLocalRef classRef)
		{
			JEnvironment env = this._call._env;
			JClassObject result = env.GetClass(classRef);
			result.SetValue(classRef);
			this._call._cache[classRef.Value] = result.Lifetime;
			return result;
		}
		/// <summary>
		/// Retrieves initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
		private TObject CreateInitialObject<TObject>(JObjectLocalRef localRef)
			where TObject : JLocalObject, IReferenceType<TObject>
		{
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TObject>();
			if (metadata.Modifier == JTypeModifier.Final) return this.CreateFinalObject<TObject>(localRef);
			
			JLocalObject jLocalTemp = this.CreateInitialObject(localRef);
			try
			{
				return (TObject)metadata.ParseInstance(jLocalTemp);
			}
			finally
			{
				if (jLocalTemp is not JClassObject) jLocalTemp.Dispose();
			}
		}
		/// <summary>
		/// Retrieves initial final <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
		private TObject CreateFinalObject<TObject>(JObjectLocalRef localRef)
			where TObject : JLocalObject, IReferenceType<TObject>
		{
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TObject>();
			JClassObject jClass = this._call._env.GetClass<TObject>();
			using JLocalObject jLocalTemp = new(jClass, localRef);
			return (TObject)metadata.ParseInstance(jLocalTemp);
		}
	}
}