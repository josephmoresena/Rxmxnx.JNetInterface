namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNINativeInterface_</c> struct. Contains all pointers to the functions of JNI.
/// </summary>
/// <remarks>JNI 24</remarks>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeInterface24 : INativeInterface<NativeInterface24>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => (Int32)JRuntimeVersion.J24;

	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>GetStringUTFLengthAsLong</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Int64 GetStringUtfLongLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
		=> OperatingSystem.IsWindows() ?
			this._windows.GetStringUtfLongLength(envRef, stringRef) :
			this._unix.GetStringUtfLongLength(envRef, stringRef);

	/// <summary>
	/// Information of <see cref="GetStringUtfLongLength"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringUtfLongLengthInfo = new()
	{
		Name = nameof(NativeInterface24.GetStringUtfLongLengthInfo), Level = JniSafetyLevels.CriticalSafe,
	};

	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Native interface for <c>JNI_VERSION_19</c>
		/// </summary>
#pragma warning disable CS0169
		private readonly NativeInterface19 _nativeInterface;
#pragma warning restore CS0169
		/// <summary>
		/// Pointer to <c>GetStringUTFLengthAsLong</c> function.
		/// Retrieves the UTF string length as long value.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JStringLocalRef, Int64> GetStringUtfLongLength;
	}

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Native interface for <c>JNI_VERSION_19</c>
		/// </summary>
#pragma warning disable CS0169
		private readonly NativeInterface19 _nativeInterface;
#pragma warning restore CS0169
		/// <summary>
		/// Pointer to <c>GetStringUTFLengthAsLong</c> function.
		/// Retrieves the UTF string length as long value.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JStringLocalRef, Int64> GetStringUtfLongLength;
	}
}