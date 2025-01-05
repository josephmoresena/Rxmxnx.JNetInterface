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

	/// <summary>
	/// Creates hash from given parameters.
	/// </summary>
	/// <param name="className">JNI name of the current type.</param>
	/// <param name="signature">JNI signature for the current type.</param>
	/// <returns>A <see cref="CStringSequence"/> containing JNI information.</returns>
	internal static CStringSequence CreateInformationSequence(ReadOnlySpan<Byte> className,
		ReadOnlySpan<Byte> signature = default)
		=> NativeUtilities.WithSafeFixed(className, signature, JDataTypeMetadata.CreateInformationSequence);
}