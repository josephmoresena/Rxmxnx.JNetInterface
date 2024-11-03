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
	internal void SetObjectCache(LocalCache localCache)
	{
		JTrace.SetObjectCache(localCache.Id, localCache.Name);
		this._cache.SetObjectCache(localCache);
	}
	/// <summary>
	/// Retrieves a global reference for given class name.
	/// </summary>
	/// <param name="metadata">Class metadata.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	internal JGlobalRef GetMainClassGlobalRef(ClassObjectMetadata metadata)
	{
		JClassLocalRef classRef = this._cache.FindMainClass(metadata.Name, metadata.ClassSignature);
		return this.GetMainClassGlobalRef(metadata, classRef);
	}
	/// <summary>
	/// Retrieves a global reference for given class name.
	/// </summary>
	/// <param name="metadata">Class metadata.</param>
	/// <param name="wClassGlobal">Wrapper class global instance.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	internal JGlobalRef GetPrimitiveMainClassGlobalRef(ClassObjectMetadata metadata,
		JGlobalBase? wClassGlobal = default)
	{
		Byte signature = metadata.ClassSignature[0];
		String className = ClassNameHelper.GetPrimitiveClassName(signature);
		JClassLocalRef classRef = !JObject.IsNullOrDefault(wClassGlobal) ?
			this._cache.FindPrimitiveClass(wClassGlobal.As<JClassLocalRef>(), className) :
			this._cache.FindPrimitiveClass(signature);
		return this.GetMainClassGlobalRef(metadata, classRef);
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
	/// <param name="withNoCheckError">Indicates whether <see cref="CheckJniError"/> should not be called.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal unsafe JFieldId GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef,
		Boolean withNoCheckError = false)
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
		if (fieldId == default && !withNoCheckError) this._cache.CheckJniError();
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
		ref readonly NativeInterface6 nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface6>(NativeInterface6.GetObjectRefTypeInfo);
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
	internal JClassObject GetReferenceTypeClass(JClassLocalRef classRef, Boolean keepReference = false)
		=> this._cache.GetClass(classRef, keepReference, JTypeKind.Undefined);
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	internal JClassObject GetObjectClass(JObjectLocalRef localRef, out JReferenceTypeMetadata typeMetadata)
	{
		using LocalFrame frame = new(this, IVirtualMachine.GetObjectClassCapacity);
		JClassLocalRef classRef = this.GetObjectClass(localRef);
		JClassObject jClass = this._cache.GetClass(classRef, true, JTypeKind.Class);
		this._cache.LoadClass(frame, classRef, jClass); // Runtime class loading.
		typeMetadata = this._cache.GetTypeMetadata(jClass);
		return jClass;
	}

	/// <summary>
	/// Loads in current cache given class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void LoadClass(JClassObject jClass) => this._cache.LoadClass(jClass);
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
	/// Checks JNI occurred error.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void CheckJniError() => this._cache.CheckJniError();

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
	internal static ReadOnlySpan<Byte> GetSafeSpan(CString? value)
	{
		if (value is null) return ReadOnlySpan<Byte>.Empty;
		return value.IsNullTerminated ? value.AsSpan() : (CString)value.Clone();
	}
	/// <inheritdoc cref="JEnvironment.GetObjectClass(JObjectLocalRef)"/>
	internal static JClassObject GetObjectClass(JEnvironment env, JObjectLocalRef localRef)
	{
		using LocalFrame frame = new(env, IVirtualMachine.GetObjectClassCapacity);
		JClassLocalRef classRef = env.GetObjectClass(localRef);
		JClassObject jClass = env.GetReferenceTypeClass(classRef);
		env._cache.LoadClass(frame, classRef, jClass); // Runtime class loading.
		return jClass;
	}
}