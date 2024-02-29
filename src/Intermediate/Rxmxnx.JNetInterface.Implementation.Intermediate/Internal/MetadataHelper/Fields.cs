namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	/// <summary>
	/// Separator class assignability.
	/// </summary>
	private static readonly CString assignableTo = new(() => " -> "u8);

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
		{ IDataType.GetHash<JClassLoaderObject>(), IReferenceType.GetMetadata<JClassLoaderObject>() },
		{ IDataType.GetHash<JBufferObject>(), IReferenceType.GetMetadata<JBufferObject>() },
		{ IDataType.GetHash<JAccessibleObject>(), IReferenceType.GetMetadata<JAccessibleObject>() },
		{ IDataType.GetHash<JExecutableObject>(), IReferenceType.GetMetadata<JExecutableObject>() },
		{ IDataType.GetHash<JMethodObject>(), IReferenceType.GetMetadata<JMethodObject>() },
		{ IDataType.GetHash<JConstructorObject>(), IReferenceType.GetMetadata<JConstructorObject>() },
		{ IDataType.GetHash<JFieldObject>(), IReferenceType.GetMetadata<JFieldObject>() },
		{ IDataType.GetHash<JExceptionObject>(), IReferenceType.GetMetadata<JExceptionObject>() },
		{ IDataType.GetHash<JRuntimeExceptionObject>(), IReferenceType.GetMetadata<JRuntimeExceptionObject>() },
		{ IDataType.GetHash<JErrorObject>(), IReferenceType.GetMetadata<JErrorObject>() },
		{ IDataType.GetHash<JModifierObject>(), IReferenceType.GetMetadata<JModifierObject>() },

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

		// Basic object arrays //
		{ IDataType.GetHash<JArrayObject<JLocalObject>>(), IReferenceType.GetMetadata<JArrayObject<JLocalObject>>() },
		{ IDataType.GetHash<JArrayObject<JClassObject>>(), IReferenceType.GetMetadata<JArrayObject<JClassObject>>() },
		{ IDataType.GetHash<JArrayObject<JStringObject>>(), IReferenceType.GetMetadata<JArrayObject<JStringObject>>() },
		{ IDataType.GetHash<JArrayObject<JNumberObject>>(), IReferenceType.GetMetadata<JArrayObject<JNumberObject>>() },
		{ IDataType.GetHash<JArrayObject<JEnumObject>>(), IReferenceType.GetMetadata<JArrayObject<JEnumObject>>() },
		{
			IDataType.GetHash<JArrayObject<JThrowableObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JThrowableObject>>()
		},
		{
			IDataType.GetHash<JArrayObject<JStackTraceElementObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JStackTraceElementObject>>()
		},
		{
			IDataType.GetHash<JArrayObject<JClassLoaderObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JClassLoaderObject>>()
		},
		{ IDataType.GetHash<JArrayObject<JBufferObject>>(), IReferenceType.GetMetadata<JArrayObject<JBufferObject>>() },
		{
			IDataType.GetHash<JArrayObject<JAccessibleObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JAccessibleObject>>()
		},
		{
			IDataType.GetHash<JArrayObject<JExecutableObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JExecutableObject>>()
		},
		{ IDataType.GetHash<JArrayObject<JMethodObject>>(), IReferenceType.GetMetadata<JArrayObject<JMethodObject>>() },
		{
			IDataType.GetHash<JArrayObject<JConstructorObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JConstructorObject>>()
		},
		{ IDataType.GetHash<JArrayObject<JFieldObject>>(), IReferenceType.GetMetadata<JArrayObject<JFieldObject>>() },
		{
			IDataType.GetHash<JArrayObject<JExceptionObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JExceptionObject>>()
		},
		{
			IDataType.GetHash<JArrayObject<JRuntimeExceptionObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JRuntimeExceptionObject>>()
		},
		{ IDataType.GetHash<JArrayObject<JErrorObject>>(), IReferenceType.GetMetadata<JArrayObject<JErrorObject>>() },

		// Wrapper object arrays //
		{
			IDataType.GetHash<JArrayObject<JBooleanObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JBooleanObject>>()
		},
		{ IDataType.GetHash<JArrayObject<JByteObject>>(), IReferenceType.GetMetadata<JArrayObject<JByteObject>>() },
		{
			IDataType.GetHash<JArrayObject<JCharacterObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JCharacterObject>>()
		},
		{ IDataType.GetHash<JArrayObject<JDoubleObject>>(), IReferenceType.GetMetadata<JArrayObject<JDoubleObject>>() },
		{ IDataType.GetHash<JArrayObject<JFloatObject>>(), IReferenceType.GetMetadata<JArrayObject<JFloatObject>>() },
		{
			IDataType.GetHash<JArrayObject<JIntegerObject>>(),
			IReferenceType.GetMetadata<JArrayObject<JIntegerObject>>()
		},
		{ IDataType.GetHash<JArrayObject<JLongObject>>(), IReferenceType.GetMetadata<JArrayObject<JLongObject>>() },
		{ IDataType.GetHash<JArrayObject<JShortObject>>(), IReferenceType.GetMetadata<JArrayObject<JShortObject>>() },

		// NIO Objects //
		{ IDataType.GetHash<JByteBufferObject>(), IReferenceType.GetMetadata<JByteBufferObject>() },
		{ IDataType.GetHash<JMappedByteBufferObject>(), IReferenceType.GetMetadata<JMappedByteBufferObject>() },
		{ IDataType.GetHash<JDirectByteBufferObject>(), IReferenceType.GetMetadata<JDirectByteBufferObject>() },

		// Basic interfaces //
		{ IDataType.GetHash<JCharSequenceObject>(), IReferenceType.GetMetadata<JCharSequenceObject>() },
		{ IDataType.GetHash<JCloneableObject>(), IReferenceType.GetMetadata<JCloneableObject>() },
		{ IDataType.GetHash<JComparableObject>(), IReferenceType.GetMetadata<JComparableObject>() },
		{ IDataType.GetHash<JSerializableObject>(), IReferenceType.GetMetadata<JSerializableObject>() },
		{ IDataType.GetHash<JAnnotatedElementObject>(), IReferenceType.GetMetadata<JAnnotatedElementObject>() },
		{ IDataType.GetHash<JGenericDeclarationObject>(), IReferenceType.GetMetadata<JGenericDeclarationObject>() },
		{ IDataType.GetHash<JTypeObject>(), IReferenceType.GetMetadata<JTypeObject>() },
		{ IDataType.GetHash<JMemberObject>(), IReferenceType.GetMetadata<JMemberObject>() },
		{ IDataType.GetHash<JDirectBufferObject>(), IReferenceType.GetMetadata<JDirectBufferObject>() },
		{ IDataType.GetHash<JAnnotationObject>(), IReferenceType.GetMetadata<JAnnotationObject>() },

		// Basic Throwable Objects //
		{ IDataType.GetHash<JLinkageErrorObject>(), IReferenceType.GetMetadata<JLinkageErrorObject>() },
		{
			IDataType.GetHash<JClassCircularityErrorObject>(),
			IReferenceType.GetMetadata<JClassCircularityErrorObject>()
		},
		{ IDataType.GetHash<JClassFormatErrorObject>(), IReferenceType.GetMetadata<JClassFormatErrorObject>() },
		{
			IDataType.GetHash<JExceptionInInitializerErrorObject>(),
			IReferenceType.GetMetadata<JExceptionInInitializerErrorObject>()
		},
		{
			IDataType.GetHash<JIncompatibleClassChangeErrorObject>(),
			IReferenceType.GetMetadata<JIncompatibleClassChangeErrorObject>()
		},
		{ IDataType.GetHash<JNoSuchFieldErrorObject>(), IReferenceType.GetMetadata<JNoSuchFieldErrorObject>() },
		{ IDataType.GetHash<JNoSuchMethodErrorObject>(), IReferenceType.GetMetadata<JNoSuchMethodErrorObject>() },
		{ IDataType.GetHash<JVirtualMachineErrorObject>(), IReferenceType.GetMetadata<JVirtualMachineErrorObject>() },
		{ IDataType.GetHash<JOutOfMemoryErrorObject>(), IReferenceType.GetMetadata<JOutOfMemoryErrorObject>() },
		{ IDataType.GetHash<JSecurityExceptionObject>(), IReferenceType.GetMetadata<JSecurityExceptionObject>() },
		{
			IDataType.GetHash<JReflectiveOperationExceptionObject>(),
			IReferenceType.GetMetadata<JReflectiveOperationExceptionObject>()
		},
		{
			IDataType.GetHash<JInstantiationExceptionObject>(),
			IReferenceType.GetMetadata<JInstantiationExceptionObject>()
		},
		{
			IDataType.GetHash<JIndexOutOfBoundsExceptionObject>(),
			IReferenceType.GetMetadata<JIndexOutOfBoundsExceptionObject>()
		},
		{
			IDataType.GetHash<JArrayIndexOutOfBoundsExceptionObject>(),
			IReferenceType.GetMetadata<JArrayIndexOutOfBoundsExceptionObject>()
		},
		{
			IDataType.GetHash<JStringIndexOutOfBoundsExceptionObject>(),
			IReferenceType.GetMetadata<JStringIndexOutOfBoundsExceptionObject>()
		},
		{ IDataType.GetHash<JArrayStoreExceptionObject>(), IReferenceType.GetMetadata<JArrayStoreExceptionObject>() },
	};
	/// <summary>
	/// Primitive reflection dictionary.
	/// </summary>
	private static readonly Dictionary<String, IReflectionMetadata?> primitiveReflectionMetadata = new()
	{
		// Basic objects //
		{ JPrimitiveTypeMetadata.FakeVoidHash, default },
		{ PrimitiveReflectionMetadata<JBoolean>.FakeHash, PrimitiveReflectionMetadata<JBoolean>.Instance },
		{ PrimitiveReflectionMetadata<JByte>.FakeHash, PrimitiveReflectionMetadata<JByte>.Instance },
		{ PrimitiveReflectionMetadata<JChar>.FakeHash, PrimitiveReflectionMetadata<JChar>.Instance },
		{ PrimitiveReflectionMetadata<JDouble>.FakeHash, PrimitiveReflectionMetadata<JDouble>.Instance },
		{ PrimitiveReflectionMetadata<JFloat>.FakeHash, PrimitiveReflectionMetadata<JFloat>.Instance },
		{ PrimitiveReflectionMetadata<JInt>.FakeHash, PrimitiveReflectionMetadata<JInt>.Instance },
		{ PrimitiveReflectionMetadata<JLong>.FakeHash, PrimitiveReflectionMetadata<JLong>.Instance },
		{ PrimitiveReflectionMetadata<JShort>.FakeHash, PrimitiveReflectionMetadata<JShort>.Instance },
	};

	/// <summary>
	/// Runtime metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, JReferenceTypeMetadata> runtimeMetadata =
		new(MetadataHelper.initialMetadata);
	/// <summary>
	/// Runtime metadata dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, IReflectionMetadata> reflectionMetadata = new();
	/// <summary>
	/// Runtime metadata assignation dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, Boolean?> assignationCache = new();
}