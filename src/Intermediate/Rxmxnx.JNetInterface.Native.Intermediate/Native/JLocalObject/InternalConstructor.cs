namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="refObj"><see cref="JReferenceObject"/> instance.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	internal JLocalObject(IEnvironment env, JReferenceObject refObj, Boolean isNativeParameter, IClass? jClass) :
		base(refObj)
	{
		this._env = env;
		this._lifetime = new(isNativeParameter, this);
		this._isDisposed = false;
		this._class = jClass;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JLocalObject(IEnvironment env, JGlobalBase jGlobal) : base(jGlobal)
	{
		this._env = env;
		this._lifetime = new(false, this);
		this._class = jGlobal.GetObjectClass(env);
		this._global = jGlobal as JGlobal;
		this._weak = jGlobal as JWeak;
	}
}