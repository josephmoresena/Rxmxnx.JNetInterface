namespace Rxmxnx.JNetInterface.Internal;

internal partial class DeadThread
{
	/// <summary>
	/// Writes Dispose() method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void TraceDispose()
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to destroy a dead JNI instance. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes IsSameObject(JObject,JObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void IsSameObjectTrace(JObject jObject, JObject? jOther)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine equality between {jObject.ToTraceText()} and {jOther?.ToTraceText()}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetReferenceType(JObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void GetReferenceTypeTrace(JObject jObject)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine reference type from {jObject.ToTraceText()}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetArrayLength(JReferenceObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void GetArrayLengthTrace(JReferenceObject jObject)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jObject.As<JObjectLocalRef>()} array length. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleasePrimitiveSequence(JArrayLocalRef,IntPtr) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void ReleasePrimitiveSequenceTrace(JArrayLocalRef arrayRef, IntPtr pointer)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} memory from {arrayRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleasePrimitiveCriticalSequence(JArrayLocalRef,ValPtr{Byte}) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void ReleasePrimitiveCriticalSequenceTrace(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {criticalPtr} critical memory from {arrayRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes IsInstanceOf{TDataType}(JReferenceObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void IsInstanceOfTrace<TDataType>(JReferenceObject jObject) where TDataType : IDataType<TDataType>
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine if {jObject.As<JObjectLocalRef>()} is an instance of {IDataType.GetMetadata<TDataType>().ClassName} class. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes IsInstanceOf{TDataType}(JReferenceObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void IsInstanceOfTrace(JReferenceObject jObject, CString className)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine if {jObject.As<JObjectLocalRef>()} is an instance of {className} class. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes MonitorExit(JObjectLocalRef) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void MonitorExitTrace(JObjectLocalRef localRef)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to exit monitor from {localRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes LocalLoad(JGlobalBase, JLocalObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void LocalLoadTrace(JGlobalBase jGlobal)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine($"Unable to locally load {jGlobal}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes Unload(JLocalObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void UnloadTrace(JLocalObject jLocal)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to unload {jLocal.LocalReference}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes Unload(JGlobalBase) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void UnloadTrace(JGlobalRef? globalRef, JWeakRef? weakRef)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to unload {globalRef?.ToString() ?? weakRef?.ToString()}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes IsParameter(JLocalObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void IsParameterTrace(JLocalObject jLocal)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jLocal.LocalReference} is parameter. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetLength(JReferenceObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void GetLengthTrace(JReferenceObject jObject)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jObject.As<JObjectLocalRef>()} string length. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes GetUtf8Length(JReferenceObject) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void GetUtf8LengthTrace(JReferenceObject jObject)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to determine {jObject.As<JObjectLocalRef>()} UTF8 string length. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleaseSequence(JStringLocalRef, ReadOnlyValPtr{Char}) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void ReleaseSequenceTrace(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleaseUtf8Sequence(JStringLocalRef, ReadOnlyValPtr{Byte}) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void ReleaseUtf8SequenceTrace(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
	/// <summary>
	/// Writes ReleaseCriticalSequence(JStringLocalRef, ReadOnlyValPtr{Char}) method call to the trace listeners.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private void ReleaseCriticalSequenceTrace(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
	{
		if (IVirtualMachine.TraceEnabled)
			Trace.WriteLine(
				$"Unable to release {pointer} critical memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
	}
}