namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial class JDataTypeMetadata
{
	/// <summary>
	/// Internal sequence.
	/// </summary>
	internal TypeInfoSequence Information => this._info;

	/// <summary>
	/// Indicates current instance is valid for current datatype.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if current instance is valid for current type; otherwise, <see langword="false"/>.
	/// </returns>
	internal Boolean IsValidForType(Type type) => this.Type == type;
}