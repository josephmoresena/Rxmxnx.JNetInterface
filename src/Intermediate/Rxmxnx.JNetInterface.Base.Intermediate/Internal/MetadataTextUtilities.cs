namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utilities set for metadata ToString() methods.
/// </summary>
internal static class MetadataTextUtilities
{
#if !PACKAGE
	/// <summary>
	/// Indicates whether detailed a ToString() is available for type metadata instances.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean TypeMetadataToStringEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableTypeMetadataToString", out Boolean disable) || !disable;
	}
#endif

	/// <summary>
	/// Retrieves a detailed report with <paramref name="typeMetadata"/> information.
	/// </summary>
	/// <param name="typeMetadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <param name="properties">Additional properties to report.</param>
	/// <returns>A detailed string representation of <paramref name="typeMetadata"/>.</returns>
	public static String GetString(JDataTypeMetadata typeMetadata, params
#if !NET9_0_OR_GREATER
		IAppendableProperty?[]
#else
		ReadOnlySpan<IAppendableProperty?>
#endif
		properties)
	{
		StringBuilder strBuild = new();
		Boolean isVoid = typeMetadata.ClassName.AsSpan().SequenceEqual(JPrimitiveTypeMetadata.VoidMetadata.ClassName);

		MetadataTextUtilities.AppendObjectBegin(strBuild);
		MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.ClassName),
		                                     ClassNameHelper.GetClassName(typeMetadata.Signature));
		MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.Kind), $"{typeMetadata.Kind}");
		switch (typeMetadata)
		{
			case JPrimitiveTypeMetadata primitiveTypeMetadata:
				MetadataTextUtilities.AppendProperty(strBuild, nameof(JPrimitiveTypeMetadata.WrapperClassName),
				                                     ClassNameHelper.GetClassName(
					                                     primitiveTypeMetadata.WrapperClassSignature));
				if (!isVoid)
				{
					MetadataTextUtilities.AppendProperty(strBuild, nameof(JPrimitiveTypeMetadata.NativeType),
					                                     primitiveTypeMetadata.NativeType.GetTypeName());
					MetadataTextUtilities.AppendProperty(strBuild, nameof(JPrimitiveTypeMetadata.UnderlineType),
					                                     $"{primitiveTypeMetadata.UnderlineType}");
				}
				break;
			default:
				if (typeMetadata.Kind is JTypeKind.Class)
					MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.Modifier),
					                                     $"{typeMetadata.Modifier}");
				foreach (IAppendableProperty? t in properties
#if !NET9_0_OR_GREATER
					         .AsSpan()
#endif
				        )
					MetadataTextUtilities.AppendProperty(strBuild, t);
				break;
		}
		if (!isVoid)
		{
			MetadataTextUtilities.AppendProperty(strBuild, "ArrayMetadata",
			                                     ClassNameHelper.GetClassName(typeMetadata.ArraySignature));
			MetadataTextUtilities.AppendProperty(strBuild, typeMetadata.ArgumentMetadata);
		}
		MetadataTextUtilities.AppendProperty(strBuild, nameof(JDataTypeMetadata.Type), $"{typeMetadata.Type}");
		MetadataTextUtilities.AppendHash(strBuild, typeMetadata.Hash);
		MetadataTextUtilities.AppendObjectEnd(strBuild);

		return strBuild.ToString();
	}
	/// <summary>
	/// Retrieves a detailed report with <paramref name="argumentMetadata"/> information.
	/// </summary>
	/// <param name="argumentMetadata">A <see cref="JArgumentMetadata"/> instance.</param>
	/// <returns>A detailed string representation of <paramref name="argumentMetadata"/>.</returns>
	public static String GetString(JArgumentMetadata argumentMetadata)
	{
		StringBuilder strBuild = new();
		MetadataTextUtilities.AppendProperty(strBuild, argumentMetadata, false);
		return strBuild.ToString();
	}
	/// <summary>
	/// Appends to <paramref name="strBuild"/> a textual item.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="value">Textual item value.</param>
	/// <param name="first">Indicates current item is the first one.</param>
	public static void AppendItem(StringBuilder strBuild, String value, Boolean first)
	{
		MetadataTextUtilities.AppendSeparator(strBuild, !first);
		if (first) strBuild.Append(' ');
		strBuild.Append(value);
	}
	/// <summary>
	/// Appends to <paramref name="strBuild"/> a pair item.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="index">Numeric item value.</param>
	/// <param name="value">Text item value.</param>
	/// <param name="first">Indicates current item is the first one.</param>
	public static void AppendItem(StringBuilder strBuild, Int32 index, String value, Boolean first)
	{
		MetadataTextUtilities.AppendSeparator(strBuild, !first);
		if (first) strBuild.Append(' ');
		MetadataTextUtilities.AppendObjectBegin(strBuild);
		strBuild.Append(index);
		MetadataTextUtilities.AppendSeparator(strBuild, true);
		strBuild.Append(value);
		MetadataTextUtilities.AppendObjectEnd(strBuild);
	}
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the beginning of an array.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	public static void AppendArrayBegin(StringBuilder strBuild) => strBuild.Append('[');
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the end of an array.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="empty">Indicates whether closing array is empty.</param>
	public static void AppendArrayEnd(StringBuilder strBuild, Boolean empty)
	{
		if (empty)
			strBuild.Append(']');
		else
			strBuild.Append(" ]");
	}

	/// <summary>
	/// Appends to <paramref name="strBuild"/> the beginning of an object.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	private static void AppendObjectBegin(StringBuilder strBuild) => strBuild.Append("{ ");
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the end of an object.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	private static void AppendObjectEnd(StringBuilder strBuild) => strBuild.Append(" }");
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the value of a property.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="propertyName">Property name.</param>
	/// <param name="value">Property value.</param>
	/// <param name="withSeparator">
	/// Indicates whether after to append property the separator should be appended.
	/// </param>
	private static void AppendProperty(StringBuilder strBuild, String propertyName, String value,
		Boolean withSeparator = true)
	{
		strBuild.Append(propertyName);
		MetadataTextUtilities.AppendEquals(strBuild);
		strBuild.Append(value);
		MetadataTextUtilities.AppendSeparator(strBuild, withSeparator);
	}
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the value of a property.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="property">Property instance.</param>
	private static void AppendProperty(StringBuilder strBuild, IAppendableProperty? property)
	{
		if (property is null) return;
		strBuild.Append(property.PropertyName);
		MetadataTextUtilities.AppendEquals(strBuild);
		property.AppendValue(strBuild);
		MetadataTextUtilities.AppendSeparator(strBuild, true);
	}
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the value of a property.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="property">Property instance.</param>
	/// <param name="withSeparator">
	/// Indicates whether after to append property the separator should be appended.
	/// </param>
	private static void AppendProperty(StringBuilder strBuild, JArgumentMetadata property, Boolean withSeparator = true)
	{
		MetadataTextUtilities.AppendObjectBegin(strBuild);
		MetadataTextUtilities.AppendProperty(strBuild, nameof(JArgumentMetadata.Signature), $"{property.Signature}");
		MetadataTextUtilities.AppendProperty(strBuild, nameof(JArgumentMetadata.Size), $"{property.Size}", false);
		MetadataTextUtilities.AppendObjectEnd(strBuild);
		MetadataTextUtilities.AppendSeparator(strBuild, withSeparator);
	}
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the element separator.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="append">Indicates whether the separator should be appended.</param>
	private static void AppendSeparator(StringBuilder strBuild, Boolean append)
	{
		if (append)
			strBuild.Append(", ");
	}
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the element assignation.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	private static void AppendEquals(StringBuilder strBuild) { strBuild.Append(" = "); }
	/// <summary>
	/// Appends to <paramref name="strBuild"/> the class hash.
	/// </summary>
	/// <param name="strBuild">A <see cref="StringBuilder"/> instance.</param>
	/// <param name="hash">Hash to append.</param>
	private static void AppendHash(StringBuilder strBuild, String hash)
	{
		ReadOnlySpan<Char> printableHash = InfoSequenceBase.GetPrintableHash(hash, out String lastChar);
		strBuild.Append(nameof(JDataTypeMetadata.Hash));
		MetadataTextUtilities.AppendEquals(strBuild);
		strBuild.Append(printableHash);
		strBuild.Append(lastChar);
	}
}