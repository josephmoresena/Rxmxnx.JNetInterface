namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Indicates whether metadata for built-in throwable objects should be auto-registered.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal static Boolean BuiltInThrowableAutoRegistered => true;
	/// <summary>
	/// Indicates whether metadata for reflection objects should be auto-registered.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal static Boolean ReflectionAutoRegistered => true;
	/// <summary>
	/// Indicates whether metadata for NIO objects should be auto-registered.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal static Boolean NioAutoRegistered => true;
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
}