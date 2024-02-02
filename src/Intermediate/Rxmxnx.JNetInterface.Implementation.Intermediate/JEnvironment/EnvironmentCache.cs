namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// This record stores cache for a <see cref="JEnvironment"/> instance.
	/// </summary>
	private sealed partial record EnvironmentCache : LocalMainClasses
	{
		/// <inheritdoc cref="JEnvironment.Reference"/>
		public readonly JEnvironmentRef Reference;
		/// <summary>
		/// Managed thread.
		/// </summary>
		public readonly Thread Thread;
		/// <inheritdoc cref="IEnvironment.Version"/>
		public readonly Int32 Version;

		/// <inheritdoc cref="JEnvironment.VirtualMachine"/>
		public readonly JVirtualMachine VirtualMachine;

		/// <summary>
		/// Ensured capacity.
		/// </summary>
		public Int32? Capacity => this._objects.Capacity;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> instance.</param>
		public EnvironmentCache(JVirtualMachine vm, JEnvironment env, JEnvironmentRef envRef) : base(env)
		{
			this._env = env;

			this.VirtualMachine = vm;
			this.Reference = envRef;
			this.Version = EnvironmentCache.GetVersion(envRef);
			this.Thread = Thread.CurrentThread;

			Task.Factory.StartNew(EnvironmentCache.FinalizeCache, this, this._cancellation.Token);
			this.LoadMainClasses();
		}

		/// <summary>
		/// Retrieves a <typeparamref name="TDelegate"/> instance for <typeparamref name="TDelegate"/>.
		/// </summary>
		/// <typeparam name="TDelegate">Type of method delegate.</typeparam>
		/// <returns>A <typeparamref name="TDelegate"/> instance.</returns>
		public TDelegate GetDelegate<TDelegate>() where TDelegate : Delegate
		{
			ValidationUtilities.ThrowIfDifferentThread(this.Thread);
			Type typeOfT = typeof(TDelegate);
			Int32 index = EnvironmentCache.delegateIndex[typeOfT];
			IntPtr ptr = this.GetPointer(index);
			return this._delegateCache.GetDelegate<TDelegate>(ptr);
		}
		/// <summary>
		/// Checks JNI occurred error.
		/// </summary>
		public void CheckJniError()
		{
			ExceptionOccurredDelegate exceptionOccurred = this.GetDelegate<ExceptionOccurredDelegate>();
			JThrowableLocalRef throwableRef = exceptionOccurred(this.Reference);
			if (throwableRef.Value == default) return;
			try
			{
				ExceptionClearDelegate exceptionClear = this.GetDelegate<ExceptionClearDelegate>();
				exceptionClear(this.Reference);
				using LocalFrame frame = new(this._env, 10);
				JClassLocalRef classRef = this._env.GetObjectClass(throwableRef.Value);
				JClassObject jClass = this.AsClassObject(classRef);
				String message = EnvironmentCache.GetThrowableMessage(jClass, throwableRef);
				ThrowableObjectMetadata objectMetadata = new(jClass, message);
				JThrowableTypeMetadata throwableMetadata =
					MetadataHelper.GetMetadata(jClass.Hash) as JThrowableTypeMetadata ??
					(JThrowableTypeMetadata)MetadataHelper.GetMetadata<JThrowableObject>();
				JGlobalRef globalRef = this.CreateGlobalRef(throwableRef.Value);
				JGlobal jGlobalThrowable = new(this.VirtualMachine, objectMetadata, false, globalRef);
				throw throwableMetadata.CreateException(jGlobalThrowable, message);
			}
			finally
			{
				ThrowDelegate jThrow = this.GetDelegate<ThrowDelegate>();
				jThrow(this.Reference, throwableRef);
			}
		}
		/// <inheritdoc cref="IEnvironment.JniSecure"/>
		public Boolean JniSecure()
			=> this.Thread.ManagedThreadId == Environment.CurrentManagedThreadId && this._criticalCount == 0;
	}
}