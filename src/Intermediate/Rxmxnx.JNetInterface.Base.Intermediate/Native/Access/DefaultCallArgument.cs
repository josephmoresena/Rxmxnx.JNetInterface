namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// Default empty argument.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public readonly struct DefaultCallArgument : ICallArgument
{
	/// <summary>
	/// Internal instance.
	/// </summary>
	internal static DefaultCallArgument Instance;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot
	{
		// NONE
	}
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public String ToTraceText() => String.Empty;
}