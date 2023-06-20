namespace Rxmxnx.JNetInterface.Native;

public partial record JAccessibleObjectDefinition
{
	/// <summary>
	/// Accessible object information.
	/// </summary>
	internal CStringSequence Information => this._sequence;
	/// <summary>
	/// The format used for <see cref="JAccessibleObjectDefinition.ToString()"/> method.
	/// </summary>
	internal abstract String ToStringFormat { get; }

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="sequence">
	/// <see cref="CStringSequence"/> containing the name and descriptor of the method.
	/// </param>
	internal JAccessibleObjectDefinition(CStringSequence sequence) => this._sequence = sequence;
}