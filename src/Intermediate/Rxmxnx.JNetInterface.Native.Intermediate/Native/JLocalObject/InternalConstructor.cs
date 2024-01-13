namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JLocalObject(IEnvironment env, JObjectLocalRef localRef, JClassObject? jClass = default) :
		base(!env.ReferenceFeature.RealEnvironment)
		=> this._lifetime = 
			this.Environment.ReferenceFeature.GetLifetime(this, localRef, jClass) ??
			new(env, this, localRef)
			{
				Class = jClass, IsRealClass = jClass is not null && jClass.IsFinal.GetValueOrDefault(),
			};
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JLocalObject(IEnvironment env, JObjectLocalRef localRef, Boolean isDummy, JClassObject? jClass = default) :
		base(isDummy)
		=> this._lifetime = 
			this.Environment.ReferenceFeature.GetLifetime(this, localRef, jClass) ??
			new(env, this, localRef)
			{
				Class = jClass, IsRealClass = jClass is not null && jClass.IsFinal.GetValueOrDefault(),
			};
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	internal JLocalObject(IEnvironment env) : base(!env.ReferenceFeature.RealEnvironment)
		=> this._lifetime = new(env, this);
}