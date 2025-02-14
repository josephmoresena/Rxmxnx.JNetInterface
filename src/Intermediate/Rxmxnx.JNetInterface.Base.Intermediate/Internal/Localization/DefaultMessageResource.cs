namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Default (English) message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class DefaultMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly DefaultMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => DefaultMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private DefaultMessageResource() { }

	String IMessageResource.InvalidType => "The requested value can't be contained in a JValue.";
	String IMessageResource.VoidArgument => "The void type can't be an instance argument.";
	String IMessageResource.VoidInstantiation => "The void type can't be an instance argument.";
	String IMessageResource.VoidArray => "The void type can't be element type of an array.";
	String IMessageResource.VoidEquality => "A Void instance can't be equatable.";
	String IMessageResource.SignatureNotAllowed => "Signature not allowed.";
	String IMessageResource.CriticalExceptionMessage
		=> "The execution is in critical state. There is a pending exception but no calls can be made through native interface.";
	String IMessageResource.UnknownExceptionMessage
		=> "There is a pending exception but the ThrowableException instance could not be created.";
	String IMessageResource.InvalidSignatureMessage => "Invalid signature.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "Definition is not primitive.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Invalid primitive type.";
	String IMessageResource.ProxyOnNonProxyProcess => "A proxy object can't process a non-proxy metadata";
	String IMessageResource.NonProxyOnProxyProcess => "A non-proxy object can't process a proxy metadata.";
	String IMessageResource.NotConstructorDefinition => "Current call definition is not a constructor.";
	String IMessageResource.NotPrimitiveObject => "Object is not primitive.";
	String IMessageResource.InvalidProxyObject => "Java Object must be proxy to perform this operation.";
	String IMessageResource.InvalidGlobalObject => "Invalid global type.";
	String IMessageResource.OnlyLocalReferencesAllowed => "JNI call only allow local references.";
	String IMessageResource.ClassRedefinition => "Redefinition class is unsupported.";
	String IMessageResource.StackTraceFixed => "Current stack frame is fixed.";
	String IMessageResource.InvalidClass => "Invalid class object.";
	String IMessageResource.UnloadedClass => "Unloaded class.";
	String IMessageResource.NotClassObject => "Object is not a class";
	String IMessageResource.SingleReferenceTransaction => "This transaction can hold only 1 reference.";
	String IMessageResource.InvalidReferenceObject => "Invalid JReferenceObject.";
	String IMessageResource.InvalidObjectMetadata => "Invalid ObjectMetadata.";
	String IMessageResource.DisposedObject => "Disposed JReferenceObject.";
	String IMessageResource.InvalidArrayLength => "Array length must be zero or positive.";
	String IMessageResource.DeadVirtualMachine => "Current JVM is not alive.";
	String IMessageResource.NotAttachedThread => "Current thread is not attached.";
	String IMessageResource.IncompatibleLibrary => "Incompatible JVM library.";
	String IMessageResource.UnmanagedMemoryContext => "The memory block is unmanaged.";

	String IMessageResource.InvalidInstantiation(CString className) => $"{className} not is an instantiable type.";
	String IMessageResource.InvalidCastTo(Type type) => $"Invalid cast to {type}.";
	String IMessageResource.InvalidCastTo(CString className)
		=> $"The current instance can't be cast to {className} type.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} must be non-empty string";
	String IMessageResource.InvalidMetadata(CString className, Type typeOfT)
		=> $"Datatype metadata for {className} doesn't match with {typeOfT} type.";
	String IMessageResource.AbstractClass(CString className) => $"{className} is an abstract type.";
	String IMessageResource.InvalidDerivationType(String typeName)
		=> $"{typeName} type can't be based on a type which is derived from it.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} type can't extend an interface type which extends it.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} interface and super interface can't be the same.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} class and super class can't be the same.";
	String IMessageResource.AnnotationType(CString interfaceName, String annotationName)
		=> $"Unable to extend {interfaceName}. {annotationName} is an annotation.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className)
		=> $"{className} type doesn't implements {interfaceName} interface.";
	String IMessageResource.
		InvalidImplementation(CString interfaceName, String className, CString missingSuperInterface)
		=> $"{className} type doesn't implements {missingSuperInterface} superinterface of {interfaceName} interface.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{className} type doesn't implements {String.Join(", ", missingSuperInterfaces)} superinterfaces of {interfaceName} interface.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName)
		=> $"{interfaceName} type doesn't extends {superInterfaceName} interface.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		CString missingSuperInterface)
		=> $"{interfaceName} type doesn't extends {missingSuperInterface} superinterface of {superInterfaceName} interface.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{interfaceName} type doesn't implements {String.Join(", ", missingSuperInterfaces)} superinterfaces of {superInterfaceName} interface.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"Any ordinal for {enumTypeName} type must be zero or positive.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} has already a field with ({ordinal}) ordinal.";
	String IMessageResource.InvalidValueName(String enumTypeName)
		=> $"Any name for {enumTypeName} type must be non-empty.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} has already a field with '{valueName}' name.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> DefaultMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	public String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{DefaultMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Missing values: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"To build {typeName} type metadata use {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} is not a valid reference type.";
	String IMessageResource.NotTypeObject(CString objectClassName, CString typeName)
		=> $"A {objectClassName} instance is not {typeName} instance.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Error creating JNI global reference to {mainClassName} class.";
	String IMessageResource.MainClassUnavailable(String mainClassName)
		=> $"Main class {mainClassName} is not available for JNI access.";
	public String PrimitiveClassUnavailable(String primitiveClassName)
		=> $"Primitive class {primitiveClassName} is not available for JNI access.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"This transaction can hold only {transactionCapacity} references.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"[{definition}] Expected: [{otherDefinition}].";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"JNI Environment ({envRef}) is assigned to another thread. Expected: {threadId} Current: {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"Current JNI status is invalid to call {functionName}.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"Current JNI version (0x{currentVersion:x8}) is invalid to call {functionName}. JNI required: 0x{requiredVersion:x8}";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"The enum field list for {enumTypeName} is invalid.  Count: {count}. Maximum ordinal: {maxOrdinal}.";
}