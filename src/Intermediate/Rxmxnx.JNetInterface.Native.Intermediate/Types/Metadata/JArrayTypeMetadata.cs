namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an array <see cref="IDataType"/> type.
/// </summary>
public abstract record JArrayTypeMetadata : JReferenceTypeMetadata
{
	/// <summary>
	/// Metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<CString, JArrayTypeMetadata> arrayMetadatas = new();

	/// <summary>
	/// Element type of current array metadata.
	/// </summary>
	public abstract JDataTypeMetadata ElementMetadata { get; }

	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Array;
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;
	/// <summary>
	/// Array deep.
	/// </summary>
	public Int32 Deep { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">JNI signature for current array type.</param>
	/// <param name="deep">Array deep.</param>
	internal JArrayTypeMetadata(ReadOnlySpan<Byte> signature, Int32 deep) : base(signature, signature)
	{
		this.Deep = deep;
		JArrayTypeMetadata.arrayMetadatas.TryAdd(this.Signature, this);
	}

	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> for <typeparamref name="TElement"/>
	/// </summary>
	/// <typeparam name="TElement">A <see cref="IArrayType{TArrayType}"/> type.</typeparam>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	protected new static JArrayTypeMetadata? GetArrayMetadata<TElement>() where TElement : IObject, IDataType<TElement>
	{
		try
		{
			JArrayTypeMetadata metadata = IArrayType.GetMetadata<JArrayObject<TElement>>();
			return JArrayTypeMetadata.arrayMetadatas.TryGetValue(metadata.ArraySignature,
			                                                     out JArrayTypeMetadata? result) ?
				result :
				IArrayType.GetMetadata<JArrayObject<JArrayObject<TElement>>>();
		}
		catch (Exception)
		{
			return default;
		}
	}
	/// <summary>
	/// Retrieves array deep.
	/// </summary>
	/// <typeparam name="TElement">A <see cref="IArrayType"/> type.</typeparam>
	/// <returns>Array type.</returns>
	protected static Int32 GetArrayDeep<TElement>() where TElement : IObject, IDataType<TElement>
	{
		Int32 i = 1;
		JArrayTypeMetadata? elementArray = IDataType.GetMetadata<TElement>() as JArrayTypeMetadata;
		while (elementArray is not null)
		{
			i++;
			elementArray = elementArray.ElementMetadata as JArrayTypeMetadata;
		}
		return i;
	}
	/// <summary>
	/// Retrieves the CLR type <typeparamref name="TElement"/> array.
	/// </summary>
	/// <typeparam name="TElement">A <see cref="IDataType{TElement}"/> element type.</typeparam>
	/// <returns>Type of <see cref="JArrayObject{TElement}"/></returns>
	protected static Type? GetArrayType<TElement>() where TElement : IObject, IDataType<TElement>
	{
		try
		{
			return typeof(JArrayObject<TElement>);
		}
		catch (Exception)
		{
			return default;
		}
	}
	/// <inheritdoc cref="JReferenceTypeMetadata.ParseInstance(JLocalObject)"/>
	/// <typeparam name="TElement">A <see cref="IDataType{TElement}"/> element type.</typeparam>
	protected static JArrayObject? ParseInstance<TElement>(JLocalObject? jLocal)
		where TElement : IObject, IDataType<TElement>
	{
		try
		{
			return jLocal as JArrayObject<TElement> ?? JArrayObject<TElement>.Create(jLocal);
		}
		catch (Exception)
		{
			return default;
		}
	}
}