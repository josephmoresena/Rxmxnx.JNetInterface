namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record EnvironmentCache
	{
		/// <summary>
		/// Cancellation token.
		/// </summary>
		private readonly CancellationTokenSource _cancellation = new();
		/// <summary>
		/// Class cache.
		/// </summary>
		private readonly ClassCache<JClassObject> _classes = new();
		/// <summary>
		/// Delegate cache.
		/// </summary>
		private readonly DelegateHelperCache _delegateCache = new();
		/// <summary>
		/// Main <see cref="JEnvironment"/> instance.
		/// </summary>
		private readonly JEnvironment _env;
		/// <summary>
		/// Number of active critical sequences.
		/// </summary>
		private Int32 _criticalCount;

		/// <summary>
		/// Object cache.
		/// </summary>
		private LocalCache _objects = new();
		/// <summary>
		/// Amount of bytes used from stack.
		/// </summary>
		private Int32 _usedStackBytes;

		/// <summary>
		/// Retrieves <see cref="AccessCache"/> for <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="AccessCache"/> instance.</returns>
		/// <exception cref="ArgumentException">Throw if <see cref="AccessCache"/> is not found.</exception>
		private AccessCache GetAccess(INativeTransaction jniTransaction, JClassObject jClass)
		{
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			return this._classes[classRef] ?? this.VirtualMachine.GetAccess(classRef) ??
				throw new ArgumentException("Invalid class object.", nameof(jClass));
		}
		/// <summary>
		/// Creates an object from given reference.
		/// </summary>
		/// <typeparam name="TResult">A <see cref="IDataType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="register">Indicates whether object must be registered.</param>
		/// <returns>A <typeparamref name="TResult"/> instance.</returns>
		private TResult? CreateObject<TResult>(JObjectLocalRef localRef, Boolean register)
			where TResult : IDataType<TResult>
		{
			this.CheckJniError();
			if (localRef == default) return default;

			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TResult>();
			JClassObject jClass;
			if (metadata.Modifier == JTypeModifier.Final)
			{
				jClass = this.GetClass<TResult>();
			}
			else
			{
				JClassLocalRef classRef = this._env.GetObjectClass(localRef);
				try
				{
					jClass = this.GetClass(classRef, false);
				}
				finally
				{
					this._env.DeleteLocalRef(classRef.Value);
				}
			}
			TResult result = (TResult)(Object)metadata.CreateInstance(jClass, localRef, true);
			if (localRef != (result as JLocalObject)!.InternalReference && register)
				this._env.DeleteLocalRef(localRef);
			return register ? this.Register(result) : result;
		}
		/// <summary>
		/// Retrieves the JNI function pointer for <paramref name="index"/>.
		/// </summary>
		/// <param name="index">JNI function index.</param>
		/// <returns>JNI function pointer.</returns>
		private IntPtr GetPointer(Int32 index)
		{
			Int32 lastNormalIndex = EnvironmentCache.delegateIndex[typeof(GetObjectRefTypeDelegate)];
			if (index <= lastNormalIndex)
				return this.Reference.Reference.Reference[index];
			index -= lastNormalIndex;
			return this.Reference.Reference.GetAdditionalPointers(this.Version)[index];
		}
		/// <summary>
		/// Indicates whether current JNI call must use <see langword="stackalloc"/> or <see langword="new"/> to
		/// hold JNI call parameter.
		/// </summary>
		/// <param name="requiredBytes">Output. Number of bytes to allocate.</param>
		/// <returns>
		/// <see langword="true"/> if current call must use <see langword="stackalloc"/>; otherwise,
		/// <see langword="false"/>.
		/// </returns>
		private Boolean UseStackAlloc(Int32 requiredBytes)
		{
			this._usedStackBytes += requiredBytes;
			if (this._usedStackBytes <= EnvironmentCache.MaxStackBytes) return true;
			this._usedStackBytes -= requiredBytes;
			return false;
		}
	}
}