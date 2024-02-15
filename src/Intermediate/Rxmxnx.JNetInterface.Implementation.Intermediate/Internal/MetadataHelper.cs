namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class helper stores a <see cref="JDataTypeMetadata"/>
/// </summary>
internal static partial class MetadataHelper
{
	/// <summary>
	/// Retrieves metadata from hash.
	/// </summary>
	/// <param name="className">A java type name.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static IReflectionMetadata? GetReflectionMetadata(ReadOnlySpan<Byte> className)
	{
		CStringSequence information = MetadataHelper.GetClassInformation(className);
		String hash = information.ToString();
		IReflectionMetadata? result;

		if (MetadataHelper.primitiveReflectionMetadata.TryGetValue(hash, out IReflectionMetadata? primitiveMetadata))
		{
			result = primitiveMetadata;
		}
		else if (MetadataHelper.runtimeMetadata.TryGetValue(hash, out JReferenceTypeMetadata? metadata))
		{
			result = metadata;
			MetadataHelper.reflectionMetadata.TryRemove(hash, out _);
		}
		else
		{
			result = new UnknownReflectionMetadata(information[1]);
			MetadataHelper.reflectionMetadata[hash] = result;
		}
		return result;
	}

	/// <summary>
	/// Retrieves metadata from hash.
	/// </summary>
	/// <param name="hash">A JNI class hash.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JReferenceTypeMetadata? GetMetadata(String hash)
		=> MetadataHelper.runtimeMetadata.GetValueOrDefault(hash);
	/// <summary>
	/// Retrieves <see cref="JDataTypeMetadata"/> metadata.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>A <see cref="JDataTypeMetadata"/> metadata.</returns>
	public static JDataTypeMetadata GetMetadata<TDataType>() where TDataType : IDataType<TDataType>
	{
		MetadataHelper.Register<TDataType>();
		return MetadataHelper.GetMetadata(IDataType.GetHash<TDataType>()) ?? IDataType.GetMetadata<TDataType>();
	}
	/// <summary>
	/// Registers <typeparamref name="TDataType"/> as valid datatype for current process.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current datatype was registered; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean Register<TDataType>() where TDataType : IDataType<TDataType>
	{
		JReferenceTypeMetadata? metadata = IDataType.GetMetadata<TDataType>() as JReferenceTypeMetadata;
		Boolean result = MetadataHelper.Register(metadata);
		MetadataHelper.Register(metadata?.GetArrayMetadata());
		return result;
	}
	/// <summary>
	/// Retrieves the class has from current <paramref name="className"/>.
	/// </summary>
	/// <param name="className">A java type name.</param>
	/// <returns><see cref="CStringSequence"/> with class information for given type.</returns>
	public static CStringSequence GetClassInformation(CString className)
	{
		CString classNameF = JDataTypeMetadata.JniParseClassName(className);
		return JDataTypeMetadata.CreateInformationSequence(classNameF);
	}
	/// <summary>
	/// Retrieves the class has from current <paramref name="className"/>.
	/// </summary>
	/// <param name="className">A java type name.</param>
	/// <returns><see cref="CStringSequence"/> with class information for given type.</returns>
	public static CStringSequence GetClassInformation(ReadOnlySpan<Byte> className)
	{
		CString classNameF = JDataTypeMetadata.JniParseClassName(className);
		return JDataTypeMetadata.CreateInformationSequence(classNameF);
	}
	/// <summary>
	/// Retrieves the class has from current <paramref name="hash"/>.
	/// </summary>
	/// <param name="hash">A JNI class hash.</param>
	/// <returns><see cref="CStringSequence"/> with class information for given type.</returns>
	public static CStringSequence GetClassInformation(String hash)
	{
		ReadOnlySpan<Byte> classInformation = hash.AsSpan().AsBytes();
		Int32 classNameLength = ITypeInformation.GetSegmentLength(classInformation, 0);
		Int32 signatureLength = ITypeInformation.GetSegmentLength(classInformation, classNameLength + 1);
		Int32 arraySignatureLength = ITypeInformation.GetSegmentLength(classInformation, signatureLength + 1);
		return new(classInformation[..classNameLength], classInformation[(classNameLength + 1)..signatureLength],
		           classInformation[(signatureLength + 1)..arraySignatureLength]);
	}
	/// <summary>
	/// Indicates whether current class name is for an array.
	/// </summary>
	/// <param name="className">A Java class name.</param>
	/// <param name="arrayHash">Output. Hash for array type.</param>
	/// <param name="arrayTypeMetadata">Output. Metadata for array type.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="className"/> is for an array; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	public static Boolean IsArrayClass(CString className, [NotNullWhen(true)] out CStringSequence? arrayHash,
		out JArrayTypeMetadata? arrayTypeMetadata)
	{
		arrayHash = default;
		arrayTypeMetadata = default;
		if (className.Length < 2 || className[0] != UnicodeObjectSignatures.ArraySignaturePrefixChar) return false;
		arrayHash = MetadataHelper.GetClassInformation(className);
		if (!MetadataHelper.runtimeMetadata.TryGetValue(arrayHash.ToString(),
		                                                out JReferenceTypeMetadata? referenceMetadata))
			referenceMetadata = MetadataHelper.IsArrayClass(className[1..], out _, out arrayTypeMetadata) ?
				arrayTypeMetadata?.GetArrayMetadata() :
				MetadataHelper.GetMetadata(JDataTypeMetadata.CreateInformationSequence(arrayHash[0][1..^1]).ToString())
				              ?.GetArrayMetadata();
		arrayTypeMetadata = (JArrayTypeMetadata?)referenceMetadata;
		MetadataHelper.Register(arrayTypeMetadata);
		return true;
	}

	/// <summary>
	/// Determines statically whether an object of <paramref name="jClass"/> can be safely cast to
	/// <paramref name="otherClass"/>.
	/// </summary>
	/// <param name="jClass">Java class instance.</param>
	/// <param name="otherClass">Other java class instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of <paramref name="jClass"/> can be safely cast to
	/// <paramref name="otherClass"/>; <see langword="null"/> if an object of <paramref name="otherClass"/>
	/// can be safely cast to <paramref name="jClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean? IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
	{
		JReferenceTypeMetadata? classMetadata = MetadataHelper.GetMetadata(jClass.Hash);
		JReferenceTypeMetadata? otherClassMetadata = MetadataHelper.GetMetadata(otherClass.Hash);
		if (classMetadata is null || otherClassMetadata is null) return default;
		if (classMetadata.Hash == otherClassMetadata.Hash) return true;
		if (otherClassMetadata.Hash == IDataType.GetHash<JLocalObject>()) return true;
		if (classMetadata.Hash == IDataType.GetHash<JLocalObject>()) return default;
		String forwardKey = MetadataHelper.GetAssignationKey(classMetadata, otherClassMetadata);
		if (MetadataHelper.assignationCache.TryGetValue(forwardKey, out Boolean? result)) return result;
		String backwardKey = MetadataHelper.GetAssignationKey(otherClassMetadata, classMetadata);
		result = MetadataHelper.IsAssignableFrom(classMetadata, otherClassMetadata);
		MetadataHelper.assignationCache.TryAdd(forwardKey, result);
		MetadataHelper.assignationCache.TryAdd(backwardKey, result.HasValue && result.Value ? null : result ?? true);
		return result;
	}
	/// <summary>
	/// Determines statically whether <paramref name="classMetadata"/> type can be safely cast to
	/// <paramref name="otherClassMetadata"/> type.
	/// </summary>
	/// <param name="classMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="otherClassMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="classMetadata"/> type can be safely cast to
	/// <paramref name="otherClassMetadata"/> type; <see langword="null"/> if <paramref name="otherClassMetadata"/> type
	/// can be safely cast to <paramref name="classMetadata"/> type; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean? IsAssignableFrom(JReferenceTypeMetadata classMetadata,
		JReferenceTypeMetadata otherClassMetadata)
	{
		switch (classMetadata)
		{
			case JArrayTypeMetadata arrayMetadata when otherClassMetadata is JArrayTypeMetadata otherArrayMetadata:
				return MetadataHelper.IsAssignableFrom(arrayMetadata, otherArrayMetadata);
			case JClassTypeMetadata when otherClassMetadata is JClassTypeMetadata:
			{
				JClassTypeMetadata? baseMetadata = otherClassMetadata.BaseMetadata;
				while (baseMetadata is not null)
				{
					if (baseMetadata.Hash == classMetadata.Hash) return true;
					baseMetadata = baseMetadata.BaseMetadata;
				}
				baseMetadata = classMetadata.BaseMetadata;
				while (baseMetadata is not null)
				{
					if (baseMetadata.Hash == otherClassMetadata.Hash) return default;
					baseMetadata = baseMetadata.BaseMetadata;
				}
				break;
			}
		}
		if (otherClassMetadata is JInterfaceTypeMetadata)
			if (classMetadata.Interfaces.Any(interfaceMetadata => interfaceMetadata.Hash == otherClassMetadata.Hash))
				return true;
		if (classMetadata is not JInterfaceTypeMetadata) return false;
		if (otherClassMetadata.Interfaces.Any(interfaceMetadata => interfaceMetadata.Hash == classMetadata.Hash))
			return default;
		return false;
	}
	/// <summary>
	/// Registers <paramref name="metadata"/> into the runtime metadata.
	/// </summary>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="metadata"/> was registered; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean Register(JReferenceTypeMetadata? metadata)
	{
		if (metadata is null || MetadataHelper.runtimeMetadata.ContainsKey(metadata.Hash)) return false;
		if (metadata.BaseMetadata is not null)
		{
			MetadataHelper.assignationCache.TryAdd(MetadataHelper.GetAssignationKey(metadata, metadata.BaseMetadata),
			                                       true);
			MetadataHelper.assignationCache.TryAdd(MetadataHelper.GetAssignationKey(metadata.BaseMetadata, metadata),
			                                       default);
			MetadataHelper.Register(metadata.BaseMetadata);
		}
		foreach (JInterfaceTypeMetadata interfaceMetadata in metadata.Interfaces)
		{
			MetadataHelper.assignationCache.TryAdd(MetadataHelper.GetAssignationKey(metadata, interfaceMetadata), true);
			MetadataHelper.assignationCache.TryAdd(MetadataHelper.GetAssignationKey(interfaceMetadata, metadata),
			                                       default);
			MetadataHelper.Register(interfaceMetadata);
		}
		if (metadata is JArrayTypeMetadata arrayMetadata)
			MetadataHelper.Register(arrayMetadata.ElementMetadata as JReferenceTypeMetadata);
		return MetadataHelper.runtimeMetadata.TryAdd(metadata.Hash, metadata);
	}
	/// <summary>
	/// Creates the assignation key for <paramref name="fromMetadata"/> to <paramref name="toMetadata"/>
	/// </summary>
	/// <param name="fromMetadata">Metadata whom type must be promoted.</param>
	/// <param name="toMetadata">Metadata whom type must be casted.</param>
	/// <returns>The assignation key.</returns>
	private static String GetAssignationKey(ITypeInformation fromMetadata, ITypeInformation toMetadata)
		=> new CStringSequence(fromMetadata.ClassName, MetadataHelper.assignableTo, toMetadata.ClassName).ToString();
}