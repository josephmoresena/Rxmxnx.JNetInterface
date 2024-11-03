namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Indicates whether metadata for built-in throwable objects should be auto-registered.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean BuiltInThrowableAutoRegistered
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableBuiltInThrowableAutoRegistration", out Boolean disable) ||
				!disable;
	}
	/// <summary>
	/// Indicates whether metadata for reflection objects should be auto-registered.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean ReflectionAutoRegistered
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableReflectionAutoRegistration", out Boolean disable) ||
				!disable;
	}
	/// <summary>
	/// Indicates whether metadata for NIO objects should be auto-registered.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean NioAutoRegistered
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableNioAutoRegistration", out Boolean disable) || !disable;
	}
}