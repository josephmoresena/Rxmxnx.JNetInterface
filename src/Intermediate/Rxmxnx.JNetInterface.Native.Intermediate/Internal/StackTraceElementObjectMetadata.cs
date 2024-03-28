namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stores the metadata of a <see cref="JStackTraceElementObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal sealed record StackTraceElementObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Stack trace information.
	/// </summary>
	public StackTraceInfo? Information { get; init; }

	/// <inheritdoc/>
	internal StackTraceElementObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is StackTraceElementObjectMetadata traceElementMetadata)
			this.Information = traceElementMetadata.Information;
	}

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="StackTraceElementObjectMetadata"/> to
	/// <see cref="StackTraceInfo"/>.
	/// </summary>
	/// <param name="metadata">A <see cref="StackTraceElementObjectMetadata"/> to implicitly convert.</param>
	[return: NotNullIfNotNull(nameof(metadata))]
	public static implicit operator StackTraceInfo?(StackTraceElementObjectMetadata? metadata) => metadata?.Information;
}