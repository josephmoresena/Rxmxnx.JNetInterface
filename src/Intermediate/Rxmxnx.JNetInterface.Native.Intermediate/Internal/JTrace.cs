namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Trace for internal use.
/// </summary>
[ExcludeFromCodeCoverage]
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
	/// <param name="elementSignature">Element signature.</param>
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
		Trace.WriteLine($"{classRef}, Name: {classInformation[0]}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and retrieving class reference using a <see cref="JClassObject"/> instance
	/// to the trace listeners.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void FindClass(JClassObject jClass, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"{jClass.Name}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and registiring reference instance to the trace listeners.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void RegisterObject(JReferenceObject? jObject, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled || jObject is null) return;
		Trace.WriteLine(jObject.ToTraceText(), callerMethod);
	}
	/// <summary>
	/// Writes a category name and deleting local reference to the trace listeners.
	/// </summary>
	/// <param name="isRegistered">Indicates whether current reference is registred.</param>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="localRef">A local object reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void Unload(Boolean isRegistered, Boolean isAttached, Boolean isAlive, JObjectLocalRef localRef,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		if (!isRegistered)
			Trace.WriteLine($"Unable to remove unregistered {localRef}.", callerMethod);
		else if (!isAttached)
			Trace.WriteLine($"Unable to remove {localRef}. Thread is not attached.", callerMethod);
		else if (isAlive)
			Trace.WriteLine($"Unable to remove {localRef}. JVM is not alive.", callerMethod);
		else
			Trace.WriteLine($"{localRef} removed.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and deleting global reference to the trace listeners.
	/// </summary>
	/// <param name="isAttached">Indicates whether current thread is attached to VM.</param>
	/// <param name="isAlive">Indicates whether current VM is alive.</param>
	/// <param name="globalRef">A global object reference.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void Unload<TGlobalRef>(Boolean isAttached, Boolean isAlive, TGlobalRef globalRef,
		[CallerMemberName] String callerMethod = "")
		where TGlobalRef : unmanaged, IObjectGlobalReferenceType<TGlobalRef>
	{
		if (!IVirtualMachine.TraceEnabled) return;
		if (!isAttached)
			Trace.WriteLine($"Unable to remove {globalRef}. Thread is not attached.", callerMethod);
		else if (!isAlive)
			Trace.WriteLine($"Unable to {globalRef}. JVM is not alive.", callerMethod);
		else
			Trace.WriteLine($"{globalRef} removed.", callerMethod);
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
		String memoryText = isCritical ? "Critical memory" : "Memory";
		if (!isAttached)
			Trace.WriteLine($"Unable to release {memoryText} 0x{pointer:0x8} {objectRef}. Thread is not attached.",
			                callerMethod);
		else if (!isAlive)
			Trace.WriteLine($"Unable to release {memoryText} 0x{pointer:0x8} {objectRef}. JVM is not alive.",
			                callerMethod);
		else if (!released)
			Trace.WriteLine($"Error attempting to release {memoryText} 0x{pointer:0x8} {objectRef}.", callerMethod);
		else
			Trace.WriteLine($"{memoryText} 0x{pointer:0x8} {objectRef} released.", callerMethod);
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
			Trace.WriteLine($"Unable to exit monitor {localRef}. Thread is not attached.", callerMethod);
		else if (!isAlive)
			Trace.WriteLine($"Unable to exit monitor {localRef}. JVM is not alive.", callerMethod);
		else if (!exited)
			Trace.WriteLine($"Error attempting to exit monitor {localRef}.", callerMethod);
		else
			Trace.WriteLine($"Exited monitor {localRef}.", callerMethod);
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
		Trace.WriteLine($"{classRef} {referenceType} {classObjectMetadata}.", callerMethod);
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
		Trace.WriteLine($"{classRef} {referenceType}.", callerMethod);
	}
	/// <summary>
	/// Writes a category name and loading global class object to the trace listeners.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void LoadGlobalClass(JClassObject jClass, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(jClass.ToTraceText(), callerMethod);
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
		Trace.WriteLine(classObjectMetadata.ToTraceText(), callerMethod);
	}
}