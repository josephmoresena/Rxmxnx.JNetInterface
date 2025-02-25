namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Risorsa di messaggi predefinita in italiano.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class ItalianMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly ItalianMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => ItalianMessageResource.instance;

	/// <summary>
	/// Costruttore privato.
	/// </summary>
	private ItalianMessageResource() { }

	String IMessageResource.InvalidType => "Il valore richiesto non può essere contenuto in un JValue.";
	String IMessageResource.VoidArgument => "Il tipo void non può essere utilizzato come argomento dell'istanza.";
	String IMessageResource.VoidInstantiation => "Il tipo void non può essere istanziato.";
	String IMessageResource.VoidArray => "Il tipo void non può essere utilizzato come tipo di elemento di un array.";
	String IMessageResource.VoidEquality => "Un'istanza Void non può essere confrontata per uguaglianza.";
	String IMessageResource.SignatureNotAllowed => "La firma non è consentita.";
	String IMessageResource.CriticalExceptionMessage
		=> "L'esecuzione è in uno stato critico. C'è un'eccezione pendente, ma nessuna chiamata può essere effettuata attraverso l'interfaccia nativa.";
	String IMessageResource.UnknownExceptionMessage
		=> "C'è un'eccezione pendente, ma l'istanza di ThrowableException non può essere creata.";
	String IMessageResource.InvalidSignatureMessage => "Firma non valida.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "La definizione non è di un tipo primitivo.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Tipo primitivo non valido.";
	String IMessageResource.ProxyOnNonProxyProcess => "Un oggetto proxy non può elaborare metadati non proxy.";
	String IMessageResource.NonProxyOnProxyProcess => "Un oggetto non proxy non può elaborare metadati proxy.";
	String IMessageResource.NotConstructorDefinition => "La definizione della chiamata corrente non è un costruttore.";
	String IMessageResource.NotPrimitiveObject => "L'oggetto non è primitivo.";
	String IMessageResource.InvalidProxyObject => "L'oggetto Java deve essere un proxy per eseguire questa operazione.";
	String IMessageResource.InvalidGlobalObject => "Oggetto globale non valido.";
	String IMessageResource.OnlyLocalReferencesAllowed => "Le chiamate JNI consentono solo riferimenti locali.";
	String IMessageResource.ClassRedefinition => "La ridefinizione di una classe non è supportata.";
	String IMessageResource.StackTraceFixed => "Il frame dello stack corrente è immutabile.";
	String IMessageResource.InvalidClass => "Oggetto classe non valido.";
	String IMessageResource.UnloadedClass => "La classe è stata scaricata.";
	String IMessageResource.NotClassObject => "L'oggetto non è una classe.";
	String IMessageResource.SingleReferenceTransaction => "Questa transazione può contenere solo un riferimento.";
	String IMessageResource.InvalidReferenceObject => "JReferenceObject non valido.";
	String IMessageResource.InvalidObjectMetadata => "Metadati dell'oggetto non validi.";
	String IMessageResource.DisposedObject => "JReferenceObject è stato eliminato.";
	String IMessageResource.InvalidArrayLength => "La lunghezza dell'array deve essere zero o positiva.";
	String IMessageResource.DeadVirtualMachine => "La JVM corrente non è in esecuzione.";
	String IMessageResource.NotAttachedThread => "Il thread corrente non è collegato alla JVM.";
	String IMessageResource.IncompatibleLibrary => "Libreria JVM incompatibile.";
	String IMessageResource.UnmanagedMemoryContext => "Il blocco di memoria è non gestito.";

	String IMessageResource.InvalidInstantiation(String className) => $"{className} non è un tipo istanziabile.";
	String IMessageResource.InvalidCastTo(Type type) => $"Cast non valido a {type}.";
	String IMessageResource.InvalidCastTo(String className)
		=> $"L'istanza corrente non può essere convertita nel tipo {className}.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} deve essere una stringa non vuota.";
	String IMessageResource.InvalidMetadata(String className, Type typeOfT)
		=> $"I metadati del tipo {className} non corrispondono al tipo {typeOfT}.";
	String IMessageResource.AbstractClass(String className) => $"{className} è un tipo astratto.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} non può estendere un'interfaccia che la estende.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} e la sua super interfaccia non possono essere uguali.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} e la sua superclasse non possono essere uguali.";
	String IMessageResource.AnnotationType(String interfaceName, String annotationName)
		=> $"Impossibile estendere {interfaceName}. {annotationName} è un'annotazione.";
	String IMessageResource.InvalidImplementation(String interfaceName, String className)
		=> $"{className} non implementa {interfaceName}.";
	String IMessageResource.InvalidImplementation(String interfaceName, String className, String missingSuperInterface)
		=> $"{className} non implementa {interfaceName}. Mancante: {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(String interfaceName, String className,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{className} non implementa {interfaceName}. Mancante: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName)
		=> $"{interfaceName} non può estendere {superInterfaceName}.";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		String missingSuperInterface)
		=> $"{interfaceName} non può estendere {superInterfaceName}. Mancante: {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{interfaceName} non può estendere {superInterfaceName}. Mancante: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"L'ordinamento per {enumTypeName} deve essere zero o positivo.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} ha già un campo con ordinamento {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName)
		=> $"Il nome per {enumTypeName} deve essere non vuoto.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} ha già un campo chiamato '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> ItalianMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{ItalianMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Mancano i valori: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} deve essere costruito usando {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} non è un tipo di riferimento valido.";
	String IMessageResource.NotTypeObject(String objectClassName, String className)
		=> $"{objectClassName} non è un oggetto tipo per {className}.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Errore durante la creazione di un riferimento globale JNI per la classe {mainClassName}.";
	String IMessageResource.MainClassUnavailable(String mainClassName)
		=> $"Classe principale {mainClassName} non disponibile.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"Classe primitiva {primitiveClassName} non disponibile.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"Capacità della transazione superata: {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"Le definizioni non corrispondono: {definition} vs {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"L'ambiente JNI ({envRef}) è assegnato a un altro thread. Operazione tentata su un thread diverso: {threadId}. Thread corrente: {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"L'ambiente JNI corrente è in uno stato non valido per chiamare in sicurezza {functionName}.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} richiede la versione 0x{requiredVersion:x8}, ma la versione corrente è 0x{currentVersion:x8}.";
	String IMessageResource.InvalidArrayClass(String className) => $"{className} non è una classe di array.";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"L'elenco dei campi enum per {enumTypeName} non è valido. Conteggio: {count}, Ordinamento massimo: {maxOrdinal}.";
}