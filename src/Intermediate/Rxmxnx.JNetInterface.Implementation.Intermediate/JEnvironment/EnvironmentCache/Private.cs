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
		private readonly ClassCache<JClassObject> _classes = new(JReferenceType.LocalRefType);
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
		/// Retrieves managed <see cref="ArrayFunctionSet"/> reference from current instance.
		/// </summary>
		/// <param name="primitiveSignature">Primitive signature char.</param>
		/// <param name="arrayFunction">Requested array function.</param>
		/// <returns>A managed <see cref="ArrayFunctionSet"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref readonly ArrayFunctionSet GetArrayFunctions(Byte primitiveSignature,
			ArrayFunctionSet.PrimitiveFunction arrayFunction)
		{
			JniMethodInfo info = primitiveSignature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => EnvironmentCache.GetBooleanArrayFunctionInfo(
					arrayFunction),
				UnicodePrimitiveSignatures.ByteSignatureChar =>
					EnvironmentCache.GetByteArrayFunctionInfo(arrayFunction),
				UnicodePrimitiveSignatures.CharSignatureChar =>
					EnvironmentCache.GetCharArrayFunctionInfo(arrayFunction),
				UnicodePrimitiveSignatures.DoubleSignatureChar => EnvironmentCache.GetDoubleArrayFunctionInfo(
					arrayFunction),
				UnicodePrimitiveSignatures.FloatSignatureChar => EnvironmentCache.GetFloatArrayFunctionInfo(
					arrayFunction),
				UnicodePrimitiveSignatures.IntSignatureChar => EnvironmentCache.GetIntArrayFunctionInfo(arrayFunction),
				UnicodePrimitiveSignatures.LongSignatureChar =>
					EnvironmentCache.GetLongArrayFunctionInfo(arrayFunction),
				UnicodePrimitiveSignatures.ShortSignatureChar => EnvironmentCache.GetShortArrayFunctionInfo(
					arrayFunction),
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
			return ref this.GetNativeInterface<NativeInterface>(info).ArrayFunctions;
		}
		/// <summary>
		/// Retrieves managed <see cref="InstanceMethodFunctionSet"/> reference from current instance.
		/// </summary>
		/// <param name="signatureChar">Signature first char.</param>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A managed <see cref="InstanceMethodFunctionSet"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref readonly InstanceMethodFunctionSet GetInstanceMethodFunctions(Byte signatureChar,
			Boolean nonVirtual)
		{
			JniMethodInfo info = signatureChar switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => EnvironmentCache.GetBooleanInstanceMethodInfo(
					nonVirtual),
				UnicodePrimitiveSignatures.ByteSignatureChar => EnvironmentCache.GetByteInstanceMethodInfo(nonVirtual),
				UnicodePrimitiveSignatures.CharSignatureChar => EnvironmentCache.GetCharInstanceMethodInfo(nonVirtual),
				UnicodePrimitiveSignatures.DoubleSignatureChar => EnvironmentCache.GetDoubleInstanceMethodInfo(
					nonVirtual),
				UnicodePrimitiveSignatures.FloatSignatureChar =>
					EnvironmentCache.GetFloatInstanceMethodInfo(nonVirtual),
				UnicodePrimitiveSignatures.IntSignatureChar => EnvironmentCache.GetIntInstanceMethodInfo(nonVirtual),
				UnicodePrimitiveSignatures.LongSignatureChar => EnvironmentCache.GetLongInstanceMethodInfo(nonVirtual),
				UnicodePrimitiveSignatures.ShortSignatureChar =>
					EnvironmentCache.GetShortInstanceMethodInfo(nonVirtual),
				UnicodePrimitiveSignatures.VoidSignatureChar => EnvironmentCache.GetVoidInstanceMethodInfo(nonVirtual),
				UnicodeObjectSignatures.ObjectSignaturePrefixChar => EnvironmentCache.GetObjectInstanceMethodInfo(
					nonVirtual),
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
			return ref this.GetNativeInterface<NativeInterface>(info).InstanceMethodFunctions;
		}
		/// <summary>
		/// Retrieves managed <see cref="MethodFunctionSet{JClassLocalRef}"/> reference from current instance.
		/// </summary>
		/// <param name="primitiveSignature">Primitive signature char.</param>
		/// <returns>A managed <see cref="MethodFunctionSet{JClassLocalRef}"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref readonly MethodFunctionSet<JClassLocalRef> GetStaticMethodFunctions(Byte primitiveSignature)
		{
			JniMethodInfo info = primitiveSignature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => NativeInterface.CallStaticBooleanMethodInfo,
				UnicodePrimitiveSignatures.ByteSignatureChar => NativeInterface.CallStaticByteMethodInfo,
				UnicodePrimitiveSignatures.CharSignatureChar => NativeInterface.CallStaticCharMethodInfo,
				UnicodePrimitiveSignatures.DoubleSignatureChar => NativeInterface.CallStaticDoubleMethodInfo,
				UnicodePrimitiveSignatures.FloatSignatureChar => NativeInterface.CallStaticFloatMethodInfo,
				UnicodePrimitiveSignatures.IntSignatureChar => NativeInterface.CallStaticIntMethodInfo,
				UnicodePrimitiveSignatures.LongSignatureChar => NativeInterface.CallStaticLongMethodInfo,
				UnicodePrimitiveSignatures.ShortSignatureChar => NativeInterface.CallStaticLongMethodInfo,
				UnicodePrimitiveSignatures.VoidSignatureChar => NativeInterface.CallStaticVoidMethodInfo,
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
			return ref this.GetNativeInterface<NativeInterface>(info).StaticMethodFunctions;
		}
		/// <summary>
		/// Retrieves managed <see cref="FieldFunctionSet{JObjectLocalRef}"/> reference from current instance.
		/// </summary>
		/// <param name="primitiveSignature">Primitive signature char.</param>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A managed <see cref="FieldFunctionSet{JObjectLocalRef}"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref readonly FieldFunctionSet<JObjectLocalRef> GetInstanceFieldFunctions(Byte primitiveSignature,
			Boolean getField)
		{
			JniMethodInfo info = primitiveSignature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => EnvironmentCache.GetInstanceBooleanFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.ByteSignatureChar => EnvironmentCache.GetInstanceByteFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.CharSignatureChar => EnvironmentCache.GetInstanceCharFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.DoubleSignatureChar => EnvironmentCache.GetInstanceDoubleFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.FloatSignatureChar => EnvironmentCache.GetInstanceFloatFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.IntSignatureChar => EnvironmentCache.GetInstanceIntFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.LongSignatureChar => EnvironmentCache.GetInstanceLongFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.ShortSignatureChar => EnvironmentCache.GetInstanceShortFieldFunctionInfo(
					getField),
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
			return ref this.GetNativeInterface<NativeInterface>(info).InstanceFieldFunctions;
		}
		/// <summary>
		/// Retrieves managed <see cref="FieldFunctionSet{JClassLocalRef}"/> reference from current instance.
		/// </summary>
		/// <param name="primitiveSignature">Primitive signature char.</param>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A managed <see cref="FieldFunctionSet{JClassLocalRef}"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref readonly FieldFunctionSet<JClassLocalRef> GetStaticFieldFunctions(Byte primitiveSignature,
			Boolean getField)
		{
			JniMethodInfo info = primitiveSignature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => EnvironmentCache.GetStaticBooleanFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.ByteSignatureChar => EnvironmentCache.GetStaticByteFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.CharSignatureChar => EnvironmentCache.GetStaticCharFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.DoubleSignatureChar => EnvironmentCache.GetStaticDoubleFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.FloatSignatureChar => EnvironmentCache.GetStaticFloatFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.IntSignatureChar => EnvironmentCache.GetStaticIntFieldFunctionInfo(getField),
				UnicodePrimitiveSignatures.LongSignatureChar => EnvironmentCache.GetStaticLongFieldFunctionInfo(
					getField),
				UnicodePrimitiveSignatures.ShortSignatureChar => EnvironmentCache.GetStaticShortFieldFunctionInfo(
					getField),
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
			return ref this.GetNativeInterface<NativeInterface>(info).StaticFieldFunctions;
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
			if (localRef != (result as JLocalObject)!.LocalReference && register)
				this._env.DeleteLocalRef(localRef);
			return register ? this.Register(result) : result;
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
		/// Throws an exception from <typeparamref name="TThrowable"/> type.
		/// </summary>
		/// <typeparam name="TThrowable"></typeparam>
		/// <param name="utf8Message">
		/// The message used to construct the <c>java.lang.Throwable</c> instance.
		/// The string is encoded in modified UTF-8.
		/// </param>
		/// <param name="throwException">
		/// Indicates whether exception should be thrown in managed code.
		/// </param>
		/// <param name="message">
		/// The message used to construct the <see cref="ThrowableException"/> instance.
		/// </param>
		private void ThrowNew<TThrowable>(ReadOnlySpan<Byte> utf8Message, Boolean throwException, String? message)
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			JResult result = this.ThrowNew<TThrowable>(utf8Message);
			ImplementationValidationUtilities.ThrowIfInvalidResult(result);

			ThrowableException throwableException =
				this.CreateThrowableException<TThrowable>(this.GetPendingException(), message);
			this.ThrowJniException(throwableException, throwException);
		}
		/// <summary>
		/// Constructs an <typeparamref name="TThrowable"/> exception with the message specified by
		/// <paramref name="message"/> and causes that exception to be thrown.
		/// </summary>
		/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
		/// <param name="message">Exception message.</param>
		/// <returns>JNI code result.</returns>
		private unsafe JResult ThrowNew<TThrowable>(ReadOnlySpan<Byte> message)
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			JClassObject jClass = this.GetClass<TThrowable>();
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ThrowNewInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			fixed (Byte* ptr = &MemoryMarshal.GetReference(message))
				return nativeInterface.ErrorFunctions.ThrowNew(this.Reference, classRef, ptr);
		}
		/// <summary>
		/// Creates JNI exception from <paramref name="throwableRef"/>.
		/// </summary>
		/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <param name="message">Throwable message.</param>
		/// <returns>A <see cref="ThrowableException"/> exception.</returns>
		private ThrowableException CreateThrowableException<TThrowable>(JThrowableLocalRef throwableRef,
			String? message) where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			JClassObject jClass = this.GetClass<TThrowable>();
			JReferenceTypeMetadata throwableMetadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TThrowable>();
			this.ClearException();
			return this.CreateThrowableException(jClass, throwableMetadata, message, throwableRef);
		}
		/// <summary>
		/// Creates JNI exception from <paramref name="throwableRef"/>.
		/// </summary>
		/// <param name="jClass">Throwable class.</param>
		/// <param name="throwableMetadata">Throwable metadata.</param>
		/// <param name="message">Throwable message.</param>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <returns>A <see cref="ThrowableException"/> exception.</returns>
		private ThrowableException CreateThrowableException(JClassObject jClass,
			JReferenceTypeMetadata throwableMetadata, String? message, JThrowableLocalRef throwableRef)
		{
			ThrowableObjectMetadata objectMetadata = new(jClass, throwableMetadata, message);
			JGlobalRef globalRef = this.CreateGlobalRef(throwableRef.Value);
			JGlobal jGlobalThrowable = new(this.VirtualMachine, objectMetadata, globalRef);

			this._env.DeleteLocalRef(throwableRef.Value);
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
			try
			{
				return throwableMessage.Value;
			}
			finally
			{
				this.FreeUnregistered(throwableMessage);
			}
		}
		/// <summary>
		/// Retrieves a <see cref="JStringObject"/> containing throwable message.
		/// </summary>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <param name="access">A <see cref="AccessCache"/> instance.</param>
		/// <returns>A <see cref="JStringObject"/> instance.</returns>
		private unsafe JStringObject GetThrowableMessage(JThrowableLocalRef throwableRef, AccessCache access)
		{
			JMethodId getNameId = access.GetMethodId(NativeFunctionSetImpl.GetMessageDefinition, this._env);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallObjectMethodInfo);
			JObjectLocalRef localRef =
				nativeInterface.InstanceMethodFunctions.MethodFunctions.CallObjectMethod.Call(
					this.Reference, throwableRef.Value, getNameId, default);
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return new(jStringClass, localRef.Transform<JObjectLocalRef, JStringLocalRef>());
		}
		/// <summary>
		/// Sets given <see cref="JThrowableLocalRef"/> reference as pending exception.
		/// </summary>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		private unsafe void Throw(JThrowableLocalRef throwableRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ThrowInfo);
			nativeInterface.ErrorFunctions.Throw(this.Reference, throwableRef);
		}
		/// <summary>
		/// Clears pending JNI exception.
		/// </summary>
		private unsafe void ClearException()
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionClearInfo);
			nativeInterface.ErrorFunctions.ExceptionClear(this.Reference);
		}
		/// <summary>
		/// Sets <paramref name="jniException"/> as managed pending exception and throws it.
		/// </summary>
		/// <param name="throwException">
		/// Indicates whether exception should be thrown in managed code.
		/// </param>
		/// <param name="jniException">A <see cref="JniException"/> instance.</param>
		/// <exception cref="ThrowableException">
		/// Throws if <paramref name="jniException"/> is not null and <paramref name="throwException"/> is
		/// <see langword="true"/>.
		/// </exception>
		private void ThrowJniException(JniException? jniException, Boolean throwException)
		{
			this.Thrown = jniException;
			if (this.Thrown is not null && throwException) throw this.Thrown;
		}
		/// <summary>
		/// Deletes and clears unregister <see cref="JLocalObject"/> instance.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		private void FreeUnregistered(JLocalObject jLocal)
		{
			this._env.DeleteLocalRef(jLocal.LocalReference);
			jLocal.ClearValue();
		}
		/// <summary>
		/// Creates a <see cref="StackDisposable"/> instance for current call.
		/// </summary>
		/// <param name="useStackAlloc">Indicates whether current call is using stack.</param>
		/// <param name="requiredBytes">Number of bytes to use from stack.</param>
		/// <returns>A <see cref="StackDisposable"/> instance.</returns>
		private StackDisposable GetStackDisposable(Boolean useStackAlloc, Int32 requiredBytes)
			=> useStackAlloc && requiredBytes > 0 ? new(this, requiredBytes) : new();
		/// <summary>
		/// Creates a <see cref="IFixedContext{Byte}.IDisposable"/> instance from an span created in stack.
		/// </summary>
		/// <param name="stackSpan">A stack created span.</param>
		/// <returns>A <see cref="IFixedContext{T}.IDisposable"/> instance</returns>
		private IFixedContext<Byte>.IDisposable GetStackContext(scoped Span<Byte> stackSpan)
		{
			StackDisposable disposable = new(this, stackSpan.Length);
			ValPtr<Byte> ptr = (ValPtr<Byte>)stackSpan.GetUnsafeIntPtr();
			return ptr.GetUnsafeFixedContext(stackSpan.Length, disposable);
		}
	}
}