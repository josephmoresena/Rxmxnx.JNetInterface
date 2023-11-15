namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JStackTraceElementObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal sealed record JStackTraceElementObjectMetadata : JObjectMetadata
{
	/// <summary>
	/// Stack trace information.
	/// </summary>
	public JStackTraceInfo? Information { get; init; }

	/// <inheritdoc/>
	internal JStackTraceElementObjectMetadata(JObjectMetadata metadata) : base(metadata)
	{
		if (metadata is JStackTraceElementObjectMetadata traceElementMetadata)
			this.Information = traceElementMetadata.Information;
	}

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JStackTraceElementObjectMetadata"/> to
	/// <see cref="JStackTraceInfo"/>.
	/// </summary>
	/// <param name="metadata">A <see cref="JStackTraceElementObjectMetadata"/> to implicity convert.</param>
	public static implicit operator JStackTraceInfo?(JStackTraceElementObjectMetadata? metadata)
		=> metadata?.Information;
}