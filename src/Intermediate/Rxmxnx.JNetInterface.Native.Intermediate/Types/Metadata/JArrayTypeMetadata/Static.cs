namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial record JArrayTypeMetadata
{
	/// <summary>
	/// <see cref="MethodInfo"/> of array metadata.
	/// </summary>
	private static readonly MethodInfo? getArrayArrayMetadataInfo;
	/// <summary>
	/// Metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, JArrayTypeMetadata> metadataCache = new();

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
		=> JArrayTypeMetadata.metadataCache.TryGetValue(elementSignature.ToHexString(),
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
		try
		{
			if (JArrayTypeMetadata.getArrayArrayMetadataInfo is null) return default;
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