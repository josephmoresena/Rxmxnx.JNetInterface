namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
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
			JReferenceTypeMetadata typeMetadata;
			JClassObject jClass;
			if (metadata.Modifier != JTypeModifier.Final)
			{
				jClass = this._env.GetObjectClass(localRef, out typeMetadata);
			}
			else
			{
				jClass = this.GetClass<TResult>();
				typeMetadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TResult>();
			}
			JLocalObject jLocal = typeMetadata.CreateInstance(jClass, localRef, true);
			TResult result = (TResult)(Object)metadata.ParseInstance(jLocal, true);
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
		/// <summary>
		/// Creates JNI exception from <paramref name="throwableRef"/>.
		/// </summary>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <returns>A <see cref="ThrowableException"/> exception.</returns>
		private ThrowableException CreateJniException(ref JThrowableLocalRef throwableRef)
		{
			using LocalFrame _ = new(this._env, 5);
			JClassObject jClass =
				this._env.GetObjectClass(throwableRef.Value, out JReferenceTypeMetadata throwableMetadata);
			String message = this.GetThrowableMessage(throwableRef);
			ThrowableObjectMetadata objectMetadata = new(jClass, throwableMetadata, message);
			JGlobalRef globalRef = this.CreateGlobalRef(throwableRef.Value);
			JGlobal jGlobalThrowable = new(this.VirtualMachine, objectMetadata, false, globalRef);

			this._env.DeleteLocalRef(throwableRef.Value);
			throwableRef = JThrowableLocalRef.FromReference(globalRef.Value);
			return throwableMetadata.CreateException(jGlobalThrowable, message)!;
		}
		/// <summary>
		/// Retrieves throwable message.
		/// </summary>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <returns>Throwable message.</returns>
		private String GetThrowableMessage(JThrowableLocalRef throwableRef)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, this.ThrowableObject);
			jniTransaction.Add(throwableRef);
			using JStringObject throwableMessage = this.GetThrowableMessage(throwableRef, access);
			return throwableMessage.Value;
		}
		/// <summary>
		/// Retrieves a <see cref="JStringObject"/> containing throwable message.
		/// </summary>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <param name="access">A <see cref="AccessCache"/> instance.</param>
		/// <returns>A <see cref="JStringObject"/> instance.</returns>
		private JStringObject GetThrowableMessage(JThrowableLocalRef throwableRef, AccessCache access)
		{
			JMethodId getNameId = access.GetMethodId(NativeFunctionSetImpl.GetMessageDefinition, this._env);
			CallObjectMethodADelegate callObjectMethod = this.GetDelegate<CallObjectMethodADelegate>();
			JObjectLocalRef localRef = callObjectMethod(this.Reference, throwableRef.Value, getNameId,
			                                            ReadOnlyValPtr<JValue>.Zero);
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return new(jStringClass, localRef.Transform<JObjectLocalRef, JStringLocalRef>());
		}
	}
}