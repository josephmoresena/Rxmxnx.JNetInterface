namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
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
			AssignationKey assignationKey = new() { FromHash = metadata.Hash, ToHash = metadata.BaseMetadata.Hash, };
			MetadataHelper.assignationCache[assignationKey] = true;
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
		AssignationKey assignationKey = new() { FromHash = metadata.Hash, ToHash = interfaceMetadata.Hash, };
		MetadataHelper.assignationCache[assignationKey] = true;
		MetadataHelper.Register(interfaceMetadata);
	}
	/// <summary>
	/// Creates runtime metadata cache.
	/// </summary>
	/// <returns>Runtime metadata cache.</returns>
	private static ConcurrentDictionary<String, JReferenceTypeMetadata> CreateRuntimeMetadata()
	{
		ConcurrentDictionary<String, JReferenceTypeMetadata> result = new(MetadataHelper.initialMetadata);
		if (JVirtualMachine.BuiltInThrowableAutoRegistered) MetadataHelper.BuiltInThrowableRegistration(result);
		if (JVirtualMachine.ReflectionAutoRegistered) MetadataHelper.ReflectionRegistration(result);
		if (JVirtualMachine.NioAutoRegistered) MetadataHelper.NioRegistration(result);
		return result;
	}
	/// <summary>
	/// Registers all built-in throwable types metadata.
	/// </summary>
	/// <param name="result">Runtime metadata cache.</param>
	private static void BuiltInThrowableRegistration(IDictionary<String, JReferenceTypeMetadata> result)
	{
		result.Add(IDataType.GetHash<JLinkageErrorObject>(), IReferenceType.GetMetadata<JLinkageErrorObject>());
		result.Add(IDataType.GetHash<JClassCircularityErrorObject>(),
		           IReferenceType.GetMetadata<JClassCircularityErrorObject>());
		result.Add(IDataType.GetHash<JClassFormatErrorObject>(), IReferenceType.GetMetadata<JClassFormatErrorObject>());
		result.Add(IDataType.GetHash<JExceptionInInitializerErrorObject>(),
		           IReferenceType.GetMetadata<JExceptionInInitializerErrorObject>());
		result.Add(IDataType.GetHash<JIncompatibleClassChangeErrorObject>(),
		           IReferenceType.GetMetadata<JIncompatibleClassChangeErrorObject>());
		result.Add(IDataType.GetHash<JNoSuchFieldErrorObject>(), IReferenceType.GetMetadata<JNoSuchFieldErrorObject>());
		result.Add(IDataType.GetHash<JNoSuchMethodErrorObject>(),
		           IReferenceType.GetMetadata<JNoSuchMethodErrorObject>());
		result.Add(IDataType.GetHash<JNoClassDefFoundErrorObject>(),
		           IReferenceType.GetMetadata<JNoClassDefFoundErrorObject>());
		result.Add(IDataType.GetHash<JVirtualMachineErrorObject>(),
		           IReferenceType.GetMetadata<JVirtualMachineErrorObject>());
		result.Add(IDataType.GetHash<JOutOfMemoryErrorObject>(), IReferenceType.GetMetadata<JOutOfMemoryErrorObject>());
		result.Add(IDataType.GetHash<JSecurityExceptionObject>(),
		           IReferenceType.GetMetadata<JSecurityExceptionObject>());
		result.Add(IDataType.GetHash<JReflectiveOperationExceptionObject>(),
		           IReferenceType.GetMetadata<JReflectiveOperationExceptionObject>());
		result.Add(IDataType.GetHash<JInstantiationExceptionObject>(),
		           IReferenceType.GetMetadata<JInstantiationExceptionObject>());
		result.Add(IDataType.GetHash<JClassNotFoundExceptionObject>(),
		           IReferenceType.GetMetadata<JClassNotFoundExceptionObject>());
		result.Add(IDataType.GetHash<JNullPointerExceptionObject>(),
		           IReferenceType.GetMetadata<JNullPointerExceptionObject>());
		result.Add(IDataType.GetHash<JIndexOutOfBoundsExceptionObject>(),
		           IReferenceType.GetMetadata<JIndexOutOfBoundsExceptionObject>());
		result.Add(IDataType.GetHash<JArrayIndexOutOfBoundsExceptionObject>(),
		           IReferenceType.GetMetadata<JArrayIndexOutOfBoundsExceptionObject>());
		result.Add(IDataType.GetHash<JStringIndexOutOfBoundsExceptionObject>(),
		           IReferenceType.GetMetadata<JStringIndexOutOfBoundsExceptionObject>());
		result.Add(IDataType.GetHash<JArrayStoreExceptionObject>(),
		           IReferenceType.GetMetadata<JArrayStoreExceptionObject>());
	}
	/// <summary>
	/// Registers all reflection types metadata.
	/// </summary>
	/// <param name="result">Runtime metadata cache.</param>
	private static void ReflectionRegistration(IDictionary<String, JReferenceTypeMetadata> result)
	{
		// Classes
		result.Add(IDataType.GetHash<JClassLoaderObject>(), IReferenceType.GetMetadata<JClassLoaderObject>());
		result.Add(IDataType.GetHash<JThreadObject>(), IReferenceType.GetMetadata<JThreadObject>());
		result.Add(IDataType.GetHash<JModuleObject>(), IReferenceType.GetMetadata<JModuleObject>());
		result.Add(IDataType.GetHash<JModifierObject>(), IReferenceType.GetMetadata<JModifierObject>());
		result.Add(IDataType.GetHash<JAccessibleObject>(), IReferenceType.GetMetadata<JAccessibleObject>());
		result.Add(IDataType.GetHash<JExecutableObject>(), IReferenceType.GetMetadata<JExecutableObject>());
		result.Add(IDataType.GetHash<JMethodObject>(), IReferenceType.GetMetadata<JMethodObject>());
		result.Add(IDataType.GetHash<JConstructorObject>(), IReferenceType.GetMetadata<JConstructorObject>());
		// Interfaces
		result.Add(IDataType.GetHash<JMemberObject>(), IReferenceType.GetMetadata<JMemberObject>());
		result.Add(IDataType.GetHash<JRunnableObject>(), IReferenceType.GetMetadata<JRunnableObject>());
		// Enums
		result.Add(IDataType.GetHash<JElementTypeObject>(), IReferenceType.GetMetadata<JElementTypeObject>());
		// Annotation
		result.Add(IDataType.GetHash<JTargetObject>(), IReferenceType.GetMetadata<JTargetObject>());
	}
	/// <summary>
	/// Registers all NIO types metadata.
	/// </summary>
	/// <param name="result">Runtime metadata cache.</param>
	private static void NioRegistration(IDictionary<String, JReferenceTypeMetadata> result)
	{
		// Classes
		result.Add(IDataType.GetHash<JBufferObject>(), IReferenceType.GetMetadata<JBufferObject>());
		result.Add(IDataType.GetHash<JByteBufferObject>(), IReferenceType.GetMetadata<JByteBufferObject>());
		result.Add(IDataType.GetHash<JCharBufferObject>(), IReferenceType.GetMetadata<JCharBufferObject>());
		result.Add(IDataType.GetHash<JDoubleBufferObject>(), IReferenceType.GetMetadata<JDoubleBufferObject>());
		result.Add(IDataType.GetHash<JFloatBufferObject>(), IReferenceType.GetMetadata<JFloatBufferObject>());
		result.Add(IDataType.GetHash<JIntBufferObject>(), IReferenceType.GetMetadata<JIntBufferObject>());
		result.Add(IDataType.GetHash<JLongBufferObject>(), IReferenceType.GetMetadata<JLongBufferObject>());
		result.Add(IDataType.GetHash<JShortBufferObject>(), IReferenceType.GetMetadata<JShortBufferObject>());
		result.Add(IDataType.GetHash<JMappedByteBufferObject>(), IReferenceType.GetMetadata<JMappedByteBufferObject>());
		result.Add(IDataType.GetHash<JDirectByteBufferObject>(), IReferenceType.GetMetadata<JDirectByteBufferObject>());
		// Interfaces
		result.Add(IDataType.GetHash<JAppendableObject>(), IReferenceType.GetMetadata<JAppendableObject>());
		result.Add(IDataType.GetHash<JReadableObject>(), IReferenceType.GetMetadata<JReadableObject>());
		result.Add(IDataType.GetHash<JDirectBufferObject>(), IReferenceType.GetMetadata<JDirectBufferObject>());
	}
}