namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCore
{
	/// <summary>
	/// Class cache.
	/// </summary>
	private readonly ClassCache<JClassObject> _classes = new(JReferenceType.LocalRefType);
	/// <summary>
	/// Main <see cref="INativeThread"/> instance.
	/// </summary>
	private readonly INativeThread _env;
	/// <summary>
	/// Indicates whether current thread is building a JNI throwable exception.
	/// </summary>
	private Boolean _buildingException;
	/// <summary>
	/// Number of active critical sequences.
	/// </summary>
	private Int32 _criticalCount;

	/// <summary>
	/// Object cache.
	/// </summary>
	private LocalCache _objects;

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
		return this._classes[classRef] ?? this.Host.TypeManager.GetAccess(classRef) ??
			throw new ArgumentException(IMessageResource.GetInstance().InvalidClass, jClass.ToTraceText());
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
			CommonNames.BooleanSignatureChar => EnvironmentCore.GetBooleanArrayFunctionInfo(arrayFunction),
			CommonNames.ByteSignatureChar => EnvironmentCore.GetByteArrayFunctionInfo(arrayFunction),
			CommonNames.CharSignatureChar => EnvironmentCore.GetCharArrayFunctionInfo(arrayFunction),
			CommonNames.DoubleSignatureChar => EnvironmentCore.GetDoubleArrayFunctionInfo(arrayFunction),
			CommonNames.FloatSignatureChar => EnvironmentCore.GetFloatArrayFunctionInfo(arrayFunction),
			CommonNames.IntSignatureChar => EnvironmentCore.GetIntArrayFunctionInfo(arrayFunction),
			CommonNames.LongSignatureChar => EnvironmentCore.GetLongArrayFunctionInfo(arrayFunction),
			CommonNames.ShortSignatureChar => EnvironmentCore.GetShortArrayFunctionInfo(arrayFunction),
			_ => throw new ArgumentException(IMessageResource.GetInstance().InvalidPrimitiveTypeMessage),
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
	private ref readonly InstanceMethodFunctionSet GetInstanceMethodFunctions(Byte signatureChar, Boolean nonVirtual)
	{
		JniMethodInfo info = signatureChar switch
		{
			CommonNames.BooleanSignatureChar => EnvironmentCore.GetBooleanInstanceMethodInfo(nonVirtual),
			CommonNames.ByteSignatureChar => EnvironmentCore.GetByteInstanceMethodInfo(nonVirtual),
			CommonNames.CharSignatureChar => EnvironmentCore.GetCharInstanceMethodInfo(nonVirtual),
			CommonNames.DoubleSignatureChar => EnvironmentCore.GetDoubleInstanceMethodInfo(nonVirtual),
			CommonNames.FloatSignatureChar => EnvironmentCore.GetFloatInstanceMethodInfo(nonVirtual),
			CommonNames.IntSignatureChar => EnvironmentCore.GetIntInstanceMethodInfo(nonVirtual),
			CommonNames.LongSignatureChar => EnvironmentCore.GetLongInstanceMethodInfo(nonVirtual),
			CommonNames.ShortSignatureChar => EnvironmentCore.GetShortInstanceMethodInfo(nonVirtual),
			CommonNames.VoidSignatureChar => EnvironmentCore.GetVoidInstanceMethodInfo(nonVirtual),
			CommonNames.ObjectSignaturePrefixChar => EnvironmentCore.GetObjectInstanceMethodInfo(nonVirtual),
			_ => throw new ArgumentException(IMessageResource.GetInstance().InvalidPrimitiveTypeMessage),
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
			CommonNames.BooleanSignatureChar => NativeInterface.CallStaticBooleanMethodInfo,
			CommonNames.ByteSignatureChar => NativeInterface.CallStaticByteMethodInfo,
			CommonNames.CharSignatureChar => NativeInterface.CallStaticCharMethodInfo,
			CommonNames.DoubleSignatureChar => NativeInterface.CallStaticDoubleMethodInfo,
			CommonNames.FloatSignatureChar => NativeInterface.CallStaticFloatMethodInfo,
			CommonNames.IntSignatureChar => NativeInterface.CallStaticIntMethodInfo,
			CommonNames.LongSignatureChar => NativeInterface.CallStaticLongMethodInfo,
			CommonNames.ShortSignatureChar => NativeInterface.CallStaticLongMethodInfo,
			CommonNames.VoidSignatureChar => NativeInterface.CallStaticVoidMethodInfo,
			_ => throw new ArgumentException(IMessageResource.GetInstance().InvalidPrimitiveTypeMessage),
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
			CommonNames.BooleanSignatureChar => EnvironmentCore.GetInstanceBooleanFieldFunctionInfo(getField),
			CommonNames.ByteSignatureChar => EnvironmentCore.GetInstanceByteFieldFunctionInfo(getField),
			CommonNames.CharSignatureChar => EnvironmentCore.GetInstanceCharFieldFunctionInfo(getField),
			CommonNames.DoubleSignatureChar => EnvironmentCore.GetInstanceDoubleFieldFunctionInfo(getField),
			CommonNames.FloatSignatureChar => EnvironmentCore.GetInstanceFloatFieldFunctionInfo(getField),
			CommonNames.IntSignatureChar => EnvironmentCore.GetInstanceIntFieldFunctionInfo(getField),
			CommonNames.LongSignatureChar => EnvironmentCore.GetInstanceLongFieldFunctionInfo(getField),
			CommonNames.ShortSignatureChar => EnvironmentCore.GetInstanceShortFieldFunctionInfo(getField),
			_ => throw new ArgumentException(IMessageResource.GetInstance().InvalidPrimitiveTypeMessage),
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
			CommonNames.BooleanSignatureChar => EnvironmentCore.GetStaticBooleanFieldFunctionInfo(getField),
			CommonNames.ByteSignatureChar => EnvironmentCore.GetStaticByteFieldFunctionInfo(getField),
			CommonNames.CharSignatureChar => EnvironmentCore.GetStaticCharFieldFunctionInfo(getField),
			CommonNames.DoubleSignatureChar => EnvironmentCore.GetStaticDoubleFieldFunctionInfo(getField),
			CommonNames.FloatSignatureChar => EnvironmentCore.GetStaticFloatFieldFunctionInfo(getField),
			CommonNames.IntSignatureChar => EnvironmentCore.GetStaticIntFieldFunctionInfo(getField),
			CommonNames.LongSignatureChar => EnvironmentCore.GetStaticLongFieldFunctionInfo(getField),
			CommonNames.ShortSignatureChar => EnvironmentCore.GetStaticShortFieldFunctionInfo(getField),
			_ => throw new ArgumentException(IMessageResource.GetInstance().InvalidPrimitiveTypeMessage),
		};
		return ref this.GetNativeInterface<NativeInterface>(info).StaticFieldFunctions;
	}
	/// <summary>
	/// Creates an object from given reference.
	/// </summary>
	/// <typeparam name="TResult">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="localRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="register">Indicates whether object must be registered.</param>
	/// <param name="useTypeClass">Indicates whether object must use <typeparamref name="TResult"/> class.</param>
	/// <returns>A <typeparamref name="TResult"/> instance.</returns>
	private TResult? CreateObject<TResult>(JObjectLocalRef localRef, Boolean register, Boolean useTypeClass)
		where TResult : IDataType<TResult>
	{
		this.CheckJniError();
		if (localRef == default) return default;

		JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TResult>();
		JReferenceTypeMetadata typeMetadata = metadata;
		JClassObject jClass = useTypeClass ?
			this.GetClass<TResult>() :
			this.GetObjectClass(localRef, metadata, out typeMetadata);
		JLocalObject jLocal = typeMetadata.CreateInstance(jClass, localRef, true);
		TResult result = (TResult)(Object)metadata.ParseInstance(jLocal, true);
		if (localRef != jLocal.LocalReference && register)
			this.DeleteLocalRef(localRef);
		return register ? this.Register(result) : result;
	}
	/// <summary>
	/// Creates an object from given reference.
	/// </summary>
	/// <typeparam name="TResult">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="jClass">Object class.</param>
	/// <param name="localRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <typeparamref name="TResult"/> instance.</returns>
	private TResult CreateObject<TResult>(JClassObject jClass, JObjectLocalRef localRef)
		where TResult : IDataType<TResult>
	{
		this.CheckJniError();
		JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TResult>();
		JReferenceTypeMetadata typeMetadata = this.GetTypeMetadata(jClass);
		JLocalObject jLocal = typeMetadata.CreateInstance(jClass, localRef, true);
		TResult result = (TResult)(Object)metadata.ParseInstance(jLocal, true);
		return this.Register(result);
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
		this.UsedStackBytes += requiredBytes;
		if (this.UsedStackBytes <= this.MaxStackBytes) return true;
		this.UsedStackBytes -= requiredBytes;
		return false;
	}
	/// <summary>
	/// Throws an exception from <typeparamref name="TThrowable"/> type.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="JThrowableObject"/> type.</typeparam>
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
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	private void ThrowNew<TThrowable>(ReadOnlySpan<Byte> utf8Message, Boolean throwException, String? message)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		JClassObject jClass = this.GetClass<TThrowable>();
		JReferenceTypeMetadata throwableMetadata =
			(JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TThrowable>();

		this.ThrowNew(jClass, throwableMetadata, utf8Message, throwException, message);
	}
	/// <summary>
	/// Throws an exception from <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="throwableMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
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
	private void ThrowNew(JClassObject jClass, JReferenceTypeMetadata throwableMetadata, ReadOnlySpan<Byte> utf8Message,
		Boolean throwException, String? message)
	{
		ImplementationValidationUtilities.ThrowIfInvalidResult(this.ThrowNew(jClass, utf8Message));

		ThrowableException throwableException = this.CreateThrowableException(jClass, throwableMetadata, message);
		this.ThrowJniException(throwableException, throwException);
	}

	/// <summary>
	/// Constructs an exception with the message specified by <paramref name="message"/>, the class specified by
	/// <paramref name="jClass"/> and causes that exception to be thrown.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="message">Exception message.</param>
	/// <returns>JNI code result.</returns>
	private unsafe JResult ThrowNew(JClassObject jClass, ReadOnlySpan<Byte> message)
	{
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.ThrowNewInfo);
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(2);
		JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
		fixed (Byte* ptr = &MemoryMarshal.GetReference(message))
			return nativeInterface.ErrorFunctions.ThrowNew(this.Reference, classRef, ptr);
	}
	/// <summary>
	/// Creates JNI exception from the thrown exception.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="throwableMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="message">Throwable message.</param>
	/// <returns>A <see cref="ThrowableException"/> exception.</returns>
	private ThrowableException CreateThrowableException(JClassObject jClass, JReferenceTypeMetadata throwableMetadata,
		String? message)
	{
		JThrowableLocalRef throwableRef = this.GetPendingException();
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
	private ThrowableException CreateThrowableException(JClassObject jClass, JReferenceTypeMetadata? throwableMetadata,
		String? message, JThrowableLocalRef throwableRef)
	{
		ThrowableObjectMetadata objectMetadata = new(jClass, message);
		try
		{
			JGlobalRef globalRef = this.CreateGlobalRef(throwableRef.Value);
			JGlobal jGlobalThrowable = new(this.Host.Value, objectMetadata, globalRef);
			return throwableMetadata?.CreateException(jGlobalThrowable, message) ??
				// Use java.lang.Throwable type metadata.
				JThrowableObject.CreateException(jGlobalThrowable, message);
		}
		catch (CriticalException)
		{
			// Unable to create throwable global object reference.
			if (!this._buildingException) throw;
			EnvironmentCore.DescribeException(this);
			this.Throw(throwableRef); // Throws pending exception at JNI.
			throw;
		}
		finally
		{
			this.DeleteLocalRef(throwableRef.Value);
		}
	}
	/// <summary>
	/// Retrieves throwable message.
	/// </summary>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	/// <returns>Throwable message.</returns>
	private String GetThrowableMessage(JThrowableLocalRef throwableRef)
	{
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(2);
		AccessCache access = this.GetAccess(jniTransaction, this.GetClass<JThrowableObject>());
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
		this.CheckJniError();
		JClassObject jStringClass = this.GetClass<JStringObject>();
		return new(jStringClass, localRef.Transform<JObjectLocalRef, JStringLocalRef>());
	}
	/// <summary>
	/// Sets given <see cref="JThrowableLocalRef"/> reference as pending exception.
	/// </summary>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	private void Throw(JThrowableLocalRef throwableRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.ThrowInfo);
		JResult result = nativeInterface.ErrorFunctions.Throw(this.Reference, throwableRef);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
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
	private void SetPendingException(JniException jniException, Boolean throwException)
	{
		this.Thrown = jniException;
		if (throwException) throw this.Thrown;
	}
	/// <summary>
	/// Deletes and clears unregister <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private void FreeUnregistered(JLocalObject jLocal)
	{
		this.DeleteLocalRef(jLocal.LocalReference);
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
	/// Creates a <see cref="IFixedContext{Byte}.IDisposable"/> instance from a span created in stack.
	/// </summary>
	/// <param name="stackSpan">A stack created span.</param>
	/// <returns>A <see cref="IFixedContext{T}.IDisposable"/> instance</returns>
	private IFixedContext<Byte>.IDisposable GetStackContext(scoped Span<Byte> stackSpan)
	{
		StackDisposable disposable = new(this, stackSpan.Length);
		ValPtr<Byte> ptr = (ValPtr<Byte>)stackSpan.GetUnsafeIntPtr();
		// ReSharper disable once HeapView.BoxingAllocation
		return ptr.GetUnsafeFixedContext(stackSpan.Length, disposable);
	}
	/// <summary>
	/// Checks and throws a Throwable exception in non-critical state.
	/// </summary>
	private void ExceptionOccurred()
	{
		JThrowableLocalRef throwableRef = this.GetPendingException();
		if (throwableRef == default) return;
		this._buildingException = true; // To avoid CheckJniError stack overflow.
		ThrowableException jniException;
		try
		{
			jniException = this.CreateThrowableException(throwableRef);
		}
		finally
		{
			// Restores initial CheckJniError state.
			this._buildingException = false;
		}
		this.ThrowJniException(jniException, true);
	}
	/// <summary>
	/// Checks and throws a JNI exception in critical or nested state.
	/// </summary>
	private void ExceptionCheck()
	{
		if (!this.HasPendingException()) return;
		CriticalException criticalException = this._criticalCount > 0 ?
			CriticalException.Instance :
			CriticalException.UnknownInstance;
		this.SetPendingException(criticalException, true);
	}
}