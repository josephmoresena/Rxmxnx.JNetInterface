namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IAccessProvider
	{
		private const Int32 maxStackBytes = 128;

		private Int32 _usedStackBytes;

		public void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			this.ReloadClass(jLocal as JClassObject);
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					GetBooleanFieldDelegate getBooleanField = this.GetDelegate<GetBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) =
						getBooleanField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x66: //B
					GetByteFieldDelegate getByteField = this.GetDelegate<GetByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) =
						getByteField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x67: //C
					GetCharFieldDelegate getCharField = this.GetDelegate<GetCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) =
						getCharField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x68: //D
					GetDoubleFieldDelegate getDoubleField = this.GetDelegate<GetDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) =
						getDoubleField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x70: //F
					GetFloatFieldDelegate getFloatField = this.GetDelegate<GetFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) =
						getFloatField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x73: //I
					GetIntFieldDelegate getIntField = this.GetDelegate<GetIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) =
						getIntField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x74: //J
					GetLongFieldDelegate getLongField = this.GetDelegate<GetLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) =
						getLongField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x83: //S
					GetShortFieldDelegate getShortField = this.GetDelegate<GetShortFieldDelegate>();
					MemoryMarshal.AsRef<Int16>(bytes) =
						getShortField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
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
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			this.ReloadClass(jLocal as JClassObject);
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					SetBooleanFieldDelegate setBooleanField = this.GetDelegate<SetBooleanFieldDelegate>();
					setBooleanField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					                MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case 0x66: //B
					SetByteFieldDelegate setByteField = this.GetDelegate<SetByteFieldDelegate>();
					setByteField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					             MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case 0x67: //C
					SetCharFieldDelegate setCharField = this.GetDelegate<SetCharFieldDelegate>();
					setCharField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					             MemoryMarshal.AsRef<Char>(bytes));
					break;
				case 0x68: //D
					SetDoubleFieldDelegate setDoubleField = this.GetDelegate<SetDoubleFieldDelegate>();
					setDoubleField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					               MemoryMarshal.AsRef<Double>(bytes));
					break;
				case 0x70: //F
					SetFloatFieldDelegate setFloatField = this.GetDelegate<SetFloatFieldDelegate>();
					setFloatField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					              MemoryMarshal.AsRef<Single>(bytes));
					break;
				case 0x73: //I
					SetIntFieldDelegate setIntField = this.GetDelegate<SetIntFieldDelegate>();
					setIntField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					            MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case 0x74: //J
					SetLongFieldDelegate setLongField = this.GetDelegate<SetLongFieldDelegate>();
					setLongField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					             MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case 0x83: //S
					SetShortFieldDelegate setShortField = this.GetDelegate<SetShortFieldDelegate>();
					setShortField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
					              MemoryMarshal.AsRef<Int16>(bytes));
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
			this.CheckJniError();
		}
		public void GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					GetStaticBooleanFieldDelegate getStaticBooleanField =
						this.GetDelegate<GetStaticBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = getStaticBooleanField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x66: //B
					GetStaticByteFieldDelegate getStaticByteField = this.GetDelegate<GetStaticByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = getStaticByteField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x67: //C
					GetStaticCharFieldDelegate getStaticCharField = this.GetDelegate<GetStaticCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = getStaticCharField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x68: //D
					GetStaticDoubleFieldDelegate getStaticDoubleField =
						this.GetDelegate<GetStaticDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) =
						getStaticDoubleField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x70: //F
					GetStaticFloatFieldDelegate getFloatField = this.GetDelegate<GetStaticFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = getFloatField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x73: //I
					GetStaticIntFieldDelegate getStaticIntField = this.GetDelegate<GetStaticIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = getStaticIntField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x74: //J
					GetStaticLongFieldDelegate getStaticLongField = this.GetDelegate<GetStaticLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = getStaticLongField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x83: //S
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
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					SetStaticBooleanFieldDelegate setStaticBooleanField =
						this.GetDelegate<SetStaticBooleanFieldDelegate>();
					setStaticBooleanField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case 0x66: //B
					SetStaticByteFieldDelegate setStaticByteField = this.GetDelegate<SetStaticByteFieldDelegate>();
					setStaticByteField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case 0x67: //C
					SetStaticCharFieldDelegate setStaticCharField = this.GetDelegate<SetStaticCharFieldDelegate>();
					setStaticCharField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Char>(bytes));
					break;
				case 0x68: //D
					SetStaticDoubleFieldDelegate setStaticDoubleField =
						this.GetDelegate<SetStaticDoubleFieldDelegate>();
					setStaticDoubleField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Double>(bytes));
					break;
				case 0x70: //F
					SetStaticFloatFieldDelegate setStaticFloatField = this.GetDelegate<SetStaticFloatFieldDelegate>();
					setStaticFloatField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Single>(bytes));
					break;
				case 0x73: //I
					SetStaticIntFieldDelegate setStaticIntField = this.GetDelegate<SetStaticIntFieldDelegate>();
					setStaticIntField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case 0x74: //J
					SetStaticLongFieldDelegate setStaticLongField = this.GetDelegate<SetStaticLongFieldDelegate>();
					setStaticLongField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case 0x83: //S
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
			AccessCache access = this.GetAccess(jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes]) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(args, argsMemory.Values);
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					CallStaticBooleanMethodADelegate callStaticBooleanMethod =
						this.GetDelegate<CallStaticBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callStaticBooleanMethod(
						this.Reference, jClass.Reference, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x66: //B
					CallStaticByteMethodADelegate callByteMethod = this.GetDelegate<CallStaticByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callByteMethod(this.Reference, jClass.Reference, methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x67: //C
					CallStaticCharMethodADelegate callCharMethod = this.GetDelegate<CallStaticCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callCharMethod(this.Reference, jClass.Reference, methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x68: //D
					CallStaticDoubleMethodADelegate callDoubleMethod =
						this.GetDelegate<CallStaticDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callDoubleMethod(
						this.Reference, jClass.Reference, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x70: //F
					CallStaticFloatMethodADelegate callFloatMethod = this.GetDelegate<CallStaticFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callFloatMethod(
						this.Reference, jClass.Reference, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x73: //I
					CallStaticIntMethodADelegate callIntMethod = this.GetDelegate<CallStaticIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callIntMethod(this.Reference, jClass.Reference, methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x74: //J
					CallStaticLongMethodADelegate callLongMethod = this.GetDelegate<CallStaticLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callLongMethod(this.Reference, jClass.Reference, methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x83: //S
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
			AccessCache access = this.GetAccess(jLocal.Class);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			this.ReloadClass(jLocal as JClassObject);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes]) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(args, argsMemory.Values);
			if (!nonVirtual)
				this.CallPrimitiveMethod(bytes, jLocal, definition.Information[1][^1], methodId, argsMemory);
			else
				this.CallNonVirtualPrimitiveMethod(bytes, jLocal, jClass, definition.Information[1][^1], methodId,
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
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			this.ReloadClass(jLocal as JClassObject);
			GetObjectFieldDelegate getObjectField = this.GetDelegate<GetObjectFieldDelegate>();
			JObjectLocalRef localRef = getObjectField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
			return this.CreateObject<TField>(localRef, true);
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
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			this.ReloadClass(jLocal as JClassObject);
			this.ReloadClass(value as JClassObject);
			SetObjectFieldDelegate setObjectField = this.GetDelegate<SetObjectFieldDelegate>();
			setObjectField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId,
			               (value as JReferenceObject)!.As<JObjectLocalRef>());
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
			ValidationUtilities.ThrowIfDummy(jClass);
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._mainClasses.Environment);
			GetStaticObjectFieldDelegate getStaticObjectField = this.GetDelegate<GetStaticObjectFieldDelegate>();
			JObjectLocalRef localRef = getStaticObjectField(this.Reference, jClass.As<JClassLocalRef>(), fieldId);
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
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._mainClasses.Environment);
			this.ReloadClass(value as JClassObject);
			SetStaticObjectFieldDelegate setObjectField = this.GetDelegate<SetStaticObjectFieldDelegate>();
			setObjectField(this.Reference, jClass.As<JClassLocalRef>(), fieldId,
			               (value as JReferenceObject)!.As<JObjectLocalRef>());
		}
		public TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition, IObject?[] args)
			where TObject : JLocalObject, IDataType<TObject>
			=> throw new NotImplementedException();
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
			AccessCache access = this.GetAccess(jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes]) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(args, argsMemory.Values);
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
			AccessCache access = this.GetAccess(jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes]) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(args, argsMemory.Values);
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
			ValidationUtilities.ThrowIfDummy(jClass);
			AccessCache access = this.GetAccess(jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			this.ReloadClass(jLocal as JClassObject);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes]) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(args, argsMemory.Values);
			JObjectLocalRef localRef;
			if (!nonVirtual)
			{
				CallObjectMethodADelegate callObjectMethod = this.GetDelegate<CallObjectMethodADelegate>();
				localRef = callObjectMethod(this.Reference, jLocal.As<JObjectLocalRef>(), methodId,
				                            (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			else
			{
				CallNonVirtualObjectMethodADelegate callNonVirtualObjectObjectMethod =
					this.GetDelegate<CallNonVirtualObjectMethodADelegate>();
				localRef = callNonVirtualObjectObjectMethod(this.Reference, jLocal.As<JObjectLocalRef>(),
				                                            jClass.Reference, methodId,
				                                            (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			this.CheckJniError();
			return this.CreateObject<TResult>(localRef, true);
		}
		public void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
			Boolean nonVirtual, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			AccessCache access = this.GetAccess(jClass);
			JMethodId methodId = access.GetMethodId(definition, this._mainClasses.Environment);
			this.ReloadClass(jLocal as JClassObject);
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes]) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(args, argsMemory.Values);
			if (!nonVirtual)
			{
				CallVoidMethodADelegate callVoidMethod = this.GetDelegate<CallVoidMethodADelegate>();
				callVoidMethod(this.Reference, jLocal.As<JObjectLocalRef>(), methodId,
				               (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			else
			{
				CallNonVirtualVoidMethodADelegate callNonVirtualVoidObjectMethod =
					this.GetDelegate<CallNonVirtualVoidMethodADelegate>();
				callNonVirtualVoidObjectMethod(this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
				                               (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			}
			this.CheckJniError();
		}

		/// <summary>
		/// Indicates whether current call must use <see langword="stackalloc"/> or <see langword="new"/> to
		/// hold JNI method parameter.
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
			this._usedStackBytes += requiredBytes;
			if (this._usedStackBytes <= JEnvironmentCache.maxStackBytes) return true;
			this._usedStackBytes -= requiredBytes;
			return false;
		}
		/// <summary>
		/// Retrieves <paramref name="argSpan"/> as a <see cref="JValue"/> span containing <paramref name="args"/> information.
		/// </summary>
		/// <param name="args">A <see cref="IObject"/> array.</param>
		/// <param name="argSpan">Destination span.</param>
		/// <exception cref="InvalidOperationException">Invalid object.</exception>
		private void CopyAsJValue(IReadOnlyList<IObject?> args, Span<Byte> argSpan)
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
						referenceObject.CopyTo(result, i);
						break;
				}
			}
		}
		/// <summary>
		/// Performs primitive method call.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="argsMemory">Fixed memory with parameters.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private void CallPrimitiveMethod(Span<Byte> bytes, JReferenceObject jLocal, Byte signature, JMethodId methodId,
			IFixedPointer argsMemory)
		{
			switch (signature)
			{
				case 0x90: //Z
					CallBooleanMethodADelegate callBooleanMethod = this.GetDelegate<CallBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callBooleanMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x66: //B
					CallByteMethodADelegate callByteMethod = this.GetDelegate<CallByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callByteMethod(this.Reference, jLocal.As<JObjectLocalRef>(),
					                                                   methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x67: //C
					CallCharMethodADelegate callCharMethod = this.GetDelegate<CallCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callCharMethod(this.Reference, jLocal.As<JObjectLocalRef>(),
					                                                  methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x68: //D
					CallDoubleMethodADelegate callDoubleMethod = this.GetDelegate<CallDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callDoubleMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x70: //F
					CallFloatMethodADelegate callFloatMethod = this.GetDelegate<CallFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callFloatMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x73: //I
					CallIntMethodADelegate callIntMethod = this.GetDelegate<CallIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callIntMethod(this.Reference, jLocal.As<JObjectLocalRef>(),
					                                                  methodId,
					                                                  (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x74: //J
					CallLongMethodADelegate callLongMethod = this.GetDelegate<CallLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callLongMethod(this.Reference, jLocal.As<JObjectLocalRef>(),
					                                                   methodId,
					                                                   (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x83: //S
					CallShortMethodADelegate callShortMethod = this.GetDelegate<CallShortMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callShortMethod(this.Reference, jLocal.As<JObjectLocalRef>(),
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
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="argsMemory">Fixed memory with parameters.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private void CallNonVirtualPrimitiveMethod(Span<Byte> bytes, JReferenceObject jLocal, JClassObject jClass,
			Byte signature, JMethodId methodId, IFixedPointer argsMemory)
		{
			switch (signature)
			{
				case 0x90: //Z
					CallNonVirtualBooleanMethodADelegate callNonVirtualBooleanMethod =
						this.GetDelegate<CallNonVirtualBooleanMethodADelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = callNonVirtualBooleanMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x66: //B
					CallNonVirtualByteMethodADelegate callNonVirtualByteMethod =
						this.GetDelegate<CallNonVirtualByteMethodADelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = callNonVirtualByteMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x67: //C
					CallNonVirtualCharMethodADelegate callNonVirtualCharMethod =
						this.GetDelegate<CallNonVirtualCharMethodADelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = callNonVirtualCharMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x68: //D
					CallNonVirtualDoubleMethodADelegate callNonVirtualDoubleMethod =
						this.GetDelegate<CallNonVirtualDoubleMethodADelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = callNonVirtualDoubleMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x70: //F
					CallNonVirtualFloatMethodADelegate callNonVirtualFloatMethod =
						this.GetDelegate<CallNonVirtualFloatMethodADelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = callNonVirtualFloatMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x73: //I
					CallNonVirtualIntMethodADelegate callNonVirtualIntMethod =
						this.GetDelegate<CallNonVirtualIntMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callNonVirtualIntMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x74: //J
					CallNonVirtualLongMethodADelegate callNonVirtualLongMethod =
						this.GetDelegate<CallNonVirtualLongMethodADelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = callNonVirtualLongMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				case 0x83: //S
					CallNonVirtualShortMethodADelegate callNonVirtualShortMethod =
						this.GetDelegate<CallNonVirtualShortMethodADelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = callNonVirtualShortMethod(
						this.Reference, jLocal.As<JObjectLocalRef>(), jClass.Reference, methodId,
						(ReadOnlyValPtr<JValue>)argsMemory.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}

		/// <summary>
		/// Creates a <see cref="IFixedContext{T}.IDisposable"/> instance from an span created in stack.
		/// </summary>
		/// <typeparam name="T">Type of elements in span.</typeparam>
		/// <param name="stackSpan">A stack created span.</param>
		/// <returns>A <see cref="IFixedContext{T}.IDisposable"/> instance</returns>
		private static IFixedContext<T>.IDisposable AllocToFixedContext<T>(scoped Span<T> stackSpan) where T : unmanaged
		{
			ValPtr<T> ptr = (ValPtr<T>)stackSpan.GetUnsafeIntPtr();
			return ptr.GetUnsafeFixedContext(stackSpan.Length);
		}
	}
}