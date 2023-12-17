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

	/// <summary>
	/// Retrieves a <see cref="JStringObject"/> containing class name.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JStringObject"/> instance.</returns>
	internal static JStringObject GetClassName(IEnvironment env, JClassLocalRef classRef)
	{
		JClassObject jClassClass = env.ClassProvider.ClassObject;
		JFunctionDefinition<JStringObject> getName = new(UnicodeMethodNames.GetClassNameMethodName);
		using JClassObject tempClass = new(jClassClass, classRef);
		try
		{
			return getName.Invoke(tempClass)!;
		}
		finally
		{
			tempClass.ClearValue();
		}
	}
}