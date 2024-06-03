namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JExecutableObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public sealed record ExecutableObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Execution definition.
	/// </summary>
	public JCallDefinition? Definition { get; internal init; }
	/// <summary>
	/// JNI declaring class hash.
	/// </summary>
	public String? ClassHash { get; internal init; }
	/// <summary>
	/// JNI method id.
	/// </summary>
	public JMethodId? MethodId { get; internal init; }

	/// <inheritdoc/>
	public ExecutableObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is not ExecutableObjectMetadata executableMetadata) return;
		this.Definition = executableMetadata.Definition;
		this.ClassHash = executableMetadata.ClassHash;
		this.MethodId = executableMetadata.MethodId;
	}

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	private ExecutableObjectMetadata(ExecutableObjectMetadata executableMetadata) : base(executableMetadata)
	{
		this.Definition = executableMetadata.Definition;
		this.ClassHash = executableMetadata.ClassHash;
		this.MethodId = executableMetadata.MethodId;
	}
}