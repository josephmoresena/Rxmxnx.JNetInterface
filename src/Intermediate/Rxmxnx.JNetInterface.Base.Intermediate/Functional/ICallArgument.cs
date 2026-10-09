namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents an abstraction for configuring call arguments into a parameter slot within a specific context.
/// </summary>
public interface ICallArgument
{
	/// <summary>
	/// Internal instance.
	/// </summary>
#pragma warning disable CA2211
	public static Empty Default = default;
#pragma warning restore CA2211

	/// <summary>
	/// Configures the argument values into the specified slot.
	/// </summary>
	/// <typeparam name="TSlot">The <see cref="IParameterSlot"/> type of the slot to configure.</typeparam>
	/// <param name="slot">The call parameter slot to configure.</param>
	/// <param name="callDefinition">The JNI call definition.</param>
	void Configure<TSlot>(TSlot slot, JCallDefinition callDefinition) where TSlot : IParameterSlot;

	/// <inheritdoc cref="Object.ToString()"/>
	/// <remarks>Use this method for trace.</remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected internal String? ToTraceText() => this.ToString();

	/// <summary>
	/// Default empty argument.
	/// </summary>
	public readonly struct Empty : ICallArgument
	{
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Configure<TSlot>(TSlot slot, JCallDefinition callDefinition) where TSlot : IParameterSlot
		{
			// NONE
		}
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public String ToTraceText() => String.Empty;
	}
}