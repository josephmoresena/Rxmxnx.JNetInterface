namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed partial class EnvironmentCache : IAccessFeature
	{
		public void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFieldDefinition definition)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			JTrace.GetField(jLocal, jClass, definition);
			this.GetPrimitiveField(bytes, localRef, definition.Descriptor[^1], fieldId);
		}
		public void SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
			ReadOnlySpan<Byte> bytes)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			EnvironmentCache.TraceSetPrimitiveField(jLocal, jClass, definition, bytes);
			this.SetPrimitiveField(localRef, bytes, definition.Descriptor[^1], fieldId);
		}
		public void GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._env);
			JTrace.GetField(default, jClass, definition);
			this.GetPrimitiveStaticField(bytes, jClass.Reference, definition.Descriptor[^1], fieldId);
		}
		public void SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._env);
			EnvironmentCache.TraceSetPrimitiveField(default, jClass, definition, bytes);
			this.SetPrimitiveStaticField(jClass.Reference, bytes, definition.Descriptor[^1], fieldId);
		}
		public void CallStaticPrimitiveFunction(Span<Byte> bytes, JClassObject jClass, JFunctionDefinition definition,
			ReadOnlySpan<IObject?> args = default)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction =
				this.GetClassTransaction(jClass, definition, out JMethodId methodId);
			this.CallStaticPrimitiveFunction(bytes, definition, jClass.Reference, args, jniTransaction, methodId);
		}
		public void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args = default)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction =
				this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
				                            out JMethodId methodId);
			JClassLocalRef classRef = nonVirtual ? jClass.Reference : default;
			this.CallPrimitiveFunction(bytes, definition, localRef, classRef, args, jniTransaction, methodId);
		}
		public TField? GetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition)
			where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.GetPrimitiveField(bytes, jLocal, jClass, definition);
				return (TField)primitiveMetadata.CreateInstance(bytes);
			}
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			JTrace.GetField(jLocal, jClass, definition);
			return this.GetObjectField<TField>(localRef, fieldId);
		}
		public TField? GetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition)
			where TField : IDataType<TField>, IObject
		{
			ImplementationValidationUtilities.ThrowIfProxy(jField);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
			{
				JTrace.GetField(jLocal, jField.DeclaringClass, definition);
				return this.GetObjectField<TField>(localRef, fieldId);
			}
			Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
			this.GetPrimitiveField(bytes, localRef, definition.Descriptor[^1], fieldId);
			return (TField)primitiveMetadata.CreateInstance(bytes);
		}
		public void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
			TField? value) where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveField(jLocal, jClass, definition, bytes);
				return;
			}
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(3);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetFieldId(definition, this._env);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			JTrace.SetField(jLocal, jClass, definition, value);
			this.SetObjectField(localRef, value, jniTransaction, fieldId);
		}
		public void SetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition,
			TField? value) where TField : IDataType<TField>, IObject
		{
			ImplementationValidationUtilities.ThrowIfProxy(jField);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(3);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveField(localRef, bytes, definition.Descriptor[^1], fieldId);
				return;
			}
			JTrace.SetField(jLocal, jField.DeclaringClass, definition, value);
			this.SetObjectField(localRef, value, jniTransaction, fieldId);
		}
		public TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
			where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.GetPrimitiveStaticField(bytes, jClass, definition);
				return (TField)primitiveMetadata.CreateInstance(bytes);
			}
			JTrace.GetField(default, jClass, definition);
			JObjectLocalRef localRef = this.GetStaticObjectField(jClass, definition);
			return this.CreateObject<TField>(localRef, true, MetadataHelper.IsFinalType<TField>());
		}
		public TField? GetStaticField<TField>(JFieldObject jField, JFieldDefinition definition)
			where TField : IDataType<TField>, IObject
		{
			ImplementationValidationUtilities.ThrowIfProxy(jField);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jField.DeclaringClass));
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
			{
				JTrace.GetField(default, jField.DeclaringClass, definition);
				JObjectLocalRef localRef = this.GetStaticObjectField(classRef, fieldId);
				return this.CreateObject<TField>(localRef, true, MetadataHelper.IsFinalType<TField>());
			}
			Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
			this.GetPrimitiveStaticField(bytes, classRef, definition.Descriptor[^1], fieldId);
			return (TField)primitiveMetadata.CreateInstance(bytes);
		}
		public void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
			where TField : IDataType<TField>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveStaticField(jClass, definition, bytes);
				return;
			}
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			JFieldId fieldId = access.GetStaticFieldId(definition, this._env);
			JTrace.SetField(default, jClass, definition, value);
			this.SetStaticObjectField(jClass.Reference, value, jniTransaction, fieldId);
		}
		public void SetStaticField<TField>(JFieldObject jField, JFieldDefinition definition, TField? value)
			where TField : IDataType<TField>, IObject
		{
			ImplementationValidationUtilities.ThrowIfProxy(jField);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jField.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TField>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(3);
			_ = jniTransaction.Add(jField);
			JFieldId fieldId = jField.FieldId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jField.DeclaringClass));
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
			{
				JTrace.SetField(default, jField.DeclaringClass, definition, value);
				this.SetStaticObjectField(classRef, value, jniTransaction, fieldId);
			}
			else
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(bytes);
				this.SetPrimitiveStaticField(classRef, bytes, definition.Descriptor[^1], fieldId);
			}
		}
		public TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
			ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IDataType<TObject>
		{
			this.CheckClassCompatibility<TObject>(jClass, out Boolean sameClass);
			JObjectLocalRef localRef = this.NewObject(jClass, definition, args);
			JTrace.CallMethod(default, jClass, definition, false, args);
			return sameClass ?
				this.CreateObject<TObject>(localRef, true, true)! :
				this.CreateObject<TObject>(jClass, localRef);
		}
		public TObject CallConstructor<TObject>(JConstructorObject jConstructor, JConstructorDefinition definition,
			ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jConstructor);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jConstructor.Definition);
			this.CheckClassCompatibility<TObject>(jConstructor.DeclaringClass, out Boolean sameClass);
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			_ = jniTransaction.Add(jConstructor);
			JMethodId methodId = jConstructor.MethodId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jConstructor.DeclaringClass));
			JObjectLocalRef localRef = this.NewObject(definition, classRef, args, jniTransaction, methodId);
			JTrace.CallMethod(default, jConstructor.DeclaringClass, definition, false, args);
			return sameClass ?
				this.CreateObject<TObject>(localRef, true, true)! :
				this.CreateObject<TObject>(jConstructor.DeclaringClass, localRef);
		}
		public TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
			ReadOnlySpan<IObject?> args) where TResult : IDataType<TResult>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TResult>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.CallStaticPrimitiveFunction(bytes, jClass, definition, args);
				return (TResult)primitiveMetadata.CreateInstance(bytes);
			}
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction =
				this.GetClassTransaction(jClass, definition, out JMethodId methodId);
			JTrace.CallMethod(default, jClass, definition, false, args);
			return this.CallObjectStaticFunction<TResult>(definition, jClass.Reference, args, jniTransaction, methodId);
		}
		public TResult? CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition definition,
			ReadOnlySpan<IObject?> args) where TResult : IDataType<TResult>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jMethod);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TResult>();
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass));
			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
				return this.CallObjectStaticFunction<TResult>(definition, classRef, args, jniTransaction, methodId);
			Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
			JTrace.CallMethod(default, jMethod.DeclaringClass, definition, false, args);
			this.CallStaticPrimitiveFunction(bytes, definition, classRef, args, jniTransaction, methodId);
			return (TResult)primitiveMetadata.CreateInstance(bytes);
		}
		public void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, ReadOnlySpan<IObject?> args)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction =
				this.GetClassTransaction(jClass, definition, out JMethodId methodId);
			JTrace.CallMethod(default, jClass, definition, false, args);
			this.CallStaticMethod(definition, jClass.Reference, args, jniTransaction, methodId);
		}
		public void CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition, ReadOnlySpan<IObject?> args)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jMethod);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass));
			JTrace.CallMethod(default, jMethod.DeclaringClass, definition, false, args);
			this.CallStaticMethod(definition, classRef, args, jniTransaction, methodId);
		}
		public TResult? CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass, JFunctionDefinition definition,
			Boolean nonVirtual, ReadOnlySpan<IObject?> args) where TResult : IDataType<TResult>
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TResult>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> bytes = stackalloc Byte[primitiveMetadata.SizeOf];
				this.CallPrimitiveFunction(bytes, jLocal, jClass, definition, nonVirtual, args);
				return (TResult)primitiveMetadata.CreateInstance(bytes);
			}
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction =
				this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
				                            out JMethodId methodId);
			JClassLocalRef classRef = nonVirtual ? jClass.Reference : default;
			JTrace.CallMethod(jLocal, jClass, definition, nonVirtual, args);
			return this.CallObjectFunction<TResult>(definition, localRef, classRef, args, jniTransaction, methodId);
		}
		public TResult? CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal,
			JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
			where TResult : IDataType<TResult>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jMethod);
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TResult>();
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
			JTrace.CallMethod(jLocal, jMethod.DeclaringClass, definition, nonVirtual, args);
			this.CallPrimitiveFunction(bytes, definition, localRef, classRef, args, jniTransaction, methodId);
			return (TResult)primitiveMetadata.CreateInstance(bytes);
		}
		public void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
			Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			using INativeTransaction jniTransaction =
				this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
				                            out JMethodId methodId);
			JClassLocalRef classRef = nonVirtual ? jClass.Reference : default;
			JTrace.CallMethod(jLocal, jClass, definition, nonVirtual, args);
			this.CallMethod(definition, localRef, classRef, args, jniTransaction, methodId);
		}
		public void CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition,
			Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jMethod);
			ImplementationValidationUtilities.ThrowIfProxy(jLocal);
			ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
			Int32 initialCapacity = nonVirtual ? 3 : 2;
			using INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(initialCapacity + definition.ReferenceCount);
			_ = jniTransaction.Add(jMethod);
			JMethodId methodId = jMethod.MethodId;
			JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
			JClassLocalRef classRef =
				nonVirtual ? jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass)) : default;
			JTrace.CallMethod(jLocal, jMethod.DeclaringClass, definition, nonVirtual, args);
			this.CallMethod(definition, localRef, classRef, args, jniTransaction, methodId);
		}
		public unsafe void RegisterNatives(JClassObject jClass, IReadOnlyList<JNativeCallEntry> calls)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.RegisterNativesInfo);
			List<MemoryHandle> handles = new(calls.Count);
			Int32 requiredBytes = calls.Count * NativeMethodValue.Size;
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(requiredBytes), requiredBytes);
			Rented<NativeMethodValue> rented = default;
			Span<NativeMethodValue> buffer = stackDisposable.UsingStack ?
				stackalloc NativeMethodValue[calls.Count] :
				EnvironmentCache.HeapAlloc(calls.Count, ref rented);
			for (Int32 i = 0; i < calls.Count; i++)
				buffer[i] = NativeMethodValue.Create(calls[i], handles);
			try
			{
				using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
				JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
				JResult result;
				fixed (NativeMethodValue* ptr = &MemoryMarshal.GetReference(buffer))
				{
					result = nativeInterface.NativeRegistryFunctions.RegisterNatives(
						this.Reference, classRef, ptr, buffer.Length);
				}
				this.CheckJniError();
				ImplementationValidationUtilities.ThrowIfInvalidResult(result);
				this.VirtualMachine.RegisterNatives(jClass.Hash, calls);
			}
			finally
			{
				rented.Free();
				handles.ForEach(h => h.Dispose());
			}
		}
		public unsafe void ClearNatives(JClassObject jClass)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.UnregisterNativesInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JResult jResult = nativeInterface.NativeRegistryFunctions.UnregisterNatives(this.Reference, classRef);
			ImplementationValidationUtilities.ThrowIfInvalidResult(jResult);
			this.VirtualMachine.UnregisterNatives(jClass.Hash);
		}
		public JCallDefinition GetDefinition(JStringObject memberName, JArrayObject<JClassObject> parameterTypes,
			JClassObject? returnType)
		{
			using LocalFrame _ =
				new(this._env, parameterTypes.Length + IVirtualMachine.GetAccessibleDefinitionCapacity);
			JArgumentMetadata[] args = this.GetCallMetadata(parameterTypes);
			if (returnType is null) return JConstructorDefinition.Create(args);
			using JNativeMemory<Byte> mem = memberName.GetNativeUtf8Chars();
			return MetadataHelper.GetCallDefinition(returnType, mem.Values, args);
		}
		public JFieldDefinition GetDefinition(JStringObject memberName, JClassObject fieldType)
		{
			using JNativeMemory<Byte> mem = memberName.GetNativeUtf8Chars();
			return MetadataHelper.GetFieldDefinition(fieldType, mem.Values);
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
			ImplementationValidationUtilities.ThrowIfProxy(declaringClass);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, declaringClass);
			JFieldId fieldId = isStatic ?
				access.GetStaticFieldId(definition, this._env) :
				access.GetFieldId(definition, this._env);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ToReflectedFieldInfo);
			JObjectLocalRef localRef =
				nativeInterface.ClassFunctions.ToReflectedField.ToReflected(
					this.Reference, declaringClass.Reference, fieldId, isStatic);
			if (localRef == default) this.CheckJniError();
			return new(this.GetClass<JFieldObject>(), localRef, definition, declaringClass);
		}
		public JMethodId GetMethodId(JExecutableObject jExecutable)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jExecutable);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.FromReflectedMethodInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(jExecutable);
			JMethodId result =
				nativeInterface.ClassFunctions.FromReflectedMethod.FromReflected(this.Reference, localRef);
			if (result == default) this.CheckJniError();
			return result;
		}
		public JFieldId GetFieldId(JFieldObject jField)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jField);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.FromReflectedMethodInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(jField);
			JFieldId result = nativeInterface.ClassFunctions.FromReflectedField.FromReflected(this.Reference, localRef);
			if (result == default) this.CheckJniError();
			return result;
		}
	}
}