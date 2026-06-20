// ReSharper disable ConvertIfStatementToReturnStatement

#if ANDROID
using JniMethodInfo = Rxmxnx.JNetInterface.Internal.JniMethodInfo;
#endif

namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNINativeInterface_</c> struct. Contains all pointers to the functions of JNI.
/// </summary>
/// <remarks>JNI 1.6</remarks>
[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NativeInterface6 : INativeInterface<NativeInterface6>
{
	/// <inheritdoc/>
	public static Int32 RequiredVersion => (Int32)JRuntimeVersion.J6;

#if !ANDROID
	/// <summary>
	/// Function set for Windows Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Windows _windows;
#endif
	/// <summary>
	/// Function set for Unix-like Operating System.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unix _unix;

	/// <summary>
	/// <c>GetObjectRefType</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JReferenceType GetObjectRefType(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._windows.GetObjectRefType(envRef, localRef);
#endif
		return this._unix.GetObjectRefType(envRef, localRef);
	}

	/// <summary>
	/// Information of <see cref="NativeInterface6.GetObjectRefType"/>
	/// </summary>
	public static readonly JniMethodInfo GetObjectRefTypeInfo = new()
	{
		Name = nameof(NativeInterface6.GetObjectRefType), Level = JniSafetyLevels.CriticalSafe,
	};

#if !ANDROID
	/// <summary>
	/// Windows function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Windows
	{
		/// <summary>
		/// Native interface for <c>JNI_VERSION_1_4</c>
		/// </summary>
#pragma warning disable CS0169
		private readonly NativeInterface4 _nativeInterface;
#pragma warning restore CS0169

		/// <summary>
		/// Pointer to <c>GetObjectRefType</c> function.
		/// Retrieves the type of given object reference.
		/// </summary>
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JReferenceType> GetObjectRefType;
	}
#endif

	/// <summary>
	/// Unix function set.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct Unix
	{
		/// <summary>
		/// Native interface for <c>JNI_VERSION_1_4</c>
		/// </summary>
#pragma warning disable CS0169
		private readonly NativeInterface4 _nativeInterface;
#pragma warning restore CS0169

		/// <summary>
		/// Pointer to <c>GetObjectRefType</c> function.
		/// Retrieves the type of given object reference.
		/// </summary>
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JReferenceType> GetObjectRefType;
	}
}