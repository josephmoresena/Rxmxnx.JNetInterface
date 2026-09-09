namespace Rxmxnx.JNetInterface.Functional;

/// <summary>
/// Represents an abstraction for configuring call arguments into a parameter slot within a specific context.
/// </summary>
public interface ICallArgument
{
	/// <summary>
	/// Configures the argument values into the specified slot.
	/// </summary>
	/// <typeparam name="TSlot">The <see cref="IParameterSlot"/> type of the slot to configure.</typeparam>
	/// <param name="slot">The call parameter slot to configure.</param>
	/// <param name="callDefinition">The JNI call definition.</param>
	void Configure<TSlot>(TSlot slot, JCallDefinition callDefinition) where TSlot : IParameterSlot;
	/// <inheritdoc cref="Object.ToString()"/>
	/// <remarks>Use this method for trace.</remarks>
	protected internal String? ToTraceText() => this.ToString();
}