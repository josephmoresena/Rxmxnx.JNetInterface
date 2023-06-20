namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal class for constants literal <see cref="String"/> values.
/// </summary>
internal static partial class CommonConstants
{
    public const String NativeReferenceFormat = "*{0}: {1}";
    public const String VirtualMachineGuidFormat = ".NET{0}";
    public const String GuidToStringFormat = "N";
    public const String IntPtrToStringFormat = "X";

    public const String CriticalExceptionMessage = "The execution is in critical state. No calls can be made through native interface.";

    public const String CodeQuality = "CodeQuality";
    public const String CheckId0051 = "IDE0051:Remove unused private members";
    public const String BinaryStructJustification = "This struct is created only by binary operations.";
    public const String JEnvironmentError = "Local object is not owned by the this JEnvironment instance.";

    public static readonly String ExceptionMessageUnavailable = "Exception message unavailable.";

    public static readonly String JNetVersionError = "JNI version must be at least "; //+ JVirtualMachine.MinimalVersion;
    public static readonly String AttachingThreadError = "Could not attach the current thread to the JVM.";
    public static readonly String PrimitveTypeError = "Primitive type is not valid for JNI.";
    public static readonly String ArrayClassConstructorError = "Cannot get a constructor from an array class.";
    public static readonly String InvalidLocalReference = "Local object has no reference.";
}