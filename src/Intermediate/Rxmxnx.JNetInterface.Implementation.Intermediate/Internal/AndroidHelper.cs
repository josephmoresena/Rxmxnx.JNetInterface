namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Helper for android.
/// </summary>
internal static unsafe class AndroidHelper
{
	/// <summary>
	/// Property value maximum length.
	/// </summary>
	private const Int32 PropValueMaxLength = 92;

	/// <summary>
	/// Android API Level.
	/// </summary>
	public static readonly Int32? ApiLevel;

	/// <summary>
	/// Static constructor.
	/// </summary>
	static AndroidHelper()
	{
		AndroidHelper.ApiLevel = default;
		if (!SystemInfo.IsLinux || NativeUtilities.LoadNativeLib("libc") is not { } libcH) return;
		ReadOnlySpan<Byte> sdkVersionPropName = "ro.build.version.sdk"u8;
		if (NativeLibrary.TryGetExport(libcH, "__system_property_get", out IntPtr propertyGetPtr))
		{
			delegate* unmanaged<Byte*, Byte*, Int32> propertyGet =
				(delegate* unmanaged<Byte*, Byte*, Int32>)propertyGetPtr;
			Span<Byte> propertyValue = stackalloc Byte[AndroidHelper.PropValueMaxLength];
			Int32 propertyLength = 0;
			fixed (Byte* namePtr = &MemoryMarshal.GetReference(sdkVersionPropName))
			fixed (Byte* valuePtr = &MemoryMarshal.GetReference(propertyValue))
				propertyLength = propertyGet(namePtr, valuePtr);
			if (propertyLength <= 0) return;
			AndroidHelper.SetApiLevelValue(propertyValue[..propertyLength]);
		}
		else if (NativeLibrary.TryGetExport(libcH, "__system_property_find", out IntPtr propertyFindPtr))
		{
			delegate* unmanaged<Byte*, void*> propertyFind = (delegate* unmanaged<Byte*, void*>)propertyFindPtr;
			void* propInfo;
			fixed (Byte* namePtr = &MemoryMarshal.GetReference(sdkVersionPropName))
				propInfo = propertyFind(namePtr);
			if (propInfo == default ||
			    !NativeLibrary.TryGetExport(libcH, "__system_property_read_callback", out IntPtr propertyReadPtr))
				return;

			delegate* unmanaged<void*, delegate* unmanaged<void*, Byte*, Byte*, UInt32, void>, void*, void>
				propertyRead =
					(delegate* unmanaged<void*, delegate* unmanaged<void*, Byte*, Byte*, UInt32, void>, void*, void>)
					propertyReadPtr;
			propertyRead(propInfo, &ReadSdkVersionProp, default);
		}
		return;
		[UnmanagedCallersOnly]
		static void ReadSdkVersionProp(void* cookie, Byte* name, Byte* value, UInt32 serial)
		{
			ReadOnlySpan<Byte> propValue = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(value);
			AndroidHelper.SetApiLevelValue(propValue);
		}
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
		if (!Int32.TryParse(chars, out Int32 apiLevel)) return;
#else
		if (Int32.TryParse(sdkVersionValue, out Int32 apiLevel)) return;
#endif
		Unsafe.AsRef(in AndroidHelper.ApiLevel) = apiLevel;
	}
}