namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCore
{
	/// <summary>
	/// Sets a static object field to given <paramref name="classRef"/> reference.
	/// </summary>
	/// <typeparam name="TField"><see cref="IDataType"/> type of field.</typeparam>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="value">The field value to set to.</param>
	/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
	/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
	private void SetStaticObjectField<TField>(JClassLocalRef classRef, TField? value, INativeTransaction jniTransaction,
		JFieldId fieldId) where TField : IDataType<TField>
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
	private void SetObjectField<TField>(JObjectLocalRef localRef, TField? value, INativeTransaction jniTransaction,
		JFieldId fieldId) where TField : IDataType<TField>
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
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(1);
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
	private JObjectLocalRef GetStaticObjectField(JClassLocalRef classRef, JFieldId fieldId,
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
	private TField? GetObjectField<TField>(JObjectLocalRef localRef, JFieldId fieldId) where TField : IDataType<TField>
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
	private JObjectLocalRef GetReflectedCall(JCallDefinition definition, JClassObject declaringClass, Boolean isStatic)
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
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the java call.</typeparam>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
	/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
	[SkipLocalsInit]
	private unsafe TResult? CallObjectFunction<TResult, TArgs>(JFunctionDefinition definition, JObjectLocalRef localRef,
		JClassLocalRef? classRef, in TArgs? args, INativeTransaction jniTransaction, JMethodId methodId)
		where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
			ref this.GetInstanceMethodFunctions(CommonNames.ObjectSignaturePrefixChar, classRef != default);
		using StackDisposable stackDisposable =
			this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
		ValPtr<JValue> buffer = stackDisposable.UsingStack ? stackalloc JValue[definition.Count].GetUnsafeValPtr() :
			requiredBytes > 0 ? (ValPtr<JValue>)NativeMemory.Alloc((UIntPtr)requiredBytes) : ValPtr<JValue>.Zero;
		JObjectLocalRef result;
		try
		{
			ParameterSlot slot = new(this, jniTransaction, buffer, definition.Count);
			slot.Configure(ref Unsafe.AsRef(in args), definition);
			result = !classRef.HasValue ?
				instanceMethodFunctions.MethodFunctions.CallObjectMethod.Call(
					this.Reference, localRef, methodId, buffer) :
				instanceMethodFunctions.NonVirtualFunctions.CallNonVirtualObjectMethod.Call(
					this.Reference, localRef, classRef.Value, methodId, buffer);
		}
		finally
		{
			if (!stackDisposable.UsingStack)
				NativeMemory.Free(buffer.Pointer.ToPointer());
		}
		JTrace.CallObjectFunction(localRef, classRef.GetValueOrDefault(), methodId, result, false);
		this.CheckJniError();
		return this.CreateObject<TResult>(result, true, MetadataHelper.IsFinalType<TResult>());
	}
	/// <summary>
	/// Invokes a static object function on given <paramref name="classRef"/> reference.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the java call.</typeparam>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
	/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	[SkipLocalsInit]
	private unsafe TResult? CallObjectStaticFunction<TResult, TArgs>(JFunctionDefinition definition,
		JClassLocalRef classRef, in TArgs? args, INativeTransaction jniTransaction, JMethodId methodId)
		where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallStaticObjectMethodInfo);
		using StackDisposable stackDisposable =
			this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
		ValPtr<JValue> buffer = stackDisposable.UsingStack ? stackalloc JValue[definition.Count].GetUnsafeValPtr() :
			requiredBytes > 0 ? (ValPtr<JValue>)NativeMemory.Alloc((UIntPtr)requiredBytes) : ValPtr<JValue>.Zero;
		JObjectLocalRef localRef;
		try
		{
			ParameterSlot slot = new(this, jniTransaction, buffer, definition.Count);
			slot.Configure(ref Unsafe.AsRef(in args), definition);
			localRef = nativeInterface.StaticMethodFunctions.CallObjectMethod.Call(
				this.Reference, classRef, methodId, buffer);
		}
		finally
		{
			if (!stackDisposable.UsingStack)
				NativeMemory.Free(buffer.Pointer.ToPointer());
		}
		JTrace.CallObjectFunction(default, classRef, methodId, localRef, false);
		this.CheckJniError();
		return this.CreateObject<TResult>(localRef, true, MetadataHelper.IsFinalType<TResult>());
	}
	/// <summary>
	/// Invokes a method on given <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the java call.</typeparam>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
	/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
	[SkipLocalsInit]
	private unsafe void CallMethod<TArgs>(JMethodDefinition definition, JObjectLocalRef localRef,
		JClassLocalRef? classRef, in TArgs? args, INativeTransaction jniTransaction, JMethodId methodId)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
			ref this.GetInstanceMethodFunctions(CommonNames.VoidSignatureChar, classRef != default);
		using StackDisposable stackDisposable =
			this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
		ValPtr<JValue> buffer = stackDisposable.UsingStack ? stackalloc JValue[definition.Count].GetUnsafeValPtr() :
			requiredBytes > 0 ? (ValPtr<JValue>)NativeMemory.Alloc((UIntPtr)requiredBytes) : ValPtr<JValue>.Zero;
		try
		{
			ParameterSlot slot = new(this, jniTransaction, buffer, definition.Count);
			slot.Configure(ref Unsafe.AsRef(in args), definition);
			if (!classRef.HasValue)
				instanceMethodFunctions.MethodFunctions.CallVoidMethod.Call(this.Reference, localRef, methodId, buffer);
			else
				instanceMethodFunctions.NonVirtualFunctions.CallNonVirtualVoidMethod.Call(
					this.Reference, localRef, classRef.Value, methodId, buffer);
		}
		finally
		{
			if (!stackDisposable.UsingStack)
				NativeMemory.Free(buffer.Pointer.ToPointer());
		}
		JTrace.CallMethod(localRef, classRef.GetValueOrDefault(), methodId);
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
	[SkipLocalsInit]
	private unsafe void CallStaticMethod<TArgs>(JMethodDefinition definition, JClassLocalRef classRef, in TArgs? args,
		INativeTransaction jniTransaction, JMethodId methodId)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallStaticVoidMethodInfo);
		using StackDisposable stackDisposable =
			this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
		ValPtr<JValue> buffer = stackDisposable.UsingStack ? stackalloc JValue[definition.Count].GetUnsafeValPtr() :
			requiredBytes > 0 ? (ValPtr<JValue>)NativeMemory.Alloc((UIntPtr)requiredBytes) : ValPtr<JValue>.Zero;
		try
		{
			ParameterSlot slot = new(this, jniTransaction, buffer, definition.Count);
			slot.Configure(ref Unsafe.AsRef(in args), definition);
			nativeInterface.StaticMethodFunctions.CallVoidMethod.Call(this.Reference, classRef, methodId, buffer);
		}
		finally
		{
			if (!stackDisposable.UsingStack)
				NativeMemory.Free(buffer.Pointer.ToPointer());
		}
		JTrace.CallMethod(default, classRef, methodId);
		this.CheckJniError();
	}

	/// <summary>
	/// Creates a new object using JNI NewObject call.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the java call.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
	private JObjectLocalRef NewObject<TArgs>(JClassObject jClass, JConstructorDefinition definition, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction =
			this.Host.MemoryManager.CreateTransaction(1 + definition.ReferenceCount);
		AccessCache access = this.GetAccess(jniTransaction, jClass);
		JMethodId methodId = access.GetMethodId(definition, this._env);
		return this.NewObject(definition, jClass.Reference, in args, jniTransaction, methodId);
	}
	/// <summary>
	/// Creates a new object using JNI NewObject call.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the java call.</typeparam>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> instance.</param>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
	/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
	/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
	[SkipLocalsInit]
	private unsafe JObjectLocalRef NewObject<TArgs>(JConstructorDefinition definition, JClassLocalRef classRef,
		in TArgs? args, INativeTransaction jniTransaction, JMethodId methodId)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewObjectInfo);
		using StackDisposable stackDisposable =
			this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
		ValPtr<JValue> buffer = stackDisposable.UsingStack ? stackalloc JValue[definition.Count].GetUnsafeValPtr() :
			requiredBytes > 0 ? (ValPtr<JValue>)NativeMemory.Alloc((UIntPtr)requiredBytes) : ValPtr<JValue>.Zero;
		JObjectLocalRef localRef;
		try
		{
			ParameterSlot slot = new(this, jniTransaction, buffer, definition.Count);
			slot.Configure(ref Unsafe.AsRef(in args), definition);
			localRef = nativeInterface.ObjectFunctions.NewObject.Call(this.Reference, classRef, methodId, buffer);
		}
		finally
		{
			if (!stackDisposable.UsingStack)
				NativeMemory.Free(buffer.Pointer.ToPointer());
		}
		JTrace.CallObjectFunction(default, classRef, methodId, localRef, true);
		this.CheckJniError();
		return localRef;
	}
}