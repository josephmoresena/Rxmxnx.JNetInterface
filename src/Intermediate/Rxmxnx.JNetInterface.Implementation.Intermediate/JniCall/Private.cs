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

	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JClassObject"/> result.</param>
	/// <typeparam name="TResult">Type of reference result.</typeparam>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	private TResult FinalizeCall<TResult>(JLocalObject? result) where TResult : unmanaged, IObjectReferenceType<TResult>
	{
		JObjectLocalRef jniResult = this.FinalizeCall(result);
		return NativeUtilities.Transform<JObjectLocalRef, TResult>(in jniResult);
	}

	public readonly ref partial struct Builder
	{
		/// <summary>
		/// <see cref="JniCall"/> instance.
		/// </summary>
		private readonly JniCall _call;

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
			return JLocalObject.Create<TObject>(jClass, metadata, localRef);
		}
	}
}