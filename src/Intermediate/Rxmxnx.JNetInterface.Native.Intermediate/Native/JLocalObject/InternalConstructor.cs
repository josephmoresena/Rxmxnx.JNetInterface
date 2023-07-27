namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jLocalRef">Local object reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JLocalObject(IEnvironment env, JObjectLocalRef jLocalRef, Boolean isDummy, Boolean isNativeParameter, JClassObject? jClass = default) :
		base(jLocalRef, isDummy)
	{
		this._env = env;
		this._lifetime = new(isNativeParameter, this);
		this._isDisposed = false;
		this._class = jClass;
		this._isRealClass = this._class is not null && this._class.IsFinal.GetValueOrDefault();
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
		this._isRealClass = true;
		this._global = jGlobal as JGlobal;
		this._weak = jGlobal as JWeak;
	}
}