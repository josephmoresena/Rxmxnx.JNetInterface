namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	/// <summary>
	/// Indicates whether final user-types should be treated as real classes at runtime.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean FinalUserTypeRuntimeEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !ANDROID || PACKAGE
		get => AppContext.TryGetSwitch("JNetInterface.EnableFinalUserTypeRuntime", out Boolean enable) && enable;
#else
		get => false;
#endif
	}
	/// <summary>
	/// Indicates whether metadata for built-in throwable objects should be auto-registered.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static Boolean BuiltInThrowableAutoRegistered => true;
	/// <summary>
	/// Indicates whether metadata for reflection objects should be auto-registered.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static Boolean ReflectionAutoRegistered => true;
	/// <summary>
	/// Indicates whether metadata for NIO objects should be auto-registered.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static Boolean NioAutoRegistered => true;
}