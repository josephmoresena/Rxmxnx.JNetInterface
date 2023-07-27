namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Retrieves the <see cref="IClass"/> from current global object.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>The <see cref="JClassObject"/> from current global object.</returns>
	internal JClassObject GetObjectClass(IEnvironment env) => env.ClassProvider.GetClass(this.ObjectClassName);
}