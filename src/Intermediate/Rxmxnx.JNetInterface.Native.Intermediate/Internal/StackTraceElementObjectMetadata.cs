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
	public JStackTraceInfo? Information { get; init; }

	/// <inheritdoc/>
	internal StackTraceElementObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is StackTraceElementObjectMetadata traceElementMetadata)
			this.Information = traceElementMetadata.Information;
	}

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="StackTraceElementObjectMetadata"/> to
	/// <see cref="JStackTraceInfo"/>.
	/// </summary>
	/// <param name="metadata">A <see cref="StackTraceElementObjectMetadata"/> to implicitly convert.</param>
	public static implicit operator JStackTraceInfo?(StackTraceElementObjectMetadata? metadata)
		=> metadata?.Information;
}