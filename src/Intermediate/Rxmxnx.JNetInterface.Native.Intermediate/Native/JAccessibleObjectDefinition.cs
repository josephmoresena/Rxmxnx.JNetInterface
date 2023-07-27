namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a <c>java.lang.Class&lt;?&gt;</c> accessible object definition.
/// </summary>
public abstract record JAccessibleObjectDefinition
{
	/// <summary>
	/// Internal <see cref="CStringSequence"/> containing the name and descriptor of accessible object.
	/// </summary>
	private readonly CStringSequence _sequence;

	/// <summary>
	/// Accessible object information.
	/// </summary>
	internal CStringSequence Information => this._sequence;

	/// <summary>
	/// The format used for <see cref="JAccessibleObjectDefinition.ToString()"/> method.
	/// </summary>
	internal abstract String ToStringFormat { get; }

	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="sequence">
	/// <see cref="CStringSequence"/> containing the name and descriptor of the method.
	/// </param>
	internal JAccessibleObjectDefinition(CStringSequence sequence) => this._sequence = sequence;

	/// <inheritdoc/>
	public override String ToString() => String.Format(this.ToStringFormat, this._sequence[0], this._sequence[1]);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._sequence.GetHashCode();

	/// <summary>
	/// Retrieves a valid signature from <paramref name="signature"/>.
	/// </summary>
	/// <param name="signature">A signature to validate.</param>
	/// <returns><paramref name="signature"/> if is a valid signature.</returns>
	protected internal static CString ValidateSignature(CString signature)
	{
		ValidationUtilities.ThrowIfInvalidSignature(signature, false);
		return signature;
	}
}