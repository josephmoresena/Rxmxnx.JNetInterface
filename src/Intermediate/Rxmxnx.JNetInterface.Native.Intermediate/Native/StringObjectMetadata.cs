namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JStringObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public sealed record StringObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Internal string value.
	/// </summary>
	public String? Value { get; internal init; }
	/// <summary>
	/// UTF-16 length.
	/// </summary>
	public Int32? Length { get; internal init; }
	/// <summary>
	/// UTF-8 length.
	/// </summary>
	public Int32? Utf8Length { get; internal init; }

	/// <inheritdoc/>
	public StringObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		StringObjectMetadata? stringMetadata = metadata as StringObjectMetadata;
		this.Value = stringMetadata?.Value!;
		this.Length = stringMetadata?.Length;
		this.Utf8Length = stringMetadata?.Utf8Length;
	}

	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private StringObjectMetadata(StringObjectMetadata stringMetadata) : base(stringMetadata)
	{
		this.Value = stringMetadata.Value;
		this.Length = stringMetadata.Length;
		this.Utf8Length = stringMetadata.Utf8Length;
	}

	/// <inheritdoc/>
	public override String ToTraceText() => $"{base.ToString()} Length: {this.Length}";
}