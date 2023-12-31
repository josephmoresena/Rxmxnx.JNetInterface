namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java classes names.
/// </summary>
internal static class ClassNames
{
	/// <summary>
	/// JNI representation of <c>java.lang</c> package.
	/// </summary>
	public const String JLangPath = "java/lang/";

	/// <summary>
	/// JNI name of <c>java.lang.Object</c> class.
	/// </summary>
	public const String JObjectClassName = ClassNames.JLangPath + "Object";
	/// <summary>
	/// JNI name of <c>java.lang.Class&lt;?&gt;</c> class.
	/// </summary>
	public const String JClassObjectClassName = ClassNames.JLangPath + "Class";
}