namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jClassRef">Local class reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	internal JClassObject(IEnvironment env, JClassLocalRef jClassRef, Boolean isDummy, Boolean isNativeParameter) :
		base(env, jClassRef.Value, isDummy, isNativeParameter, env.ClassProvider.ClassObject) { }
}