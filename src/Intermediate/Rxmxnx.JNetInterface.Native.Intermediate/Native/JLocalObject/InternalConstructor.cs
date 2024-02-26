namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	internal JLocalObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass.IsProxy)
		=> this.Lifetime = new(jClass.Environment, this, localRef) { Class = jClass, IsRealClass = jClass.IsFinal, };
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JLocalObject(IEnvironment env, JGlobalBase jGlobal) : base(jGlobal)
	{
		this.Lifetime = new(env, this, jGlobal);
		JLocalObject.ProcessMetadata(this, jGlobal.ObjectMetadata);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JLocalObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal)
	{
		jLocal.Lifetime.Load(this);
		this.Lifetime = jLocal.Lifetime;
		this.Lifetime.SetClass(jClass);
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
		=> this.Lifetime =
			env.ReferenceFeature.GetLifetime(
				this, new() { Class = jClass, LocalReference = localRef, OverrideClass = false, });
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	internal JLocalObject(IEnvironment env) : base(!env.NoProxy) => this.Lifetime = new(env, this);
}