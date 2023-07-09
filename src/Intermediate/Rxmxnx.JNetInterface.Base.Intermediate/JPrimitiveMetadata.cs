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
	/// JNI signature for the primitive type wrapper.
	/// </summary>
	public abstract CString ClassSignature { get; }
	/// <summary>
	/// Size of current primitive type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }
}

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="IPrimitive"/>.</typeparam>
internal sealed record JPrimitiveMetadata<TPrimitive> : JPrimitiveMetadata where TPrimitive : unmanaged, IPrimitive
{
	/// <inheritdoc/>
	public override CString ArraySignature => TPrimitive.ArraySignature;
	/// <inheritdoc/>
	public override CString ClassSignature => TPrimitive.ClassSignature;
	/// <inheritdoc/>
	public override Int32 SizeOf => NativeUtilities.SizeOf<TPrimitive>();
}