namespace Rxmxnx.JNetInterface;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
partial class JEnvironment
{
	/// <summary>
	/// Class cache.
	/// </summary>
	internal ClassCache ClassCache => this._cache.GetClassCache();
	/// <summary>
	/// Local cache.
	/// </summary>
	internal LocalCache LocalCache => this._cache.GetLocalCache();
	/// <inheritdoc cref="IClassFeature.ClassObject"/>
	internal JClassObject ClassObject => this._cache.ClassObject;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	internal JEnvironment(IVirtualMachine vm, JEnvironmentRef envRef)
		=> this._cache = new((JVirtualMachine)vm, this, envRef);
	/// <summary>
	/// Sets current object cache.
	/// </summary>
	/// <param name="localCache">A <see cref="LocalCache"/> instance.</param>
	internal void SetObjectCache(LocalCache localCache) => this._cache.SetObjectCache(localCache);
	/// <summary>
	/// Retrieves a global reference for given class name.
	/// </summary>
	/// <param name="metadata">Class metadata name.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	internal unsafe JGlobalRef GetClassGlobalRef(ClassObjectMetadata metadata)
	{
		JClassLocalRef classRef;
		if (metadata.ClassSignature.Length == 1)
			//Primitive Class
			classRef = this._cache.FindPrimitiveClass(metadata.ClassSignature[0]);
		else
			fixed (Byte* ptr = &MemoryMarshal.GetReference(metadata.Name.AsSpan()))
				classRef = this._cache.FindClass(new(ptr));
		try
		{
			JGlobalRef globalRef = this._cache.CreateGlobalRef(classRef.Value);
			return globalRef;
		}
		finally
		{
			this.DeleteLocalRef(classRef.Value);
		}
	}
	/// <summary>
	/// Deletes <paramref name="globalRef"/>.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	internal unsafe void DeleteGlobalRef(JGlobalRef globalRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.DeleteGlobalRefInfo);
		nativeInterface.ReferenceFunctions.DeleteGlobalRef.DeleteRef(this.Reference, globalRef);
	}
	/// <summary>
	/// Deletes <paramref name="weakRef"/>.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	internal unsafe void DeleteWeakGlobalRef(JWeakRef weakRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.DeleteWeakGlobalRefInfo);
		nativeInterface.WeakGlobalFunctions.DeleteWeakGlobalRef.DeleteRef(this.Reference, weakRef);
	}
	/// <summary>
	/// Retrieves field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal unsafe JFieldId GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetFieldIdInfo);
		JFieldId fieldId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			fieldId = nativeInterface.InstanceFieldFunctions.GetFieldId.GetId(
				this.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, fieldId);
		if (fieldId == default) this._cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves static field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal unsafe JFieldId GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticFieldIdInfo);
		JFieldId fieldId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			fieldId = nativeInterface.StaticFieldFunctions.GetFieldId.GetId(
				this.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, fieldId);
		if (fieldId == default) this._cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	internal unsafe JMethodId GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.InstanceMethodFunctions.MethodFunctions.GetMethodId.GetId(
				this.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves static method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal unsafe JMethodId GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.StaticMethodFunctions.GetMethodId.GetId(
				this.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves type of given reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JReferenceType"/> value.</returns>
	internal unsafe JReferenceType GetReferenceType(JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetObjectRefTypeInfo);
		JReferenceType result = nativeInterface.GetObjectRefType(this._cache.Reference, localRef);
		this._cache.CheckJniError();
		return result;
	}
	/// <summary>
	/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	internal JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference = false)
		=> this._cache.GetClass(classRef, keepReference);
	/// <inheritdoc cref="IClassFeature.GetClass{TObject}()"/>
	internal JClassObject GetClass<TObject>() where TObject : JReferenceObject, IReferenceType<TObject>
		=> this._cache.GetClass<TObject>();
	/// <summary>
	/// Deletes <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference to remove.</param>
	internal unsafe void DeleteLocalRef(JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.DeleteLocalRefInfo);
		nativeInterface.ReferenceFunctions.DeleteLocalRef.DeleteRef(this.Reference, localRef);
	}
	/// <summary>
	/// Retrieves object class reference.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	internal unsafe JClassLocalRef GetObjectClass(JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.GetObjectClassInfo);
		JClassLocalRef classRef = nativeInterface.ObjectFunctions.GetObjectClass(this.Reference, localRef);
		if (classRef.IsDefault) this._cache.CheckJniError();
		return classRef;
	}
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="metadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	internal JClassObject GetObjectClass(JObjectLocalRef localRef, out JReferenceTypeMetadata metadata)
	{
		using LocalFrame frame = new(this, 2);
		JClassLocalRef classRef = this.GetObjectClass(localRef);
		JClassObject jClass = this._cache.GetClass(classRef, true);
		frame[jClass.LocalReference] = jClass.Lifetime.GetCacheable();
		metadata = this._cache.GetTypeMetadata(jClass);
		return jClass;
	}

	/// <summary>
	/// Loads in current cache given class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	internal void LoadClass(JClassObject jClass) => this._cache.LoadClass(jClass);
	/// <summary>
	/// Creates a new local reference for <paramref name="objectRef"/>.
	/// </summary>
	/// <typeparam name="TObjectRef">A <see cref="IWrapper{JObjectLocalRef}"/> type.</typeparam>
	/// <param name="objectRef">A <see cref="IWrapper{JObjectLocalRef}"/> reference.</param>
	internal unsafe JObjectLocalRef CreateLocalRef<TObjectRef>(TObjectRef objectRef)
		where TObjectRef : unmanaged, INativeType<TObjectRef>, IWrapper<JObjectLocalRef>
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.NewLocalRefInfo);
		JObjectLocalRef localRef =
			nativeInterface.ReferenceFunctions.NewLocalRef.NewRef(this.Reference, objectRef.Value);
		JTrace.CreateLocalRef(objectRef, localRef);
		if (localRef == default) this._cache.CheckJniError();
		return localRef;
	}
	/// <summary>
	/// Sends JNI fatal error signal to VM.
	/// </summary>
	/// <param name="errorMessage">Error message.</param>
	internal unsafe void FatalError(ReadOnlySpan<Byte> errorMessage)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.FatalErrorInfo);
		fixed (Byte* ptr = &MemoryMarshal.GetReference(errorMessage))
			nativeInterface.ErrorFunctions.FatalError(this.Reference, ptr);
	}

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>The <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.</returns>
	internal static JEnvironment GetEnvironment(JEnvironmentRef reference)
	{
		JVirtualMachine vm = EnvironmentCache.GetVirtualMachine(reference);
		return vm.GetEnvironment(reference);
	}
	/// <summary>
	/// Retrieves safe read-only span from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="CString"/> instance.</param>
	/// <returns>A binary read-only span from <paramref name="value"/>.</returns>
	public static ReadOnlySpan<Byte> GetSafeSpan(CString? value)
	{
		if (value is null)
			return ReadOnlySpan<Byte>.Empty;
		return value.IsNullTerminated ? value.AsSpan() : (CString)value.Clone();
	}
}