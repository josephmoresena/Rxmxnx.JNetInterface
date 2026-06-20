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

		// Array Objects //
		{ MetadataHelper.ObjectArrayMetadata.Hash, MetadataHelper.ObjectArrayMetadata },
		{ MetadataHelper.ObjectArrayArrayMetadata.Hash, MetadataHelper.ObjectArrayArrayMetadata },

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
	/// Basic metadata dictionary.
	/// </summary>
	private static readonly Dictionary<String, Boolean> initialBuiltIn = new()
	{
		// Basic objects //
		{ IDataType.GetHash<JLocalObject>(), false },
		{ IDataType.GetHash<JClassObject>(), false },
		{ IDataType.GetHash<JStringObject>(), false },
		{ IDataType.GetHash<JNumberObject>(), false },
		{ IDataType.GetHash<JEnumObject>(), false },
		{ IDataType.GetHash<JThrowableObject>(), false },
		{ IDataType.GetHash<JStackTraceElementObject>(), false },
		{ IDataType.GetHash<JExceptionObject>(), false },
		{ IDataType.GetHash<JRuntimeExceptionObject>(), false },
		{ IDataType.GetHash<JErrorObject>(), false },

		// Wrapper objects //
		{ IDataType.GetHash<JVoidObject>(), false },
		{ IDataType.GetHash<JBooleanObject>(), false },
		{ IDataType.GetHash<JByteObject>(), false },
		{ IDataType.GetHash<JCharacterObject>(), false },
		{ IDataType.GetHash<JDoubleObject>(), false },
		{ IDataType.GetHash<JFloatObject>(), false },
		{ IDataType.GetHash<JIntegerObject>(), false },
		{ IDataType.GetHash<JLongObject>(), false },
		{ IDataType.GetHash<JShortObject>(), false },

		// Array Objects //
		{ MetadataHelper.ObjectArrayMetadata.Hash, false },
		{ MetadataHelper.ObjectArrayArrayMetadata.Hash, false },

		// Primitive arrays //
		{ IDataType.GetHash<JArrayObject<JBoolean>>(), false },
		{ IDataType.GetHash<JArrayObject<JByte>>(), false },
		{ IDataType.GetHash<JArrayObject<JChar>>(), false },
		{ IDataType.GetHash<JArrayObject<JDouble>>(), false },
		{ IDataType.GetHash<JArrayObject<JFloat>>(), false },
		{ IDataType.GetHash<JArrayObject<JInt>>(), false },
		{ IDataType.GetHash<JArrayObject<JLong>>(), false },
		{ IDataType.GetHash<JArrayObject<JShort>>(), false },

		// Basic interfaces //
		{ IDataType.GetHash<JCharSequenceObject>(), false },
		{ IDataType.GetHash<JCloneableObject>(), false },
		{ IDataType.GetHash<JComparableObject>(), false },
		{ IDataType.GetHash<JSerializableObject>(), false },
		{ IDataType.GetHash<JAnnotatedElementObject>(), false },
		{ IDataType.GetHash<JGenericDeclarationObject>(), false },
		{ IDataType.GetHash<JTypeObject>(), false },
		{ IDataType.GetHash<JAnnotationObject>(), false },
	};
	/// <summary>
	/// Runtime metadata storage.
	/// </summary>
	private static readonly RuntimeMetadataStorage storage = new();
	/// <summary>
	/// Runtime metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, UnknownReflectionMetadata> reflectionMetadata = new();
}