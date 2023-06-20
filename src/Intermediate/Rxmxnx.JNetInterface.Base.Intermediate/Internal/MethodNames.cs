namespace Rxmxnx.JNetInterface.Internal;

public static class MethodNames
{
	/// <summary>
	/// JNI name for class constructors.
	/// </summary>
	public const String ConstructorName = "<init>";

    /// <summary>
    /// JNI name of <c>java.lang.Class&lt;?&gt;.getName()</c> method.
    /// </summary>
    internal const String GetClassNameMethodName = "getName";
    /// <summary>
    /// JNI name of <c>java.lang.Throwable.getMessage()</c> method.
    /// </summary>
    internal const String GetThrowableMessageMethodName = "getMessage";
    /// <summary>
    /// JNI name of <c>java.lang.System.getProperty(String key)</c> method.
    /// </summary>
    internal const String GetPropertyMethodName = "getProperty";
}