namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// This class stores cache for a <see cref="JEnvironment"/> instance.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed partial class EnvironmentCache : LocalMainClasses
	{
		/// <summary>
		/// Current thrown exception.
		/// </summary>
		public JniException? Thrown { get; private set; }
		/// <summary>
		/// Maximum number of bytes usable from stack.
		/// </summary>
		public Int32 MaxStackBytes { get; private set; } = EnvironmentCache.MinStackBytes;
		/// <summary>
		/// Amount of bytes used from stack.
		/// </summary>
		public Int32 UsedStackBytes { get; private set; }
		/// <summary>
		/// Ensured capacity.
		/// </summary>
		public Int32? Capacity => this._objects.Capacity;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
		public EnvironmentCache(JVirtualMachine vm, JEnvironment env, JEnvironmentRef envRef) : base(vm, envRef, env)
		{
			this._env = env;
			this._objects = new(this._classes);
			if (this.Version < NativeInterface.RequiredVersion) return; // Avoid class loading if unsupported version.
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
		public void CheckJniError()
		{
			if (this._criticalCount > 0 || this._buildingException)
				this.ExceptionCheck();
			else
				this.ExceptionOccurred();
			this.Thrown = default; // Clears current exception.
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
			if (this.Thrown == throwableException)
			{
				if (this.Thrown is not null && throwException)
					throw this.Thrown; // Rethrows pending exception.
				return;
			}
			if (throwableException is null)
			{
				this.Thrown = default; // Clears pending exception.
				this.ClearException();
				return;
			}

			try
			{
				ImplementationValidationUtilities.ThrowIfProxy(throwableException.GlobalThrowable);
				ImplementationValidationUtilities.ThrowIfDefault(throwableException.GlobalThrowable);
				this.SetPendingException(throwableException, throwException);
			}
			finally
			{
				// Throws current exception in JNI
				this.Throw(throwableException.ThrowableRef);
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
		/// Checks if there is a pending JNI exception.
		/// </summary>
		/// <returns><see langword="true"/> if there is pending JNI exception; otherwise, <see langword="false"/>.</returns>
		public unsafe Boolean HasPendingException()
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionCheckInfo);
			return nativeInterface.ExceptionCheck(this.Reference).Value;
		}
		/// <summary>
		/// Creates JNI exception from <paramref name="throwableRef"/>.
		/// </summary>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <returns>A <see cref="ThrowableException"/> exception.</returns>
		public ThrowableException CreateThrowableException(JThrowableLocalRef throwableRef)
		{
			this.ClearException();
			JReferenceTypeMetadata? throwableMetadata = default;
			String? message = default;
			JClassObject jClass;

			try
			{
				jClass = this._env.GetObjectClass(throwableRef.Value, out throwableMetadata);
			}
			catch (CriticalException)
			{
				// Unable to retrieve throwable object class.
				jClass = this.GetClass<JThrowableObject>(); // Retrieves java.lang.Throwable class.
				if (!this._buildingException) throw;
				this._env.DescribeException();
				this.Thrown = null;
			}
			try
			{
				message = this.GetThrowableMessage(throwableRef);
			}
			catch (CriticalException)
			{
				// Unable to retrieve throwable object message.
				if (!this._buildingException) throw;
				this._env.DescribeException();
				this.Thrown = null;
			}
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
		/// <summary>
		/// Clears pending JNI exception.
		/// </summary>
		public unsafe void ClearException()
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionClearInfo);
			nativeInterface.ErrorFunctions.ExceptionClear(this.Reference);
		}
		/// <summary>
		/// Sets number of bytes usable by JNI calls from stack.
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetUsableStackBytes(Int32 value)
		{
			Int32 min = EnvironmentCache.MinStackBytes > this.UsedStackBytes ?
				EnvironmentCache.MinStackBytes :
				this.UsedStackBytes;
			if (value < min)
				throw new ArgumentOutOfRangeException(nameof(value),
				                                      $"Usable stack bytes should be greater or equal to {min}.");
			this.MaxStackBytes = value;
		}
	}
}