namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class helper stores a <see cref="JDataTypeMetadata"/>
/// </summary>
internal static partial class MetadataHelper
{
	/// <summary>
	/// Retrieves <see cref="JArgumentMetadata"/> metadata for <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JArgumentMetadata"/> instance.</returns>
	public static JArgumentMetadata GetArgumentMetadata(JClassObject jClass)
	{
		switch (jClass.ClassSignature[0])
		{
			case CommonNames.BooleanSignatureChar:
				return JArgumentMetadata.Get<JBoolean>();
			case CommonNames.ByteSignatureChar:
				return JArgumentMetadata.Get<JByte>();
			case CommonNames.CharSignatureChar:
				return JArgumentMetadata.Get<JChar>();
			case CommonNames.DoubleSignatureChar:
				return JArgumentMetadata.Get<JDouble>();
			case CommonNames.FloatSignatureChar:
				return JArgumentMetadata.Get<JFloat>();
			case CommonNames.IntSignatureChar:
				return JArgumentMetadata.Get<JInt>();
			case CommonNames.LongSignatureChar:
				return JArgumentMetadata.Get<JLong>();
			case CommonNames.ShortSignatureChar:
				return JArgumentMetadata.Get<JShort>();
			default:
				if (MetadataHelper.runtimeMetadata.TryGetValue(jClass.Hash, out JReferenceTypeMetadata? typeMetadata))
				{
					MetadataHelper.reflectionMetadata.TryRemove(jClass.Hash, out _); // Removes unknown if exists.
					return typeMetadata.ArgumentMetadata;
				}
				if (MetadataHelper.reflectionMetadata.TryGetValue(jClass.Hash, out UnknownReflectionMetadata? unknown))
					return unknown.ArgumentMetadata;

				// Create unknown metadata for reflection.
				unknown = new(jClass.Name);
				MetadataHelper.reflectionMetadata[jClass.Hash] = unknown;
				return unknown.ArgumentMetadata;
		}
	}
	/// <summary>
	/// Retrieves <see cref="JFieldDefinition"/> metadata for <paramref name="jClass"/> and
	/// <paramref name="fieldName"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="fieldName">Definition field name.</param>
	/// <returns>A <see cref="JFieldDefinition"/> instance.</returns>
	public static JFieldDefinition GetFieldDefinition(JClassObject jClass, ReadOnlySpan<Byte> fieldName)
	{
		switch (jClass.ClassSignature[0])
		{
			case CommonNames.BooleanSignatureChar:
				return new JFieldDefinition<JBoolean>(fieldName);
			case CommonNames.ByteSignatureChar:
				return new JFieldDefinition<JByte>(fieldName);
			case CommonNames.CharSignatureChar:
				return new JFieldDefinition<JChar>(fieldName);
			case CommonNames.DoubleSignatureChar:
				return new JFieldDefinition<JDouble>(fieldName);
			case CommonNames.FloatSignatureChar:
				return new JFieldDefinition<JFloat>(fieldName);
			case CommonNames.IntSignatureChar:
				return new JFieldDefinition<JInt>(fieldName);
			case CommonNames.LongSignatureChar:
				return new JFieldDefinition<JLong>(fieldName);
			case CommonNames.ShortSignatureChar:
				return new JFieldDefinition<JShort>(fieldName);
			default:
				if (MetadataHelper.runtimeMetadata.TryGetValue(jClass.Hash, out JReferenceTypeMetadata? typeMetadata))
				{
					MetadataHelper.reflectionMetadata.TryRemove(jClass.Hash, out _); // Removes unknown if exists.
					return typeMetadata.CreateFieldDefinition(fieldName);
				}
				if (MetadataHelper.reflectionMetadata.TryGetValue(jClass.Hash, out UnknownReflectionMetadata? unknown))
					return unknown.CreateFieldDefinition(fieldName);

				// Create unknown metadata for reflection.
				unknown = new(jClass.Name);
				MetadataHelper.reflectionMetadata[jClass.Hash] = unknown;
				return unknown.CreateFieldDefinition(fieldName);
		}
	}
	/// <summary>
	/// Retrieves <see cref="JFieldDefinition"/> metadata for <paramref name="jClass"/>,
	/// <paramref name="callName"/> and <paramref name="paramsMetadata"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="callName">Definition call name.</param>
	/// <param name="paramsMetadata">Definition parameters metadata.</param>
	/// <returns>A <see cref="JCallDefinition"/> instance.</returns>
	public static JCallDefinition GetCallDefinition(JClassObject jClass, ReadOnlySpan<Byte> callName,
		JArgumentMetadata[] paramsMetadata)
	{
		switch (jClass.ClassSignature[0])
		{
			case CommonNames.VoidSignatureChar: // Void call is a Method.
				return JMethodDefinition.Create(callName, paramsMetadata);
			case CommonNames.BooleanSignatureChar:
				return JFunctionDefinition<JBoolean>.Create(callName, paramsMetadata);
			case CommonNames.ByteSignatureChar:
				return JFunctionDefinition<JByte>.Create(callName, paramsMetadata);
			case CommonNames.CharSignatureChar:
				return JFunctionDefinition<JChar>.Create(callName, paramsMetadata);
			case CommonNames.DoubleSignatureChar:
				return JFunctionDefinition<JDouble>.Create(callName, paramsMetadata);
			case CommonNames.FloatSignatureChar:
				return JFunctionDefinition<JFloat>.Create(callName, paramsMetadata);
			case CommonNames.IntSignatureChar:
				return JFunctionDefinition<JInt>.Create(callName, paramsMetadata);
			case CommonNames.LongSignatureChar:
				return JFunctionDefinition<JLong>.Create(callName, paramsMetadata);
			case CommonNames.ShortSignatureChar:
				return JFunctionDefinition<JShort>.Create(callName, paramsMetadata);
			default:
				if (MetadataHelper.runtimeMetadata.TryGetValue(jClass.Hash, out JReferenceTypeMetadata? typeMetadata))
				{
					MetadataHelper.reflectionMetadata.TryRemove(jClass.Hash, out _); // Removes unknown if exists.
					return typeMetadata.CreateFunctionDefinition(callName, paramsMetadata);
				}
				if (MetadataHelper.reflectionMetadata.TryGetValue(jClass.Hash, out UnknownReflectionMetadata? unknown))
					return unknown.CreateFunctionDefinition(callName, paramsMetadata);

				// Create unknown metadata for reflection.
				unknown = new(jClass.Name);
				MetadataHelper.reflectionMetadata[jClass.Hash] = unknown;
				return unknown.CreateFunctionDefinition(callName, paramsMetadata);
		}
	}
	/// <summary>
	/// Retrieves metadata from hash.
	/// </summary>
	/// <param name="hash">A JNI class hash.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JReferenceTypeMetadata? GetMetadata(String hash)
	{
		JReferenceTypeMetadata? result = MetadataHelper.runtimeMetadata.GetValueOrDefault(hash);
		if (result is not null) return result;
		if (MetadataHelper.classTree.TryGetValue(hash, out String? value))
			result = MetadataHelper.GetMetadata(value); // Retrieves metadata from cache.
		else if (MetadataHelper.viewTree.TryGetValue(hash, out HashesSet? set))
			result = set.GetViewMetadata();
		return result;
	}
	/// <summary>
	/// Retrieves metadata from class name.
	/// </summary>
	/// <param name="className">A JNI class name.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JReferenceTypeMetadata? GetMetadata(ReadOnlySpan<Byte> className)
	{
		CStringSequence classInformation = MetadataHelper.GetClassInformation(className, false);
		return MetadataHelper.runtimeMetadata.GetValueOrDefault(classInformation.ToString());
	}
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
	/// Retrieves array metadata from element class name.
	/// </summary>
	/// <param name="elementClassName">A JNI class name.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JArrayTypeMetadata? GetArrayMetadata(ReadOnlySpan<Byte> elementClassName)
	{
		CStringSequence elementClassInformation = MetadataHelper.GetClassInformation(elementClassName, false);
		JReferenceTypeMetadata? elementMetadata =
			MetadataHelper.runtimeMetadata.GetValueOrDefault(elementClassInformation.ToString());
		JArrayTypeMetadata? result = elementMetadata?.GetArrayMetadata();
		MetadataHelper.Register(result);
		return result;
	}
	public static JArrayTypeMetadata? GetArrayMetadata(JReferenceTypeMetadata? elementMetadata)
	{
		JArrayTypeMetadata? result = elementMetadata?.GetArrayMetadata();
		MetadataHelper.Register(result);
		return result;
	}
	/// <summary>
	/// Registers <typeparamref name="TDataType"/> as valid datatype for the current process.
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
	/// <param name="escape">Indicates whether <paramref name="className"/> should be escaped.</param>
	/// <returns><see cref="CStringSequence"/> with class information for given type.</returns>
	public static CStringSequence GetClassInformation(ReadOnlySpan<Byte> className, Boolean escape)
	{
		ReadOnlySpan<Byte> jniClassName = escape ? JDataTypeMetadata.JniEscapeClassName(className) : className;
		return JDataTypeMetadata.CreateInformationSequence(jniClassName);
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
		JReferenceTypeMetadata? fromMetadata = MetadataHelper.GetMetadata(jClass.Hash);
		JReferenceTypeMetadata? toMetadata = MetadataHelper.GetMetadata(otherClass.Hash);
		return MetadataHelper.IsAssignableFrom(fromMetadata, toMetadata);
	}
	/// <summary>
	/// Register class tree.
	/// </summary>
	/// <param name="hashClass">Hash class.</param>
	/// <param name="superClassHash">Super class hash.</param>
	public static void RegisterSuperClass(String hashClass, String superClassHash)
	{
		if (hashClass.AsSpan().SequenceEqual(superClassHash)) return;
		MetadataHelper.classTree[hashClass] = superClassHash;
	}
	/// <summary>
	/// Register class tree.
	/// </summary>
	/// <param name="hashView">Hash class view.</param>
	/// <param name="superViewHash">Super view hash.</param>
	public static void RegisterSuperView(String hashView, String superViewHash)
	{
		if (hashView.AsSpan().SequenceEqual(superViewHash)) return;
		if (!MetadataHelper.viewTree.TryGetValue(hashView, out HashesSet? set))
		{
			set = new();
			MetadataHelper.viewTree[hashView] = set;
		}
		set.Add(superViewHash);
	}

	/// <summary>
	/// Determines statically whether an object of <paramref name="fromMetadata"/> can be safely cast to
	/// <paramref name="toMetadata"/>.
	/// </summary>
	/// <param name="fromMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="toMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of <paramref name="fromMetadata"/> can be safely cast to
	/// <paramref name="toMetadata"/>; <see langword="null"/> if an object of <paramref name="toMetadata"/>
	/// can be safely cast to <paramref name="fromMetadata"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2234,
	                 Justification = CommonConstants.BackwardOperationJustification)]
	private static Boolean? IsAssignableFrom(JReferenceTypeMetadata? fromMetadata, JReferenceTypeMetadata? toMetadata)
	{
		Boolean? result = MetadataHelper.IsBasicAssignable(fromMetadata, toMetadata);
		if (!result.HasValue || result.Value) return result;
		String forwardKey = MetadataHelper.GetAssignationKey(fromMetadata!, toMetadata!);
		if (MetadataHelper.assignationCache.TryGetValue(forwardKey, out result)) return result;
		String backwardKey = MetadataHelper.GetAssignationKey(toMetadata!, fromMetadata!);
		result = MetadataHelper.GetAssignation(fromMetadata!, toMetadata!);
		MetadataHelper.assignationCache.TryAdd(forwardKey, result);
		MetadataHelper.assignationCache.TryAdd(backwardKey, result.HasValue && result.Value ? null : result ?? true);
		return result;
	}
	/// <summary>
	/// Determines statically whether an object of <paramref name="classMetadata"/> can be safely cast to
	/// <paramref name="otherClassMetadata"/>.
	/// </summary>
	/// <param name="classMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="otherClassMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of <paramref name="classMetadata"/> can be safely cast to
	/// <paramref name="otherClassMetadata"/>; <see langword="null"/> if an object of
	/// <paramref name="otherClassMetadata"/> can be safely cast to <paramref name="classMetadata"/>;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean? IsBasicAssignable([NotNullWhen(false)] JReferenceTypeMetadata? classMetadata,
		[NotNullWhen(false)] JReferenceTypeMetadata? otherClassMetadata)
	{
		if (classMetadata is null || otherClassMetadata is null ||
		    classMetadata.Hash == IDataType.GetHash<JLocalObject>()) return default;
		return classMetadata.Hash == otherClassMetadata.Hash ||
			otherClassMetadata.Hash == IDataType.GetHash<JLocalObject>();
	}
	/// <summary>
	/// Determines statically whether <paramref name="fromMetadata"/> type can be safely cast to
	/// <paramref name="toMetadata"/> type.
	/// </summary>
	/// <param name="fromMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="toMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="fromMetadata"/> type can be safely cast to
	/// <paramref name="toMetadata"/> type; <see langword="null"/> if <paramref name="toMetadata"/> type
	/// can be safely cast to <paramref name="fromMetadata"/> type; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean? GetAssignation(JReferenceTypeMetadata fromMetadata, JReferenceTypeMetadata toMetadata)
	{
		switch (fromMetadata)
		{
			case JArrayTypeMetadata arrayMetadata when toMetadata is JArrayTypeMetadata otherArrayMetadata:
				return MetadataHelper.IsAssignableFrom(arrayMetadata.ElementMetadata as JReferenceTypeMetadata,
				                                       otherArrayMetadata.ElementMetadata as JReferenceTypeMetadata);
			case JClassTypeMetadata classMetadata when toMetadata is JClassTypeMetadata otherClassMetadata:
			{
				if (MetadataHelper.IsForwardAssignable(toMetadata, classMetadata)) return default;
				if (MetadataHelper.IsForwardAssignable(fromMetadata, otherClassMetadata)) return true;
				break;
			}
		}
		return !MetadataHelper.HasInterface(toMetadata, fromMetadata as JInterfaceTypeMetadata) ?
			MetadataHelper.HasInterface(fromMetadata, toMetadata as JInterfaceTypeMetadata) :
			default(Boolean?);
	}
	/// <summary>
	/// Indicates whether <paramref name="classMetadata"/> is forward assignable to
	/// <paramref name="otherClassMetadata"/>.
	/// </summary>
	/// <param name="classMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="otherClassMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="classMetadata"/> is forward assignable
	/// <paramref name="otherClassMetadata"/> type; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean IsForwardAssignable(ITypeInformation classMetadata, JClassTypeMetadata otherClassMetadata)
	{
		JClassTypeMetadata? baseMetadata = otherClassMetadata.BaseMetadata;
		while (baseMetadata is not null)
		{
			if (baseMetadata.Hash == classMetadata.Hash) return true;
			baseMetadata = baseMetadata.BaseMetadata;
		}
		return false;
	}
	/// <summary>
	/// Indicates whether <paramref name="fromMetadata"/> has <paramref name="interfaceMetadata"/> as
	/// interface.
	/// </summary>
	/// <param name="fromMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="fromMetadata"/> has
	/// <paramref name="interfaceMetadata"/> type as interface; otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean HasInterface(JReferenceTypeMetadata fromMetadata, JInterfaceTypeMetadata? interfaceMetadata)
		=> interfaceMetadata is not null && fromMetadata.Interfaces.Contains(interfaceMetadata);
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
		metadata.Interfaces.ForEach(metadata, MetadataHelper.RegisterInterfaceAssignation);
		if (metadata is JArrayTypeMetadata arrayMetadata)
			MetadataHelper.Register(arrayMetadata.ElementMetadata as JReferenceTypeMetadata);
		return MetadataHelper.runtimeMetadata.TryAdd(metadata.Hash, metadata);
	}
	/// <summary>
	/// Registers assignation of <paramref name="metadata"/> to <paramref name="interfaceMetadata"/>.
	/// </summary>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
	private static void RegisterInterfaceAssignation(JReferenceTypeMetadata metadata,
		JInterfaceTypeMetadata interfaceMetadata)
	{
		MetadataHelper.assignationCache.TryAdd(MetadataHelper.GetAssignationKey(metadata, interfaceMetadata), true);
		MetadataHelper.assignationCache.TryAdd(MetadataHelper.GetAssignationKey(interfaceMetadata, metadata), default);
		MetadataHelper.Register(interfaceMetadata);
	}
	/// <summary>
	/// Creates the assignation key for <paramref name="fromMetadata"/> to <paramref name="toMetadata"/>
	/// </summary>
	/// <param name="fromMetadata">Metadata whom type must be promoted.</param>
	/// <param name="toMetadata">Metadata whom type must be casted.</param>
	/// <returns>The assignation key.</returns>
	private static String GetAssignationKey(ITypeInformation fromMetadata, ITypeInformation toMetadata)
		=> new CStringSequence(fromMetadata.ClassName.AsSpan(), MetadataHelper.assignableTo, toMetadata.ClassName)
			.ToString();
}