namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNINativeInterface_</c> struct. Contains all pointers to the functions of JNI.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2292,
                 Justification = CommonConstants.BinaryStructJustification)]
#endif
internal readonly partial struct JNativeInterface : INativeType
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JNativeInterface;

	/// <summary>
	/// Pointer to <c>GetVersion</c> function. Retrieves the version of the <c>JNIEnv</c> interface.
	/// </summary>
	public IntPtr GetVersionPointer
	{
		get => this._getVersionPointer;
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		init => this._getVersionPointer = value;
	}
	/// <summary>
	/// Pointer to JNI function.
	/// </summary>
	public IntPtr this[Int32 index]
	{
		get => this._operations[index];
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		init => this._operations[index] = value;
	}
}