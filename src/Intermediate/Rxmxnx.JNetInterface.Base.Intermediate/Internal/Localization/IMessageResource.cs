namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// This interface exposes a message resource type.
/// </summary>
internal partial interface IMessageResource
{
	/// <summary>
	/// Current instance.
	/// </summary>
	private protected static abstract IMessageResource Instance { get; }

	/// <summary>
	/// Message for invalid type exception.
	/// </summary>
	String InvalidType { get; }
	/// <summary>
	/// Message for void argument exception.
	/// </summary>
	String VoidArgument { get; }
	/// <summary>
	/// Message for void instantiation exception.
	/// </summary>
	String VoidInstantiation { get; }
	/// <summary>
	/// Message for void array exception.
	/// </summary>
	String VoidArray { get; }
	/// <summary>
	/// Message for void equality exception.
	/// </summary>
	String VoidEquality { get; }
	/// <summary>
	/// Message for signature not allowed exception.
	/// </summary>
	String SignatureNotAllowed { get; }
	/// <summary>
	/// Message for critical exception.
	/// </summary>
	String CriticalExceptionMessage { get; }
	/// <summary>
	/// Message for unknown critical exception.
	/// </summary>
	String UnknownExceptionMessage { get; }
	/// <summary>
	/// Message for invalid signature exception.
	/// </summary>
	String InvalidSignatureMessage { get; }
	/// <summary>
	/// Message for invalid primitive definition exception.
	/// </summary>
	String InvalidPrimitiveDefinitionMessage { get; }
	/// <summary>
	/// Message for invalid primitive type exception.
	/// </summary>
	String InvalidPrimitiveTypeMessage { get; }
	/// <summary>
	/// Message for proxy on non-proxy process exception.
	/// </summary>
	String ProxyOnNonProxyProcess { get; }
	/// <summary>
	/// Message for non-proxy on proxy process exception.
	/// </summary>
	String NonProxyOnProxyProcess { get; }
	/// <summary>
	/// Message for not constructor definition exception.
	/// </summary>
	String NotConstructorDefinition { get; }
	/// <summary>
	/// Message for not primitive object exception.
	/// </summary>
	String NotPrimitiveObject { get; }
	/// <summary>
	/// Message for invalid proxy object exception.
	/// </summary>
	String InvalidProxyObject { get; }
	/// <summary>
	/// Message for invalid global object exception.
	/// </summary>
	String InvalidGlobalObject { get; }
	/// <summary>
	/// Message for only local references allowed exception.
	/// </summary>
	String OnlyLocalReferencesAllowed { get; }
	/// <summary>
	/// Message for class redefinition exception.
	/// </summary>
	String ClassRedefinition { get; }
	/// <summary>
	/// Message for stack trace fixed exception.
	/// </summary>
	String StackTraceFixed { get; }
	/// <summary>
	/// Message for invalid class exception.
	/// </summary>
	String InvalidClass { get; }
	/// <summary>
	/// Message for unloaded class exception.
	/// </summary>
	String UnloadedClass { get; }
	/// <summary>
	/// Message for object is not class exception.
	/// </summary>
	String NotClassObject { get; }
	/// <summary>
	/// Message for single reference transaction exception.
	/// </summary>
	String SingleReferenceTransaction { get; }
	/// <summary>
	/// Message for invalid reference object exception.
	/// </summary>
	String InvalidReferenceObject { get; }
	/// <summary>
	/// Message for invalid object metadata exception.
	/// </summary>
	String InvalidObjectMetadata { get; }
	/// <summary>
	/// Message for disposed reference object exception.
	/// </summary>
	String DisposedObject { get; }
	/// <summary>
	/// Message for invalid array exception.
	/// </summary>
	String InvalidArrayLength { get; }
	/// <summary>
	/// Message for dead JVM exception.
	/// </summary>
	String DeadVirtualMachine { get; }
	/// <summary>
	/// Message for not attached thread exception.
	/// </summary>
	String NotAttachedThread { get; }
	/// <summary>
	/// Message for incompatible library exception.
	/// </summary>
	String IncompatibleLibrary { get; }
	/// <summary>
	/// Message for unmanaged memory context exception.
	/// </summary>
	String UnmanagedMemoryContext { get; }

	/// <summary>
	/// Message for instantiation exception.
	/// </summary>
	String InvalidInstantiation(CString className);
	/// <summary>
	/// Message for invalid casting exception.
	/// </summary>
	String InvalidCastTo(Type type);
	/// <summary>
	/// Message for invalid casting exception.
	/// </summary>
	String InvalidCastTo(CString className);
	/// <summary>
	/// Message for invalid casting exception.
	/// </summary>
	String EmptyString(String paramName);
	/// <summary>
	/// Message for invalid metadata exception.
	/// </summary>
	String InvalidMetadata(CString className, Type typeOfT);
	/// <summary>
	/// Message for abstract class exception.
	/// </summary>
	String AbstractClass(CString className);
	/// <summary>
	/// Message for invalid derivation type exception.
	/// </summary>
	String InvalidDerivationType(String typeName);
	/// <summary>
	/// Message for invalid interface extension exception.
	/// </summary>
	String InvalidInterfaceExtension(String interfaceName);
	/// <summary>
	/// Message for same interface extension exception.
	/// </summary>
	String SameInterfaceExtension(String interfaceName);
	/// <summary>
	/// Message for same class extension exception.
	/// </summary>
	String SameClassExtension(String className);
	/// <summary>
	/// Message for annotation type exception.
	/// </summary>
	String AnnotationType(CString interfaceName, String annotationName);
	/// <summary>
	/// Message for invalid interface implementation exception.
	/// </summary>
	String InvalidImplementation(CString interfaceName, String className);
	/// <summary>
	/// Message for invalid interface implementation exception.
	/// </summary>
	String InvalidImplementation(CString interfaceName, String className, CString missingSuperInterface);
	/// <summary>
	/// Message for invalid interface implementation exception.
	/// </summary>
	String InvalidImplementation(CString interfaceName, String className, IReadOnlySet<CString> missingSuperInterfaces);
	/// <summary>
	/// Message for invalid interface extension exception.
	/// </summary>
	String InvalidExtension(CString superInterfaceName, String interfaceName);
	/// <summary>
	/// Message for invalid interface extension exception.
	/// </summary>
	String InvalidExtension(CString superInterfaceName, String interfaceName, CString missingSuperInterface);
	/// <summary>
	/// Message for invalid interface extension exception.
	/// </summary>
	String InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces);
	/// <summary>
	/// Message for invalid ordinal exception.
	/// </summary>
	String InvalidOrdinal(String enumTypeName);
	/// <summary>
	/// Message for duplicate ordinal exception.
	/// </summary>
	String DuplicateOrdinal(String enumTypeName, Int32 ordinal);
	/// <summary>
	/// Message for invalid enum value exception.
	/// </summary>
	String InvalidValueName(String enumTypeName);
	/// <summary>
	/// Message for duplicate enum value exception.
	/// </summary>
	String DuplicateValueName(String enumTypeName, CString valueName);
	/// <summary>
	/// Message for invalid enum value list exception.
	/// </summary>
	String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal);
	/// <summary>
	/// Message for invalid enum value list exception.
	/// </summary>
	String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing);
	/// <summary>
	/// Message for invalid builder type exception.
	/// </summary>
	String InvalidBuilderType(String typeName, String expectedBuilder);
	/// <summary>
	/// Message for invalid reference type exception.
	/// </summary>
	String InvalidReferenceType(String typeName);
	/// <summary>
	/// Message for invalid class instance exception.
	/// </summary>
	String NotTypeObject(CString objectClassName, CString className);
	/// <summary>
	/// Message for main class global error exception.
	/// </summary>
	String MainClassGlobalError(String mainClassName);
	/// <summary>
	/// Message for main class unavailable exception.
	/// </summary>
	String MainClassUnavailable(String mainClassName);
	/// <summary>
	/// Message for main class unavailable exception.
	/// </summary>
	String PrimitiveClassUnavailable(String primitiveClassName);
	/// <summary>
	/// Message for transaction capacity overflow exception.
	/// </summary>
	String OverflowTransactionCapacity(Int32 transactionCapacity);
	/// <summary>
	/// Message for definition not matching exception.
	/// </summary>
	String DefinitionNotMatch(JAccessibleObjectDefinition definition, JAccessibleObjectDefinition otherDefinition);
	/// <summary>
	/// Message for different thread exception.
	/// </summary>
	String DifferentThread(JEnvironmentRef envRef, Int32 threadId);
	/// <summary>
	/// Message for call on unsafe status exception.
	/// </summary>
	String CallOnUnsafe(String functionName);
	/// <summary>
	/// Message for invalid call version exception.
	/// </summary>
	String InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion);
}