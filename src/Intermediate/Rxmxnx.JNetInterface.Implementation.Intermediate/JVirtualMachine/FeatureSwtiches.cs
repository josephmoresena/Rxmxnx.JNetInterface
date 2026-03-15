namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Indicates whether the current execution has a fixed android version.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal static Boolean IsFixedAndroid => false;
	/// <summary>
	/// Indicates whether the current execution has a fixed runtime version.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal static Boolean IsFixedRuntimeVersion => false;
	/// <summary>
	/// Fixed runtime version.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal static JRuntimeVersion FixedRuntimeVersion => default;
	/// <summary>
	/// Maximum Android API level.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	// ReSharper disable once MemberCanBePrivate.Global
	internal static Int32 MaxAndroidApiLevel => Int16.MaxValue;
}