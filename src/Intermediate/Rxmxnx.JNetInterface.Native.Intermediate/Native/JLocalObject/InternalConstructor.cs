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
	internal JLocalObject(IEnvironment env, JObjectLocalRef jLocalRef, Boolean isDummy, Boolean isNativeParameter,
		JClassObject? jClass = default) : base(jLocalRef, isDummy)
		=> this._lifetime = new(isNativeParameter, env, this)
		{
			Class = jClass, 
			IsRealClass = jClass is not null && jClass.IsFinal.GetValueOrDefault(),
		};
}