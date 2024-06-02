namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// This record stores cache for a <see cref="JEnvironment"/> instance.
	/// </summary>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
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
			this._objects = new(this._classes);

			this.VirtualMachine = vm;
			this.Reference = envRef;
			this.Version = EnvironmentCache.GetVersion(envRef);
			this.Thread = Thread.CurrentThread;

			Task.Factory.StartNew(EnvironmentCache.FinalizeCache, this, this._cancellation.Token);
			this.LoadMainClasses();
		}

		/// <summary>
		/// Retrieves managed <see cref="NativeInterface"/> reference from current instance.
		/// </summary>
		/// <param name="info">A <see cref="JniMethodInfo"/> instance.</param>
		/// <returns>A managed <see cref="NativeInterface"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref readonly TNativeInterface GetNativeInterface<TNativeInterface>(JniMethodInfo info)
			where TNativeInterface : unmanaged, INativeInterface<TNativeInterface>
		{
			ImplementationValidationUtilities.ThrowIfDifferentThread(this.Reference, this.Thread);
			ImplementationValidationUtilities.ThrowIfInvalidVirtualMachine(this.VirtualMachine.IsAlive);
			ImplementationValidationUtilities.ThrowIfNotAttached(this._env.IsAttached);
			ImplementationValidationUtilities.ThrowIfUnsafe(info.Name, this.JniSecure(info.Level));
			ImplementationValidationUtilities.ThrowIfInvalidVersion(info.Name, TNativeInterface.RequiredVersion,
			                                                        this.Version);
			ref readonly JEnvironmentValue refValue = ref this.Reference.Reference;
			return ref Unsafe.AsRef<TNativeInterface>(refValue.Pointer.ToPointer());
		}
		/// <summary>
		/// Checks JNI occurred error.
		/// </summary>
		public unsafe void CheckJniError()
		{
			if (this._criticalCount > 0)
			{
				ref readonly NativeInterface nativeInterface =
					ref this.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionCheckInfo);
				if (!nativeInterface.ExceptionCheck(this.Reference).Value) return;
				this.ThrowJniException(CriticalException.Instance, true);
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
				ImplementationValidationUtilities.ThrowIfProxy(throwableException?.Global);
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
					ImplementationValidationUtilities.ThrowIfDefault(throwableException.Global);
					this.Throw(throwableException.Global.As<JThrowableLocalRef>());
				}
			}
		}
		/// <summary>
		/// Retrieves exception occured reference.
		/// </summary>
		/// <returns>Pending exception <see cref="JThrowableLocalRef"/> reference.</returns>
		public unsafe JThrowableLocalRef GetPendingException()
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionOccurredInfo);
			return nativeInterface.ErrorFunctions.ExceptionOccurred(this.Reference);
		}
		/// <summary>
		/// Creates JNI exception from <paramref name="throwableRef"/>.
		/// </summary>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <returns>A <see cref="ThrowableException"/> exception.</returns>
		public ThrowableException CreateThrowableException(JThrowableLocalRef throwableRef)
		{
			this.ClearException();

			JClassObject jClass =
				this._env.GetObjectClass(throwableRef.Value, out JReferenceTypeMetadata throwableMetadata);
			String message = this.GetThrowableMessage(throwableRef);
			return this.CreateThrowableException(jClass, throwableMetadata, message, throwableRef);
		}
		/// <summary>
		/// Deletes the current local reference frame.
		/// </summary>
		/// <param name="result">Current result.</param>
		public unsafe void DeleteLocalFrame(JLocalObject? result)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.PopLocalFrameInfo);
			JObjectLocalRef localRef = result?.LocalReference ?? default;
			localRef = nativeInterface.ReferenceFunctions.PopLocalFrame(this.Reference, localRef);
			result?.SetValue(localRef);
			this.Register(result);
		}
	}
}