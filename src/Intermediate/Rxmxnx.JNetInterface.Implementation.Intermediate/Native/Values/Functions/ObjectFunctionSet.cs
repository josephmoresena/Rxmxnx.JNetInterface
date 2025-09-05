namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe partial struct ObjectFunctionSet
{
	/// <summary>
	/// Pointer to <c>AllocObject</c> function.
	/// Allocates a new Java object without invoking any of the constructors for the object.
	/// </summary>
	private readonly AllocObjectPtr _allocObject;
	/// <summary>
	/// Pointers to <c>NewObject</c> functions.
	/// Constructs a new Java object.
	/// </summary>
	public readonly CallGenericFunction<JClassLocalRef, JObjectLocalRef> NewObject;

	/// <summary>
	/// Pointer to <c>GetObjectClass</c> function.
	/// Returns the class of an object.
	/// </summary>
	private readonly GetObjectClassPtr _getObjectClass;
	/// <summary>
	/// Pointer to <c>IsInstanceOf</c> function.
	/// Tests whether an object is an instance of a class.
	/// </summary>
	private readonly IsInstanceOfPtr _instanceOf;

	/// <summary>
	/// <c>AllocObject</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef AllocObject(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> OperatingSystem.IsWindows() ?
			this._allocObject.Windows(envRef, classRef) :
			this._allocObject.Unix(envRef, classRef);
	/// <summary>
	/// <c>GetObjectClass</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JClassLocalRef GetObjectClass(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> OperatingSystem.IsWindows() ?
			this._getObjectClass.Windows(envRef, localRef) :
			this._getObjectClass.Unix(envRef, localRef);
	/// <summary>
	/// <c>IsInstanceOf</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean IsInstanceOf(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef)
		=> OperatingSystem.IsWindows() ?
			this._instanceOf.Windows(envRef, localRef, classRef) :
			this._instanceOf.Unix(envRef, localRef, classRef);
}