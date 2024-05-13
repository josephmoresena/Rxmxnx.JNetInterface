namespace Rxmxnx.JNetInterface.Types.Metadata;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3963,
                 Justification = CommonConstants.ReflectionFreeModeJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3011,
                 Justification = CommonConstants.ReflectionPrivateUseJustification)]
public abstract partial record JArrayTypeMetadata
{
	/// <summary>
	/// Name of <see cref="IArrayType.GetArrayArrayMetadata{TElement}"/> method.
	/// </summary>
	private const String GetArrayArrayMetadataName = nameof(IArrayType.GetArrayArrayMetadata);
	/// <summary>
	/// Flags of <see cref="IArrayType.GetArrayArrayMetadata{TElement}"/> method.
	/// </summary>
	private const BindingFlags GetArrayArrayMetadataFlags = BindingFlags.NonPublic | BindingFlags.Static;

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
	[ExcludeFromCodeCoverage]
	static JArrayTypeMetadata()
	{
		try
		{
			JArrayTypeMetadata.getArrayArrayMetadataInfo = JArrayTypeMetadata.ReflectGetArrayArrayMetadataMethod();
		}
		catch (Exception ex)
		{
			JTrace.ReflectGetArrayArrayMetadataMethodError(ex);
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
			JArrayTypeMetadata.TryGetArrayArrayMetadataWithReflection(elementSignature, typeofElement);
	/// <summary>
	/// Retrieves array deep.
	/// </summary>
	/// <typeparam name="TElement">A <see cref="IArrayType"/> type.</typeparam>
	/// <returns>Array type.</returns>
	protected static Int32 GetArrayDimension<TElement>() where TElement : IObject, IDataType<TElement>
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
	[ExcludeFromCodeCoverage]
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
	/// Indicates whether <paramref name="elementMetadata"/> is final.
	/// </summary>
	/// <param name="elementMetadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="elementMetadata"/> is final; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	protected static Boolean IsFinalElementType(JDataTypeMetadata elementMetadata)
	{
		while (elementMetadata is JArrayTypeMetadata arrayMetadata)
			elementMetadata = arrayMetadata.ElementMetadata;
		return elementMetadata.Modifier == JTypeModifier.Final;
	}

	/// <summary>
	/// Tries to retrieve metadata for the array of arrays of <paramref name="typeofElement"/>.
	/// </summary>
	/// <param name="typeofElement">Type of array element.</param>
	/// <param name="elementSignature">Element signature.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> for the array of arrays of <paramref name="typeofElement"/>.</returns>
	[RequiresDynamicCode("Calls System.Reflection.MethodInfo.MakeGenericMethod(params Type[])")]
	[ExcludeFromCodeCoverage]
	private static JArrayTypeMetadata? TryGetArrayArrayMetadataWithReflection(CString elementSignature,
		Type typeofElement)
	{
		try
		{
			return JArrayTypeMetadata.GetArrayArrayMetadataWithReflection(typeofElement);
		}
		catch (Exception ex)
		{
			JTrace.GetArrayArrayMetadataWithReflectionError(elementSignature, ex);
			return default;
		}
	}
	/// <summary>
	/// Retrieves metadata for the array of arrays of <paramref name="typeofElement"/>.
	/// </summary>
	/// <param name="typeofElement">Type of array element.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> for the array of arrays of <paramref name="typeofElement"/>.</returns>
	[RequiresDynamicCode("Calls System.Reflection.MethodInfo.MakeGenericMethod(params Type[])")]
	private static JArrayTypeMetadata? GetArrayArrayMetadataWithReflection(Type typeofElement)
	{
		if (JArrayTypeMetadata.getArrayArrayMetadataInfo is null) return default;
		MethodInfo getGenericArrayArrayMetadataInfo =
			JArrayTypeMetadata.getArrayArrayMetadataInfo.MakeGenericMethod(typeofElement);
		Func<JArrayTypeMetadata> getGenericArrayArrayMetadata =
			getGenericArrayArrayMetadataInfo.CreateDelegate<Func<JArrayTypeMetadata>>();
		return getGenericArrayArrayMetadata();
	}
	/// <summary>
	/// Retrieves a <see cref="MethodInfo"/> instance for
	/// generic <see cref="IArrayType.GetArrayArrayMetadata{TElement}"/> method.
	/// </summary>
	/// <returns>
	/// A <see cref="MethodInfo"/> for <see cref="IArrayType.GetArrayArrayMetadata{TElement}"/> method.
	/// </returns>
	private static MethodInfo ReflectGetArrayArrayMetadataMethod()
	{
		Type typeofT = typeof(IArrayType);
		return typeofT.GetMethod(JArrayTypeMetadata.GetArrayArrayMetadataName,
		                         JArrayTypeMetadata.GetArrayArrayMetadataFlags)!;
	}
}