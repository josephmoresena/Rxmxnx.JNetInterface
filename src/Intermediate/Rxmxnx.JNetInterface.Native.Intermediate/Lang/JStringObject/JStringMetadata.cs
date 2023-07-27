namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private sealed record JStringMetadata : JObjectMetadata
	{
		/// <summary>
		/// Internal string value.
		/// </summary>
		public String Value { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
		public JStringMetadata(JObjectMetadata metadata) : base(metadata) => this.Value = default!;
	}
}