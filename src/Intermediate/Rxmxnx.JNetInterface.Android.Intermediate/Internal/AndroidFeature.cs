namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Android Runtime Feature flags
/// </summary>
internal static class AndroidFeature
{
	/// <summary>
	/// Maximum Android API level.
	/// </summary>
	private static Int32 MaxAndroidApiLevel => Int16.MaxValue;

	/// <summary>
	/// Indicates whether the current execution is fixed to Android runtime.
	/// </summary>
	public static Boolean IsFixedAndroid => false;
	/// <summary>
	/// Retrieves the fixed API level.
	/// </summary>
	public static Int32? ApiLevel
		=> AndroidFeature.MaxAndroidApiLevel < Int16.MaxValue ? AndroidFeature.MaxAndroidApiLevel : null;
}