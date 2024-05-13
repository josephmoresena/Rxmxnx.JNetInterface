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
			JTrace.SetObjectField(default, classRef, fieldId, valueLocalRef);
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
			JTrace.SetObjectField(localRef, default, fieldId, valueLocalRef);
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
			JTrace.GetObjectField(default, classRef, fieldId, localRef);
			this.CheckJniError();
			return localRef;
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
			JTrace.GetObjectField(localRef, default, fieldId, resultLocalRef);
			this.CheckJniError();
			return this.CreateObject<TField>(resultLocalRef, true);
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
			JTrace.CallObjectFunction(localRef, classRef, methodId, resultLocalRef, false);
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
				this.CallPrimitiveFunction(bytes, localRef, definition.Information[1][^1], methodId, argsMemory);
			else
				this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, definition.Information[1][^1], methodId,
				                                     argsMemory);
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
			JTrace.CallObjectFunction(default, classRef, methodId, localRef, false);
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
				JTrace.CallMethod(localRef, default, methodId);
			}
			else
			{
				CallNonVirtualVoidMethodADelegate callNonVirtualVoidObjectMethod =
					this.GetDelegate<CallNonVirtualVoidMethodADelegate>();
				callNonVirtualVoidObjectMethod(this.Reference, localRef, classRef, methodId,
				                               (ReadOnlyValPtr<JValue>)argsMemory.Pointer);
				JTrace.CallMethod(localRef, classRef, methodId);
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
			JTrace.CallMethod(default, classRef, methodId);
			this.CheckJniError();
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
			JTrace.CallObjectFunction(default, classRef, methodId, localRef, true);
			this.CheckJniError();
			return localRef;
		}
	}
}