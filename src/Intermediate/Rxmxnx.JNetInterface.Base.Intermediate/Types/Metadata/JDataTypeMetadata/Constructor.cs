namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial class JDataTypeMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="information">Internal sequence information.</param>
	private protected JDataTypeMetadata(TypeInfoSequence information) => this._info = information;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">JNI name of the current type.</param>
	private protected JDataTypeMetadata(ReadOnlySpan<Byte> className) => this._info = new(className);
}