namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JStringObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private sealed record JStringObjectMetadata : JObjectMetadata
	{
		/// <summary>
		/// Internal string value.
		/// </summary>
		public String? Value { get; init; }
		/// <summary>
		/// UTF-16 length.
		/// </summary>
		public Int32? Length { get; init; }
		/// <summary>
		/// UTF-8 length.
		/// </summary>
		public Int32? Utf8Length { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
		public JStringObjectMetadata(JObjectMetadata metadata) : base(metadata)
		{
			JStringObjectMetadata? stringMetadata = metadata as JStringObjectMetadata;
			this.Value = stringMetadata?.Value!;
			this.Length = stringMetadata?.Length;
			this.Utf8Length = stringMetadata?.Utf8Length;
		}
	}
}