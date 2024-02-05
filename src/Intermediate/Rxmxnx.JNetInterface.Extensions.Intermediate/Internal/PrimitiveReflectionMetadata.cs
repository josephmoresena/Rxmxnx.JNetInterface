namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stores a reflection metadata for <typeparamref name="TPrimitive"/> type.
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType"/> type.</typeparam>
internal sealed class PrimitiveReflectionMetadata<TPrimitive> : IReflectionMetadata
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Instance.
	/// </summary>
	public static readonly PrimitiveReflectionMetadata<TPrimitive> Instance = new();

	/// <summary>
	/// Fake hash.
	/// </summary>
	public static String FakeHash => PrimitiveReflectionMetadata<TPrimitive>.Instance._hash;
	/// <summary>
	/// Fake hash.
	/// </summary>
	private readonly String _hash;

	/// <summary>
	/// Constructor.
	/// </summary>
	private PrimitiveReflectionMetadata()
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		CString className = metadata.ClassName;
		this._hash = JDataTypeMetadata.CreateInformationSequence(className).ToString();
	}

	/// <inheritdoc/>
	public JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TPrimitive>();

	/// <inheritdoc/>
	public JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName, JArgumentMetadata[] metadata)
		=> JFunctionDefinition<TPrimitive>.Create(functionName, metadata);
	/// <inheritdoc/>
	public JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
		=> new JFieldDefinition<TPrimitive>(fieldName);
}