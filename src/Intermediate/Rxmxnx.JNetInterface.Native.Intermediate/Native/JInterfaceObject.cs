namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local interface instance.
/// </summary>
public abstract partial class JInterfaceObject : JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	protected JInterfaceObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	protected JInterfaceObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}