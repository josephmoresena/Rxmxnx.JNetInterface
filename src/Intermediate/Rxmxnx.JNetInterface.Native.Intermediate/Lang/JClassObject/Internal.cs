namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(JClassObjectMetadata);

	/// <summary>
	/// JNI class reference.
	/// </summary>
	internal JClassLocalRef Reference => this.As<JClassLocalRef>();

	/// <summary>
	/// Retrieves a <see cref="JStringObject"/> containing class name.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="isPrimitive">Indicates whether current class is primitive.</param>
	/// <returns>A <see cref="JStringObject"/> instance.</returns>
	internal static JStringObject GetClassName(IEnvironment env, JClassLocalRef classRef, out Boolean isPrimitive)
	{
		JClassObject jClassClass = env.ClassFeature.ClassObject;
		using JClassObject tempClass = new(jClassClass, classRef);
		try
		{
			isPrimitive = env.Functions.IsPrimitiveClass(tempClass);
			return env.Functions.GetClassName(tempClass);
		}
		finally
		{
			tempClass.ClearValue();
		}
	}
}