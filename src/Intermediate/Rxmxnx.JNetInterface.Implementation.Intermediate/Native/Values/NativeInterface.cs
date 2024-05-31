namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Function pointer based-struct replacement for <see cref="JNativeInterface"/> type.
/// </summary>
/// <remarks>JNI 1.2</remarks>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe partial struct NativeInterface : INativeInterface<NativeInterface>
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
#pragma warning disable CS0169
	private readonly JNativeInterface.ComReserved _reserved;
#pragma warning restore CS0169

	/// <inheritdoc cref="JNativeInterface.GetVersionPointer"/>
	public readonly delegate* unmanaged<JEnvironmentRef, Int32> GetVersion;
	/// <summary>
	/// Pointers to <c>DefineClass</c>, <c>FindClass</c>, <c>FromReflectedMethod</c>,
	/// <c>FromReflectedField</c>, <c>ToReflectedMethod</c>, <c>GetSuperclass</c>,
	/// <c>IsAssignableFrom</c> and <c>ToReflectedField</c> functions.
	/// </summary>
	public readonly ClassFunctionSet ClassFunctions;
	/// <summary>
	/// Pointers to <c>Throw</c>, <c>ThrowNew</c>, <c>ExceptionOccurred</c>,
	/// <c>ExceptionDescribe</c>, <c>ExceptionClear</c> and <c>FatalError</c> functions.
	/// </summary>
	public readonly ErrorFunctionSet ErrorFunctions;
	/// <summary>
	/// Pointers to <c>PushLocalFrame</c>, <c>PopLocalFrame</c>, <c>NewGlobalRef</c>,
	/// <c>DeleteGlobalRef</c>, <c>DeleteLocalRef</c>, <c>IsSameObject</c>,
	/// <c>NewLocalRef</c> and<c>EnsureLocalCapacity</c> functions.
	/// </summary>
	public readonly ReferenceFunctionSet ReferenceFunctions;
	/// <summary>
	/// Pointers to <c>AllocObject</c>, <c>NewObject</c>, <c>GetObjectClass</c>
	/// and <c>IsInstanceOf</c> functions.
	/// </summary>
	public readonly ObjectFunctionSet ObjectFunctions;
	/// <summary>
	/// Pointers to <c>GetMethodID</c>, <c>Call&lt;type&gt;Method</c> and <c>CallNonvirtual&lt;type&gt;Method</c> functions.
	/// </summary>
	public readonly InstanceMethodFunctionSet InstanceMethodFunctions;
	/// <summary>
	/// Pointers to <c>GetFieldID</c>, <c>Get&lt;type&gt;Field</c> and <c>Set&lt;type&gt;Field</c> functions.
	/// </summary>
	public readonly FieldFunctionSet<JObjectLocalRef> InstanceFieldFunctions;
	/// <summary>
	/// Pointers to <c>GetStaticMethodID</c> and <c>CallStatic&lt;type&gt;Method</c> functions.
	/// </summary>
	public readonly MethodFunctionSet<JClassLocalRef> StaticMethodFunctions;
	/// <summary>
	/// Pointers to <c>GetStaticFieldID</c>, <c>GetStatic&lt;type&gt;Field</c> and <c>SetStatic&lt;type&gt;Field</c> functions.
	/// </summary>
	public readonly FieldFunctionSet<JClassLocalRef> StaticFieldFunctions;
	/// <summary>
	/// Pointers to Java string functions.
	/// </summary>
	public readonly StringFunctionSet StringFunctions;
	/// <summary>
	/// Pointers to Java array functions.
	/// </summary>
	public readonly ArrayFunctionSet ArrayFunctions;
	/// <summary>
	/// Pointers to <c>RegisterNatives</c> and <c>UnregisterNatives</c> functions.
	/// </summary>
	public readonly NativeRegistryFunctionSet NativeRegistryFunctions;
	/// <summary>
	/// Pointers to <c>MonitorEnter</c> and <c>MonitorExit</c> functions.
	/// </summary>
	public readonly MonitorFunctionSet MonitorFunctions;
	/// <summary>
	/// Pointer to <c>GetJavaVM</c> function.
	/// Returns the Java VM interface (used in the Invocation API) associated with the current thread.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, out JVirtualMachineRef, JResult> GetVirtualMachine;
	/// <summary>
	/// Pointers to <c>GetStringRegion</c> and <c>GetStringUTFRegion</c> functions.
	/// </summary>
	public readonly StringRegionFunctionSet StringRegionFunctions;
	/// <summary>
	/// Pointers to <c>GetPrimitiveArrayCritical</c> and <c>ReleasePrimitiveArrayCritical</c> functions.
	/// </summary>
	public readonly PrimitiveArrayCriticalFunctionSet PrimitiveArrayCriticalFunctions;
	/// <summary>
	/// Pointers to <c>GetStringCritical</c> and <c>ReleaseStringCritical</c> functions.
	/// </summary>
	public readonly StringCriticalFunctionSet StringCriticalFunctions;
	/// <summary>
	/// Pointers to <c>NewWeakGlobalRef</c> and <c>DeleteWeakGlobalRef</c> functions.
	/// </summary>
	public readonly WeakReferenceFunctionSet WeakGlobalFunctions;
	/// <summary>
	/// Pointer to <c>ExceptionCheck</c> function.
	/// Checks for pending exceptions without creating a local reference to the exception object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JBoolean> ExceptionCheck;
}