namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
public abstract partial record JPrimitiveMetadata : JDataTypeMetadata
{
	/// <summary>
	/// Size of current primitive type in bytes.
	/// </summary>
	private readonly Int32 _valueSize;
	
	/// <summary>
	/// Size of current primitive type in bytes.
	/// </summary>
	public override Int32 SizeOf => this._valueSize;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type of <see cref="IDataType"/>.</param>
	/// <param name="valueSize">Size of current primitive type in bytes.</param>
	internal JPrimitiveMetadata(Type type, Int32 valueSize) : base(type)
	{
		this._valueSize = valueSize;
	}
}

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="IPrimitive"/>.</typeparam>
internal sealed record JPrimitiveMetadata<TPrimitive>() : 
	JPrimitiveMetadata(typeof(TPrimitive), NativeUtilities.SizeOf<TPrimitive>()) 
	where TPrimitive : unmanaged, IPrimitive
{
	/// <inheritdoc/>
	public override CString ClassName => TPrimitive.ClassName;
	/// <inheritdoc/>
	public override CString Signature => TPrimitive.Signature;
	/// <inheritdoc/>
	public override CString ClassSignature => TPrimitive.ClassSignature;
	/// <inheritdoc/>
	public override CString ArraySignature => TPrimitive.ArraySignature;
}