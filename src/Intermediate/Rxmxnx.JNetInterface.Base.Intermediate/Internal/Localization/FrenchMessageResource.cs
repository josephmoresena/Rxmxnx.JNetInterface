namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// French message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class FrenchMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly FrenchMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => FrenchMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private FrenchMessageResource() { }

	String IMessageResource.InvalidType => "La valeur demandée ne peut pas être contenue dans un JValue.";
	String IMessageResource.VoidArgument => "Le type void ne peut pas être utilisé comme argument d'instance.";
	String IMessageResource.VoidInstantiation => "Le type void ne peut pas être instancié.";
	String IMessageResource.VoidArray => "Le type void ne peut pas être utilisé comme type d'élément d'un tableau.";
	String IMessageResource.VoidEquality => "Une instance de Void ne peut pas être comparable.";
	String IMessageResource.SignatureNotAllowed => "La signature n'est pas autorisée.";
	String IMessageResource.CriticalExceptionMessage
		=> "L'exécution est dans un état critique. Une exception est en attente, mais aucun appel ne peut être effectué via l'interface native.";
	String IMessageResource.UnknownExceptionMessage
		=> "Une exception est en attente, mais l'instance ThrowableException n'a pas pu être créée.";
	String IMessageResource.InvalidSignatureMessage => "Signature invalide.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "La définition n'est pas un type primitif.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Type primitif invalide.";
	String IMessageResource.ProxyOnNonProxyProcess => "Un objet proxy ne peut pas traiter des métadonnées non-proxy.";
	String IMessageResource.NonProxyOnProxyProcess => "Un objet non-proxy ne peut pas traiter des métadonnées proxy.";
	String IMessageResource.NotConstructorDefinition => "L'appel actuel n'est pas un constructeur.";
	String IMessageResource.NotPrimitiveObject => "L'objet n'est pas primitif.";
	String IMessageResource.InvalidProxyObject => "L'objet Java doit être un proxy pour effectuer cette opération.";
	String IMessageResource.InvalidGlobalObject => "Objet global invalide.";
	String IMessageResource.OnlyLocalReferencesAllowed => "Les appels JNI n'autorisent que les références locales.";
	String IMessageResource.ClassRedefinition => "La redéfinition d'une classe n'est pas prise en charge.";
	String IMessageResource.StackTraceFixed => "La pile d'exécution actuelle est fixée.";
	String IMessageResource.InvalidClass => "Classe invalide.";
	String IMessageResource.UnloadedClass => "La classe a été déchargée.";
	String IMessageResource.NotClassObject => "L'objet n'est pas une classe.";
	String IMessageResource.SingleReferenceTransaction => "Cette transaction ne peut contenir qu'une seule référence.";
	String IMessageResource.InvalidReferenceObject => "JReferenceObject invalide.";
	String IMessageResource.InvalidObjectMetadata => "Métadonnées d'objet invalides.";
	String IMessageResource.DisposedObject => "JReferenceObject a été libéré.";
	String IMessageResource.InvalidArrayLength => "La longueur du tableau doit être zéro ou positive.";
	String IMessageResource.DeadVirtualMachine => "La JVM actuelle n'est pas en cours d'exécution.";
	String IMessageResource.NotAttachedThread => "Le thread actuel n'est pas attaché à la JVM.";
	String IMessageResource.IncompatibleLibrary => "Bibliothèque JVM incompatible.";
	String IMessageResource.UnmanagedMemoryContext => "Le bloc mémoire est non géré.";

	String IMessageResource.InvalidInstantiation(CString className) => $"{className} n'est pas un type instantiable.";
	String IMessageResource.InvalidCastTo(Type type) => $"Conversion invalide vers {type}.";
	String IMessageResource.InvalidCastTo(CString className)
		=> $"L'instance actuelle ne peut pas être convertie en type {className}.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} doit être une chaîne non vide.";
	String IMessageResource.InvalidMetadata(CString className, Type typeOfT)
		=> $"Les métadonnées de type pour {className} ne correspondent pas au type {typeOfT}.";
	String IMessageResource.AbstractClass(CString className) => $"{className} est un type abstrait.";
	String IMessageResource.InvalidDerivationType(String typeName)
		=> $"{typeName} ne peut pas hériter d'un type basé sur lui-même.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} ne peut pas étendre une interface qui l'étend déjà.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} et son interface parente ne peuvent pas être identiques.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} et sa superclasse ne peuvent pas être identiques.";
	String IMessageResource.AnnotationType(CString interfaceName, String annotationName)
		=> $"Impossible d'étendre {interfaceName}. {annotationName} est une annotation.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className)
		=> $"{className} n'implémente pas {interfaceName}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		CString missingSuperInterface)
		=> $"{className} n'implémente pas {interfaceName}. Manquant : {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{className} n'implémente pas {interfaceName}. Manquants : {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName)
		=> $"{interfaceName} ne peut pas étendre {superInterfaceName}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		CString missingSuperInterface)
		=> $"{interfaceName} ne peut pas étendre {superInterfaceName}. Manquant : {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{interfaceName} ne peut pas étendre {superInterfaceName}. Manquants : {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"L'ordinal pour {enumTypeName} doit être zéro ou positif.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} possède déjà un champ avec l'ordinal {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName) => $"Le nom pour {enumTypeName} doit être non vide.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} possède déjà un champ nommé '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> FrenchMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{FrenchMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Valeurs manquantes : {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} doit être construit en utilisant {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName)
		=> $"{typeName} n'est pas un type de référence valide.";
	String IMessageResource.NotTypeObject(CString objectClassName, CString className)
		=> $"{objectClassName} n'est pas un objet de type pour {className}.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Erreur globale dans la classe principale {mainClassName}.";
	String IMessageResource.MainClassUnavailable(String mainClassName)
		=> $"Erreur lors de la création d'une référence globale JNI pour la classe {mainClassName}.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"La classe primitive {primitiveClassName} est indisponible.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"Capacité de transaction dépassée : {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"Les définitions ne correspondent pas : {definition} vs {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"L'environnement JNI ({envRef}) est assigné à un autre thread. Opération tentée sur un thread différent : {threadId}. Thread actuel : {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"L'environnement JNI actuel est dans un état invalide pour appeler en toute sécurité {functionName}.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} nécessite la version 0x{requiredVersion:x8}, mais la version actuelle est 0x{currentVersion:x8}.";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"La liste des champs enum pour {enumTypeName} est invalide. Nombre d'éléments : {count}, Ordinal maximal : {maxOrdinal}.";
}