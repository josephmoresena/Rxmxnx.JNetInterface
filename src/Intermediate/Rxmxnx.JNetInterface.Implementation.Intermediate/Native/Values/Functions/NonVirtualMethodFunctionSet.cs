namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java non-virtual methods through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct NonVirtualMethodFunctionSet
{
	/// <summary>
	/// Pointers to <c>CallNonvirtualObjectMethod</c> functions.
	/// Calls an <c>Object</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JObjectLocalRef> CallNonVirtualObjectMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualBooleanMethod</c> functions.
	/// Calls a <c>boolean</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JBoolean> CallNonVirtualBooleanMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualByteMethod</c> functions.
	/// Calls a <c>byte</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JByte> CallNonVirtualByteMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualCharMethod</c> functions.
	/// Calls a <c>char</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JChar> CallNonVirtualCharMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualShortMethod</c> functions.
	/// Calls a <c>short</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JShort> CallNonVirtualShortMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualIntMethod</c> functions.
	/// Calls an <c>int</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JInt> CallNonVirtualIntMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualLongMethod</c> functions.
	/// Calls a <c>long</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JLong> CallNonVirtualLongMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualFloatMethod</c> functions.
	/// Calls a <c>float</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JFloat> CallNonVirtualFloatMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualDoubleMethod</c> functions.
	/// Calls a <c>double</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualGenericFunction<JDouble> CallNonVirtualDoubleMethod;
	/// <summary>
	/// Pointers to <c>CallNonvirtualVoidMethod</c> functions.
	/// Calls a <c>void</c> non-virtual method.
	/// </summary>
	public readonly CallNonVirtualMethodFunction CallNonVirtualVoidMethod;
}