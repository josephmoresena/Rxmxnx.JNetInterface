namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCache
{
	/// <summary>
	/// Retrieves safe read-only span from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="CString"/> instance.</param>
	/// <returns>A binary read-only span from <paramref name="value"/>.</returns>
	public static ReadOnlySpan<Byte> GetSafeSpan(CString? value)
	{
		if (value is null) return ReadOnlySpan<Byte>.Empty;
		return value.IsNullTerminated ? value.AsSpan() : (CString)value.Clone();
	}
	/// <summary>
	/// Retrieves a <see cref="JVirtualMachineRef"/> from given <paramref name="envRef"/>.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>A <see cref="JVirtualMachineRef"/> value.</returns>
	public static unsafe JVirtualMachineRef GetVirtualMachineRef(JEnvironmentRef envRef)
	{
		ref readonly NativeInterface nativeInterface = ref *(NativeInterface*)envRef.InterfacePointer;
		JniException? jniException = nativeInterface.GetVirtualMachine(envRef, out JVirtualMachineRef vmRef);
		return jniException is null ? vmRef : throw jniException;
	}
	/// <summary>
	/// Retrieves static field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="withNoCheckError">Indicates whether <see cref="CheckJniError"/> should not be called.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	public static unsafe JFieldId GetStaticFieldId(EnvironmentCache cache, JFieldDefinition definition,
		JClassLocalRef classRef, Boolean withNoCheckError = false)
	{
		ref readonly NativeInterface nativeInterface =
			ref cache.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticFieldIdInfo);
		JFieldId fieldId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			fieldId = nativeInterface.StaticFieldFunctions.GetFieldId.GetId(
				cache.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, fieldId);
		if (fieldId == default && !withNoCheckError) cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	public static unsafe JFieldId GetFieldId(EnvironmentCache cache, JFieldDefinition definition,
		JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref cache.GetNativeInterface<NativeInterface>(NativeInterface.GetFieldIdInfo);
		JFieldId fieldId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			fieldId = nativeInterface.InstanceFieldFunctions.GetFieldId.GetId(
				cache.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, fieldId);
		if (fieldId == default) cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	public static unsafe JMethodId GetMethodId(EnvironmentCache cache, JCallDefinition definition,
		JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref cache.GetNativeInterface<NativeInterface>(NativeInterface.GetMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.InstanceMethodFunctions.MethodFunctions.GetMethodId.GetId(
				cache.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) cache.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves static method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	public static unsafe JMethodId GetStaticMethodId(EnvironmentCache cache, JCallDefinition definition,
		JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref cache.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.StaticMethodFunctions.GetMethodId.GetId(
				cache.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) cache.CheckJniError();
		return methodId;
	}
	/// <inheritdoc cref="IEnvironment.DescribeException()"/>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	public static void DescribeException(EnvironmentCache cache)
	{
		ref readonly NativeInterface nativeInterface =
			ref cache.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionDescribeInfo);
		nativeInterface.ErrorFunctions.ExceptionDescribe(cache.Reference);
	}
	/// <summary>
	/// Retrieves a global reference for given class reference.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="typeInformation">Type information.</param>
	/// <param name="classRef">A local class reference.</param>
	/// <param name="deleteLocalRef">Indicates whether local class reference should be deleted.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	internal static JGlobalRef GetMainClassGlobalRef(EnvironmentCache cache, ITypeInformation typeInformation,
		JClassLocalRef classRef, Boolean deleteLocalRef = true)
	{
		try
		{
			JGlobalRef globalRef = cache.CreateGlobalRef(classRef.Value, true);
			if (globalRef != default) return globalRef;
		}
		finally
		{
			if (deleteLocalRef)
				cache.DeleteLocalRef(classRef.Value);
		}

		EnvironmentCache.DescribeException(cache);
		cache.ClearException(); // Clears JNI exception.

		IMessageResource resource = IMessageResource.GetInstance();
		String className = typeInformation.JavaClassName;
		String message = resource.MainClassGlobalError(className);
		throw new NotSupportedException(message);
	}
	/// <summary>
	/// Sends JNI fatal error signal to VM.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="errorMessage">Error message.</param>
	public static unsafe void FatalError(EnvironmentCache cache, ReadOnlySpan<Byte> errorMessage)
	{
		ref readonly NativeInterface nativeInterface =
			ref cache.GetNativeInterface<NativeInterface>(NativeInterface.FatalErrorInfo);
		fixed (Byte* ptr = &MemoryMarshal.GetReference(errorMessage))
			nativeInterface.ErrorFunctions.FatalError(cache.Reference, ptr);
	}
	/// <summary>
	/// Retrieves object class reference.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="localRef">Object instance to get class.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	public static JClassLocalRef GetObjectClass(EnvironmentCache cache, JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref cache.GetNativeInterface<NativeInterface>(NativeInterface.GetObjectClassInfo);
		JClassLocalRef classRef = nativeInterface.ObjectFunctions.GetObjectClass(cache.Reference, localRef);
		if (classRef == default) cache.CheckJniError();
		JTrace.GetObjectClass(localRef, classRef);
		return classRef;
	}
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	public static JClassObject GetObjectClass(EnvironmentCache cache, JObjectLocalRef localRef,
		out JReferenceTypeMetadata typeMetadata)
	{
		using LocalFrame frame = new(cache._env, IVirtualMachine.GetObjectClassCapacity);
		JClassLocalRef classRef = EnvironmentCache.GetObjectClass(cache, localRef);
		JClassObject jClass = cache.GetClass(classRef, true, JTypeKind.Class, true);
		cache.LoadClass(frame, classRef, jClass); // Runtime class loading.
		typeMetadata = cache.GetTypeMetadata(jClass);
		return jClass;
	}
}