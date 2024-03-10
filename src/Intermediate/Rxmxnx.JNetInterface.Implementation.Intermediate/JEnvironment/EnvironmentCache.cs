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
		/// Current thrown exception.
		/// </summary>
		public JniException? Thrown { get; private set; }

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
			JniDelegateInfo info = EnvironmentCache.delegateIndex[typeOfT];
			ValidationUtilities.ThrowIfUnsafe(info.Name, this.JniSecure(info.Level));
			IntPtr ptr = this.GetPointer(info.Index);
			return this._delegateCache.GetDelegate<TDelegate>(ptr);
		}
		/// <summary>
		/// Checks JNI occurred error.
		/// </summary>
		public void CheckJniError()
		{
			if (this._criticalCount > 0)
			{
				ExceptionCheckDelegate exceptionCheck = this.GetDelegate<ExceptionCheckDelegate>();
				if (exceptionCheck(this.Reference) != JBoolean.TrueValue) return;
				this.ThrowJniException(new CriticalException(), true);
			}
			else
			{
				JThrowableLocalRef throwableRef = this.GetPendingException();
				if (throwableRef.IsDefault) return;
				this.ThrowJniException(this.CreateThrowableException(throwableRef), true);
			}
		}
		/// <inheritdoc cref="IEnvironment.JniSecure"/>
		/// <param name="level">JNI call level.</param>
		public Boolean JniSecure(JniSafetyLevels level = JniSafetyLevels.None)
			=> this.Thread.ManagedThreadId == Environment.CurrentManagedThreadId &&
				(level.HasFlag(JniSafetyLevels.CriticalSafe) || this._criticalCount == 0) &&
				(level.HasFlag(JniSafetyLevels.ErrorSafe) || this.Thrown is null);
		/// <summary>
		/// Sets <paramref name="throwableException"/> as pending exception and throws it.
		/// </summary>
		/// <param name="throwableException">A <see cref="ThrowableException"/> instance.</param>
		/// <param name="throwException">
		/// Indicates whether exception should be thrown in managed code.
		/// </param>
		/// <exception cref="ThrowableException">
		/// Throws if <paramref name="throwableException"/> is not null.
		/// </exception>
		public void ThrowJniException(ThrowableException? throwableException, Boolean throwException)
		{
			if (this.Thrown == throwableException) return;
			try
			{
				ValidationUtilities.ThrowIfProxy(throwableException?.Global);
				this.ThrowJniException(throwableException as JniException, throwException);
			}
			finally
			{
				if (throwableException is null)
				{
					this.ClearException();
				}
				else
				{
					ValidationUtilities.ThrowIfDefault(throwableException.Global);
					this.Throw(throwableException.Global.As<JThrowableLocalRef>());
				}
			}
		}
	}
}