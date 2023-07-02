namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
public abstract partial record JPrimitiveMetadata
{
	/// <summary>
	/// JNI signature for an array of current primitive type.
	/// </summary>
	public abstract CString ArraySignature { get; }
	/// <summary>
	/// Size of current primitive type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }
	/// <summary>
	/// Managed type of internal value of <see cref="IPrimitive"/>.
	/// </summary>
	internal abstract Type Type { get; }
}

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="IPrimitive"/>.</typeparam>
internal sealed record JPrimitiveMetadata<TPrimitive> : JPrimitiveMetadata where TPrimitive : unmanaged, IPrimitive
{
	/// <inheritdoc cref="JPrimitiveMetadata.Type"/>
	private static readonly Type type = typeof(TPrimitive);
	
	/// <inheritdoc cref="JPrimitiveMetadata.Type"/>
	public Type PrimitiveType => JPrimitiveMetadata<TPrimitive>.type;
	/// <inheritdoc/>
	public override CString ArraySignature => TPrimitive.ArraySignature;
	/// <inheritdoc/>
	public override Int32 SizeOf => NativeUtilities.SizeOf<TPrimitive>();
	/// <inheritdoc/>
	internal override Type Type => JPrimitiveMetadata<TPrimitive>.type;
}