using System.Diagnostics;

namespace Rxmxnx.JNetInterface;

public readonly ref partial struct JNativeCallAdapter
{
	/// <summary>
	/// Current <see cref="INativeThread"/> instance.
	/// </summary>
	private readonly INativeThread _env;
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
		this._env = JEnvironment.GetEnvironment(envRef); // Internal dependency.
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
	private TResult FinalizeCall<TResult>(JLocalObject? result)
		where TResult : unmanaged, IObjectReferenceType, INativePointerType<TResult>
	{
		JObjectLocalRef jniResult = this.FinalizeCall(result);
		return TResult.New(jniResult.Pointer);
	}

	public readonly ref partial struct Builder
	{
		/// <summary>
		/// <see cref="JNativeCallAdapter"/> instance.
		/// </summary>
		private readonly JNativeCallAdapter _callAdapter;

		/// <summary>
		/// Retrieves initial final <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
#if !NET8_0_OR_GREATER
		[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
		private TObject CreateFinalObject<TObject>(JObjectLocalRef localRef)
			where TObject : JReferenceObject, IReferenceType<TObject>
		{
			INativeThread env = this._callAdapter._env;
			JReferenceTypeMetadata typeMetadata = (JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TObject>();
			JClassObject jClass = typeMetadata.GetClass(env);
			NativeCallUtilities.ThrowIfNotLocalReference(env, localRef);
			return (TObject)this.CreateObject(jClass, localRef, typeMetadata);
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
			JReferenceTypeMetadata classMetadata, JReferenceTypeMetadata? typeMetadata = default)
		{
			INativeThread env = this._callAdapter._env;
			CallFrame cache = this._callAdapter._cache;
			cache.Activate(out LocalCache previous);
			try
			{
				return NativeCallUtilities.CreateAlienObject(cache, jClass, localRef, classMetadata, typeMetadata);
			}
			finally
			{
				// Revert cache activation.
				env.LocalCache = previous;
			}
		}
	}
}