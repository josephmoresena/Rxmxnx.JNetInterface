namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	/// <summary>
	/// Object metadata.
	/// </summary>
	public static readonly JClassTypeMetadata ObjectMetadata = IClassType.GetMetadata<JLocalObject>();
	/// <summary>
	/// Object array metadata.
	/// </summary>
	public static readonly JArrayTypeMetadata ObjectArrayMetadata = IArrayType.GetArrayMetadata<JLocalObject>();
	/// <summary>
	/// Object array array metadata.
	/// </summary>
	public static readonly JArrayTypeMetadata ObjectArrayArrayMetadata =
		IArrayType.GetArrayArrayMetadata<JLocalObject>();
	/// <summary>
	/// Hashes of main classes.
	/// </summary>
	public static readonly IImmutableSet<String> MainClassHashes = ImmutableHashSet.Create(
		IDataType.GetHash<JClassObject>(), IDataType.GetHash<JThrowableObject>(),
		IDataType.GetHash<JStackTraceElementObject>(), ClassObjectMetadata.VoidMetadata.Hash,
		IDataType.GetHash<JBoolean>(), IDataType.GetHash<JByte>(), IDataType.GetHash<JChar>(),
		IDataType.GetHash<JDouble>(), IDataType.GetHash<JFloat>(), IDataType.GetHash<JInt>(),
		IDataType.GetHash<JLong>(), IDataType.GetHash<JShort>());

	/// <summary>
	/// Basic metadata dictionary.
	/// </summary>
	private static readonly Dictionary<String, JReferenceTypeMetadata> initialMetadata = new()
	{
		// Basic objects //
		{ IDataType.GetHash<JLocalObject>(), IReferenceType.GetMetadata<JLocalObject>() },
		{ IDataType.GetHash<JClassObject>(), IReferenceType.GetMetadata<JClassObject>() },
		{ IDataType.GetHash<JStringObject>(), IReferenceType.GetMetadata<JStringObject>() },
		{ IDataType.GetHash<JNumberObject>(), IReferenceType.GetMetadata<JNumberObject>() },
		{ IDataType.GetHash<JEnumObject>(), IReferenceType.GetMetadata<JEnumObject>() },
		{ IDataType.GetHash<JThrowableObject>(), IReferenceType.GetMetadata<JThrowableObject>() },
		{ IDataType.GetHash<JStackTraceElementObject>(), IReferenceType.GetMetadata<JStackTraceElementObject>() },
		{ IDataType.GetHash<JExceptionObject>(), IReferenceType.GetMetadata<JExceptionObject>() },
		{ IDataType.GetHash<JRuntimeExceptionObject>(), IReferenceType.GetMetadata<JRuntimeExceptionObject>() },
		{ IDataType.GetHash<JErrorObject>(), IReferenceType.GetMetadata<JErrorObject>() },

		// Wrapper objects //
		{ IDataType.GetHash<JVoidObject>(), IReferenceType.GetMetadata<JVoidObject>() },
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

		// Basic interfaces //
		{ IDataType.GetHash<JCharSequenceObject>(), IReferenceType.GetMetadata<JCharSequenceObject>() },
		{ IDataType.GetHash<JCloneableObject>(), IReferenceType.GetMetadata<JCloneableObject>() },
		{ IDataType.GetHash<JComparableObject>(), IReferenceType.GetMetadata<JComparableObject>() },
		{ IDataType.GetHash<JSerializableObject>(), IReferenceType.GetMetadata<JSerializableObject>() },
		{ IDataType.GetHash<JAnnotatedElementObject>(), IReferenceType.GetMetadata<JAnnotatedElementObject>() },
		{ IDataType.GetHash<JGenericDeclarationObject>(), IReferenceType.GetMetadata<JGenericDeclarationObject>() },
		{ IDataType.GetHash<JTypeObject>(), IReferenceType.GetMetadata<JTypeObject>() },
		{ IDataType.GetHash<JAnnotationObject>(), IReferenceType.GetMetadata<JAnnotationObject>() },
	};
	/// <summary>
	/// Builtin metadata dictionary.
	/// </summary>
	private static readonly HashSet<String> builtInFinal = [];
	/// <summary>
	/// Runtime metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, JReferenceTypeMetadata> runtimeMetadata =
		MetadataHelper.CreateRuntimeMetadata();
	/// <summary>
	/// Runtime metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, UnknownReflectionMetadata> reflectionMetadata = new();
	/// <summary>
	/// Runtime metadata assignation dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<AssignationKey, Boolean> assignationCache = new();

	/// <summary>
	/// Runtime class metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, String> classTree = new();
	/// <summary>
	/// Runtime view metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, HashesSet> viewTree = new();
}