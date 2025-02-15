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
		MetadataHelper.InitialRegister<JLinkageErrorObject>(result);
		MetadataHelper.InitialRegister<JClassCircularityErrorObject>(result);
		MetadataHelper.InitialRegister<JUnsatisfiedLinkErrorObject>(result);
		MetadataHelper.InitialRegister<JClassFormatErrorObject>(result);
		MetadataHelper.InitialRegister<JExceptionInInitializerErrorObject>(result);
		MetadataHelper.InitialRegister<JIncompatibleClassChangeErrorObject>(result);
		MetadataHelper.InitialRegister<JNoSuchFieldErrorObject>(result);
		MetadataHelper.InitialRegister<JNoSuchMethodErrorObject>(result);
		MetadataHelper.InitialRegister<JNoClassDefFoundErrorObject>(result);
		MetadataHelper.InitialRegister<JVirtualMachineErrorObject>(result);
		MetadataHelper.InitialRegister<JInternalErrorObject>(result);
		MetadataHelper.InitialRegister<JOutOfMemoryErrorObject>(result);
		MetadataHelper.InitialRegister<JSecurityExceptionObject>(result);
		MetadataHelper.InitialRegister<JInterruptedExceptionObject>(result);
		MetadataHelper.InitialRegister<JParseExceptionObject>(result);
		MetadataHelper.InitialRegister<JIoExceptionObject>(result);
		MetadataHelper.InitialRegister<JFileNotFoundExceptionObject>(result);
		MetadataHelper.InitialRegister<JMalformedUrlExceptionObject>(result);
		MetadataHelper.InitialRegister<JReflectiveOperationExceptionObject>(result);
		MetadataHelper.InitialRegister<JInstantiationExceptionObject>(result);
		MetadataHelper.InitialRegister<JClassNotFoundExceptionObject>(result);
		MetadataHelper.InitialRegister<JIllegalAccessExceptionObject>(result);
		MetadataHelper.InitialRegister<JInvocationTargetExceptionObject>(result);
		MetadataHelper.InitialRegister<JArrayStoreExceptionObject>(result);
		MetadataHelper.InitialRegister<JNullPointerExceptionObject>(result);
		MetadataHelper.InitialRegister<JIllegalStateExceptionObject>(result);
		MetadataHelper.InitialRegister<JClassCastExceptionObject>(result);
		MetadataHelper.InitialRegister<JArithmeticExceptionObject>(result);
		MetadataHelper.InitialRegister<JIllegalArgumentExceptionObject>(result);
		MetadataHelper.InitialRegister<JNumberFormatExceptionObject>(result);
		MetadataHelper.InitialRegister<JIndexOutOfBoundsExceptionObject>(result);
		MetadataHelper.InitialRegister<JArrayIndexOutOfBoundsExceptionObject>(result);
		MetadataHelper.InitialRegister<JStringIndexOutOfBoundsExceptionObject>(result);
	}
	/// <summary>
	/// Registers all reflection types metadata.
	/// </summary>
	/// <param name="result">Runtime metadata cache.</param>
	private static void ReflectionRegistration(IDictionary<String, JReferenceTypeMetadata> result)
	{
		// Classes
		MetadataHelper.InitialRegister<JClassLoaderObject>(result);
		MetadataHelper.InitialRegister<JThreadObject>(result);
		MetadataHelper.InitialRegister<JModuleObject>(result);
		MetadataHelper.InitialRegister<JModifierObject>(result);
		MetadataHelper.InitialRegister<JAccessibleObject>(result);
		MetadataHelper.InitialRegister<JExecutableObject>(result);
		MetadataHelper.InitialRegister<JMethodObject>(result);
		MetadataHelper.InitialRegister<JConstructorObject>(result);
		// Interfaces
		MetadataHelper.InitialRegister<JMemberObject>(result);
		MetadataHelper.InitialRegister<JRunnableObject>(result);
		// Enums
		MetadataHelper.InitialRegister<JElementTypeObject>(result);
		// Annotations
		MetadataHelper.InitialRegister<JTargetObject>(result);
	}
	/// <summary>
	/// Registers all NIO types metadata.
	/// </summary>
	/// <param name="result">Runtime metadata cache.</param>
	private static void NioRegistration(IDictionary<String, JReferenceTypeMetadata> result)
	{
		// Classes
		MetadataHelper.InitialRegister<JBufferObject>(result);
		MetadataHelper.InitialRegister<JByteBufferObject>(result);
		MetadataHelper.InitialRegister<JCharBufferObject>(result);
		MetadataHelper.InitialRegister<JDoubleBufferObject>(result);
		MetadataHelper.InitialRegister<JFloatBufferObject>(result);
		MetadataHelper.InitialRegister<JIntBufferObject>(result);
		MetadataHelper.InitialRegister<JLongBufferObject>(result);
		MetadataHelper.InitialRegister<JShortBufferObject>(result);
		MetadataHelper.InitialRegister<JMappedByteBufferObject>(result);
		MetadataHelper.InitialRegister<JDirectByteBufferObject>(result);
		// Interfaces
		MetadataHelper.InitialRegister<JAppendableObject>(result);
		MetadataHelper.InitialRegister<JReadableObject>(result);
		MetadataHelper.InitialRegister<JDirectBufferObject>(result);
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
		if (typeMetadata.Modifier == JTypeModifier.Final)
			MetadataHelper.builtInFinal.Add(typeMetadata.Hash);
	}
	/// <summary>
	/// Indicates whether the type of <paramref name="typeMetadata"/> is built-in final type.
	/// </summary>
	/// <param name="typeMetadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the type of <paramref name="typeMetadata"/> is built-in final type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	private static Boolean IsBuiltInFinalType(JDataTypeMetadata typeMetadata)
	{
		if (typeMetadata is JEnumTypeMetadata or JPrimitiveTypeMetadata) return true;
		if (JProxyObject.ProxyTypeMetadata.Equals((typeMetadata as JReferenceTypeMetadata)?.BaseMetadata))
			return true;
		if (MetadataHelper.initialMetadata.ContainsKey(typeMetadata.Hash) ||
		    MetadataHelper.builtInFinal.Contains(typeMetadata.Hash)) return true;
		return typeMetadata is JArrayTypeMetadata arrayTypeMetadata &&
			MetadataHelper.IsBuiltInFinalType(arrayTypeMetadata.ElementMetadata);
	}
}