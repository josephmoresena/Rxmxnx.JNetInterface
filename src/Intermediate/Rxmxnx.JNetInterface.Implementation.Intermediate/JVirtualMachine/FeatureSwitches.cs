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
	/// <summary>
	/// Indicates whether <see cref="JVoidObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean VoidObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => AppContext.TryGetSwitch("JNetInterface.EnableVoidObjectMainClass", out Boolean enable) && enable;
	}
	/// <summary>
	/// Indicates whether <see cref="JBooleanObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean BooleanObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableBooleanObjectMainClass", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether <see cref="JByteObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean ByteObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableByteObjectMainClass", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether <see cref="JCharacterObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean CharacterObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableCharacterObjectMainClass", out Boolean disable) ||
				!disable;
	}
	/// <summary>
	/// Indicates whether <see cref="JDoubleObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean DoubleObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableDoubleObjectMainClass", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether <see cref="JDoubleObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean FloatObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableFloatObjectMainClass", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether <see cref="JIntegerObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean IntegerObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableIntegerObjectMainClass", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether <see cref="JLongObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean LongObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableLongObjectMainClass", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether <see cref="JShortObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean ShortObjectMainClassEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableShortObjectMainClass", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether primitive classes are main classes.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static Boolean PrimitiveMainClassesEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisablePrimitiveMainClasses", out Boolean disable) || !disable;
	}
}