namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record JEnvironmentCache : IAccessFeature
	{
		public void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			this.GetPrimitiveField(bytes, localRef, definition.Information[1][^1], fieldId);
		}
		public void SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
			ReadOnlySpan<Byte> bytes)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			this.SetPrimitiveField(localRef, bytes, definition.Information[1][^1], fieldId);
		}
		public void GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._env);
			this.GetPrimitiveStaticField(bytes, jClass.Reference, definition.Information[1][^1], fieldId);
		}
		public void SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._env);
			this.SetPrimitiveStaticField(jClass.Reference, bytes, definition.Information[1][^1], fieldId);
		}
		public void CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass, JFunctionDefinition definition,
			IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction =
				this.GetClassTransaction(jClass, definition, out JMethodId methodId);
			this.CallPrimitiveStaticFunction(bytes, definition, jClass.Reference, args, jniTransaction, methodId);
		}
		public void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction =
				this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
				                            out JMethodId methodId);
			JClassLocalRef classRef = nonVirtual ? jClass.Reference : default;
			this.CallPrimitiveFunction(bytes, definition, localRef, classRef, args, jniTransaction, methodId);
		}
		public TField? GetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition)
			where TField : IObject, IDataType<TField>
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
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			return this.GetObjectField<TField>(localRef, fieldId);
		}
		public TField? GetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition)
			where TField : IDataType<TField>, IObject
		{
			ValidationUtilities.ThrowIfDummy(jField);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			ValidationUtilities.ThrowIfDummy(jLocal);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
				return this.GetObjectField<TField>(localRef, fieldId);
			Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
			this.GetPrimitiveField(bytes, localRef, definition.Information[1][^1], fieldId);
			return (TField)primitiveMetadata.CreateInstance(bytes);
		}
		public void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
			TField? value) where TField : IObject, IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveField(jLocal, jClass, definition, bytes);
				return;
			}
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(3);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			this.SetObjectField(localRef, value, jniTransaction, fieldId);
		}
		public void SetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition,
			TField? value) where TField : IDataType<TField>, IObject
		{
			ValidationUtilities.ThrowIfDummy(jField);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			ValidationUtilities.ThrowIfDummy(jLocal);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(3);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveField(localRef, bytes, definition.Information[1][^1], fieldId);
				return;
			}
			this.SetObjectField(localRef, value, jniTransaction, fieldId);
		}
		public TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
			where TField : IObject, IDataType<TField>
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
		public TField? GetStaticField<TField>(JFieldObject jField, JFieldDefinition definition)
			where TField : IDataType<TField>, IObject
		{
			ValidationUtilities.ThrowIfDummy(jField);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jField.DeclaringClass));
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
			{
				JObjectLocalRef localRef = this.GetStaticObjectField(classRef, fieldId);
				return this.CreateObject<TField>(localRef, true);
			}
			Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
			this.GetPrimitiveStaticField(bytes, classRef, definition.Information[1][^1], fieldId);
			return (TField)primitiveMetadata.CreateInstance(bytes);
		}
		public void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
			where TField : IObject, IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveStaticField(jClass, definition, bytes);
				return;
			}
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._env);
			this.SetStaticObjectField(jClass.Reference, value, jniTransaction, fieldId);
		}
		public void SetStaticField<TField>(JFieldObject jField, JFieldDefinition definition, TField? value)
			where TField : IDataType<TField>, IObject
		{
			ValidationUtilities.ThrowIfDummy(jField);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(3);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jField.DeclaringClass));
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
			{
				this.SetStaticObjectField(classRef, value, jniTransaction, fieldId);
			}
			else
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveStaticField(classRef, bytes, definition.Information[1][^1], fieldId);
			}
		}
		public TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition, IObject?[] args)
			where TObject : JLocalObject, IDataType<TObject>
		{
			JObjectLocalRef localRef = this.NewObject(jClass, definition, args);
			return this.CreateObject<TObject>(localRef, true)!;
		}
		public TObject CallConstructor<TObject>(JMethodObject jMethod, JConstructorDefinition definition,
			IObject?[] args) where TObject : JLocalObject, IClassType<TObject>
		{
			ValidationUtilities.ThrowIfDummy(jMethod);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass));
			JObjectLocalRef localRef = this.NewObject(definition, classRef, args, jniTransaction, methodId);
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
			using INativeTransaction jniTransaction =
				this.GetClassTransaction(jClass, definition, out JMethodId methodId);
			return this.CallObjectStaticFunction<TResult>(definition, jClass.Reference, args, jniTransaction, methodId);
		}
		public TResult? CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition definition,
			IObject?[] args) where TResult : IDataType<TResult>
		{
			ValidationUtilities.ThrowIfDummy(jMethod);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TResult>();
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass));
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
				return this.CallObjectStaticFunction<TResult>(definition, classRef, args, jniTransaction, methodId);
			Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
			this.CallPrimitiveStaticFunction(bytes, definition, classRef, args, jniTransaction, methodId);
			return (TResult)primitiveMetadata.CreateInstance(bytes);
		}
		public void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction =
				this.GetClassTransaction(jClass, definition, out JMethodId methodId);
			this.CallStaticMethod(definition, jClass.Reference, args, jniTransaction, methodId);
		}
		public void CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jMethod);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass));
			this.CallStaticMethod(definition, classRef, args, jniTransaction, methodId);
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
			using INativeTransaction jniTransaction =
				this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
				                            out JMethodId methodId);
			JClassLocalRef classRef = nonVirtual ? jClass.Reference : default;
			return this.CallObjectFunction<TResult>(definition, localRef, classRef, args, jniTransaction, methodId);
		}
		public TResult? CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal,
			JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>
		{
			ValidationUtilities.ThrowIfDummy(jMethod);
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TResult>();
			Int32 initialCapacity = nonVirtual ? 3 : 2;
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(initialCapacity + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			JClassLocalRef classRef =
				nonVirtual ? jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass)) : default;
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
				return this.CallObjectFunction<TResult>(definition, localRef, classRef, args, jniTransaction, methodId);
			Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
			this.CallPrimitiveFunction(bytes, definition, localRef, classRef, args, jniTransaction, methodId);
			return (TResult)primitiveMetadata.CreateInstance(bytes);
		}
		public void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
			Boolean nonVirtual, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfDummy(jClass);
			using INativeTransaction jniTransaction =
				this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
				                            out JMethodId methodId);
			JClassLocalRef classRef = nonVirtual ? jClass.Reference : default;
			this.CallMethod(definition, localRef, classRef, args, jniTransaction, methodId);
		}
		public void CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition,
			Boolean nonVirtual, IObject?[] args)
		{
			ValidationUtilities.ThrowIfDummy(jMethod);
			ValidationUtilities.ThrowIfDummy(jLocal);
			ValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			Int32 initialCapacity = nonVirtual ? 3 : 2;
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(initialCapacity + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			JClassLocalRef classRef =
				nonVirtual ? jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass)) : default;
			this.CallMethod(definition, localRef, classRef, args, jniTransaction, methodId);
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
				using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
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
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JResult result = unregisterNatives(this.Reference, classRef);
			if (result != JResult.Ok) throw new JniException(result);
			this.VirtualMachine.UnregisterNatives(this.ClassObject.Hash);
		}
		public JCallDefinition GetDefinition(JStringObject memberName, JArrayObject<JClassObject> parameterTypes,
			JClassObject? returnType)
		{
			using LocalFrame localFrame = new(this._env, parameterTypes.Length + 2);
			JArgumentMetadata[] args = this.GetCallMetadata(parameterTypes!);
			if (returnType is null) return JConstructorDefinition.Create(args);
			IReflectionMetadata? returnMetadata = this.GetReflectionMetadata(returnType);
			using JNativeMemory<Byte> mem = memberName.GetNativeUtf8Chars();
			return returnMetadata is null ?
				JMethodDefinition.Create(mem.Values, args) :
				returnMetadata.CreateFunctionDefinition(mem.Values, args);
		}
		public JFieldDefinition GetDefinition(JStringObject memberName, JClassObject fieldType)
		{
			IReflectionMetadata returnMetadata = this.GetReflectionMetadata(fieldType)!;
			using JNativeMemory<Byte> mem = memberName.GetNativeUtf8Chars();
			return returnMetadata.CreateFieldDefinition(mem.Values);
		}
		public JMethodObject GetReflectedFunction(JFunctionDefinition definition, JClassObject declaringClass,
			Boolean isStatic)
		{
			JObjectLocalRef localRef = this.GetReflectedCall(definition, declaringClass, isStatic);
			return new(this.GetClass<JMethodObject>(), localRef, definition, declaringClass);
		}
		public JMethodObject GetReflectedMethod(JMethodDefinition definition, JClassObject declaringClass,
			Boolean isStatic)
		{
			JObjectLocalRef localRef = this.GetReflectedCall(definition, declaringClass, isStatic);
			return new(this.GetClass<JMethodObject>(), localRef, definition, declaringClass);
		}
		public JConstructorObject GetReflectedConstructor(JConstructorDefinition definition,
			JClassObject declaringClass)
		{
			JObjectLocalRef localRef = this.GetReflectedCall(definition, declaringClass, false);
			return new(this.GetClass<JConstructorObject>(), localRef, definition, declaringClass);
		}
		public JFieldObject GetReflectedField(JFieldDefinition definition, JClassObject declaringClass,
			Boolean isStatic)
		{
			ValidationUtilities.ThrowIfDummy(declaringClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, declaringClass);
			JFieldId fieldId = isStatic ?
				access.GetStaticFieldId(definition, this._env) :
				access.GetFieldId(definition, this._env);
			ToReflectedFieldDelegate toReflectedField = this.GetDelegate<ToReflectedFieldDelegate>();
			JObjectLocalRef localRef = toReflectedField(this.Reference, declaringClass.Reference, fieldId,
			                                            isStatic ? JBoolean.TrueValue : JBoolean.FalseValue);
			if (localRef == default) this.CheckJniError();
			return new(this.GetClass<JFieldObject>(), localRef, definition, declaringClass);
		}
		public JMethodId GetMethodId(JExecutableObject jExecutable)
		{
			ValidationUtilities.ThrowIfDummy(jExecutable);
			FromReflectedMethodDelegate fromReflectedMethod = this.GetDelegate<FromReflectedMethodDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(jExecutable);
			JMethodId result = fromReflectedMethod(this.Reference, localRef);
			if (result == default) this.CheckJniError();
			return result;
		}
		public JFieldId GetFieldId(JFieldObject jField)
		{
			ValidationUtilities.ThrowIfDummy(jField);
			FromReflectedFieldDelegate fromReflectedField = this.GetDelegate<FromReflectedFieldDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(jField);
			JFieldId result = fromReflectedField(this.Reference, localRef);
			if (result == default) this.CheckJniError();
			return result;
		}

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
			ValidationUtilities.ThrowIfDummy(declaringClass);
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
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			JObjectLocalRef resultLocalRef;
			if (classRef.Value == default)
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
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			if (classRef.Value == default)
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
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
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
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
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
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);

			if (classRef.Value == default)
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
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			CallStaticVoidMethodADelegate callStaticVoidMethod = this.GetDelegate<CallStaticVoidMethodADelegate>();
			callStaticVoidMethod(this.Reference, classRef, methodId, (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves the <see cref="IReflectionMetadata"/> instance for <paramref name="returnType"/>.
		/// </summary>
		/// <param name="returnType">A <see cref="JClassObject"/> instance.</param>
		/// <returns><see cref="IReflectionMetadata"/> instance for <paramref name="returnType"/>.</returns>
		private IReflectionMetadata? GetReflectionMetadata(JClassObject returnType)
		{
			using JStringObject className = InternalFunctionCache.Instance.GetClassName(returnType);
			using JNativeMemory<Byte> mem = className.GetNativeUtf8Chars();
			return MetadataHelper.GetReflectionMetadata(mem.Values);
		}
		/// <summary>
		/// Retrieves a <see cref="JArgumentMetadata"/> array from <paramref name="parameterTypes"/>.
		/// </summary>
		/// <param name="parameterTypes">A <see cref="JClassObject"/> list.</param>
		/// <returns><see cref="JArgumentMetadata"/> array from <paramref name="parameterTypes"/>.</returns>
		private JArgumentMetadata[] GetCallMetadata(IReadOnlyList<JClassObject> parameterTypes)
		{
			JArgumentMetadata[] args = new JArgumentMetadata[parameterTypes.Count];
			for (Int32 i = 0; i < parameterTypes.Count; i++)
			{
				using JClassObject jClass = parameterTypes[i];
				using JStringObject className = InternalFunctionCache.Instance.GetClassName(jClass);
				using JNativeMemory<Byte> mem = className.GetNativeUtf8Chars();
				args[i] = MetadataHelper.GetReflectionMetadata(mem.Values)!.ArgumentMetadata;
			}
			return args;
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
			ValidationUtilities.ThrowIfDummy(jClass);
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
			using IFixedContext<Byte>.IDisposable argsMemory = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
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
			ValidationUtilities.ThrowIfDummy(jClass);
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