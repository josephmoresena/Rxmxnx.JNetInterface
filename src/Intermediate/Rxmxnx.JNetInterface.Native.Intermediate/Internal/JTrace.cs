namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Trace for internal use.
/// </summary>
[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
                 Justification = CommonConstants.NonStandardTraceJustification)]
internal static partial class JTrace
{
	/// <summary>
	/// Writes a category name and error occured attempted to reflect GetArrayArrayMetadataMethod to the trace listeners.
	/// </summary>
	/// <param name="ex">A <see cref="Exception"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void ReflectGetArrayArrayMetadataMethodError(Exception ex,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			$"Unable to create {nameof(MethodInfo)} instance of {nameof(IArrayType)}.{nameof(IArrayType.GetArrayArrayMetadata)}<>(). {ex.Message}",
			callerMethod);
	}
	/// <summary>
	/// Writes a category name and error occured attempted to retrieve array metadata using reflection to the trace listeners.
	/// </summary>
	/// <param name="elementSignature">Array element signature.</param>
	/// <param name="ex">A <see cref="Exception"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetArrayArrayMetadataWithReflectionError(CString elementSignature, Exception ex,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"Unable to create {nameof(JArrayTypeMetadata)} instance of [[{elementSignature}. {ex.Message}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and retrieving class using a <see cref="JClassLocalRef"/> reference
	/// to the trace listeners.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="classInformation">A <see cref="CStringSequence"/> containing class information.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetClass(JClassLocalRef classRef, CStringSequence classInformation,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			!classRef.IsDefault ?
				$"thread: {Environment.CurrentManagedThreadId} {classRef} name: {classInformation[0]}" :
				$"thread: {Environment.CurrentManagedThreadId} name: {classInformation[0]}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and retrieving class reference or retrieving type metadata using a
	/// <see cref="JClassObject"/> instance to the trace listeners.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetMetadataOrFindClass(JClassObject jClass, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {jClass.Name}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and retrieving class reference using class name to the trace listeners.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void FindClass(CString className, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {className}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and retrieving type metadata for <paramref name="jClass"/> using
	/// super <see cref="JClassObject"/> instance.
	/// to the trace listeners.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="jSuperClass">A super <see cref="JClassObject"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetSuperTypeMetadata(JClassObject jClass, JClassObject jSuperClass,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {jClass.Name} <- {jSuperClass.Name}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and registiring reference instance to the trace listeners.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="cacheId">Cache identifier.</param>
	/// <param name="cacheName">Type of cache.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void RegisterObject(JReferenceObject? jObject, Guid cacheId, String cacheName,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		if (jObject is not null)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} {jObject.ToTraceText()} at {cacheName} cache {cacheId}",
				callerMethod);
	}
	/// <summary>
	/// Writes a category name and deleting local reference to the trace listeners.
	/// </summary>
	/// <param name="isRegistered">Indicates whether current reference is registred.</param>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="localRef">A local object reference.</param>
	/// <param name="cacheId">Cache identifier.</param>
	/// <param name="cacheName">Type of cache.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void Unload(Boolean isRegistered, Boolean isAttached, Boolean isAlive, JObjectLocalRef localRef,
		Guid cacheId, String cacheName, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		if (localRef == default) return;
		if (!isRegistered)
			Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} Unable to remove unregistered {localRef}.",
			                callerMethod);
		else if (!isAttached)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to remove {localRef}. Thread is not attached.",
				callerMethod);
		else if (!isAlive)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to remove {localRef}. JVM is not alive.",
				callerMethod);
		else
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} {localRef} removed from {cacheName} cache {cacheId}.",
				callerMethod);
	}
	/// <summary>
	/// Writes a category name and deleting global reference to the trace listeners.
	/// </summary>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="globalRef">A global object reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void UnloadGlobal<TGlobalRef>(Boolean isAttached, Boolean isAlive, TGlobalRef globalRef,
		[CallerMemberName] String callerMethod = "")
		where TGlobalRef : unmanaged, IObjectGlobalReferenceType<TGlobalRef>,
		IEqualityOperators<TGlobalRef, TGlobalRef, Boolean>
	{
		if (!IVirtualMachine.TraceEnabled) return;
		if (globalRef == default) return;
		JTrace.UnloadNonGenericGlobal(isAttached, isAlive, $"{globalRef}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and releasing native memory to the trace listeners.
	/// </summary>
	/// <param name="isCritical">Indicates whether current VM is alive.</param>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="released">Indicates whether memory was successfully released.</param>
	/// <param name="objectRef">Object reference.</param>
	/// <param name="pointer">Pointer to memory.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void ReleaseMemory<TObjectRef>(Boolean isCritical, Boolean isAttached, Boolean isAlive,
		Boolean released, TObjectRef objectRef, IntPtr pointer, [CallerMemberName] String callerMethod = "")
		where TObjectRef : unmanaged, IObjectReferenceType<TObjectRef>
	{
		if (!IVirtualMachine.TraceEnabled) return;
		JTrace.ReleaseNonGenericMemory(isCritical, isAttached, isAlive, released, pointer, $"{objectRef}",
		                               callerMethod);
	}
	/// <summary>
	/// Writes a category name and exiting monitor to the trace listeners.
	/// </summary>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="exited">Indicates whether monitor was successfully exited.</param>
	/// <param name="localRef">Object reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void MonitorExit(Boolean isAttached, Boolean isAlive, Boolean exited, JObjectLocalRef localRef,
		[CallerMemberName] String callerMethod = "")
	{
		if (!isAttached)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to exit monitor {localRef}. Thread is not attached.",
				callerMethod);
		else if (!isAlive)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Unable to exit monitor {localRef}. JVM is not alive.",
				callerMethod);
		else if (!exited)
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} Error attempting to exit monitor {localRef}.",
				callerMethod);
		else
			Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} Exited monitor {localRef}.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and parsing object as class instance to the trace listeners.
	/// </summary>
	/// <param name="classRef">Class reference.</param>
	/// <param name="referenceType">Type of class reference.</param>
	/// <param name="classObjectMetadata">Loaded class metadata.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void AsClassObject(JClassLocalRef classRef, JReferenceType referenceType,
		ClassObjectMetadata? classObjectMetadata, [CallerMemberName] String callerMethod = "")
	{
		if (classObjectMetadata is null)
		{
			JTrace.GetClassInfo(classRef, referenceType, callerMethod);
			return;
		}
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} {classRef} {referenceType} {classObjectMetadata}.",
			callerMethod);
	}
	/// <summary>
	/// Writes a category name and retrieving class information to the trace listeners.
	/// </summary>
	/// <param name="classRef">Class reference.</param>
	/// <param name="referenceType">Type of class reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetClassInfo(JClassLocalRef classRef, JReferenceType referenceType,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {classRef} {referenceType}.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and loading global class object to the trace listeners.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void LoadGlobalClass(JClassObject jClass, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {jClass.ToTraceText()}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and loading class metadata to the trace listeners.
	/// </summary>
	/// <param name="classObjectMetadata">A <see cref="ClassObjectMetadata"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void LoadClassMetadata(ClassObjectMetadata classObjectMetadata,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {classObjectMetadata.ToTraceText()}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and creation of local reference to the trace listeners.
	/// </summary>
	/// <param name="objectRef">A JNI object reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void CreateLocalRef<TObjectRef>(TObjectRef objectRef, [CallerMemberName] String callerMethod = "")
		where TObjectRef : unmanaged, INativeType<TObjectRef>, IWrapper<JObjectLocalRef>
	{
		if (!IVirtualMachine.TraceEnabled) return;
		JTrace.CreateNonGenericLocalRef($"{objectRef}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and creation of local reference to the trace listeners.
	/// </summary>
	/// <param name="objectRef">A JNI object reference.</param>
	/// <param name="localRef">Local JNI object reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void CreateLocalRef<TObjectRef>(TObjectRef objectRef, JObjectLocalRef localRef,
		[CallerMemberName] String callerMethod = "")
		where TObjectRef : unmanaged, INativeType<TObjectRef>, IWrapper<JObjectLocalRef>
	{
		if (!IVirtualMachine.TraceEnabled) return;
		JTrace.CreateNonGenericLocalRef($"{objectRef}", localRef, callerMethod);
	}
	/// <summary>
	/// Writes a category name and finallization call to the trace listeners.
	/// </summary>
	/// <param name="result">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void FinalizeCall(JLocalObject result, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {result.ToTraceText()}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and using type metadata for <paramref name="jClass"/>
	/// to the trace listeners.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void UseTypeMetadata(JClassObject jClass, JReferenceTypeMetadata typeMetadata,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} {jClass.Name} uses type metadata from {typeMetadata.ClassName}.",
			callerMethod);
	}
	/// <summary>
	/// Writes a category name and using type metadata for <paramref name="arraySignature"/>
	/// to the trace listeners.
	/// </summary>
	/// <param name="arraySignature">Array JNI signature.</param>
	/// <param name="typeMetadata">A <see cref="JArrayTypeMetadata"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void UseTypeMetadata(ReadOnlySpan<Byte> arraySignature, JArrayTypeMetadata typeMetadata,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} {Encoding.UTF8.GetString(arraySignature)} uses type metadata from {typeMetadata.ClassName}.",
			callerMethod);
	}
	/// <summary>
	/// Writes a category name and invoking action at <paramref name="thread"/>
	/// to the trace listeners.
	/// </summary>
	/// <param name="thread">A <see cref="IThread"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void InvokeAt(IThread thread, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			$"thread: {Environment.CurrentManagedThreadId} {thread.Reference} name: {thread.Name} daemon: {thread.Daemon}",
			callerMethod);
	}
	/// <summary>
	/// Writes a category name and setting local cache with <paramref name="cacheId"/> identifier.
	/// to the trace listeners.
	/// </summary>
	/// <param name="cacheId">A <see cref="Guid"/> cache identifier.</param>
	/// <param name="nameCache">Type of cache.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void SetObjectCache(Guid cacheId, String nameCache, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {nameCache} cache: {cacheId}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and deleting local cache with <paramref name="cacheId"/> identifier.
	/// to the trace listeners.
	/// </summary>
	/// <param name="cacheId">A <see cref="Guid"/> cache identifier.</param>
	/// <param name="result">Resulting object.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void DeleteObjectCache(Guid cacheId, JLocalObject? result,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			result is null ?
				$"thread: {Environment.CurrentManagedThreadId} local cache: {cacheId}" :
				$"thread: {Environment.CurrentManagedThreadId} local cache: {cacheId} result: {result.ToTraceText()}",
			callerMethod);
	}
}