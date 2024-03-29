namespace Rxmxnx.JNetInterface;

public readonly ref partial struct JNativeCallAdapter
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
	private JNativeCallAdapter(JEnvironmentRef envRef)
	{
		this._env = (JEnvironment)JEnvironment.GetEnvironment(envRef);
		this._cache = new(this._env);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	private JNativeCallAdapter(JVirtualMachine vm, JEnvironmentRef envRef)
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
		return TResult.FromReference(in jniResult);
	}

	public readonly ref partial struct Builder
	{
		/// <summary>
		/// <see cref="JNativeCallAdapter"/> instance.
		/// </summary>
		private readonly JNativeCallAdapter _callAdapter;

		/// <summary>
		/// Throws an exception if <paramref name="localRef"/> is not a local reference.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <exception cref="ArgumentException">
		/// Throws an exception if <paramref name="localRef"/> is not a local reference.
		/// </exception>
		private void ThrowIfNotLocalReference(JObjectLocalRef localRef)
		{
			JEnvironment env = this._callAdapter._env;
			if (env.GetReferenceType(localRef) != JReferenceType.LocalRefType)
				throw new ArgumentException("JNI call only allow local references.");
		}
		/// <summary>
		/// Throws an exception if <paramref name="localRef"/> is not a class reference.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <exception cref="ArgumentException">
		/// Throws an exception if <paramref name="localRef"/> is not a class reference.
		/// </exception>
		private void ThrowIfNotClassObject(JObjectLocalRef localRef)
		{
			JEnvironment env = this._callAdapter._env;
			JClassObject jClass = this.GetObjectClass(localRef, true);
			if (!jClass.Name.AsSpan().SequenceEqual(env.ClassObject.Name))
				throw new ArgumentException($"A {jClass.Name} instance is not {env.ClassObject.Name} instance.");
		}
		/// <inheritdoc cref="JEnvironment.GetObjectClass(JObjectLocalRef)"/>
		private JClassObject GetObjectClass(JObjectLocalRef localRef, Boolean validateReference = false)
		{
			JEnvironment env = this._callAdapter._env;
			if (validateReference) this.ThrowIfNotLocalReference(localRef);
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
			JClassObject jClass = this._callAdapter._env.GetClass<TObject>();
			TObject result;
			if (!JLocalObject.IsClassType<TObject>())
			{
				this.ThrowIfNotLocalReference(localRef);
				result = JLocalObject.Create<TObject>(jClass, metadata, localRef);
			}
			else
			{
				JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
				JClassObject jClassResult = this.CreateInitialClass(classRef, true);
				result = (TObject)metadata.ParseInstance(jClassResult);
				if (!Object.ReferenceEquals(result, jClassResult)) jClassResult.Dispose();
			}
			return result;
		}
	}
}