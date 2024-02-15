namespace Rxmxnx.JNetInterface.Native.Dummies;

public abstract partial class EnvironmentProxy
{
	/// <summary>
	/// Creates a <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.
	/// </summary>
	/// <param name="env">A <see cref="EnvironmentProxy"/> instance.</param>
	/// <returns>A <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.</returns>
	public static JClassObject CreateClassObject(EnvironmentProxy env) => new(env);
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	public static JLocalObject CreateObject(JClassObject jClass, JObjectLocalRef localRef) => new(jClass, localRef);
}