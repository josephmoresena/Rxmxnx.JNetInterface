namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java methods through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct MethodFunctionSet<TReceiver>
	where TReceiver : unmanaged, INativeType<TReceiver>, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointers to <c>GetMethodID</c> function.
	/// Returns the method ID for a method of a class or interface.
	/// </summary>
	/// <remarks>The method is determined by its name and signature.</remarks>
	public readonly GetAccessibleIdFunction<JMethodId> GetMethodId;
	/// <summary>
	/// Pointers to <c>CallObjectMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JObjectLocalRef> CallObjectMethod;
	/// <summary>
	/// Pointers to <c>CallBooleanMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JBoolean> CallBooleanMethod;
	/// <summary>
	/// Pointers to <c>CallByteMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JByte> CallByteMethod;
	/// <summary>
	/// Pointers to <c>CallCharMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JChar> CallCharMethod;
	/// <summary>
	/// Pointers to <c>CallShortMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JShort> CallShortMethod;
	/// <summary>
	/// Pointers to <c>CallIntMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JInt> CallIntMethod;
	/// <summary>
	/// Pointers to <c>CallLongMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JLong> CallLongMethod;
	/// <summary>
	/// Pointers to <c>CallFloatMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JFloat> CallFloatMethod;
	/// <summary>
	/// Pointers to <c>CallDoubleMethod</c> functions.
	/// </summary>
	public readonly CallGenericFunction<TReceiver, JDouble> CallDoubleMethod;
	/// <summary>
	/// Pointers to <c>CallVoidMethod</c> functions.
	/// </summary>
	public readonly CallMethodFunction<TReceiver> CallVoidMethod;
}