namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	private sealed partial class RuntimeMetadataStorage
	{
		/// <summary>
		/// Registers all built-in throwable types metadata.
		/// </summary>
		/// <param name="result">Runtime metadata cache.</param>
		private static void BuiltInThrowableRegistration(IDictionary<String, JReferenceTypeMetadata> result)
		{
			RuntimeMetadataStorage.InitialRegister<JLinkageErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JClassCircularityErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JUnsatisfiedLinkErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JClassFormatErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JExceptionInInitializerErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JIncompatibleClassChangeErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JNoSuchFieldErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JNoSuchMethodErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JNoClassDefFoundErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JVirtualMachineErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JInternalErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JOutOfMemoryErrorObject>(result);
			RuntimeMetadataStorage.InitialRegister<JSecurityExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JInterruptedExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JParseExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JIoExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JFileNotFoundExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JMalformedUrlExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JReflectiveOperationExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JInstantiationExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JClassNotFoundExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JIllegalAccessExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JInvocationTargetExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JArrayStoreExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JNullPointerExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JIllegalStateExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JClassCastExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JArithmeticExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JIllegalArgumentExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JNumberFormatExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JIndexOutOfBoundsExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JArrayIndexOutOfBoundsExceptionObject>(result);
			RuntimeMetadataStorage.InitialRegister<JStringIndexOutOfBoundsExceptionObject>(result);
		}
		/// <summary>
		/// Registers all reflection types metadata.
		/// </summary>
		/// <param name="result">Runtime metadata cache.</param>
		private static void ReflectionRegistration(IDictionary<String, JReferenceTypeMetadata> result)
		{
			// Classes
			RuntimeMetadataStorage.InitialRegister<JClassLoaderObject>(result);
			RuntimeMetadataStorage.InitialRegister<JThreadObject>(result);
			RuntimeMetadataStorage.InitialRegister<JModuleObject>(result);
			RuntimeMetadataStorage.InitialRegister<JModifierObject>(result);
			RuntimeMetadataStorage.InitialRegister<JAccessibleObject>(result);
			RuntimeMetadataStorage.InitialRegister<JExecutableObject>(result);
			RuntimeMetadataStorage.InitialRegister<JMethodObject>(result);
			RuntimeMetadataStorage.InitialRegister<JConstructorObject>(result);
			// Interfaces
			RuntimeMetadataStorage.InitialRegister<JMemberObject>(result);
			RuntimeMetadataStorage.InitialRegister<JRunnableObject>(result);
			// Enums
			RuntimeMetadataStorage.InitialRegister<JElementTypeObject>(result);
			// Annotations
			RuntimeMetadataStorage.InitialRegister<JTargetObject>(result);
		}
		/// <summary>
		/// Registers all NIO types metadata.
		/// </summary>
		/// <param name="result">Runtime metadata cache.</param>
		private static void NioRegistration(IDictionary<String, JReferenceTypeMetadata> result)
		{
			// Classes
			RuntimeMetadataStorage.InitialRegister<JBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JByteBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JCharBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JDoubleBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JFloatBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JIntBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JLongBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JShortBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JMappedByteBufferObject>(result);
			RuntimeMetadataStorage.InitialRegister<JDirectByteBufferObject>(result);
			// Interfaces
			RuntimeMetadataStorage.InitialRegister<JAppendableObject>(result);
			RuntimeMetadataStorage.InitialRegister<JReadableObject>(result);
			RuntimeMetadataStorage.InitialRegister<JDirectBufferObject>(result);
		}
		/// <summary>
		/// Registers <typeparamref name="TReference"/> metadata.
		/// </summary>
		/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
		/// <param name="result">Runtime metadata cache.</param>
		private static void
			InitialRegister<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>(
				IDictionary<String, JReferenceTypeMetadata> result)
			where TReference : JReferenceObject, IReferenceType<TReference>
		{
			JReferenceTypeMetadata typeMetadata = IReferenceType.GetMetadata<TReference>();
			result.Add(typeMetadata.Hash, typeMetadata);
		}

		/// <summary>
		/// State for interface assignation.
		/// </summary>
		private readonly struct InterfaceAssignationState
		{
			public String FromHash { get; init; }
			public ConcurrentDictionary<AssignationKey, Boolean> AssignationCache { get; init; }
		}
	}
}