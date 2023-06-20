namespace Rxmxnx.JNetInterface;

#pragma warning disable CS8618
public static partial class UnicodeMethodNames
{
    /// <summary>
    /// JNI name for class constructors.
    /// </summary>
    [DefaultValue(MethodNames.ConstructorName)]
    public static readonly CString ConstructorName;

    /// <summary>
    /// JNI name of <c>java.lang.Class&lt;?&gt;.getName()</c> method.
    /// </summary>
    [DefaultValue(MethodNames.GetClassNameMethodName)]
    internal static readonly CString GetClassNameMethodName;
    /// <summary>
    /// JNI name of <c>java.lang.Throwable.getMessage()</c> method.
    /// </summary>
    [DefaultValue(MethodNames.GetThrowableMessageMethodName)]
    internal static readonly CString GetThrowableMessageMethodName;
    /// <summary>
    /// JNI name of <c>java.lang.System.getProperty(String key)</c> method.
    /// </summary>
    [DefaultValue(MethodNames.GetPropertyMethodName)]
    internal static readonly CString GetPropertyMethodName;
}

