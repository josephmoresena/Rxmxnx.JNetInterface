namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Spanish message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class SpanishMessageResource : IMessageResource
{
	/// <summary>
	/// Singleton instance of <see cref="SpanishMessageResource"/>.
	/// </summary>
	private static readonly SpanishMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => SpanishMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private SpanishMessageResource() { }

	String IMessageResource.InvalidType => "El valor solicitado no puede estar contenido en un JValue.";
	String IMessageResource.VoidArgument => "El tipo void no puede usarse como argumento de instancia.";
	String IMessageResource.VoidInstantiation => "El tipo void no puede instanciarse.";
	String IMessageResource.VoidArray => "El tipo void no puede usarse como tipo de elemento de un array.";
	String IMessageResource.VoidEquality => "Una instancia de Void no puede ser equiparable.";
	String IMessageResource.SignatureNotAllowed => "La firma no está permitida.";
	String IMessageResource.CriticalExceptionMessage
		=> "La ejecución está en un estado crítico. Hay una excepción pendiente, pero no se pueden realizar llamadas a través de la interfaz nativa.";
	String IMessageResource.UnknownExceptionMessage
		=> "Hay una excepción pendiente, pero no se pudo crear la instancia de ThrowableException.";
	String IMessageResource.InvalidSignatureMessage => "Firma no válida.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "La definición no es un tipo primitivo.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Tipo primitivo no válido.";
	String IMessageResource.ProxyOnNonProxyProcess => "Un objeto proxy no puede procesar metadatos no proxy.";
	String IMessageResource.NonProxyOnProxyProcess => "Un objeto no proxy no puede procesar metadatos proxy.";
	String IMessageResource.NotConstructorDefinition => "La definición de la llamada actual no es un constructor.";
	String IMessageResource.NotPrimitiveObject => "El objeto no es primitivo.";
	String IMessageResource.InvalidProxyObject => "El objeto Java debe ser un proxy para realizar esta operación.";
	String IMessageResource.InvalidGlobalObject => "Objeto global no válido.";
	String IMessageResource.OnlyLocalReferencesAllowed => "Las llamadas JNI solo permiten referencias locales.";
	String IMessageResource.ClassRedefinition => "No se admite la redefinición de una clase.";
	String IMessageResource.StackTraceFixed => "El marco de pila actual es inmutable.";
	String IMessageResource.InvalidClass => "Objeto de clase no válido.";
	String IMessageResource.UnloadedClass => "La clase ha sido descargada.";
	String IMessageResource.NotClassObject => "El objeto no es una clase.";
	String IMessageResource.SingleReferenceTransaction => "Esta transacción solo puede contener una referencia.";
	String IMessageResource.InvalidReferenceObject => "JReferenceObject no válido.";
	String IMessageResource.InvalidObjectMetadata => "Metadatos de objeto no válidos.";
	String IMessageResource.DisposedObject => "JReferenceObject ha sido eliminado.";
	String IMessageResource.InvalidArrayLength => "La longitud del array debe ser cero o positiva.";
	String IMessageResource.DeadVirtualMachine => "La JVM actual no está en ejecución.";
	String IMessageResource.NotAttachedThread => "El hilo actual no está adjunto a la JVM.";
	String IMessageResource.IncompatibleLibrary => "Biblioteca JVM incompatible.";
	String IMessageResource.UnmanagedMemoryContext => "El bloque de memoria es no administrado.";

	String IMessageResource.InvalidInstantiation(CString className) => $"{className} no es un tipo instanciable.";
	String IMessageResource.InvalidCastTo(Type type) => $"Conversión no válida a {type}.";
	String IMessageResource.InvalidCastTo(CString className)
		=> $"La instancia actual no puede convertirse al tipo {className}.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} debe ser una cadena no vacía.";
	String IMessageResource.InvalidMetadata(CString className, Type typeOfT)
		=> $"Los metadatos del tipo de datos para {className} no coinciden con el tipo {typeOfT}.";
	String IMessageResource.AbstractClass(CString className) => $"{className} es un tipo abstracto.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} no puede extender una interfaz que lo extiende.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} y su superinterfaz no pueden ser la misma.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} y su superclase no pueden ser la misma.";
	String IMessageResource.AnnotationType(CString interfaceName, String annotationName)
		=> $"No se puede extender {interfaceName}. {annotationName} es una anotación.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className)
		=> $"{className} no implementa {interfaceName}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		CString missingSuperInterface)
		=> $"{className} no implementa {interfaceName}. Falta: {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{className} no implementa {interfaceName}. Faltan: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName)
		=> $"{interfaceName} no puede extender {superInterfaceName}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		CString missingSuperInterface)
		=> $"{interfaceName} no puede extender {superInterfaceName}. Falta: {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{interfaceName} no puede extender {superInterfaceName}. Faltan: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"El ordinal para {enumTypeName} debe ser cero o positivo.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} ya tiene un campo con ordinal {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName)
		=> $"El nombre para {enumTypeName} debe ser no vacío.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} ya tiene un campo llamado '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> SpanishMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{SpanishMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Valores faltantes: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} debe ser construido usando {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} no es un tipo de referencia válido.";
	String IMessageResource.NotTypeObject(CString objectClassName, CString className)
		=> $"{objectClassName} no es un objeto de tipo para {className}.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Error al crear una referencia global JNI para la clase {mainClassName}.";
	String IMessageResource.MainClassUnavailable(String mainClassName)
		=> $"La clase principal {mainClassName} no está disponible.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"La clase primitiva {primitiveClassName} no está disponible.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"Capacidad de transacción excedida: {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"Las definiciones no coinciden: {definition} vs {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"El entorno JNI ({envRef}) está asignado a otro hilo. Intento de operación en un hilo diferente: {threadId}. Hilo actual: {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"El entorno JNI actual está en un estado inválido para llamar de forma segura a {functionName}.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} requiere la versión 0x{requiredVersion:x8}, pero la versión actual es 0x{currentVersion:x8}.";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"La lista de campos del enumerado {enumTypeName} es inválida. Cantidad: {count}, Ordinal máximo: {maxOrdinal}.";
}