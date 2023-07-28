namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jStrRef">Local string reference.</param>
	/// <param name="value">Internal value.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	internal JStringObject(IEnvironment env, JStringLocalRef jStrRef, String? value, Boolean isDummy,
		Boolean isNativeParameter) : base(env, jStrRef.Value, isDummy, isNativeParameter,
		                                  env.ClassProvider.StringClassObject)
		=> this._value = value;
}