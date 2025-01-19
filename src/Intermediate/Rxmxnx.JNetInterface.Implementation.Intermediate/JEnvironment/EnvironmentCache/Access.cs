namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Sets a static object field to given <paramref name="classRef"/> reference.
		/// </summary>
		/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="value">The field value to set to.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private unsafe void SetStaticObjectField<TField>(JClassLocalRef classRef, TField? value,
			INativeTransaction jniTransaction, JFieldId fieldId) where TField : IDataType<TField>
		{
			JObjectLocalRef valueLocalRef = this.UseObject(jniTransaction, value as JReferenceObject);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.SetStaticObjectFieldInfo);
			nativeInterface.StaticFieldFunctions.SetObjectField.Set(this.Reference, classRef, fieldId, valueLocalRef);
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
		private unsafe void SetObjectField<TField>(JObjectLocalRef localRef, TField? value,
			INativeTransaction jniTransaction, JFieldId fieldId) where TField : IDataType<TField>
		{
			JObjectLocalRef valueLocalRef = this.UseObject(jniTransaction, value as JReferenceObject);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.SetObjectFieldInfo);
			nativeInterface.InstanceFieldFunctions.SetObjectField.Set(this.Reference, localRef, fieldId, valueLocalRef);
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
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
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
		/// <param name="withNoCheckError">Indicates whether <see cref="CheckJniError"/> should not be called.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
		private unsafe JObjectLocalRef GetStaticObjectField(JClassLocalRef classRef, JFieldId fieldId,
			Boolean withNoCheckError = false)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticObjectFieldInfo);
			JObjectLocalRef localRef =
				nativeInterface.StaticFieldFunctions.GetObjectField.Get(this.Reference, classRef, fieldId);
			JTrace.GetObjectField(default, classRef, fieldId, localRef);
			if (!withNoCheckError) this.CheckJniError();
			return localRef;
		}
		/// <summary>
		/// Retrieves an object field on <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TField"><see cref="IDataType"/> type of field result.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <returns><typeparamref name="TField"/> field instance.</returns>
		private unsafe TField? GetObjectField<TField>(JObjectLocalRef localRef, JFieldId fieldId)
			where TField : IDataType<TField>
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetObjectFieldInfo);
			JObjectLocalRef resultLocalRef =
				nativeInterface.InstanceFieldFunctions.GetObjectField.Get(this.Reference, localRef, fieldId);
			JTrace.GetObjectField(localRef, default, fieldId, resultLocalRef);
			this.CheckJniError();
			return this.CreateObject<TField>(resultLocalRef, true, MetadataHelper.IsFinalType<TField>());
		}
		/// <summary>
		/// Retrieves a <see cref="JObjectLocalRef"/> reflected from current definition on
		/// <paramref name="declaringClass"/>.
		/// </summary>
		/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
		/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="isStatic">
		/// Indicates whether <paramref name="definition"/> matches with a static method in <paramref name="declaringClass"/>.
		/// </param>
		/// <returns>A <see cref="JMethodObject"/> instance.</returns>
		private unsafe JObjectLocalRef GetReflectedCall(JCallDefinition definition, JClassObject declaringClass,
			Boolean isStatic)
		{
			ImplementationValidationUtilities.ThrowIfProxy(declaringClass);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.ToReflectedMethodInfo);
			using INativeTransaction jniTransaction = isStatic ?
				this.GetClassTransaction(declaringClass, definition, out JMethodId methodId, false) :
				this.GetInstanceTransaction(declaringClass, definition, out methodId);
			JObjectLocalRef localRef =
				nativeInterface.ClassFunctions.ToReflectedMethod.ToReflected(
					this.Reference, declaringClass.Reference, methodId, isStatic);
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
		private unsafe TResult? CallObjectFunction<TResult>(JFunctionDefinition definition, JObjectLocalRef localRef,
			JClassLocalRef classRef, ReadOnlySpan<IObject?> args, INativeTransaction jniTransaction, JMethodId methodId)
			where TResult : IDataType<TResult>
		{
			ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
				ref this.GetInstanceMethodFunctions(CommonNames.ObjectSignaturePrefixChar, !classRef.IsDefault);
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
			Rented<Byte> rented = default;
			Span<JValue> buffer = this.CopyAsJValue(jniTransaction, args,
			                                        stackDisposable.UsingStack ?
				                                        stackalloc Byte[requiredBytes] :
				                                        EnvironmentCache.HeapAlloc(requiredBytes, ref rented));
			JObjectLocalRef resultLocalRef;
			fixed (JValue* ptr = &MemoryMarshal.GetReference(buffer))
			{
				resultLocalRef = classRef.IsDefault ?
					instanceMethodFunctions.MethodFunctions.CallObjectMethod.Call(
						this.Reference, localRef, methodId, ptr) :
					instanceMethodFunctions.NonVirtualFunctions.CallNonVirtualObjectMethod.Call(
						this.Reference, localRef, classRef, methodId, ptr);
			}
			rented.Free();
			JTrace.CallObjectFunction(localRef, classRef, methodId, resultLocalRef, false);
			this.CheckJniError();
			return this.CreateObject<TResult>(resultLocalRef, true, MetadataHelper.IsFinalType<TResult>());
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
		private unsafe void CallPrimitiveFunction(Span<Byte> bytes, JFunctionDefinition definition,
			JObjectLocalRef localRef, JClassLocalRef classRef, ReadOnlySpan<IObject?> args,
			INativeTransaction jniTransaction, JMethodId methodId)
		{
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
			Rented<Byte> rented = default;
			Span<JValue> buffer = this.CopyAsJValue(jniTransaction, args,
			                                        stackDisposable.UsingStack ?
				                                        stackalloc Byte[requiredBytes] :
				                                        EnvironmentCache.HeapAlloc(requiredBytes, ref rented));
			fixed (JValue* ptr = &MemoryMarshal.GetReference(buffer))
			{
				if (classRef.IsDefault)
					this.CallPrimitiveFunction(bytes, localRef, definition.Descriptor[^1], methodId, ptr);
				else
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, definition.Descriptor[^1], methodId,
					                                     ptr);
			}
			rented.Free();
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
		private unsafe TResult? CallObjectStaticFunction<TResult>(JFunctionDefinition definition,
			JClassLocalRef classRef, ReadOnlySpan<IObject?> args, INativeTransaction jniTransaction, JMethodId methodId)
			where TResult : IDataType<TResult>
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallStaticObjectMethodInfo);
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
			Rented<Byte> rented = default;
			Span<JValue> buffer = this.CopyAsJValue(jniTransaction, args,
			                                        stackDisposable.UsingStack ?
				                                        stackalloc Byte[requiredBytes] :
				                                        EnvironmentCache.HeapAlloc(requiredBytes, ref rented));
			JObjectLocalRef localRef;
			fixed (JValue* ptr = &MemoryMarshal.GetReference(buffer))
			{
				localRef = nativeInterface.StaticMethodFunctions.CallObjectMethod.Call(
					this.Reference, classRef, methodId, ptr);
			}
			rented.Free();
			JTrace.CallObjectFunction(default, classRef, methodId, localRef, false);
			this.CheckJniError();
			return this.CreateObject<TResult>(localRef, true, MetadataHelper.IsFinalType<TResult>());
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
		private unsafe void CallMethod(JMethodDefinition definition, JObjectLocalRef localRef, JClassLocalRef classRef,
			ReadOnlySpan<IObject?> args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
				ref this.GetInstanceMethodFunctions(CommonNames.VoidSignatureChar, !classRef.IsDefault);
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
			Rented<Byte> rented = default;
			Span<JValue> buffer = this.CopyAsJValue(jniTransaction, args,
			                                        stackDisposable.UsingStack ?
				                                        stackalloc Byte[requiredBytes] :
				                                        EnvironmentCache.HeapAlloc(requiredBytes, ref rented));
			fixed (JValue* ptr = &MemoryMarshal.GetReference(buffer))
			{
				if (classRef.IsDefault)
					instanceMethodFunctions.MethodFunctions.CallVoidMethod.Call(
						this.Reference, localRef, methodId, ptr);
				else
					instanceMethodFunctions.NonVirtualFunctions.CallNonVirtualVoidMethod.Call(
						this.Reference, localRef, classRef, methodId, ptr);
			}
			rented.Free();
			JTrace.CallMethod(localRef, classRef, methodId);
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
		private unsafe void CallStaticMethod(JMethodDefinition definition, JClassLocalRef classRef,
			ReadOnlySpan<IObject?> args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallStaticVoidMethodInfo);
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
			Rented<Byte> rented = default;
			Span<JValue> buffer = this.CopyAsJValue(jniTransaction, args,
			                                        stackDisposable.UsingStack ?
				                                        stackalloc Byte[requiredBytes] :
				                                        EnvironmentCache.HeapAlloc(requiredBytes, ref rented));
			fixed (JValue* ptr = &MemoryMarshal.GetReference(buffer))
				nativeInterface.StaticMethodFunctions.CallVoidMethod.Call(this.Reference, classRef, methodId, ptr);
			rented.Free();
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
			ReadOnlySpan<IObject?> args)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
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
		private unsafe JObjectLocalRef NewObject(JConstructorDefinition definition, JClassLocalRef classRef,
			ReadOnlySpan<IObject?> args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewObjectInfo);
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
			Rented<Byte> rented = default;
			Span<JValue> buffer = this.CopyAsJValue(jniTransaction, args,
			                                        stackDisposable.UsingStack ?
				                                        stackalloc Byte[requiredBytes] :
				                                        EnvironmentCache.HeapAlloc(requiredBytes, ref rented));
			JObjectLocalRef localRef;
			fixed (JValue* ptr = &MemoryMarshal.GetReference(buffer))
				localRef = nativeInterface.ObjectFunctions.NewObject.Call(this.Reference, classRef, methodId, ptr);
			rented.Free();
			JTrace.CallObjectFunction(default, classRef, methodId, localRef, true);
			this.CheckJniError();
			return localRef;
		}
	}
}