namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java classes names.
/// </summary>
internal static class ClassNames
{
	/// <summary>
	/// Java class name of primitive <c>boolean</c> class.
	/// </summary>
	public const String BooleanPrimitive = "boolean";
	/// <summary>
	/// Java class name of primitive <c>byte</c> class.
	/// </summary>
	public const String BytePrimitive = "byte";
	/// <summary>
	/// Java class name of primitive <c>char</c> class.
	/// </summary>
	public const String CharPrimitive = "char";
	/// <summary>
	/// Java class name of primitive <c>double</c> class.
	/// </summary>
	public const String DoublePrimitive = "double";
	/// <summary>
	/// Java class name of primitive <c>float</c> class.
	/// </summary>
	public const String FloatPrimitive = "float";
	/// <summary>
	/// Java class name of primitive <c>int</c> class.
	/// </summary>
	public const String IntPrimitive = "int";
	/// <summary>
	/// Java class name of primitive <c>long</c> class.
	/// </summary>
	public const String LongPrimitive = "long";
	/// <summary>
	/// Java class name of primitive <c>short</c> class.
	/// </summary>
	public const String ShortPrimitive = "short";

	/// <summary>
	/// JNI name of <c>java.lang.Object</c> class.
	/// </summary>
	public const String Object = ClassNames.LangPackage + "Object";
	/// <summary>
	/// JNI name of <c>java.lang.Class&lt;?&gt;</c> class.
	/// </summary>
	public const String ClassObject = ClassNames.LangPackage + "Class";
	/// <summary>
	/// JNI name of <c>java.lang.reflect.Proxy</c> class.
	/// </summary>
	public const String ProxyObject = ClassNames.ReflectPackage + "Proxy";

	#region Package Name
	/// <summary>
	/// JNI representation of <c>java.lang</c> package.
	/// </summary>
	private const String LangPackage = "java/lang/";
	/// <summary>
	/// JNI representation of <c>java.lang.reflect</c> package.
	/// </summary>
	private const String ReflectPackage = ClassNames.LangPackage + "reflect/";
	#endregion
}