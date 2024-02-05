namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNINativeInterface_</c> struct. Contains all pointers to the functions of JNI.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2292,
                 Justification = CommonConstants.BinaryStructJustification)]
internal readonly partial struct JNativeInterface : INativeType<JNativeInterface>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JNativeInterface;

	/// <summary>
	/// Pointer to <c>GetVersion</c> function. Retrieves the version of the <c>JNIEnv</c> interface.
	/// </summary>
	public IntPtr GetVersionPointer
	{
		get => this._getVersionPointer;
		init => this._getVersionPointer = value;
	}
	/// <summary>
	/// Pointer to JNI function.
	/// </summary>
	public IntPtr this[Int32 index]
	{
		get => this._operations[index];
		init => this._operations[index] = value;
	}
}