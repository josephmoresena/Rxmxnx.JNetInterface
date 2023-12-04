namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class helper stores a <see cref="JDataTypeMetadata"/>
/// </summary>
internal static class JMetadataHelper
{
	/// <summary>
	/// Separator class assignability.
	/// </summary>
	private static readonly CString assignableTo = new(() => " -> "u8);
	/// <summary>
	/// Escape for Java class name -> JNI class name.
	/// </summary>
	private static readonly CString classNameEscape = new(() => "./"u8);
	
	/// <summary>
	/// Basic metadata dictionary.
	/// </summary>
	private static readonly Dictionary<String, JReferenceTypeMetadata> initialDictionary = new()
	{
		// Basic objects //
		{ IDataType.GetHash<JLocalObject>(), IReferenceType.GetMetadata<JLocalObject>() },
		{ IDataType.GetHash<JClassObject>(), IReferenceType.GetMetadata<JClassObject>() },
		{ IDataType.GetHash<JStringObject>(), IReferenceType.GetMetadata<JStringObject>() },
		{ IDataType.GetHash<JNumberObject>(), IReferenceType.GetMetadata<JNumberObject>() },
		{ IDataType.GetHash<JEnumObject>(), IReferenceType.GetMetadata<JEnumObject>() },
		{ IDataType.GetHash<JThrowableObject>(), IReferenceType.GetMetadata<JThrowableObject>() },
		{ IDataType.GetHash<JStackTraceElementObject>(), IReferenceType.GetMetadata<JStackTraceElementObject>() },
		
		// Wrapper objects //
		{ IDataType.GetHash<JBooleanObject>(), IReferenceType.GetMetadata<JBooleanObject>() },
		{ IDataType.GetHash<JByteObject>(), IReferenceType.GetMetadata<JByteObject>() },
		{ IDataType.GetHash<JCharacterObject>(), IReferenceType.GetMetadata<JCharacterObject>() },
		{ IDataType.GetHash<JDoubleObject>(), IReferenceType.GetMetadata<JDoubleObject>() },
		{ IDataType.GetHash<JFloatObject>(), IReferenceType.GetMetadata<JFloatObject>() },
		{ IDataType.GetHash<JIntegerObject>(), IReferenceType.GetMetadata<JIntegerObject>() },
		{ IDataType.GetHash<JLongObject>(), IReferenceType.GetMetadata<JLongObject>() },
		{ IDataType.GetHash<JShortObject>(), IReferenceType.GetMetadata<JShortObject>() },
		
		// Primitive arrays //
		{ IDataType.GetHash<JArrayObject<JBoolean>>(), IReferenceType.GetMetadata<JArrayObject<JBoolean>>() },
		{ IDataType.GetHash<JArrayObject<JByte>>(), IReferenceType.GetMetadata<JArrayObject<JByte>>() },
		{ IDataType.GetHash<JArrayObject<JChar>>(), IReferenceType.GetMetadata<JArrayObject<JChar>>() },
		{ IDataType.GetHash<JArrayObject<JDouble>>(), IReferenceType.GetMetadata<JArrayObject<JDouble>>() },
		{ IDataType.GetHash<JArrayObject<JFloat>>(), IReferenceType.GetMetadata<JArrayObject<JFloat>>() },
		{ IDataType.GetHash<JArrayObject<JInt>>(), IReferenceType.GetMetadata<JArrayObject<JInt>>() },
		{ IDataType.GetHash<JArrayObject<JLong>>(), IReferenceType.GetMetadata<JArrayObject<JLong>>() },
		{ IDataType.GetHash<JArrayObject<JShort>>(), IReferenceType.GetMetadata<JArrayObject<JShort>>() },
		
		// Basic object arrays //
		{ IDataType.GetHash<JArrayObject<JLocalObject>>(), IReferenceType.GetMetadata<JArrayObject<JLocalObject>>() },
		{ IDataType.GetHash<JArrayObject<JClassObject>>(), IReferenceType.GetMetadata<JArrayObject<JClassObject>>() },
		{ IDataType.GetHash<JArrayObject<JStringObject>>(), IReferenceType.GetMetadata<JArrayObject<JStringObject>>() },
		{ IDataType.GetHash<JArrayObject<JNumberObject>>(), IReferenceType.GetMetadata<JArrayObject<JNumberObject>>() },
		{ IDataType.GetHash<JArrayObject<JEnumObject>>(), IReferenceType.GetMetadata<JArrayObject<JEnumObject>>() },
		{ IDataType.GetHash<JArrayObject<JThrowableObject>>(), IReferenceType.GetMetadata<JArrayObject<JThrowableObject>>() },
		{ IDataType.GetHash<JArrayObject<JStackTraceElementObject>>(), IReferenceType.GetMetadata<JArrayObject<JStackTraceElementObject>>() },
		
		// Wrapper object arrays //
		{ IDataType.GetHash<JArrayObject<JBooleanObject>>(), IReferenceType.GetMetadata<JArrayObject<JBooleanObject>>() },
		{ IDataType.GetHash<JArrayObject<JByteObject>>(), IReferenceType.GetMetadata<JArrayObject<JByteObject>>() },
		{ IDataType.GetHash<JArrayObject<JCharacterObject>>(), IReferenceType.GetMetadata<JArrayObject<JCharacterObject>>() },
		{ IDataType.GetHash<JArrayObject<JDoubleObject>>(), IReferenceType.GetMetadata<JArrayObject<JDoubleObject>>() },
		{ IDataType.GetHash<JArrayObject<JFloatObject>>(), IReferenceType.GetMetadata<JArrayObject<JFloatObject>>() },
		{ IDataType.GetHash<JArrayObject<JIntegerObject>>(), IReferenceType.GetMetadata<JArrayObject<JIntegerObject>>() },
		{ IDataType.GetHash<JArrayObject<JLongObject>>(), IReferenceType.GetMetadata<JArrayObject<JLongObject>>() },
		{ IDataType.GetHash<JArrayObject<JShortObject>>(), IReferenceType.GetMetadata<JArrayObject<JShortObject>>() },
		
		// Basic interfaces //
		{ IDataType.GetHash<JCharSequenceObject>(), IReferenceType.GetMetadata<JCharSequenceObject>() },
		{ IDataType.GetHash<JCloneableObject>(), IReferenceType.GetMetadata<JCloneableObject>() },
		{ IDataType.GetHash<JComparableObject>(), IReferenceType.GetMetadata<JComparableObject>() },
		{ IDataType.GetHash<JSerializableObject>(), IReferenceType.GetMetadata<JSerializableObject>() },
		{ IDataType.GetHash<JAnnotatedElementObject>(), IReferenceType.GetMetadata<JAnnotatedElementObject>() },
		{ IDataType.GetHash<JGenericDeclarationObject>(), IReferenceType.GetMetadata<JGenericDeclarationObject>() },
		{ IDataType.GetHash<JTypeObject>(), IReferenceType.GetMetadata<JTypeObject>() },
		
	};

	/// <summary>
	/// Runtime metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, JReferenceTypeMetadata> runtimeMetadata = 
		new(JMetadataHelper.initialDictionary);
	private static readonly ConcurrentDictionary<String, Boolean?> assignableFrom = new();

	/// <summary>
	/// Retrieves metadata from hash.
	/// </summary>
	/// <param name="hash">A JNI class hash.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JReferenceTypeMetadata? GetMetadata(String hash) => JMetadataHelper.runtimeMetadata.GetValueOrDefault(hash);
	/// <summary>
	/// Retrieves <see cref="JDataTypeMetadata"/> metadata.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>A <see cref="JDataTypeMetadata"/> metadata.</returns>
	public static JDataTypeMetadata GetMetadata<TDataType>() where TDataType : IDataType<TDataType>
	{
		JMetadataHelper.Register<TDataType>();
		return JMetadataHelper.GetMetadata(IDataType.GetHash<TDataType>()) ?? IDataType.GetMetadata<TDataType>();
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
		Boolean result = JMetadataHelper.Register(metadata);
		JMetadataHelper.Register(metadata?.GetArrayMetadata());
		return result;
	}
	/// <summary>
	/// Retrieves the class has from current <paramref name="className"/>.
	/// </summary>
	/// <param name="className">A java type name.</param>
	/// <returns>Hash for given type.</returns>
	public static CStringSequence GetHashSequence(CString className)
	{
		CString classNameF = !className.Contains(JMetadataHelper.classNameEscape[0]) ?
			className : CString.Create(className.Select(JMetadataHelper.EscapeClassNameChar).ToArray());
		return JDataTypeMetadata.CreateInformationSequence(classNameF);
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
	public static Boolean IsArrayClass(CString className, [NotNullWhen(true)] out CStringSequence? arrayHash, out JArrayTypeMetadata? arrayTypeMetadata)
	{
		arrayHash = default;
		arrayTypeMetadata = default;
		if (className.Length < 2 || className[0] != UnicodeObjectSignatures.ArraySignaturePrefix[0]) return false;
		arrayHash = JDataTypeMetadata.CreateInformationSequence(className);
		if (!JMetadataHelper.runtimeMetadata.TryGetValue(arrayHash.ToString(),
		                                                 out JReferenceTypeMetadata? referenceMetadata))
		{
			referenceMetadata = 
				JMetadataHelper.IsArrayClass(className[1..], out _, out arrayTypeMetadata) ? 
					arrayTypeMetadata?.GetArrayMetadata() : 
					JMetadataHelper.GetMetadata(
							JDataTypeMetadata.CreateInformationSequence(className[1..^1]).ToString())?
						.GetArrayMetadata();
		}
		arrayTypeMetadata = (JArrayTypeMetadata?)referenceMetadata;
		JMetadataHelper.Register(arrayTypeMetadata);
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
		JReferenceTypeMetadata? classMetadata = JMetadataHelper.GetMetadata(jClass.Hash);
		JReferenceTypeMetadata? otherClassMetadata = JMetadataHelper.GetMetadata(otherClass.Hash);
		if (classMetadata is null || otherClassMetadata is null) return default;
		if (classMetadata.Hash == otherClassMetadata.Hash) return true;
		if (otherClassMetadata.Hash == IDataType.GetHash<JLocalObject>()) return true;
		if (classMetadata.Hash == IDataType.GetHash<JLocalObject>()) return default;
		CStringSequence forwardKey = new(classMetadata.ClassName, JMetadataHelper.assignableTo, otherClassMetadata.ClassName);
		if (JMetadataHelper.assignableFrom.TryGetValue(forwardKey.ToString(), out Boolean? result)) return result;
		CStringSequence backwardKey = new(otherClassMetadata.ClassName, JMetadataHelper.assignableTo, classMetadata.ClassName);
		result = JMetadataHelper.IsAssignableFrom(classMetadata, otherClassMetadata);
		JMetadataHelper.assignableFrom.TryAdd(forwardKey.ToString(), result);
		JMetadataHelper.assignableFrom.TryAdd(backwardKey.ToString(), result.HasValue && result.Value ? null : result ?? true);
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
		if (classMetadata is JClassTypeMetadata && otherClassMetadata is JClassTypeMetadata)
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
		if (metadata is null || JMetadataHelper.runtimeMetadata.ContainsKey(metadata.Hash)) return false;
		JMetadataHelper.Register(metadata.BaseMetadata);
		foreach (JInterfaceTypeMetadata interfaceMetadata in metadata.Interfaces)
			JMetadataHelper.Register(interfaceMetadata);
		if (metadata is JArrayTypeMetadata arrayMetadata)
			JMetadataHelper.Register(arrayMetadata.ElementMetadata as JReferenceTypeMetadata);
		return JMetadataHelper.runtimeMetadata.TryAdd(metadata.Hash, metadata);
	}
	/// <summary>
	/// Escapes Java class name char to JNI class name.
	/// </summary>
	/// <param name="classNameChar">A Java class name char.</param>
	/// <returns>A JNI class name char.</returns>
	private static Byte EscapeClassNameChar(Byte classNameChar) 
		=> classNameChar == JMetadataHelper.classNameEscape[0] ? JMetadataHelper.classNameEscape[1] : classNameChar;
}