namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents an abstraction for configuring call arguments into a parameter slot within a specific context.
/// </summary>
public interface ICallArgument
{
	/// <summary>
	/// Internal instance.
	/// </summary>
	public static ref DefaultCallArgument Default => ref DefaultCallArgument.Instance;

	/// <summary>
	/// Configures the argument values into the specified slot.
	/// </summary>
	/// <typeparam name="TSlot">The <see cref="IParameterSlot"/> type of the slot to configure.</typeparam>
	/// <param name="slot">The call parameter slot to configure.</param>
	void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot;

	/// <inheritdoc cref="Object.ToString()"/>
	/// <remarks>Use this method for trace.</remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected internal String? ToTraceText() => this.ToString();
}