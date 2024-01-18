namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	internal JLocalObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass.IsDummy)
		=> this._lifetime = this.GetLifetime(jClass.Environment, localRef, jClass, true);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JLocalObject(IEnvironment env, JObjectLocalRef localRef, JClassObject? jClass = default) :
		base(!env.ReferenceFeature.RealEnvironment)
		=> this._lifetime = this.GetLifetime(env, localRef, jClass);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	internal JLocalObject(IEnvironment env) : base(!env.ReferenceFeature.RealEnvironment)
		=> this._lifetime = new(env, this);
}