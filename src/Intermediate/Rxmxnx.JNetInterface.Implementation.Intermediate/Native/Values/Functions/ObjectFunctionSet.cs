namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct ObjectFunctionSet
{
	/// <summary>
	/// Pointer to <c>AllocObject</c> function.
	/// Allocates a new Java object without invoking any of the constructors for the object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JObjectLocalRef> AllocObject;
	/// <summary>
	/// Pointers to <c>NewObject</c> functions.
	/// Constructs a new Java object.
	/// </summary>
	public readonly CallGenericFunction<JClassLocalRef, JObjectLocalRef> NewObject;

	/// <summary>
	/// Pointer to <c>GetObjectClass</c> function.
	/// Returns the class of an object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef> GetObjectClass;
	/// <summary>
	/// Pointer to <c>IsInstanceOf</c> function.
	/// Tests whether an object is an instance of a class.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JBoolean> IsInstanceOf;
}