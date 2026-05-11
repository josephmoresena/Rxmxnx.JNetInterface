namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCore
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
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="withNoCheckError">Indicates whether <see cref="CheckJniError"/> should not be called.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	public static unsafe JFieldId GetStaticFieldId(EnvironmentCore core, JFieldDefinition definition,
		JClassLocalRef classRef, Boolean withNoCheckError = false)
	{
		ref readonly NativeInterface nativeInterface =
			ref core.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticFieldIdInfo);
		JFieldId fieldId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			fieldId = nativeInterface.StaticFieldFunctions.GetFieldId.GetId(
				core.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, fieldId);
		if (fieldId == default && !withNoCheckError) core.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	public static unsafe JFieldId GetFieldId(EnvironmentCore core, JFieldDefinition definition, JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref core.GetNativeInterface<NativeInterface>(NativeInterface.GetFieldIdInfo);
		JFieldId fieldId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			fieldId = nativeInterface.InstanceFieldFunctions.GetFieldId.GetId(
				core.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, fieldId);
		if (fieldId == default) core.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	public static unsafe JMethodId GetMethodId(EnvironmentCore core, JCallDefinition definition,
		JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref core.GetNativeInterface<NativeInterface>(NativeInterface.GetMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.InstanceMethodFunctions.MethodFunctions.GetMethodId.GetId(
				core.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) core.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves static method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	public static unsafe JMethodId GetStaticMethodId(EnvironmentCore core, JCallDefinition definition,
		JClassLocalRef classRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref core.GetNativeInterface<NativeInterface>(NativeInterface.GetStaticMethodIdInfo);
		JMethodId methodId;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(definition.Name.AsSpan()))
		fixed (Byte* signaturePtr = &MemoryMarshal.GetReference(definition.Descriptor.AsSpan()))
		{
			methodId = nativeInterface.StaticMethodFunctions.GetMethodId.GetId(
				core.Reference, classRef, namePtr, signaturePtr);
		}
		JTrace.GetAccessibleId(classRef, definition, methodId);
		if (methodId == default) core.CheckJniError();
		return methodId;
	}
	/// <inheritdoc cref="IEnvironment.DescribeException()"/>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	public static void DescribeException(EnvironmentCore core)
	{
		ref readonly NativeInterface nativeInterface =
			ref core.GetNativeInterface<NativeInterface>(NativeInterface.ExceptionDescribeInfo);
		nativeInterface.ErrorFunctions.ExceptionDescribe(core.Reference);
	}
	/// <summary>
	/// Retrieves a global reference for given class reference.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="typeInformation">Type information.</param>
	/// <param name="classRef">A local class reference.</param>
	/// <param name="deleteLocalRef">Indicates whether local class reference should be deleted.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	internal static JGlobalRef GetMainClassGlobalRef(EnvironmentCore core, ITypeInformation typeInformation,
		JClassLocalRef classRef, Boolean deleteLocalRef = true)
	{
		try
		{
			JGlobalRef globalRef = core.CreateGlobalRef(classRef.Value, true);
			if (globalRef != default) return globalRef;
		}
		finally
		{
			if (deleteLocalRef)
				core.DeleteLocalRef(classRef.Value);
		}

		EnvironmentCore.DescribeException(core);
		core.ClearException(); // Clears JNI exception.

		IMessageResource resource = IMessageResource.GetInstance();
		String className = typeInformation.JavaClassName;
		String message = resource.MainClassGlobalError(className);
		throw new NotSupportedException(message);
	}
#if !ANDROID
	/// <summary>
	/// Sends JNI fatal error signal to VM.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="errorMessage">Error message.</param>
	public static unsafe void FatalError(EnvironmentCore core, ReadOnlySpan<Byte> errorMessage)
	{
		ref readonly NativeInterface nativeInterface =
			ref core.GetNativeInterface<NativeInterface>(NativeInterface.FatalErrorInfo);
		fixed (Byte* ptr = &MemoryMarshal.GetReference(errorMessage))
			nativeInterface.ErrorFunctions.FatalError(core.Reference, ptr);
	}
#endif
	/// <summary>
	/// Retrieves object class reference.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="localRef">Object instance to get class.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	public static JClassLocalRef GetObjectClass(EnvironmentCore core, JObjectLocalRef localRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref core.GetNativeInterface<NativeInterface>(NativeInterface.GetObjectClassInfo);
		JClassLocalRef classRef = nativeInterface.ObjectFunctions.GetObjectClass(core.Reference, localRef);
		if (classRef == default) core.CheckJniError();
		JTrace.GetObjectClass(localRef, classRef);
		return classRef;
	}
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	public static JClassObject GetObjectClass(EnvironmentCore core, JObjectLocalRef localRef,
		out JReferenceTypeMetadata typeMetadata)
	{
		using LocalFrame frame = new(core._env, IVirtualMachine.GetObjectClassCapacity);
		JClassLocalRef classRef = EnvironmentCore.GetObjectClass(core, localRef);
		JClassObject jClass = core.GetClass(classRef, true, JTypeKind.Class, true);
		core.LoadClass(frame, classRef, jClass); // Runtime class loading.
		typeMetadata = core.GetTypeMetadata(jClass);
		return jClass;
	}
	/// <summary>
	/// Parses <paramref name="throwableRef"/> to a <see cref="ThrowableException"/> instance.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	public static ThrowableException? ParseException(EnvironmentCore core, JThrowableLocalRef throwableRef)
	{
		if (throwableRef == default) return default;
		ThrowableException jniException = core.CreateThrowableException(throwableRef);
		core.ThrowJniException(jniException, false);
		return jniException;
	}
	/// <summary>
	/// Indicates whether validation of <paramref name="jGlobal"/> can be avoided.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jGlobal"/> validation can be avoided;
	/// otherwise, <see langword="false"/>;
	/// </returns>
	public static Boolean IsValidationAvoidable(EnvironmentCore? core, JGlobalBase jGlobal)
	{
		if (core is null || !core.Host.MemoryManager.SecureRemove(jGlobal.As<JObjectLocalRef>())) return true;
		Boolean isWeak = jGlobal is JWeak;
		if (!isWeak && LocalMainClasses.IsMainGlobal(jGlobal as JGlobal))
			return true;
		return Random.Shared.Next(0, 10) > (!isWeak ? 5 : 2);
	}
	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
#pragma warning disable CA1859
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
#pragma warning restore CA1859
}