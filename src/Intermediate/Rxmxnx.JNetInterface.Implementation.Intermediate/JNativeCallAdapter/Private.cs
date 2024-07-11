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
		this._env = JEnvironment.GetEnvironment(envRef);
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
	private TResult FinalizeCall<TResult>(JLocalObject? result) where TResult : unmanaged, IObjectReferenceType
	{
		JObjectLocalRef jniResult = this.FinalizeCall(result);
		return NativeUtilities.Transform<JObjectLocalRef, TResult>(in jniResult);
	}

	public readonly ref partial struct Builder
	{
		/// <summary>
		/// <see cref="JNativeCallAdapter"/> instance.
		/// </summary>
		private readonly JNativeCallAdapter _callAdapter;

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
			Builder.ThrowIfNotLocalReference(env, localRef);
			JClassObject jClass = JVirtualMachine.CheckClassRefNativeCallEnabled ?
				JEnvironment.GetObjectClass(env, localRef) :
				env.ClassObject;
			if (!jClass.Name.AsSpan().SequenceEqual(env.ClassObject.Name))
				throw new ArgumentException($"A {jClass.Name} instance is not {env.ClassObject.Name} instance.");
		}
		/// <inheritdoc
		///     cref="JEnvironment.GetObjectClass(Rxmxnx.JNetInterface.Native.References.JObjectLocalRef,out Rxmxnx.JNetInterface.Types.Metadata.JReferenceTypeMetadata)"/>
		private JClassObject GetObjectClass(JObjectLocalRef localRef, out JReferenceTypeMetadata typeMetadata,
			Boolean validateReference = false)
		{
			JEnvironment env = this._callAdapter._env;
			if (validateReference) Builder.ThrowIfNotLocalReference(env, localRef);
			return env.GetObjectClass(localRef, out typeMetadata);
		}
		/// <summary>
		/// Retrieves initial final <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
		private TObject CreateFinalObject<TObject>(JObjectLocalRef localRef)
			where TObject : JReferenceObject, IReferenceType<TObject>
		{
			JEnvironment env = this._callAdapter._env;
			JReferenceTypeMetadata typeMetadata = (JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TObject>();
			JClassObject jClass = typeMetadata.GetClass(env);
			if (!JLocalObject.IsClassType<TObject>())
			{
				Builder.ThrowIfNotLocalReference(env, localRef);
				return (TObject)this.CreateObject(jClass, localRef, typeMetadata, typeMetadata);
			}
			JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
			return (TObject)(Object)this.CreateInitialClass(classRef, true);
		}
		/// <summary>
		/// Creates an object of type <paramref name="typeMetadata"/> using <paramref name="jClass"/> and
		/// <paramref name="localRef"/>.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classMetadata"><paramref name="jClass"/> type metadata.</param>
		/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
		/// <returns>
		/// Creates an object of type <paramref name="typeMetadata"/> using <paramref name="jClass"/> and
		/// <paramref name="localRef"/>.
		/// </returns>
		private JReferenceObject CreateObject(JClassObject jClass, JObjectLocalRef localRef,
			JReferenceTypeMetadata classMetadata, JReferenceTypeMetadata typeMetadata)
		{
			JLocalObject jLocal = classMetadata.CreateInstance(jClass, localRef, true);
			Boolean disposeParse = typeMetadata.Modifier != JTypeModifier.Final && classMetadata != typeMetadata;
			JReferenceObject result = typeMetadata.ParseInstance(jLocal, disposeParse);
			this._callAdapter._cache.RegisterParameter(localRef, result as ILocalObject ?? jLocal);
			return result;
		}
	}
}