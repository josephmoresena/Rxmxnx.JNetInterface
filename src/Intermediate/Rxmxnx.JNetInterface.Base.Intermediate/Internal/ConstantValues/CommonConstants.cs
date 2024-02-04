namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Internal class of constant <see cref="String"/> values.
/// </summary>
internal static class CommonConstants
{
	public const String NativeReferenceFormat = "*{0}: {1}";
	public const String VirtualMachineGuidFormat = ".NET{0}";
	public const String GuidToStringFormat = "N";
	public const String IntPtrToStringFormat = "X";

	public const String CriticalExceptionMessage =
		"The execution is in critical state. No calls can be made through native interface.";

	public const String CodeQuality = "CodeQuality";
	public const String CheckId0051 = "IDE0051:Remove unused private members";
	public const String BinaryStructJustification = "This struct is created only by binary operations.";
	public const String JEnvironmentError = "Local object is not owned by the this JEnvironment instance.";

	public const String ExceptionMessageUnavailable = "Exception message unavailable.";

	public const String JNetVersionError = "JNI version must be at least "; //+ JVirtualMachine.MinimalVersion;
	public const String AttachingThreadError = "Could not attach the current thread to the JVM.";
	public const String PrimitiveTypeError = "Primitive type is not valid for JNI.";
	public const String ArrayClassConstructorError = "Cannot get a constructor from an array class.";
	public const String InvalidLocalReference = "Local object has no reference.";
}