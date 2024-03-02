namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Sets a static object field to given <paramref name="classRef"/> reference.
		/// </summary>
		/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="value">The field value to set to.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void SetStaticObjectField<TField>(JClassLocalRef classRef, TField? value,
			INativeTransaction jniTransaction, JFieldId fieldId) where TField : IObject, IDataType<TField>
		{
			JObjectLocalRef valueLocalRef = this.UseObject(jniTransaction, value as JReferenceObject);
			SetStaticObjectFieldDelegate setObjectField = this.GetDelegate<SetStaticObjectFieldDelegate>();
			setObjectField(this.Reference, classRef, fieldId, valueLocalRef);
		}
		/// <summary>
		/// Sets a primitive static field to given <paramref name="classRef"/> reference.
		/// </summary>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void SetPrimitiveStaticField(JClassLocalRef classRef, ReadOnlySpan<Byte> bytes, Byte signature,
			JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					SetStaticBooleanFieldDelegate setStaticBooleanField =
						this.GetDelegate<SetStaticBooleanFieldDelegate>();
					setStaticBooleanField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					SetStaticByteFieldDelegate setStaticByteField = this.GetDelegate<SetStaticByteFieldDelegate>();
					setStaticByteField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					SetStaticCharFieldDelegate setStaticCharField = this.GetDelegate<SetStaticCharFieldDelegate>();
					setStaticCharField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<Char>(bytes));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					SetStaticDoubleFieldDelegate setStaticDoubleField =
						this.GetDelegate<SetStaticDoubleFieldDelegate>();
					setStaticDoubleField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<Double>(bytes));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					SetStaticFloatFieldDelegate setStaticFloatField = this.GetDelegate<SetStaticFloatFieldDelegate>();
					setStaticFloatField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<Single>(bytes));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					SetStaticIntFieldDelegate setStaticIntField = this.GetDelegate<SetStaticIntFieldDelegate>();
					setStaticIntField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					SetStaticLongFieldDelegate setStaticLongField = this.GetDelegate<SetStaticLongFieldDelegate>();
					setStaticLongField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					SetStaticShortFieldDelegate setStaticShortField = this.GetDelegate<SetStaticShortFieldDelegate>();
					setStaticShortField(this.Reference, classRef, fieldId, MemoryMarshal.AsRef<Int16>(bytes));
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves a primitive static field from given <paramref name="classRef"/> reference.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void GetPrimitiveStaticField(Span<Byte> bytes, JClassLocalRef classRef, Byte signature,
			JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					GetStaticBooleanFieldDelegate getStaticBooleanField =
						this.GetDelegate<GetStaticBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = getStaticBooleanField(this.Reference, classRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					GetStaticByteFieldDelegate getStaticByteField = this.GetDelegate<GetStaticByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = getStaticByteField(this.Reference, classRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					GetStaticCharFieldDelegate getStaticCharField = this.GetDelegate<GetStaticCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = getStaticCharField(this.Reference, classRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					GetStaticDoubleFieldDelegate getStaticDoubleField =
						this.GetDelegate<GetStaticDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = getStaticDoubleField(this.Reference, classRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					GetStaticFloatFieldDelegate getFloatField = this.GetDelegate<GetStaticFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = getFloatField(this.Reference, classRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					GetStaticIntFieldDelegate getStaticIntField = this.GetDelegate<GetStaticIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = getStaticIntField(this.Reference, classRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					GetStaticLongFieldDelegate getStaticLongField = this.GetDelegate<GetStaticLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = getStaticLongField(this.Reference, classRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					GetStaticShortFieldDelegate getStaticShortField = this.GetDelegate<GetStaticShortFieldDelegate>();
					MemoryMarshal.AsRef<Int16>(bytes) = getStaticShortField(this.Reference, classRef, fieldId);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Sets a field to given <paramref name="localRef"/> reference.
		/// </summary>
		/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="value">The field value to set to.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void SetObjectField<TField>(JObjectLocalRef localRef, TField? value, INativeTransaction jniTransaction,
			JFieldId fieldId) where TField : IObject, IDataType<TField>
		{
			JObjectLocalRef valueLocalRef = this.UseObject(jniTransaction, value as JReferenceObject);
			SetObjectFieldDelegate setObjectField = this.GetDelegate<SetObjectFieldDelegate>();
			setObjectField(this.Reference, localRef, fieldId, valueLocalRef);
		}
		/// <summary>
		/// Sets a primitive field to given <paramref name="localRef"/> reference.
		/// </summary>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void SetPrimitiveField(JObjectLocalRef localRef, ReadOnlySpan<Byte> bytes, Byte signature,
			JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					SetBooleanFieldDelegate setBooleanField = this.GetDelegate<SetBooleanFieldDelegate>();
					setBooleanField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					SetByteFieldDelegate setByteField = this.GetDelegate<SetByteFieldDelegate>();
					setByteField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					SetCharFieldDelegate setCharField = this.GetDelegate<SetCharFieldDelegate>();
					setCharField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Char>(bytes));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					SetDoubleFieldDelegate setDoubleField = this.GetDelegate<SetDoubleFieldDelegate>();
					setDoubleField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Double>(bytes));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					SetFloatFieldDelegate setFloatField = this.GetDelegate<SetFloatFieldDelegate>();
					setFloatField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Single>(bytes));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					SetIntFieldDelegate setIntField = this.GetDelegate<SetIntFieldDelegate>();
					setIntField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					SetLongFieldDelegate setLongField = this.GetDelegate<SetLongFieldDelegate>();
					setLongField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					SetShortFieldDelegate setShortField = this.GetDelegate<SetShortFieldDelegate>();
					setShortField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Int16>(bytes));
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves an object field on <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TField"><see cref="IDataType"/> type of field result.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <returns><typeparamref name="TField"/> field instance.</returns>
		private TField? GetObjectField<TField>(JObjectLocalRef localRef, JFieldId fieldId)
			where TField : IObject, IDataType<TField>
		{
			GetObjectFieldDelegate getObjectField = this.GetDelegate<GetObjectFieldDelegate>();
			JObjectLocalRef resultLocalRef = getObjectField(this.Reference, localRef, fieldId);
			this.CheckJniError();
			return this.CreateObject<TField>(resultLocalRef, true);
		}
		/// <summary>
		/// Retrieves a primitive field from given <paramref name="localRef"/> reference.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void GetPrimitiveField(Span<Byte> bytes, JObjectLocalRef localRef, Byte signature, JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					GetBooleanFieldDelegate getBooleanField = this.GetDelegate<GetBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = getBooleanField(this.Reference, localRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					GetByteFieldDelegate getByteField = this.GetDelegate<GetByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = getByteField(this.Reference, localRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					GetCharFieldDelegate getCharField = this.GetDelegate<GetCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = getCharField(this.Reference, localRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					GetDoubleFieldDelegate getDoubleField = this.GetDelegate<GetDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = getDoubleField(this.Reference, localRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					GetFloatFieldDelegate getFloatField = this.GetDelegate<GetFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = getFloatField(this.Reference, localRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					GetIntFieldDelegate getIntField = this.GetDelegate<GetIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = getIntField(this.Reference, localRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					GetLongFieldDelegate getLongField = this.GetDelegate<GetLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = getLongField(this.Reference, localRef, fieldId);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					GetShortFieldDelegate getShortField = this.GetDelegate<GetShortFieldDelegate>();
					MemoryMarshal.AsRef<Int16>(bytes) = getShortField(this.Reference, localRef, fieldId);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves a <see cref="JObjectLocalRef"/> reflected from current definition on
		/// <paramref name="declaringClass"/>.
		/// </summary>
		/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
		/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="isStatic">
		/// Indicates whether <paramref name="definition"/> matches with an static method in <paramref name="declaringClass"/>.
		/// </param>
		/// <returns>A <see cref="JMethodObject"/> instance.</returns>
		private JObjectLocalRef GetReflectedCall(JCallDefinition definition, JClassObject declaringClass,
			Boolean isStatic)
		{
			ValidationUtilities.ThrowIfProxy(declaringClass);
			using INativeTransaction jniTransaction = isStatic ?
				this.GetClassTransaction(declaringClass, definition, out JMethodId methodId, false) :
				this.GetInstanceTransaction(declaringClass, definition, out methodId);
			ToReflectedMethodDelegate toReflectedMethod = this.GetDelegate<ToReflectedMethodDelegate>();
			JObjectLocalRef localRef = toReflectedMethod(this.Reference, declaringClass.Reference, methodId,
			                                             isStatic ? JBoolean.TrueValue : JBoolean.FalseValue);
			if (localRef == default) this.CheckJniError();
			return localRef;
		}
		/// <summary>
		/// Invokes an object function on given <see cref="JObjectLocalRef"/> reference.
		/// </summary>
		/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		private TResult? CallObjectFunction<TResult>(JFunctionDefinition definition, JObjectLocalRef localRef,
			JClassLocalRef classRef, IObject?[] args, INativeTransaction jniTransaction, JMethodId methodId)
			where TResult : IDataType<TResult>
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			JObjectLocalRef resultLocalRef;
			if (classRef.IsDefault)
			{
				CallObjectMethodADelegate callObjectMethod = this.GetDelegate<CallObjectMethodADelegate>();
				resultLocalRef = callObjectMethod(this.Reference, localRef, methodId,
				                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			else
			{
				CallNonVirtualObjectMethodADelegate callNonVirtualObjectObjectMethod =
					this.GetDelegate<CallNonVirtualObjectMethodADelegate>();
				resultLocalRef = callNonVirtualObjectObjectMethod(this.Reference, localRef, classRef, methodId,
				                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			this.CheckJniError();
			return this.CreateObject<TResult>(resultLocalRef, true);
		}
		/// <summary>
		/// Invokes a primitive function on given <see cref="JObjectLocalRef"/> reference.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		private void CallPrimitiveFunction(Span<Byte> bytes, JFunctionDefinition definition, JObjectLocalRef localRef,
			JClassLocalRef classRef, IObject?[] args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			if (classRef.IsDefault)
				this.CallPrimitiveMethod(bytes, localRef, definition.Information[1][^1], methodId, argsMemory);
			else
				this.CallNonVirtualPrimitiveMethod(bytes, localRef, classRef, definition.Information[1][^1], methodId,
				                                   argsMemory);
			this.CheckJniError();
		}
		/// <summary>
		/// Invokes a primitive static function on given <paramref name="classRef"/> reference.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		private void CallPrimitiveStaticFunction(Span<Byte> bytes, JFunctionDefinition definition,
			JClassLocalRef classRef, IObject?[] args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			switch (definition.Information[1][^1])
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					CallStaticBooleanMethodADelegate callStaticBooleanMethod =
						this.GetDelegate<CallStaticBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callStaticBooleanMethod(
						this.Reference, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					CallStaticByteMethodADelegate callByteMethod = this.GetDelegate<CallStaticByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callByteMethod(this.Reference, classRef, methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					CallStaticCharMethodADelegate callCharMethod = this.GetDelegate<CallStaticCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callCharMethod(this.Reference, classRef, methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					CallStaticDoubleMethodADelegate callDoubleMethod =
						this.GetDelegate<CallStaticDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callDoubleMethod(
						this.Reference, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					CallStaticFloatMethodADelegate callFloatMethod = this.GetDelegate<CallStaticFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callFloatMethod(
						this.Reference, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					CallStaticIntMethodADelegate callIntMethod = this.GetDelegate<CallStaticIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callIntMethod(this.Reference, classRef, methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					CallStaticLongMethodADelegate callLongMethod = this.GetDelegate<CallStaticLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callLongMethod(this.Reference, classRef, methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar: //S
					CallStaticShortMethodADelegate callShortMethod = this.GetDelegate<CallStaticShortMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callShortMethod(this.Reference, classRef, methodId,
					                                                    (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Invokes a static object function on given <paramref name="classRef"/> reference.
		/// </summary>
		/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		/// <returns><typeparamref name="TResult"/> function result.</returns>
		private TResult? CallObjectStaticFunction<TResult>(JFunctionDefinition definition, JClassLocalRef classRef,
			IObject?[] args, INativeTransaction jniTransaction, JMethodId methodId) where TResult : IDataType<TResult>
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			CallStaticObjectMethodADelegate callStaticObjectMethod =
				this.GetDelegate<CallStaticObjectMethodADelegate>();
			JObjectLocalRef localRef = callStaticObjectMethod(this.Reference, classRef, methodId,
			                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			this.CheckJniError();
			return this.CreateObject<TResult>(localRef, true);
		}
		/// <summary>
		/// Invokes a method on given <see cref="JObjectLocalRef"/> reference.
		/// </summary>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		private void CallMethod(JMethodDefinition definition, JObjectLocalRef localRef, JClassLocalRef classRef,
			IObject?[] args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);

			if (classRef.IsDefault)
			{
				CallVoidMethodADelegate callVoidMethod = this.GetDelegate<CallVoidMethodADelegate>();
				callVoidMethod(this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			else
			{
				CallNonVirtualVoidMethodADelegate callNonVirtualVoidObjectMethod =
					this.GetDelegate<CallNonVirtualVoidMethodADelegate>();
				callNonVirtualVoidObjectMethod(this.Reference, localRef, classRef, methodId,
				                               (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}

			this.CheckJniError();
		}
		/// <summary>
		/// Invokes a static method on given <paramref name="classRef"/> reference.
		/// </summary>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		private void CallStaticMethod(JMethodDefinition definition, JClassLocalRef classRef, IObject?[] args,
			INativeTransaction jniTransaction, JMethodId methodId)
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			CallStaticVoidMethodADelegate callStaticVoidMethod = this.GetDelegate<CallStaticVoidMethodADelegate>();
			callStaticVoidMethod(this.Reference, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			this.CheckJniError();
		}
		/// <summary>
		/// Uses <paramref name="jObject"/> into <paramref name="jniTransaction"/>.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference</returns>
		private JObjectLocalRef UseObject(INativeTransaction jniTransaction, JReferenceObject? jObject)
			=> jObject is JLocalObject jLocal ? this.UseObject(jniTransaction, jLocal) : jniTransaction.Add(jObject);
		/// <summary>
		/// Uses <paramref name="jLocal"/> into <paramref name="jniTransaction"/>.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference</returns>
		private JObjectLocalRef UseObject(INativeTransaction jniTransaction, JLocalObject? jLocal)
			=> jLocal is JClassObject jLocalClass ?
				jniTransaction.Add(this.ReloadClass(jLocalClass).Value) :
				jniTransaction.Add(jLocal);
		/// <summary>
		/// Indicates whether current call must use <see langword="stackalloc"/> or <see langword="new"/> to
		/// hold JNI call parameter.
		/// </summary>
		/// <param name="jCall">A <see cref="JCallDefinition"/> instance.</param>
		/// <param name="requiredBytes">Output. Number of bytes to allocate.</param>
		/// <returns>
		/// <see langword="true"/> if current call must use <see langword="stackalloc"/>; otherwise,
		/// <see langword="false"/>.
		/// </returns>
		private Boolean UseStackAlloc(JCallDefinition jCall, out Int32 requiredBytes)
		{
			requiredBytes = jCall.Count * JValue.Size;
			return this.UseStackAlloc(requiredBytes);
		}
		/// <summary>
		/// Retrieves <paramref name="argSpan"/> as a <see cref="JValue"/> span containing <paramref name="args"/> information.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> transaction.</param>
		/// <param name="args">A <see cref="IObject"/> array.</param>
		/// <param name="argSpan">Destination span.</param>
		/// <exception cref="InvalidOperationException">Invalid object.</exception>
		private void CopyAsJValue(INativeTransaction jniTransaction, IReadOnlyList<IObject?> args, Span<Byte> argSpan)
		{
			Span<JValue> result = argSpan.AsValues<Byte, JValue>();
			for (Int32 i = 0; i < args.Count; i++)
			{
				switch (args[i])
				{
					case null:
						result[i] = JValue.Empty;
						continue;
					case IPrimitiveType:
						args[i]!.CopyTo(result, i);
						break;
					default:
						JReferenceObject referenceObject = (args[i] as JReferenceObject)!;
						ValidationUtilities.ThrowIfProxy(referenceObject);
						this.ReloadClass(referenceObject as JClassObject);
						ValidationUtilities.ThrowIfDefault(referenceObject, $"Invalid object at {i}.");
						jniTransaction.Add(referenceObject);
						args[i]!.CopyTo(result, i);
						break;
				}
			}
		}
		/// <summary>
		/// Performs primitive method call.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="argsMemory">Fixed memory with parameters.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private void CallPrimitiveMethod(Span<Byte> bytes, JObjectLocalRef localRef, Byte signature, JMethodId methodId,
			IFixedPointer argsMemory)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					CallBooleanMethodADelegate callBooleanMethod = this.GetDelegate<CallBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callBooleanMethod(
						this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					CallByteMethodADelegate callByteMethod = this.GetDelegate<CallByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callByteMethod(this.Reference, localRef,
					                                                   methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					CallCharMethodADelegate callCharMethod = this.GetDelegate<CallCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callCharMethod(this.Reference, localRef,
					                                                  methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					CallDoubleMethodADelegate callDoubleMethod = this.GetDelegate<CallDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callDoubleMethod(
						this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					CallFloatMethodADelegate callFloatMethod = this.GetDelegate<CallFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callFloatMethod(
						this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					CallIntMethodADelegate callIntMethod = this.GetDelegate<CallIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callIntMethod(this.Reference, localRef,
					                                                  methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					CallLongMethodADelegate callLongMethod = this.GetDelegate<CallLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callLongMethod(this.Reference, localRef,
					                                                   methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					CallShortMethodADelegate callShortMethod = this.GetDelegate<CallShortMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callShortMethod(this.Reference, localRef,
					                                                    methodId,
					                                                    (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Performs primitive non-Virtual method call.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="argsMemory">Fixed memory with parameters.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private void CallNonVirtualPrimitiveMethod(Span<Byte> bytes, JObjectLocalRef localRef, JClassLocalRef classRef,
			Byte signature, JMethodId methodId, IFixedPointer argsMemory)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					CallNonVirtualBooleanMethodADelegate callNonVirtualBooleanMethod =
						this.GetDelegate<CallNonVirtualBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callNonVirtualBooleanMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					CallNonVirtualByteMethodADelegate callNonVirtualByteMethod =
						this.GetDelegate<CallNonVirtualByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callNonVirtualByteMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					CallNonVirtualCharMethodADelegate callNonVirtualCharMethod =
						this.GetDelegate<CallNonVirtualCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callNonVirtualCharMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					CallNonVirtualDoubleMethodADelegate callNonVirtualDoubleMethod =
						this.GetDelegate<CallNonVirtualDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callNonVirtualDoubleMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					CallNonVirtualFloatMethodADelegate callNonVirtualFloatMethod =
						this.GetDelegate<CallNonVirtualFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callNonVirtualFloatMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					CallNonVirtualIntMethodADelegate callNonVirtualIntMethod =
						this.GetDelegate<CallNonVirtualIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callNonVirtualIntMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					CallNonVirtualLongMethodADelegate callNonVirtualLongMethod =
						this.GetDelegate<CallNonVirtualLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callNonVirtualLongMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					CallNonVirtualShortMethodADelegate callNonVirtualShortMethod =
						this.GetDelegate<CallNonVirtualShortMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callNonVirtualShortMethod(
						this.Reference, localRef, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Creates a new object using JNI NewObject call.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
		private JObjectLocalRef NewObject(JClassObject jClass, JConstructorDefinition definition,
			params IObject?[] args)
		{
			ValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(1 + definition.ReferenceCount);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JMethodId methodId = access.GetMethodId(definition, this._env);
			return this.NewObject(definition, jClass.Reference, args, jniTransaction, methodId);
		}
		/// <summary>
		/// Creates a new object using JNI NewObject call.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> instance.</param>
		/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
		private JObjectLocalRef NewObject(JConstructorDefinition definition, JClassLocalRef classRef, IObject?[] args,
			INativeTransaction jniTransaction, JMethodId methodId)
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			NewObjectADelegate newObject = this.GetDelegate<NewObjectADelegate>();
			JObjectLocalRef localRef = newObject(this.Reference, classRef, methodId,
			                                     (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			this.CheckJniError();
			return localRef;
		}
		/// <summary>
		/// Retrieves static field object instance reference.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
		private JObjectLocalRef GetStaticObjectField(JClassObject jClass, JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._env);
			return this.GetStaticObjectField(jClass.Reference, fieldId);
		}
		/// <summary>
		/// Retrieves static field object instance reference.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="fieldId">A <see cref="JFieldId"/> identifier.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
		private JObjectLocalRef GetStaticObjectField(JClassLocalRef classRef, JFieldId fieldId)
		{
			GetStaticObjectFieldDelegate getStaticObjectField = this.GetDelegate<GetStaticObjectFieldDelegate>();
			JObjectLocalRef localRef = getStaticObjectField(this.Reference, classRef, fieldId);
			this.CheckJniError();
			return localRef;
		}
		/// <summary>
		/// Creates <see cref="INativeTransaction"/> for class transaction.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="definition">Transaction call definition.</param>
		/// <param name="methodId">Call method id.</param>
		/// <param name="execution">Indicates whether transaction is for call execution..</param>
		/// <returns>A <see cref="INativeTransaction"/> instance.</returns>
		private INativeTransaction GetClassTransaction(JClassObject jClass, JCallDefinition definition,
			out JMethodId methodId, Boolean execution = true)
		{
			INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(1 + (execution ? definition.ReferenceCount : 0));
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			methodId = access.GetStaticMethodId(definition, this._env);
			return jniTransaction;
		}
		/// <summary>
		/// Creates <see cref="INativeTransaction"/> for instance transaction.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="definition">Transaction call definition.</param>
		/// <param name="methodId">Output. Call method id.</param>
		/// <returns>A <see cref="INativeTransaction"/> instance.</returns>
		private INativeTransaction GetInstanceTransaction(JClassObject jClass, JCallDefinition definition,
			out JMethodId methodId)
		{
			INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			methodId = access.GetMethodId(definition, this._env);
			return jniTransaction;
		}
		/// <summary>
		/// Creates <see cref="INativeTransaction"/> for instance transaction.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="definition">Transaction call definition.</param>
		/// <param name="localRef">Output. Used object reference.</param>
		/// <param name="methodId">Output. Call method id.</param>
		/// <returns>A <see cref="INativeTransaction"/> instance.</returns>
		private INativeTransaction GetInstanceTransaction(JClassObject jClass, JLocalObject jLocal,
			JCallDefinition definition, out JObjectLocalRef localRef, out JMethodId methodId)
		{
			INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			methodId = access.GetMethodId(definition, this._env);
			localRef = this.UseObject(jniTransaction, jLocal);
			return jniTransaction;
		}
	}
}