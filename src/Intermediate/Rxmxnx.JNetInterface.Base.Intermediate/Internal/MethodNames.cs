namespace Rxmxnx.JNetInterface.Internal;

internal static class MethodNames
{
    /// <summary>
    /// JNI name for class constructors.
    /// </summary>
    public const String ConstructorName = "<init>";

    /// <summary>
    /// JNI name of <c>java.lang.Class&lt;?&gt;.getName()</c> method.
    /// </summary>
    public const String GetClassNameMethodName = "getName";
    /// <summary>
    /// JNI name of <c>java.lang.Throwable.getMessage()</c> method.
    /// </summary>
    public const String GetThrowableMessageMethodName = "getMessage";
    /// <summary>
    /// JNI name of <c>java.lang.System.getProperty(String key)</c> method.
    /// </summary>
    public const String GetPropertyMethodName = "getProperty";
}