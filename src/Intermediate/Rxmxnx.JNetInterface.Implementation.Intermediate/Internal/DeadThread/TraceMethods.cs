namespace Rxmxnx.JNetInterface.Internal;

internal partial class DeadThread
{
	/// <summary>
	/// Writes Dispose() method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void TraceDispose()
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to destroy a dead JNI instance. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes IsSameObject(JObject,JObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void IsSameObjectTrace(JObject jObject, JObject? jOther)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine equality between {jObject.ToTraceText()} and {jOther?.ToTraceText()}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetReferenceType(JObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void GetReferenceTypeTrace(JObject jObject)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine reference type from {jObject.ToTraceText()}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetArrayLength(JReferenceObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void GetArrayLengthTrace(JReferenceObject jObject)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jObject.As<JObjectLocalRef>()} array length. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleasePrimitiveSequence(JArrayLocalRef,IntPtr) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void ReleasePrimitiveSequenceTrace(JArrayLocalRef arrayRef, IntPtr pointer)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} memory from {arrayRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleasePrimitiveCriticalSequence(JArrayLocalRef,ValPtr{Byte}) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void ReleasePrimitiveCriticalSequenceTrace(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {criticalPtr} critical memory from {arrayRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes IsInstanceOf{TDataType}(JReferenceObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void IsInstanceOfTrace(JReferenceObject jObject, CString className)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine if {jObject.As<JObjectLocalRef>()} is an instance of {className} class. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes MonitorExit(JObjectLocalRef) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void MonitorExitTrace(JObjectLocalRef localRef)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to exit monitor from {localRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes LocalLoad(JGlobalBase, JLocalObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void LocalLoadTrace(JGlobalBase jGlobal)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine($"Unable to locally load {jGlobal}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes Unload(JLocalObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void UnloadTrace(JLocalObject jLocal)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to unload {jLocal.LocalReference}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes Unload(JGlobalBase) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void UnloadTrace(JGlobalRef? globalRef, JWeakRef? weakRef)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to unload {globalRef?.ToString() ?? weakRef?.ToString()}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes IsParameter(JLocalObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void IsParameterTrace(JLocalObject jLocal)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jLocal.LocalReference} is parameter. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetLength(JReferenceObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void GetLengthTrace(JReferenceObject jObject)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jObject.As<JObjectLocalRef>()} string length. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetUtf8Length(JReferenceObject) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void GetUtf8LengthTrace(JReferenceObject jObject, Boolean isLongLength)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jObject.As<JObjectLocalRef>()} UTF8 string {(isLongLength ? "long " : "")}length. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleaseSequence(JStringLocalRef, ReadOnlyValPtr{Char}) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void ReleaseSequenceTrace(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleaseUtf8Sequence(JStringLocalRef, ReadOnlyValPtr{Byte}) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void ReleaseUtf8SequenceTrace(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleaseCriticalSequence(JStringLocalRef, ReadOnlyValPtr{Char}) method call to the trace listeners.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private void ReleaseCriticalSequenceTrace(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
	{
		if (JVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} critical memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
}