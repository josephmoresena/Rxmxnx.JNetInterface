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

	String IMessageResource.InvalidType => "The requested value cannot be contained in a JValue.";
	String IMessageResource.VoidArgument => "The void type cannot be used as an instance argument.";
	String IMessageResource.VoidInstantiation => "The void type cannot be instantiated.";
	String IMessageResource.VoidArray => "The void type cannot be used as an array element type.";
	String IMessageResource.VoidEquality => "A Void instance cannot be equatable.";
	String IMessageResource.SignatureNotAllowed => "Signature is not allowed.";
	String IMessageResource.CriticalExceptionMessage
		=> "Execution is in a critical state. There is a pending exception, but no calls can be made through the native interface.";
	String IMessageResource.UnknownExceptionMessage
		=> "There is a pending exception, but the ThrowableException instance could not be created.";
	String IMessageResource.InvalidSignatureMessage => "Invalid signature.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "Definition is not a primitive type.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Invalid primitive type.";
	String IMessageResource.ProxyOnNonProxyProcess => "A proxy object cannot process non-proxy metadata.";
	String IMessageResource.NonProxyOnProxyProcess => "A non-proxy object cannot process proxy metadata.";
	String IMessageResource.NotConstructorDefinition => "The current call definition is not a constructor.";
	String IMessageResource.NotPrimitiveObject => "The object is not primitive.";
	String IMessageResource.InvalidProxyObject => "The Java object must be a proxy to perform this operation.";
	String IMessageResource.InvalidGlobalObject => "Invalid global object.";
	String IMessageResource.OnlyLocalReferencesAllowed => "JNI calls only allow local references.";
	String IMessageResource.ClassRedefinition => "Redefining a class is not supported.";
	String IMessageResource.StackTraceFixed => "The current stack frame is immutable.";
	String IMessageResource.InvalidClass => "Invalid class object.";
	String IMessageResource.UnloadedClass => "The class has been unloaded.";
	String IMessageResource.NotClassObject => "The object is not a class.";
	String IMessageResource.SingleReferenceTransaction => "This transaction can hold only one reference.";
	String IMessageResource.InvalidReferenceObject => "Invalid JReferenceObject.";
	String IMessageResource.InvalidObjectMetadata => "Invalid ObjectMetadata.";
	String IMessageResource.DisposedObject => "JReferenceObject has been disposed.";
	String IMessageResource.InvalidArrayLength => "Array length must be zero or positive.";
	String IMessageResource.DeadVirtualMachine => "The current JVM is not running.";
	String IMessageResource.NotAttachedThread => "The current thread is not attached to the JVM.";
	String IMessageResource.IncompatibleLibrary => "Incompatible JVM library.";
	String IMessageResource.UnmanagedMemoryContext => "The memory block is unmanaged.";

	String IMessageResource.InvalidInstantiation(CString className) => $"{className} is not an instantiable type.";
	String IMessageResource.InvalidCastTo(Type type) => $"Invalid cast to {type}.";
	String IMessageResource.InvalidCastTo(CString className)
		=> $"The current instance cannot be cast to type {className}.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} must be a non-empty string.";
	String IMessageResource.InvalidMetadata(CString className, Type typeOfT)
		=> $"Datatype metadata for {className} does not match type {typeOfT}.";
	String IMessageResource.AbstractClass(CString className) => $"{className} is an abstract type.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} cannot extend an interface that extends it.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} and its super interface cannot be the same.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} and its superclass cannot be the same.";
	String IMessageResource.AnnotationType(CString interfaceName, String annotationName)
		=> $"Unable to extend {interfaceName}. {annotationName} is an annotation.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className)
		=> $"{className} does not implement {interfaceName}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		CString missingSuperInterface)
		=> $"{className} does not implement {interfaceName}. Missing: {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{className} does not implement {interfaceName}. Missing: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName)
		=> $"{interfaceName} cannot extend {superInterfaceName}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		CString missingSuperInterface)
		=> $"{interfaceName} cannot extend {superInterfaceName}. Missing: {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{interfaceName} cannot extend {superInterfaceName}. Missing: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"The ordinal for {enumTypeName} must be zero or positive.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} already has a field with ordinal {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName) => $"The name for {enumTypeName} must be non-empty.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} already has a field named '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> DefaultMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{DefaultMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Missing values: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} must be built using {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} is not a valid reference type.";
	String IMessageResource.NotTypeObject(CString objectClassName, CString className)
		=> $"{objectClassName} is not a type object for {className}.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Error while creating a JNI global reference for the {mainClassName} class.";
	String IMessageResource.MainClassUnavailable(String mainClassName) => $"Main class {mainClassName} is unavailable.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"Primitive class {primitiveClassName} is unavailable.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"Transaction capacity overflow: {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"Definitions do not match: {definition} vs {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"JNI Environment ({envRef}) is assigned to another thread. Operation attempted on a different thread: {threadId}. Current thread: {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"The current JNI environment is in an invalid state to safely call {functionName}.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} requires version 0x{requiredVersion:x8}, but current version is 0x{currentVersion:x8}.";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"The enum field list for {enumTypeName} is invalid. Count: {count}, Maximum ordinal: {maxOrdinal}.";
}