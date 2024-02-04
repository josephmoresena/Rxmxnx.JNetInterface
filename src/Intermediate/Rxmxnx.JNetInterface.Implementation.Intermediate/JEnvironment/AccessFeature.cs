namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record EnvironmentCache : IAccessFeature
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
		public void RegisterNatives(JClassObject jClass, IReadOnlyList<JNativeCallEntry> calls)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			RegisterNativesDelegate registerNatives = this.GetDelegate<RegisterNativesDelegate>();
			Int32 requiredBytes = calls.Count * JNativeMethodValue.Size;
			Boolean useStackAlloc = this.UseStackAlloc(requiredBytes);
			List<MemoryHandle> handles = new(calls.Count);
			using IFixedContext<JNativeMethodValue>.IDisposable argsMemory = useStackAlloc ?
				EnvironmentCache.AllocToFixedContext(stackalloc JNativeMethodValue[calls.Count], this) :
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
	}
}