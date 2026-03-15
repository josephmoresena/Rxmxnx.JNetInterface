namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Helper for android.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3963)]
#endif
internal static unsafe class AndroidHelper
{
	/// <summary>
	/// Property value maximum length.
	/// </summary>
	private const Int32 PropValueMaxLength = 92;

	/// <summary>
	/// Indicates whether the current process is from Zygote.
	/// </summary>
	public static readonly Boolean IsZygote;
	/// <summary>
	/// Android API Level.
	/// </summary>
	public static readonly Int32? ApiLevel;

	/// <summary>
	/// <c>ro.build.version.sdk</c> property name.
	/// </summary>
	private static ReadOnlySpan<Byte> SdkVersionPropName => "ro.build.version.sdk"u8;

	/// <summary>
	/// Static constructor.
	/// </summary>
	static AndroidHelper()
	{
		AndroidHelper.ApiLevel = default;
		AndroidHelper.IsZygote = false;
		if (!SystemInfo.IsLinux || NativeUtilities.LoadNativeLib("libc") is not { } libcH) return;

		if (NativeLibrary.TryGetExport(libcH, "__system_property_get", out IntPtr getAddr))
			AndroidHelper.ReadApiLevel(getAddr);
		else if (NativeLibrary.TryGetExport(libcH, "__system_property_find", out IntPtr findAddr))
			AndroidHelper.FindAndRead(findAddr, libcH);

		if (!AndroidHelper.ApiLevel.HasValue ||
		    !NativeLibrary.TryGetExport(libcH, "readlink", out IntPtr readLinkAddr)) return;
		AndroidHelper.IsZygote = AndroidHelper.IsForkedFromZygote(readLinkAddr);
	}

	/// <summary>
	/// Sets the value of <see cref="AndroidHelper.ApiLevel"/> from <paramref name="sdkVersionValue"/>.
	/// </summary>
	/// <param name="sdkVersionValue">UTF-8 <c>ro.build.version.sdk</c> value.</param>
	private static void SetApiLevelValue(ReadOnlySpan<Byte> sdkVersionValue)
	{
#if !NET8_0_OR_GREATER
		Span<Char> chars = stackalloc Char[sdkVersionValue.Length];
		_ = Encoding.UTF8.GetChars(sdkVersionValue, chars);
		if (!Int32.TryParse(chars, NumberStyles.Integer, CultureInfo.InvariantCulture, out Int32 apiLevel)) return;
#else
		if (!Int32.TryParse(sdkVersionValue, NumberStyles.Integer, CultureInfo.InvariantCulture,
		                    out Int32 apiLevel)) return;
#endif
		Unsafe.AsRef(in AndroidHelper.ApiLevel) = apiLevel;
	}
	/// <summary>
	/// Uses <c>__system_property_get</c> function to initialize <see cref="AndroidHelper.ApiLevel"/>.
	/// </summary>
	/// <param name="propertyGetAddr"><c>__system_property_get</c> address.</param>
	private static void ReadApiLevel(IntPtr propertyGetAddr)
	{
		delegate* unmanaged<Byte*, Byte*, Int32>
			propertyGet = (delegate* unmanaged<Byte*, Byte*, Int32>)propertyGetAddr;
		Span<Byte> propertyValue = stackalloc Byte[AndroidHelper.PropValueMaxLength];
		Int32 propertyLength;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(AndroidHelper.SdkVersionPropName))
		fixed (Byte* valuePtr = &MemoryMarshal.GetReference(propertyValue))
			propertyLength = propertyGet(namePtr, valuePtr);
		if (propertyLength <= 0) return;
		AndroidHelper.SetApiLevelValue(propertyValue[..propertyLength]);
	}
	/// <summary>
	/// Uses <c>__system_property_read_callback</c> function to initialize <see cref="AndroidHelper.ApiLevel"/>.
	/// </summary>
	/// <param name="propertyFindAddr"><c>__system_property_find</c> address.</param>
	/// <param name="libcH"><c>libc.so</c> handle.</param>
	private static void FindAndRead(IntPtr propertyFindAddr, IntPtr libcH)
	{
		delegate* unmanaged<Byte*, void*> propertyFind = (delegate* unmanaged<Byte*, void*>)propertyFindAddr;
		void* propInfo;
		fixed (Byte* namePtr = &MemoryMarshal.GetReference(AndroidHelper.SdkVersionPropName))
			propInfo = propertyFind(namePtr);
		if (propInfo == default ||
		    !NativeLibrary.TryGetExport(libcH, "__system_property_read_callback", out IntPtr propertyReadPtr)) return;
		delegate* unmanaged<void*, delegate* unmanaged<void*, Byte*, Byte*, UInt32, void>, void*, void> propertyRead =
			(delegate* unmanaged<void*, delegate* unmanaged<void*, Byte*, Byte*, UInt32, void>, void*, void >)
			propertyReadPtr;
		propertyRead(propInfo, &ReadSdkVersionProp, default);
		return;
		[UnmanagedCallersOnly]
		static void ReadSdkVersionProp(void* cookie, Byte* name, Byte* value, UInt32 serial)
		{
			ReadOnlySpan<Byte> propValue = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(value);
			AndroidHelper.SetApiLevelValue(propValue);
		}
	}
	/// <summary>
	/// Uses <c>readlink</c> function to determinate if the current process is forked from Zygote.
	/// </summary>
	/// <param name="readlinkAddr"><c>readlink</c> address.</param>
	/// <returns>
	/// <see langword="true"/> if current process is forked from Zygote; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean IsForkedFromZygote(IntPtr readlinkAddr)
	{
		Span<Byte> exeLinkContent = stackalloc Byte[AndroidHelper.PropValueMaxLength];
		Int32 exeLinkContentLength;
		delegate* unmanaged<Byte*, Byte*, UIntPtr, IntPtr> readlink =
			(delegate* unmanaged<Byte*, Byte*, UIntPtr, IntPtr>)readlinkAddr;
		fixed (Byte* exeLinkPathPtr = &MemoryMarshal.GetReference("/proc/self/exe"u8))
		fixed (Byte* exeLinkContentPtr = &MemoryMarshal.GetReference(exeLinkContent))
			exeLinkContentLength = (Int32)readlink(exeLinkPathPtr, exeLinkContentPtr, (UIntPtr)exeLinkContent.Length);
		return exeLinkContentLength > 0 && exeLinkContent.IndexOf("/system/bin/app_process"u8) == 0;
	}
}