namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JStringObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private sealed record StringObjectMetadata : ObjectMetadata
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
		/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
		public StringObjectMetadata(ObjectMetadata metadata) : base(metadata)
		{
			StringObjectMetadata? stringMetadata = metadata as StringObjectMetadata;
			this.Value = stringMetadata?.Value!;
			this.Length = stringMetadata?.Length;
			this.Utf8Length = stringMetadata?.Utf8Length;
		}
	}
}