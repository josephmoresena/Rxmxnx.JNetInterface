namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utilities set for metadata ToString() methods.
/// </summary>
internal static class MetadataTextUtilities
{
	public const String OpenArray = "[ ";
	public const String CloseArray = " ]";
	public const String OpenObject = "{ ";
	public const String CloseObject = " }";
	public const String Separator = ", ";
	public const String EqualsText = " = ";

	public static String GetString(JDataTypeMetadata typeMetadata, params IAppendableProperty?[] properties)
	{
		StringBuilder strBuild = new();
		Boolean isVoid = typeMetadata.ClassName.AsSpan().SequenceEqual(JPrimitiveTypeMetadata.VoidMetadata.ClassName);

		strBuild.Append(MetadataTextUtilities.OpenObject);

		MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.ClassName),
		                                     $"{typeMetadata.ClassName}");
		strBuild.Append(MetadataTextUtilities.Separator);
		MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.Type), $"{typeMetadata.Type}");
		strBuild.Append(MetadataTextUtilities.Separator);
		MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.Kind), $"{typeMetadata.Kind}");
		strBuild.Append(MetadataTextUtilities.Separator);
		if (!isVoid)
		{
			MetadataTextUtilities.AppendProperty(strBuild,
			                                     typeMetadata.ArgumentMetadata.ToAppendableProperty(
				                                     nameof(JDataTypeMetadata.ArgumentMetadata)));
			strBuild.Append(MetadataTextUtilities.Separator);
			MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.ArraySignature),
			                                     $"{typeMetadata.ArraySignature}");
			strBuild.Append(MetadataTextUtilities.Separator);
		}
		switch (typeMetadata)
		{
			case JPrimitiveTypeMetadata primitiveTypeMetadata:
				MetadataTextUtilities.AppendProperty(strBuild, nameof(JPrimitiveTypeMetadata.UnderlineType),
				                                     $"{primitiveTypeMetadata.UnderlineType}");
				strBuild.Append(MetadataTextUtilities.Separator);
				if (!isVoid)
				{
					MetadataTextUtilities.AppendProperty(strBuild, nameof(JPrimitiveTypeMetadata.NativeType),
					                                     primitiveTypeMetadata.NativeType.GetTypeName());
					strBuild.Append(MetadataTextUtilities.Separator);
				}
				MetadataTextUtilities.AppendProperty(strBuild, nameof(JPrimitiveTypeMetadata.WrapperClassName),
				                                     $"{primitiveTypeMetadata.WrapperClassName}");
				strBuild.Append(MetadataTextUtilities.Separator);
				if (!isVoid)
				{
					MetadataTextUtilities.AppendProperty(strBuild, nameof(JPrimitiveTypeMetadata.UnderlineType),
					                                     $"{primitiveTypeMetadata.UnderlineType}");
					strBuild.Append(MetadataTextUtilities.Separator);
				}
				break;
			default:
				if (typeMetadata.Kind is JTypeKind.Class or JTypeKind.Array)
				{
					MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.Modifier),
					                                     $"{typeMetadata.Modifier}");
					strBuild.Append(MetadataTextUtilities.Separator);
				}
				for (Int32 i = 0; i < properties.Length; i++)
				{
					MetadataTextUtilities.AppendProperty(strBuild, properties[i]);
					strBuild.Append(MetadataTextUtilities.Separator);
				}
				break;
		}

		MetadataTextUtilities.AppendHash(strBuild, typeMetadata.Hash);

		strBuild.Append(MetadataTextUtilities.CloseObject);

		return strBuild.ToString();
	}

	private static void AppendProperty(StringBuilder strBuild, String propertyName, String value)
	{
		strBuild.Append(propertyName);
		strBuild.Append(MetadataTextUtilities.EqualsText);
		strBuild.Append(value);
	}
	private static void AppendProperty(StringBuilder strBuild, IAppendableProperty? property)
	{
		if (property is null) return;
		strBuild.Append(property.PropertyName);
		strBuild.Append(MetadataTextUtilities.EqualsText);
		property.AppendValue(strBuild);
	}
	private static void AppendHash(StringBuilder strBuild, String hash)
	{
		ReadOnlySpan<Char> printableHash = ITypeInformation.GetPrintableHash(hash, out String lastChar);
		strBuild.Append(nameof(JDataTypeMetadata.Hash));
		strBuild.Append(MetadataTextUtilities.EqualsText);
		strBuild.Append(printableHash);
		strBuild.Append(lastChar);
	}
}