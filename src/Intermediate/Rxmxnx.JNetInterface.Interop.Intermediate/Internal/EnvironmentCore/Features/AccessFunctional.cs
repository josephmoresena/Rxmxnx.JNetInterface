namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class EnvironmentCore
{
	public void CallStaticPrimitiveFunction<TArgs>(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction = this.GetClassTransaction(jClass, definition, out JMethodId methodId);
		this.CallStaticPrimitiveFunction(bytes, definition, jClass.Reference, in args, jniTransaction, methodId);
	}
	public void CallPrimitiveFunction<TArgs>(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jLocal);
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction =
			this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
			                            out JMethodId methodId);
		JClassLocalRef? classRef = nonVirtual ? jClass.Reference : null;
		this.CallPrimitiveFunction(bytes, definition, localRef, classRef, in args, jniTransaction, methodId);
	}
	public TObject CallConstructor<TObject, TArgs>(JClassObject jClass, JConstructorDefinition definition,
		in TArgs? args) where TObject : JLocalObject, IDataType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		this.CheckClassCompatibility<TObject>(jClass, out Boolean sameClass);
		JObjectLocalRef localRef = this.NewObject(jClass, definition, in args);
		JTrace.CallMethod(default, jClass, definition, false, in args);
		return sameClass ?
			this.CreateObject<TObject>(localRef, true, true)! :
			this.CreateObject<TObject>(jClass, localRef);
	}
#if !NET8_0_OR_GREATER || ANDROID
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public TObject CallConstructor<TObject, TArgs>(JConstructorObject jConstructor, JConstructorDefinition definition,
		in TArgs? args) where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jConstructor);
		ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jConstructor.Definition);
		this.CheckClassCompatibility<TObject>(jConstructor.DeclaringClass, out Boolean sameClass);
		using INativeTransaction jniTransaction =
			this.Host.MemoryManager.CreateTransaction(2 + definition.ReferenceCount);
		_ = jniTransaction.Add(jConstructor);
		JMethodId methodId = jConstructor.MethodId;
		JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jConstructor.DeclaringClass));
		JObjectLocalRef localRef = this.NewObject(definition, classRef, in args, jniTransaction, methodId);
		JTrace.CallMethod(default, jConstructor.DeclaringClass, definition, false, in args);
		return sameClass ?
			this.CreateObject<TObject>(localRef, true, true)! :
			this.CreateObject<TObject>(jConstructor.DeclaringClass, localRef);
	}
	public TResult? CallStaticFunction<TResult, TArgs>(JClassObject jClass, JFunctionDefinition definition,
		in TArgs? args) where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<TResult>())
		{
			Span<Byte> bytes = stackalloc Byte[IDataType.GetMetadata<TResult>().SizeOf];
			this.CallStaticPrimitiveFunction(bytes, jClass, definition, in args);
			return Unsafe.As<Byte, TResult>(ref MemoryMarshal.GetReference(bytes));
		}
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction = this.GetClassTransaction(jClass, definition, out JMethodId methodId);
		JTrace.CallMethod(default, jClass, definition, false, args);
		return this.CallObjectStaticFunction<TResult, TArgs>(definition, jClass.Reference, in args, jniTransaction,
		                                                     methodId);
	}
	public TResult? CallStaticFunction<TResult, TArgs>(JMethodObject jMethod, JFunctionDefinition definition,
		in TArgs? args) where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jMethod);
		ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
		using INativeTransaction jniTransaction =
			this.Host.MemoryManager.CreateTransaction(2 + definition.ReferenceCount);
		_ = jniTransaction.Add(jMethod);
		JMethodId methodId = jMethod.MethodId;
		JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass));
		if (RuntimeHelpers.IsReferenceOrContainsReferences<TResult>())
			return this.CallObjectStaticFunction<TResult, TArgs>(definition, classRef, in args, jniTransaction,
			                                                     methodId);
		Span<Byte> bytes = stackalloc Byte[IDataType.GetMetadata<TResult>().SizeOf];
		JTrace.CallMethod(default, jMethod.DeclaringClass, definition, false, in args);
		this.CallStaticPrimitiveFunction(bytes, definition, classRef, in args, jniTransaction, methodId);
		return Unsafe.As<Byte, TResult>(ref MemoryMarshal.GetReference(bytes));
	}
	public void CallStaticMethod<TArgs>(JClassObject jClass, JMethodDefinition definition, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction = this.GetClassTransaction(jClass, definition, out JMethodId methodId);
		JTrace.CallMethod(default, jClass, definition, false, in args);
		this.CallStaticMethod(definition, jClass.Reference, in args, jniTransaction, methodId);
	}
	public void CallStaticMethod<TArgs>(JMethodObject jMethod, JMethodDefinition definition, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jMethod);
		ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
		using INativeTransaction jniTransaction =
			this.Host.MemoryManager.CreateTransaction(2 + definition.ReferenceCount);
		_ = jniTransaction.Add(jMethod);
		JMethodId methodId = jMethod.MethodId;
		JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass));
		JTrace.CallMethod(default, jMethod.DeclaringClass, definition, false, in args);
		this.CallStaticMethod(definition, classRef, in args, jniTransaction, methodId);
	}
	public TResult? CallFunction<TResult, TArgs>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, in TArgs? args) where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<TResult>())
		{
			Span<Byte> bytes = stackalloc Byte[IDataType.GetMetadata<TResult>().SizeOf];
			this.CallPrimitiveFunction(bytes, jLocal, jClass, definition, nonVirtual, in args);
			return Unsafe.As<Byte, TResult>(ref MemoryMarshal.GetReference(bytes));
		}
		ImplementationValidationUtilities.ThrowIfProxy(jLocal);
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction =
			this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
			                            out JMethodId methodId);
		JClassLocalRef? classRef = nonVirtual ? jClass.Reference : null;
		JTrace.CallMethod(jLocal, jClass, definition, nonVirtual, in args);
		return this.CallObjectFunction<TResult, TArgs>(definition, localRef, classRef, in args, jniTransaction,
		                                               methodId);
	}
	public TResult? CallFunction<TResult, TArgs>(JMethodObject jMethod, JLocalObject jLocal,
		JFunctionDefinition definition, Boolean nonVirtual, in TArgs? args) where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jMethod);
		ImplementationValidationUtilities.ThrowIfProxy(jLocal);
		ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
		Int32 initialCapacity = nonVirtual ? 3 : 2;
		using INativeTransaction jniTransaction =
			this.Host.MemoryManager.CreateTransaction(initialCapacity + definition.ReferenceCount);
		_ = jniTransaction.Add(jMethod);
		JMethodId methodId = jMethod.MethodId;
		JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
		JClassLocalRef? classRef = nonVirtual ? jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass)) : null;
		if (RuntimeHelpers.IsReferenceOrContainsReferences<TResult>())
			return this.CallObjectFunction<TResult, TArgs>(definition, localRef, classRef, in args, jniTransaction,
			                                               methodId);
		Span<Byte> bytes = stackalloc Byte[IDataType.GetMetadata<TResult>().SizeOf];
		JTrace.CallMethod(jLocal, jMethod.DeclaringClass, definition, nonVirtual, in args);
		this.CallPrimitiveFunction(bytes, definition, localRef, classRef, in args, jniTransaction, methodId);
		return Unsafe.As<Byte, TResult>(ref MemoryMarshal.GetReference(bytes));
	}
	public void CallMethod<TArgs>(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jLocal);
		ImplementationValidationUtilities.ThrowIfProxy(jClass);
		using INativeTransaction jniTransaction =
			this.GetInstanceTransaction(jClass, jLocal, definition, out JObjectLocalRef localRef,
			                            out JMethodId methodId);
		JClassLocalRef? classRef = nonVirtual ? jClass.Reference : null;
		JTrace.CallMethod(jLocal, jClass, definition, nonVirtual, in args);
		this.CallMethod(definition, localRef, classRef, in args, jniTransaction, methodId);
	}
	public void CallMethod<TArgs>(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition,
		Boolean nonVirtual, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ImplementationValidationUtilities.ThrowIfProxy(jMethod);
		ImplementationValidationUtilities.ThrowIfProxy(jLocal);
		ImplementationValidationUtilities.ThrowIfNotMatchDefinition(definition, jMethod.Definition);
		Int32 initialCapacity = nonVirtual ? 3 : 2;
		using INativeTransaction jniTransaction =
			this.Host.MemoryManager.CreateTransaction(initialCapacity + definition.ReferenceCount);
		_ = jniTransaction.Add(jMethod);
		JMethodId methodId = jMethod.MethodId;
		JObjectLocalRef localRef = this.UseObject(jniTransaction, jLocal);
		JClassLocalRef? classRef = nonVirtual ? jniTransaction.Add(this.ReloadClass(jMethod.DeclaringClass)) : null;
		JTrace.CallMethod(jLocal, jMethod.DeclaringClass, definition, nonVirtual, in args);
		this.CallMethod(definition, localRef, classRef, in args, jniTransaction, methodId);
	}
}