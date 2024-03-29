namespace Rxmxnx.JNetInterface.Reflect;

public partial class JExecutableObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JExecutableObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	protected sealed record ExecutableObjectMetadata : ObjectMetadata
	{
		/// <summary>
		/// Execution definition.
		/// </summary>
		public JCallDefinition? Definition { get; init; }
		/// <summary>
		/// JNI declaring class hash.
		/// </summary>
		public String? ClassHash { get; init; }

		/// <summary>
		/// JNI method id.
		/// </summary>
		internal JMethodId? MethodId { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
		public ExecutableObjectMetadata(ObjectMetadata metadata) : base(metadata)
		{
			if (metadata is not ExecutableObjectMetadata executableMetadata) return;
			this.Definition = executableMetadata.Definition;
			this.ClassHash = executableMetadata.ClassHash;
			this.MethodId = executableMetadata.MethodId;
		}
	}
}