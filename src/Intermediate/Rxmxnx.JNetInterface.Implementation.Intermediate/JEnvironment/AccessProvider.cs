namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IAccessProvider
	{
		public void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			switch (definition.Information[1][^1])
			{
				case 0x5A: //Z
					GetBooleanFieldDelegate getBooleanField = this.GetDelegate<GetBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = getBooleanField(this.Reference, localRef, fieldId);
					break;
				case 0x42: //B
					GetByteFieldDelegate getByteField = this.GetDelegate<GetByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = getByteField(this.Reference, localRef, fieldId);
					break;
				case 0x43: //C
					GetCharFieldDelegate getCharField = this.GetDelegate<GetCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = getCharField(this.Reference, localRef, fieldId);
					break;
				case 0x44: //D
					GetDoubleFieldDelegate getDoubleField = this.GetDelegate<GetDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = getDoubleField(this.Reference, localRef, fieldId);
					break;
				case 0x46: //F
					GetFloatFieldDelegate getFloatField = this.GetDelegate<GetFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = getFloatField(this.Reference, localRef, fieldId);
					break;
				case 0x49: //I
					GetIntFieldDelegate getIntField = this.GetDelegate<GetIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = getIntField(this.Reference, localRef, fieldId);
					break;
				case 0x4A: //J
					GetLongFieldDelegate getLongField = this.GetDelegate<GetLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = getLongField(this.Reference, localRef, fieldId);
					break;
				case 0x53: //S
					GetShortFieldDelegate getShortField = this.GetDelegate<GetShortFieldDelegate>();
					MemoryMarshal.AsRef<Int16>(bytes) = getShortField(this.Reference, localRef, fieldId);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		public void SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
			ReadOnlySpan<Byte> bytes)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			switch (definition.Information[1][^1])
			{
				case 0x5A: //Z
					SetBooleanFieldDelegate setBooleanField = this.GetDelegate<SetBooleanFieldDelegate>();
					setBooleanField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case 0x42: //B
					SetByteFieldDelegate setByteField = this.GetDelegate<SetByteFieldDelegate>();
					setByteField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case 0x43: //C
					SetCharFieldDelegate setCharField = this.GetDelegate<SetCharFieldDelegate>();
					setCharField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Char>(bytes));
					break;
				case 0x44: //D
					SetDoubleFieldDelegate setDoubleField = this.GetDelegate<SetDoubleFieldDelegate>();
					setDoubleField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Double>(bytes));
					break;
				case 0x46: //F
					SetFloatFieldDelegate setFloatField = this.GetDelegate<SetFloatFieldDelegate>();
					setFloatField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Single>(bytes));
					break;
				case 0x49: //I
					SetIntFieldDelegate setIntField = this.GetDelegate<SetIntFieldDelegate>();
					setIntField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case 0x4A: //J
					SetLongFieldDelegate setLongField = this.GetDelegate<SetLongFieldDelegate>();
					setLongField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case 0x53: //S
					SetShortFieldDelegate setShortField = this.GetDelegate<SetShortFieldDelegate>();
					setShortField(this.Reference, localRef, fieldId, MemoryMarshal.AsRef<Int16>(bytes));
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		public void GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._mainClasses.Environment);
			switch (definition.Information[1][^1])
			{
				case 0x5A: //Z
					GetStaticBooleanFieldDelegate getStaticBooleanField =
						this.GetDelegate<GetStaticBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = getStaticBooleanField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x42: //B
					GetStaticByteFieldDelegate getStaticByteField = this.GetDelegate<GetStaticByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = getStaticByteField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x43: //C
					GetStaticCharFieldDelegate getStaticCharField = this.GetDelegate<GetStaticCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = getStaticCharField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x44: //D
					GetStaticDoubleFieldDelegate getStaticDoubleField =
						this.GetDelegate<GetStaticDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) =
						getStaticDoubleField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x46: //F
					GetStaticFloatFieldDelegate getFloatField = this.GetDelegate<GetStaticFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = getFloatField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x49: //I
					GetStaticIntFieldDelegate getStaticIntField = this.GetDelegate<GetStaticIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = getStaticIntField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x4A: //J
					GetStaticLongFieldDelegate getStaticLongField = this.GetDelegate<GetStaticLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = getStaticLongField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x53: //S
					GetStaticShortFieldDelegate getStaticShortField = this.GetDelegate<GetStaticShortFieldDelegate>();
					MemoryMarshal.AsRef<Int16>(bytes) = getStaticShortField(this.Reference, jClass.Reference, fieldId);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		public void SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._mainClasses.Environment);
			switch (definition.Information[1][^1])
			{
				case 0x5A: //Z
					SetStaticBooleanFieldDelegate setStaticBooleanField =
						this.GetDelegate<SetStaticBooleanFieldDelegate>();
					setStaticBooleanField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case 0x42: //B
					SetStaticByteFieldDelegate setStaticByteField = this.GetDelegate<SetStaticByteFieldDelegate>();
					setStaticByteField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case 0x43: //C
					SetStaticCharFieldDelegate setStaticCharField = this.GetDelegate<SetStaticCharFieldDelegate>();
					setStaticCharField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Char>(bytes));
					break;
				case 0x44: //D
					SetStaticDoubleFieldDelegate setStaticDoubleField =
						this.GetDelegate<SetStaticDoubleFieldDelegate>();
					setStaticDoubleField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Double>(bytes));
					break;
				case 0x46: //F
					SetStaticFloatFieldDelegate setStaticFloatField = this.GetDelegate<SetStaticFloatFieldDelegate>();
					setStaticFloatField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Single>(bytes));
					break;
				case 0x49: //I
					SetStaticIntFieldDelegate setStaticIntField = this.GetDelegate<SetStaticIntFieldDelegate>();
					setStaticIntField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case 0x4A: //J
					SetStaticLongFieldDelegate setStaticLongField = this.GetDelegate<SetStaticLongFieldDelegate>();
					setStaticLongField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case 0x53: //S
					SetStaticShortFieldDelegate setStaticShortField = this.GetDelegate<SetStaticShortFieldDelegate>();
					setStaticShortField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int16>(bytes));
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}

		public void CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass, JFunctionDefinition definition,
			IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JMethodId methodId = access.GetStaticMethodId(definition, this._mainClasses.Environment);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			switch (definition.Information[1][^1])
			{
				case 0x5A: //Z
					CallStaticBooleanMethodADelegate callStaticBooleanMethod =
						this.GetDelegate<CallStaticBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callStaticBooleanMethod(
						this.Reference, jClass.Reference, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x42: //B
					CallStaticByteMethodADelegate callByteMethod = this.GetDelegate<CallStaticByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callByteMethod(this.Reference, jClass.Reference, methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x43: //C
					CallStaticCharMethodADelegate callCharMethod = this.GetDelegate<CallStaticCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callCharMethod(this.Reference, jClass.Reference, methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x44: //D
					CallStaticDoubleMethodADelegate callDoubleMethod =
						this.GetDelegate<CallStaticDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callDoubleMethod(
						this.Reference, jClass.Reference, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x46: //F
					CallStaticFloatMethodADelegate callFloatMethod = this.GetDelegate<CallStaticFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callFloatMethod(
						this.Reference, jClass.Reference, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x49: //I
					CallStaticIntMethodADelegate callIntMethod = this.GetDelegate<CallStaticIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callIntMethod(this.Reference, jClass.Reference, methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x4A: //J
					CallStaticLongMethodADelegate callLongMethod = this.GetDelegate<CallStaticLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callLongMethod(this.Reference, jClass.Reference, methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x53: //S
					CallStaticShortMethodADelegate callShortMethod = this.GetDelegate<CallStaticShortMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callShortMethod(this.Reference, jClass.Reference, methodId,
					                                                    (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		public void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jLocal.Class);
			JMethodId methodId = access.GetStaticMethodId(definition, this._mainClasses.Environment);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			if (!nonVirtual)
				this.CallPrimitiveMethod(bytes, localRef, definition.Information[1][^1], methodId, argsMemory);
			else
				this.CallNonVirtualPrimitiveMethod(bytes, localRef, jClass, definition.Information[1][^1], methodId,
				                                   argsMemory);
			this.CheckJniError();
		}

		public TField? GetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition)
			where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.GetPrimitiveField(bytes, jLocal, jClass, definition);
				return (TField)primitiveMetadata.CreateInstance(bytes);
			}
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			this.ReloadClass(jLocal as JClassObject);
			GetObjectFieldDelegate getObjectField = this.GetDelegate<GetObjectFieldDelegate>();
			JObjectLocalRef resultLocalRef = getObjectField(this.Reference, localRef, fieldId);
			return this.CreateObject<TField>(resultLocalRef, true);
		}
		public void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
			TField? value) where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				(value as IPrimitiveType)!.CopyTo(bytes);
				this.SetPrimitiveField(jLocal, jClass, definition, bytes);
				return;
			}
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			JObjectLocalRef valueLocalRef = this.UseObject(jniTransaction, value as JLocalObject);
			SetObjectFieldDelegate setObjectField = this.GetDelegate<SetObjectFieldDelegate>();
			setObjectField(this.Reference, localRef, fieldId, valueLocalRef);
		}
		public TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
			where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.GetPrimitiveStaticField(bytes, jClass, definition);
				return (TField)primitiveMetadata.CreateInstance(bytes);
			}
			JObjectLocalRef localRef = this.GetStaticObjectField(jClass, definition);
			return this.CreateObject<TField>(localRef, true);
		}
		public void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
			where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				(value as IPrimitiveType)!.CopyTo(bytes);
				this.SetPrimitiveStaticField(jClass, definition, bytes);
				return;
			}
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._mainClasses.Environment);
			JObjectLocalRef valueLocalRef = this.UseObject(jniTransaction, value as JLocalObject);
			SetStaticObjectFieldDelegate setObjectField = this.GetDelegate<SetStaticObjectFieldDelegate>();
			setObjectField(this.Reference, jClass.Reference, fieldId, valueLocalRef);
		}
		public TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition, IObject?[] args)
			where TObject : JLocalObject, IDataType<TObject>
		{
			JObjectLocalRef localRef = this.NewObject(jClass, definition, args);
			return this.CreateObject<TObject>(localRef, true)!;
		}
		public TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
			IObject?[] args) where TResult : IDataType<TResult>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TResult>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.CallPrimitiveStaticFunction(bytes, jClass, definition, args);
				return (TResult)primitiveMetadata.CreateInstance(bytes);
			}
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JMethodId methodId = access.GetStaticMethodId(definition, this._mainClasses.Environment);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			CallStaticObjectMethodADelegate callStaticObjectMethod =
				this.GetDelegate<CallStaticObjectMethodADelegate>();
			JObjectLocalRef localRef = callStaticObjectMethod(this.Reference, jClass.Reference, methodId,
			                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			this.CheckJniError();
			return this.CreateObject<TResult>(localRef, true);
		}
		public void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JMethodId methodId = access.GetStaticMethodId(definition, this._mainClasses.Environment);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			CallStaticVoidMethodADelegate callStaticVoidMethod = this.GetDelegate<CallStaticVoidMethodADelegate>();
			callStaticVoidMethod(this.Reference, jClass.Reference, methodId,
			                     (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			this.CheckJniError();
		}
		public TResult? CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass, JFunctionDefinition definition,
			Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TResult>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.CallPrimitiveFunction(bytes, jLocal, jClass, definition, nonVirtual, args);
				return (TResult)primitiveMetadata.CreateInstance(bytes);
			}
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			JObjectLocalRef resultLocalRef;
			if (!nonVirtual)
			{
				CallObjectMethodADelegate callObjectMethod = this.GetDelegate<CallObjectMethodADelegate>();
				resultLocalRef = callObjectMethod(this.Reference, localRef, methodId,
				                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			else
			{
				CallNonVirtualObjectMethodADelegate callNonVirtualObjectObjectMethod =
					this.GetDelegate<CallNonVirtualObjectMethodADelegate>();
				resultLocalRef = callNonVirtualObjectObjectMethod(this.Reference, localRef, jClass.Reference, methodId,
				                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			this.CheckJniError();
			return this.CreateObject<TResult>(resultLocalRef, true);
		}
		public void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
			Boolean nonVirtual, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			if (!nonVirtual)
			{
				CallVoidMethodADelegate callVoidMethod = this.GetDelegate<CallVoidMethodADelegate>();
				callVoidMethod(this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			else
			{
				CallNonVirtualVoidMethodADelegate callNonVirtualVoidObjectMethod =
					this.GetDelegate<CallNonVirtualVoidMethodADelegate>();
				callNonVirtualVoidObjectMethod(this.Reference, localRef, jClass.Reference, methodId,
				                               (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			this.CheckJniError();
		}
		public void RegisterNatives(JClassObject jClass, IReadOnlyList<JNativeCall> calls)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			RegisterNativesDelegate registerNatives = this.GetDelegate<RegisterNativesDelegate>();
			Int32 requiredBytes = calls.Count * JNativeMethodValue.Size;
			Boolean useStackAlloc = this.UseStackAlloc(requiredBytes);
			List<MemoryHandle> handles = new(calls.Count);
			using IFixedContext<JNativeMethodValue>.IDisposable argsMemory = useStackAlloc ?
				JEnvironmentCache.AllocToFixedContext(stackalloc JNativeMethodValue[calls.Count], this) :
				new JNativeMethodValue[calls.Count].AsMemory().GetFixedContext();
			for (Int32 i = 0; i < calls.Count; i++)
				argsMemory.Values[i] = JNativeMethodValue.Create(calls[i], handles);
			try
			{
				using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
				JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
				JResult result = registerNatives(this.Reference, classRef,
				                                 (ReadOnlyValPtr<JNativeMethodValue>)argsMemory.Pointer,
				                                 argsMemory.Values.Length);
				if (result != JResult.Ok)
				{
					this.CheckJniError();
					throw new JniException(result);
				}
				this.VirtualMachine.RegisterNatives(this.ClassObject.Hash, calls);
			}
			finally
			{
				handles.ForEach(h => h.Dispose());
			}
		}
		public void ClearNatives(JClassObject jClass)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			UnregisterNativesDelegate unregisterNatives = this.GetDelegate<UnregisterNativesDelegate>();
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JResult result = unregisterNatives(this.Reference, classRef);
			if (result != JResult.Ok) throw new JniException(result);
			this.VirtualMachine.UnregisterNatives(this.ClassObject.Hash);
		}

		/// <summary>
		/// Uses <paramref name="jLocal"/> into <paramref name="jniTransaction"/>.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="JniTransaction"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference</returns>
		private JObjectLocalRef UseObject(JniTransaction jniTransaction, JLocalObject? jLocal)
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
		/// <param name="jniTransaction">A <see cref="JniTransaction"/> transaction.</param>
		/// <param name="args">A <see cref="IObject"/> array.</param>
		/// <param name="argSpan">Destination span.</param>
		/// <exception cref="InvalidOperationException">Invalid object.</exception>
		private void CopyAsJValue(JniTransaction jniTransaction, IReadOnlyList<IObject?> args, Span<Byte> argSpan)
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
						ValidationUtilities.ThrowIfDummy(referenceObject);
						this.ReloadClass(referenceObject as JClassObject);
						ValidationUtilities.ThrowIfDefault(referenceObject, $"Invalid object at {i}.");
						jniTransaction.Add(referenceObject);
						referenceObject.CopyTo(result, i);
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
				case 0x5A: //Z
					CallBooleanMethodADelegate callBooleanMethod = this.GetDelegate<CallBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callBooleanMethod(
						this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x42: //B
					CallByteMethodADelegate callByteMethod = this.GetDelegate<CallByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callByteMethod(this.Reference, localRef,
					                                                   methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x43: //C
					CallCharMethodADelegate callCharMethod = this.GetDelegate<CallCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callCharMethod(this.Reference, localRef,
					                                                  methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x44: //D
					CallDoubleMethodADelegate callDoubleMethod = this.GetDelegate<CallDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callDoubleMethod(
						this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x46: //F
					CallFloatMethodADelegate callFloatMethod = this.GetDelegate<CallFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callFloatMethod(
						this.Reference, localRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x49: //I
					CallIntMethodADelegate callIntMethod = this.GetDelegate<CallIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callIntMethod(this.Reference, localRef,
					                                                  methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x4A: //J
					CallLongMethodADelegate callLongMethod = this.GetDelegate<CallLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callLongMethod(this.Reference, localRef,
					                                                   methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x53: //S
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
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="argsMemory">Fixed memory with parameters.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private void CallNonVirtualPrimitiveMethod(Span<Byte> bytes, JObjectLocalRef localRef, JClassObject jClass,
			Byte signature, JMethodId methodId, IFixedPointer argsMemory)
		{
			switch (signature)
			{
				case 0x5A: //Z
					CallNonVirtualBooleanMethodADelegate callNonVirtualBooleanMethod =
						this.GetDelegate<CallNonVirtualBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callNonVirtualBooleanMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x42: //B
					CallNonVirtualByteMethodADelegate callNonVirtualByteMethod =
						this.GetDelegate<CallNonVirtualByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callNonVirtualByteMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x43: //C
					CallNonVirtualCharMethodADelegate callNonVirtualCharMethod =
						this.GetDelegate<CallNonVirtualCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callNonVirtualCharMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x44: //D
					CallNonVirtualDoubleMethodADelegate callNonVirtualDoubleMethod =
						this.GetDelegate<CallNonVirtualDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callNonVirtualDoubleMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x46: //F
					CallNonVirtualFloatMethodADelegate callNonVirtualFloatMethod =
						this.GetDelegate<CallNonVirtualFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callNonVirtualFloatMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x49: //I
					CallNonVirtualIntMethodADelegate callNonVirtualIntMethod =
						this.GetDelegate<CallNonVirtualIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callNonVirtualIntMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x4A: //J
					CallNonVirtualLongMethodADelegate callNonVirtualLongMethod =
						this.GetDelegate<CallNonVirtualLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callNonVirtualLongMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x53: //S
					CallNonVirtualShortMethodADelegate callNonVirtualShortMethod =
						this.GetDelegate<CallNonVirtualShortMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callNonVirtualShortMethod(
						this.Reference, localRef, jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Creates a new object using JNI NewObject call.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="definition"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		private JObjectLocalRef NewObject(JClassObject jClass, JConstructorDefinition definition,
			params IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			NewObjectADelegate newObject = this.GetDelegate<NewObjectADelegate>();
			JObjectLocalRef localRef = newObject(this.Reference, jClass.Reference, methodId,
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
			ValidationUtilities.ThrowIfDummy(jClass);
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._mainClasses.Environment);
			GetStaticObjectFieldDelegate getStaticObjectField = this.GetDelegate<GetStaticObjectFieldDelegate>();
			JObjectLocalRef localRef = getStaticObjectField(this.Reference, jClass.Reference, fieldId);
			return localRef;
		}

		/// <summary>
		/// Creates a <see cref="IFixedContext{T}.IDisposable"/> instance from an span created in stack.
		/// </summary>
		/// <typeparam name="T">Type of elements in span.</typeparam>
		/// <param name="stackSpan">A stack created span.</param>
		/// <param name="cache">Instance to free stack bytes.</param>
		/// <returns>A <see cref="IFixedContext{T}.IDisposable"/> instance</returns>
		private static IFixedContext<T>.IDisposable AllocToFixedContext<T>(scoped Span<T> stackSpan,
			JEnvironmentCache? cache = default) where T : unmanaged
		{
			StackDisposable? disposable =
				cache is not null ? new(cache, stackSpan.Length * NativeUtilities.SizeOf<T>()) : default;
			ValPtr<T> ptr = (ValPtr<T>)stackSpan.GetUnsafeIntPtr();
			return ptr.GetUnsafeFixedContext(stackSpan.Length, disposable);
		}
	}
}