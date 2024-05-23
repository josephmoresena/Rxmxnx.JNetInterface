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
				UnicodePrimitiveSignatures.BooleanSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetBooleanArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface
						.ReleaseBooleanArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetBooleanArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetBooleanArrayRegionInfo,
					_ => NativeInterface.NewBooleanArrayInfo,
				},
				UnicodePrimitiveSignatures.ByteSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetByteArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseByteArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetByteArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetByteArrayRegionInfo,
					_ => NativeInterface.NewByteArrayInfo,
				},
				UnicodePrimitiveSignatures.CharSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetCharArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseCharArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetCharArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetCharArrayRegionInfo,
					_ => NativeInterface.NewCharArrayInfo,
				},
				UnicodePrimitiveSignatures.DoubleSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetDoubleArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements =>
						NativeInterface.ReleaseDoubleArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetDoubleArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetDoubleArrayRegionInfo,
					_ => NativeInterface.NewDoubleArrayInfo,
				},
				UnicodePrimitiveSignatures.FloatSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetFloatArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseFloatArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetFloatArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetFloatArrayRegionInfo,
					_ => NativeInterface.NewFloatArrayInfo,
				},
				UnicodePrimitiveSignatures.IntSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetIntArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseIntArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetIntArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetIntArrayRegionInfo,
					_ => NativeInterface.NewIntArrayInfo,
				},
				UnicodePrimitiveSignatures.LongSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetLongArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseLongArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetLongArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetLongArrayRegionInfo,
					_ => NativeInterface.NewLongArrayInfo,
				},
				UnicodePrimitiveSignatures.ShortSignatureChar => arrayFunction switch
				{
					ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetShortArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseShortArrayElementsInfo,
					ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetShortArrayRegionInfo,
					ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetShortArrayRegionInfo,
					_ => NativeInterface.NewShortArrayInfo,
				},
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
				UnicodePrimitiveSignatures.BooleanSignatureChar => nonVirtual ?
					NativeInterface.CallBooleanMethodInfo :
					NativeInterface.CallNonVirtualBooleanMethodInfo,
				UnicodePrimitiveSignatures.ByteSignatureChar => nonVirtual ?
					NativeInterface.CallByteMethodInfo :
					NativeInterface.CallNonVirtualByteMethodInfo,
				UnicodePrimitiveSignatures.CharSignatureChar => nonVirtual ?
					NativeInterface.CallCharMethodInfo :
					NativeInterface.CallNonVirtualCharMethodInfo,
				UnicodePrimitiveSignatures.DoubleSignatureChar => nonVirtual ?
					NativeInterface.CallDoubleMethodInfo :
					NativeInterface.CallNonVirtualCharMethodInfo,
				UnicodePrimitiveSignatures.FloatSignatureChar => nonVirtual ?
					NativeInterface.CallFloatMethodInfo :
					NativeInterface.CallNonVirtualFloatMethodInfo,
				UnicodePrimitiveSignatures.IntSignatureChar => nonVirtual ?
					NativeInterface.CallIntMethodInfo :
					NativeInterface.CallNonVirtualIntMethodInfo,
				UnicodePrimitiveSignatures.LongSignatureChar => nonVirtual ?
					NativeInterface.CallLongMethodInfo :
					NativeInterface.CallNonVirtualLongMethodInfo,
				UnicodePrimitiveSignatures.ShortSignatureChar => nonVirtual ?
					NativeInterface.CallShortMethodInfo :
					NativeInterface.CallNonVirtualShortMethodInfo,
				UnicodePrimitiveSignatures.VoidSignatureChar => nonVirtual ?
					NativeInterface.CallVoidMethodInfo :
					NativeInterface.CallNonVirtualVoidMethodInfo,
				UnicodeObjectSignatures.ObjectSignaturePrefixChar => nonVirtual ?
					NativeInterface.CallObjectMethodInfo :
					NativeInterface.CallNonVirtualObjectMethodInfo,
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
		/// <param name="getField">Indicates whether current call is get field value.</param>
		/// <returns>A managed <see cref="FieldFunctionSet{JObjectLocalRef}"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref readonly FieldFunctionSet<JObjectLocalRef> GetInstanceFieldFunctions(Byte primitiveSignature,
			Boolean getField)
		{
			JniMethodInfo info = primitiveSignature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => getField ?
					NativeInterface.GetBooleanFieldInfo :
					NativeInterface.SetBooleanFieldInfo,
				UnicodePrimitiveSignatures.ByteSignatureChar => getField ?
					NativeInterface.GetByteFieldInfo :
					NativeInterface.SetByteFieldInfo,
				UnicodePrimitiveSignatures.CharSignatureChar => getField ?
					NativeInterface.GetCharFieldInfo :
					NativeInterface.SetCharFieldInfo,
				UnicodePrimitiveSignatures.DoubleSignatureChar => getField ?
					NativeInterface.GetDoubleFieldInfo :
					NativeInterface.SetDoubleFieldInfo,
				UnicodePrimitiveSignatures.FloatSignatureChar => getField ?
					NativeInterface.GetFloatFieldInfo :
					NativeInterface.SetFloatFieldInfo,
				UnicodePrimitiveSignatures.IntSignatureChar => getField ?
					NativeInterface.GetIntFieldInfo :
					NativeInterface.SetIntFieldInfo,
				UnicodePrimitiveSignatures.LongSignatureChar => getField ?
					NativeInterface.GetLongFieldInfo :
					NativeInterface.SetLongFieldInfo,
				UnicodePrimitiveSignatures.ShortSignatureChar => getField ?
					NativeInterface.GetShortFieldInfo :
					NativeInterface.SetShortFieldInfo,
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
			return ref this.GetNativeInterface<NativeInterface>(info).InstanceFieldFunctions;
		}
		/// <summary>
		/// Retrieves managed <see cref="FieldFunctionSet{JClassLocalRef}"/> reference from current instance.
		/// </summary>
		/// <param name="primitiveSignature">Primitive signature char.</param>
		/// <param name="getField">Indicates whether current call is get field value.</param>
		/// <returns>A managed <see cref="FieldFunctionSet{JClassLocalRef}"/> reference from current instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref readonly FieldFunctionSet<JClassLocalRef> GetStaticFieldFunctions(Byte primitiveSignature,
			Boolean getField)
		{
			JniMethodInfo info = primitiveSignature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => getField ?
					NativeInterface.GetStaticBooleanFieldInfo :
					NativeInterface.SetStaticBooleanFieldInfo,
				UnicodePrimitiveSignatures.ByteSignatureChar => getField ?
					NativeInterface.GetStaticByteFieldInfo :
					NativeInterface.SetStaticByteFieldInfo,
				UnicodePrimitiveSignatures.CharSignatureChar => getField ?
					NativeInterface.GetStaticCharFieldInfo :
					NativeInterface.SetStaticCharFieldInfo,
				UnicodePrimitiveSignatures.DoubleSignatureChar => getField ?
					NativeInterface.GetStaticDoubleFieldInfo :
					NativeInterface.SetStaticDoubleFieldInfo,
				UnicodePrimitiveSignatures.FloatSignatureChar => getField ?
					NativeInterface.GetStaticFloatFieldInfo :
					NativeInterface.SetStaticFloatFieldInfo,
				UnicodePrimitiveSignatures.IntSignatureChar => getField ?
					NativeInterface.GetStaticIntFieldInfo :
					NativeInterface.SetStaticIntFieldInfo,
				UnicodePrimitiveSignatures.LongSignatureChar => getField ?
					NativeInterface.GetStaticLongFieldInfo :
					NativeInterface.SetStaticLongFieldInfo,
				UnicodePrimitiveSignatures.ShortSignatureChar => getField ?
					NativeInterface.GetStaticShortFieldInfo :
					NativeInterface.SetStaticShortFieldInfo,
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
			JResult result = utf8Message.WithSafeFixed(this, EnvironmentCache.ThrowNew<TThrowable>);
			ImplementationValidationUtilities.ThrowIfInvalidResult(result);

			ThrowableException throwableException =
				this.CreateThrowableException<TThrowable>(this.GetPendingException(), message);
			this.ThrowJniException(throwableException, throwException);
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
					this.Reference, throwableRef.Value, getNameId, ReadOnlyValPtr<JValue>.Zero);
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
	}
}