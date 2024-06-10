namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial class JDataTypeMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="information">Internal sequence information.</param>
	private protected JDataTypeMetadata(CStringSequence information)
	{
		this._sequence = information;
		this._className = this._sequence[0];
		this._signature = this._sequence[1];
		this._arraySignature = this._sequence[2];
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="className">JNI name of the current type.</param>
	/// <param name="signature">JNI signature for the current type.</param>
	private protected JDataTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature = default)
	{
		this._sequence = JDataTypeMetadata.CreateInformationSequence(className, signature);
		this._className = this._sequence[0];
		this._signature = this._sequence[1];
		this._arraySignature = this._sequence[2];
	}
}