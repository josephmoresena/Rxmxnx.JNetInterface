namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Standard Runtime Feature flags
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
internal static class JavaStandardFeature
{
	/// <summary>
	/// Fixed runtime version.
	/// </summary>
	private static Int32 MaxRuntimeVersion => Int32.MaxValue;

	/// <summary>
	/// Indicates whether the current execution has a fixed runtime version.
	/// </summary>
	public static Boolean IsFixedRuntimeVersion => false;

	/// <summary>
	/// Retrieves the valid runtime version.
	/// </summary>
	/// <returns>The valid runtime version.</returns>
	public static JRuntimeVersion? GetRuntimeVersion()
	{
		if (JavaStandardFeature.MaxRuntimeVersion == Int32.MaxValue) return default;
		return (JRuntimeVersion)JavaStandardFeature.MaxRuntimeVersion;
	}
	/// <summary>
	/// Retrieves the valid JNI version.
	/// </summary>
	/// <returns>The valid JNI version.</returns>
	public static Int32? GetInterfaceVersion() => JavaStandardFeature.GetRuntimeVersion()?.GetInterfaceVersion();
}