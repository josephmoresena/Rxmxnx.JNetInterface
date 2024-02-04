namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JArrayObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private sealed record JArrayObjectMetadata : ObjectMetadata
	{
		/// <summary>
		/// Internal array length.
		/// </summary>
		public Int32 Length { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
		public JArrayObjectMetadata(ObjectMetadata metadata) : base(metadata)
		{
			if (metadata is JArrayObjectMetadata arrayMetadata)
				this.Length = arrayMetadata.Length;
		}
	}
}