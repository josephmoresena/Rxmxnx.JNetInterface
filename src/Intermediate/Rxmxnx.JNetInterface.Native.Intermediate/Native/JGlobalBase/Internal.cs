namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Retrieves the <see cref="IClass"/> from current global object.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>The <see cref="IClass"/> from current global object.</returns>
	internal IClass? GetObjectClass(IEnvironment env)
		=> !this.ObjectClassName.Equals(JObject.JObjectClassName) ?
			env.ClassProvider.GetClass(this.ObjectClassName) :
			default;
}