namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jLocalRef">Local object reference.</param>
	/// <param name="ordinal">Enum instance ordinal.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JEnumObject(IEnvironment env, JObjectLocalRef jLocalRef, Int32? ordinal, Boolean isDummy) : base(
		env, jLocalRef, isDummy, env.ClassProvider.StringClassObject)
		=> this._ordinal = ordinal;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JEnumObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JEnumObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass) { }
}