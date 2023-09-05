namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JArrayObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private sealed record JArrayObjectMetadata : JObjectMetadata
	{
		/// <summary>
		/// Internal array length.
		/// </summary>
		public Int32 Length { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
		public JArrayObjectMetadata(JObjectMetadata metadata) : base(metadata)
		{
			if (metadata is JArrayObjectMetadata arrayMetadata)
				this.Length = arrayMetadata.Length;
		}
	}
}