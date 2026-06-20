namespace Rxmxnx.JNetInterface;

public partial interface IVirtualMachine
{
	/// <summary>
	/// Minimum virtual machine version required for any JNI thread.
	/// </summary>
	public const Int32 MinimalVersion = (Int32)JRuntimeVersion.J6;

	/// <summary>
	/// Indicates whether metadata validation is enabled.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean MetadataValidationEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableMetadataValidation", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether detailed a ToString() is available for type metadata instances.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean TypeMetadataToStringEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableTypeMetadataToString", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether metadata for jagged arrays is auto-generated.
	/// </summary>
	/// <remarks>In reflection-free mode this feature is unavailable.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean JaggedArrayAutoGenerationEnabled => true;
}