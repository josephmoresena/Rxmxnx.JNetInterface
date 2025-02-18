namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// German message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class GermanMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly GermanMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => GermanMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private GermanMessageResource() { }

	String IMessageResource.InvalidType => "Der angeforderte Wert kann nicht in einem JValue enthalten sein.";
	String IMessageResource.VoidArgument => "Der void-Typ kann nicht als Instanzargument verwendet werden.";
	String IMessageResource.VoidInstantiation => "Der void-Typ kann nicht instanziiert werden.";
	String IMessageResource.VoidArray => "Der void-Typ kann nicht als Array-Elementtyp verwendet werden.";
	String IMessageResource.VoidEquality => "Eine Void-Instanz kann nicht vergleichbar sein.";
	String IMessageResource.SignatureNotAllowed => "Signatur ist nicht erlaubt.";
	String IMessageResource.CriticalExceptionMessage
		=> "Die Ausführung befindet sich in einem kritischen Zustand. Es gibt eine ausstehende Ausnahme, aber keine Aufrufe können über die native Schnittstelle gemacht werden.";
	String IMessageResource.UnknownExceptionMessage
		=> "Es gibt eine ausstehende Ausnahme, aber die ThrowableException-Instanz konnte nicht erstellt werden.";
	String IMessageResource.InvalidSignatureMessage => "Ungültige Signatur.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "Definition ist kein primitiver Typ.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Ungültiger primitiver Typ.";
	String IMessageResource.ProxyOnNonProxyProcess => "Ein Proxy-Objekt kann keine Nicht-Proxy-Metadaten verarbeiten.";
	String IMessageResource.NonProxyOnProxyProcess => "Ein Nicht-Proxy-Objekt kann keine Proxy-Metadaten verarbeiten.";
	String IMessageResource.NotConstructorDefinition => "Die aktuelle Aufrufdefinition ist kein Konstruktor.";
	String IMessageResource.NotPrimitiveObject => "Das Objekt ist nicht primitiv.";
	String IMessageResource.InvalidProxyObject
		=> "Das Java-Objekt muss ein Proxy sein, um diese Operation auszuführen.";
	String IMessageResource.InvalidGlobalObject => "Ungültiges globales Objekt.";
	String IMessageResource.OnlyLocalReferencesAllowed => "JNI-Aufrufe erlauben nur lokale Referenzen.";
	String IMessageResource.ClassRedefinition => "Das Neudefinieren einer Klasse wird nicht unterstützt.";
	String IMessageResource.StackTraceFixed => "Der aktuelle Stackframe ist festgelegt.";
	String IMessageResource.InvalidClass => "Ungültiges Klassenobjekt.";
	String IMessageResource.UnloadedClass => "Die Klasse wurde entladen.";
	String IMessageResource.NotClassObject => "Das Objekt ist keine Klasse.";
	String IMessageResource.SingleReferenceTransaction => "Diese Transaktion kann nur eine Referenz enthalten.";
	String IMessageResource.InvalidReferenceObject => "Ungültiges JReferenceObject.";
	String IMessageResource.InvalidObjectMetadata => "Ungültige ObjectMetadata.";
	String IMessageResource.DisposedObject => "JReferenceObject wurde freigegeben.";
	String IMessageResource.InvalidArrayLength => "Array-Länge muss null oder positiv sein.";
	String IMessageResource.DeadVirtualMachine => "Die aktuelle JVM läuft nicht.";
	String IMessageResource.NotAttachedThread => "Der aktuelle Thread ist nicht an die JVM angehängt.";
	String IMessageResource.IncompatibleLibrary => "Inkompatible JVM-Bibliothek.";
	String IMessageResource.UnmanagedMemoryContext => "Der Speicherblock ist nicht verwaltet.";

	String IMessageResource.InvalidInstantiation(CString className) => $"{className} ist kein instanziierbarer Typ.";
	String IMessageResource.InvalidCastTo(Type type) => $"Ungültige Umwandlung in {type}.";
	String IMessageResource.InvalidCastTo(CString className)
		=> $"Die aktuelle Instanz kann nicht in den Typ {className} umgewandelt werden.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} muss eine nicht-leere Zeichenkette sein.";
	String IMessageResource.InvalidMetadata(CString className, Type typeOfT)
		=> $"Die Datentyp-Metadaten für {className} stimmen nicht mit dem Typ {typeOfT} überein.";
	String IMessageResource.AbstractClass(CString className) => $"{className} ist ein abstrakter Typ.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} kann keine Schnittstelle erweitern, die sie bereits erweitert.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} und ihre übergeordnete Schnittstelle können nicht dieselbe sein.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} und ihre Superklasse können nicht dieselbe sein.";
	String IMessageResource.AnnotationType(CString interfaceName, String annotationName)
		=> $"Erweiterung von {interfaceName} nicht möglich. {annotationName} ist eine Annotation.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className)
		=> $"{className} implementiert {interfaceName} nicht.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		CString missingSuperInterface)
		=> $"{className} implementiert {interfaceName} nicht. Fehlend: {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{className} implementiert {interfaceName} nicht. Fehlend: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName)
		=> $"{interfaceName} kann {superInterfaceName} nicht erweitern.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		CString missingSuperInterface)
		=> $"{interfaceName} kann {superInterfaceName} nicht erweitern. Fehlend: {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{interfaceName} kann {superInterfaceName} nicht erweitern. Fehlend: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"Der Ordinalwert für {enumTypeName} muss null oder positiv sein.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} hat bereits ein Feld mit dem Ordinalwert {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName)
		=> $"Der Name für {enumTypeName} muss nicht leer sein.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} hat bereits ein Feld mit dem Namen '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> GermanMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{GermanMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Fehlende Werte: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} muss mit {expectedBuilder} erstellt werden.";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} ist kein gültiger Referenztyp.";
	String IMessageResource.NotTypeObject(CString objectClassName, CString className)
		=> $"{objectClassName} ist kein Typobjekt für {className}.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Fehler beim Erstellen einer JNI-Globalreferenz für die Klasse {mainClassName}.";
	String IMessageResource.MainClassUnavailable(String mainClassName)
		=> $"Hauptklasse {mainClassName} ist nicht verfügbar.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"Primitive Klasse {primitiveClassName} ist nicht verfügbar.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"Transaktionskapazitätsüberlauf: {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"Definitionen stimmen nicht überein: {definition} vs {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"JNI-Umgebung ({envRef}) ist einem anderen Thread zugewiesen. Vorgang wurde in einem anderen Thread versucht: {threadId}. Aktueller Thread: {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"Die aktuelle JNI-Umgebung befindet sich in einem ungültigen Zustand, um {functionName} sicher aufzurufen.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} erfordert Version 0x{requiredVersion:x8}, aber die aktuelle Version ist 0x{currentVersion:x8}.";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"Die Aufzählungsliste für {enumTypeName} ist ungültig. Anzahl: {count}, Maximaler Ordinalwert: {maxOrdinal}.";
}