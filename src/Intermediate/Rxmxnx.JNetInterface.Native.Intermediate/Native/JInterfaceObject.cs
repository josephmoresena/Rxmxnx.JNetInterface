namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local interface instance.
/// </summary>
public abstract partial class JInterfaceObject : JLocalObject, IInterfaceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Interface;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	protected JInterfaceObject(JLocalObject jLocal) : base(jLocal, jLocal.Class) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	protected JInterfaceObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}