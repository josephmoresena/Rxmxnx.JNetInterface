namespace Rxmxnx.JNetInterface.Reflect;

public partial class JFieldObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JFieldObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private record FieldObjectMetadata : ObjectMetadata
	{
		/// <summary>
		/// Field definition.
		/// </summary>
		public JFieldDefinition? Definition { get; init; }
		/// <summary>
		/// JNI declaring class hash.
		/// </summary>
		public String? ClassHash { get; init; }

		/// <summary>
		/// JNI field id.
		/// </summary>
		internal JFieldId? MethodId { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
		public FieldObjectMetadata(ObjectMetadata metadata) : base(metadata)
		{
			if (metadata is not FieldObjectMetadata executableMetadata) return;
			this.Definition = executableMetadata.Definition;
			this.ClassHash = executableMetadata.ClassHash;
			this.MethodId = executableMetadata.MethodId;
		}
	}
}