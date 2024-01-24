namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for an array <see cref="IDataType"/> type.
/// </summary>
public abstract record JArrayTypeMetadata : JReferenceTypeMetadata
{
	/// <summary>
	/// <see cref="MethodInfo"/> of array metadata.
	/// </summary>
	private static readonly MethodInfo? getArrayArrayMetadataInfo;
	/// <summary>
	/// Metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, JArrayTypeMetadata> arrayMetadatas = new();

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
	/// Static constructor.
	/// </summary>
	static JArrayTypeMetadata()
	{
		try
		{
			Type typeofT = typeof(IArrayType);
			JArrayTypeMetadata.getArrayArrayMetadataInfo =
				typeofT.GetMethod(nameof(IArrayType.GetArrayArrayMetadata),
				                  BindingFlags.NonPublic | BindingFlags.Static);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(
				$"Unable to create {nameof(MethodInfo)} instance of {nameof(IArrayType)}.{nameof(IArrayType.GetArrayArrayMetadata)}<>(). {ex.Message}");
		}
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="signature">JNI signature for current array type.</param>
	/// <param name="deep">Array deep.</param>
	internal JArrayTypeMetadata(ReadOnlySpan<Byte> signature, Int32 deep) : base(signature, signature)
	{
		this.Deep = deep;
		JArrayTypeMetadata.arrayMetadatas.TryAdd(this.Signature.ToHexString(), this);
	}
	/// <summary>
	/// Sets the object element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <param name="value">Object instance.</param>
	internal abstract void SetObjectElement(JArrayObject jArray, Int32 index, JLocalObject? value);

	/// <summary>
	/// Retrieves metadata for the array of arrays of <paramref name="typeofElement"/>.
	/// </summary>
	/// <param name="typeofElement">Type of array element.</param>
	/// <param name="elementSignature">Element signature.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> for the array of arrays of <paramref name="typeofElement"/>.</returns>
	[UnconditionalSuppressMessage(
		"AOT",
		"IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.",
		Justification = "Alternatives to avoid reflection use.")]
	protected static JArrayTypeMetadata? GetArrayArrayMetadata(CString elementSignature, Type typeofElement)
		=> JArrayTypeMetadata.arrayMetadatas.TryGetValue(elementSignature.ToHexString(),
		                                                 out JArrayTypeMetadata? result) ?
			result :
			JArrayTypeMetadata.GetArrayArrayMetadataWithReflection(elementSignature, typeofElement);
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

	/// <summary>
	/// Retrieves metadata for the array of arrays of <paramref name="typeofElement"/>.
	/// </summary>
	/// <param name="typeofElement">Type of array element.</param>
	/// <param name="elementSignature">Element signature.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> for the array of arrays of <paramref name="typeofElement"/>.</returns>
	[RequiresDynamicCode("Calls System.Reflection.MethodInfo.MakeGenericMethod(params Type[])")]
	private static JArrayTypeMetadata? GetArrayArrayMetadataWithReflection(CString elementSignature, Type typeofElement)
	{
		if (JArrayTypeMetadata.getArrayArrayMetadataInfo is null) return default;
		try
		{
			MethodInfo getGenericArrayArrayMetadataInfo =
				JArrayTypeMetadata.getArrayArrayMetadataInfo.MakeGenericMethod(typeofElement);
			Func<JArrayTypeMetadata> getGenericArrayArrayMetadata =
				getGenericArrayArrayMetadataInfo.CreateDelegate<Func<JArrayTypeMetadata>>();
			return getGenericArrayArrayMetadata();
		}
		catch (Exception ex)
		{
			Debug.WriteLine(
				$"Unable to create {nameof(JArrayTypeMetadata)} instance of [[{elementSignature}. {ex.Message}");
			return default;
		}
	}
}