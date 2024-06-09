namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
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