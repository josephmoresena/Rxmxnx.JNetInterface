namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JMethodDefinition
{
	/// <inheritdoc/>
	internal JMethodDefinition(AccessibleInfoSequence info, Int32 callSize, Int32[] sizes, Int32 referenceCount) : base(
		info, callSize, sizes, referenceCount) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	protected JMethodDefinition(ReadOnlySpan<Byte> methodName,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<JArgumentMetadata> metadata
#else
		ReadOnlySpan<JArgumentMetadata> metadata
#endif
	) : base(methodName, metadata) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <remarks>This constructor should never be inherited.</remarks>
	private JMethodDefinition(ReadOnlySpan<Byte> methodName) : base(methodName, []) { }
}