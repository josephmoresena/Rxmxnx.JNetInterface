namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	internal JLocalObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass.IsProxy)
		=> this._lifetime = new(jClass.Environment, this, localRef)
		{
			Class = jClass, IsRealClass = jClass.IsFinal.GetValueOrDefault(),
		};
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JLocalObject(IEnvironment env, JGlobalBase jGlobal) : base(jGlobal)
	{
		this._lifetime = new(env, this, jGlobal);
		JLocalObject.ProcessMetadata(this, jGlobal.ObjectMetadata);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JLocalObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal)
	{
		jLocal._lifetime.Load(this);
		this._lifetime = jLocal._lifetime;
		this._lifetime.SetClass(jClass);
		if (jLocal is JInterfaceObject jInterface)
			JLocalObject.ProcessMetadata(this, jInterface.ObjectMetadata);
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JLocalObject(IEnvironment env, JObjectLocalRef localRef, JClassObject? jClass = default) :
		base(!env.NoProxy)
		=> this._lifetime =
			env.ReferenceFeature.GetLifetime(
				this, new() { Class = jClass, LocalReference = localRef, OverrideClass = false, });
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	internal JLocalObject(IEnvironment env) : base(!env.NoProxy) => this._lifetime = new(env, this);
}