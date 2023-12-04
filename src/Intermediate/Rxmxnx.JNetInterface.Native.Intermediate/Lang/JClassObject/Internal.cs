namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(JClassObjectMetadata);
	/// <summary>
	/// <see cref="JFunctionDefinition{JStringObject}"/> to retrieve class name.
	/// </summary>
	internal static readonly JFunctionDefinition<JStringObject> GetNameFunctionDefinition =
		new(UnicodeMethodNames.GetClassNameMethodName);

	/// <summary>
	/// JNI class reference.
	/// </summary>
	internal JClassLocalRef Reference => this.As<JClassLocalRef>();
}