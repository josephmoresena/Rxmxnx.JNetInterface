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
	internal JStringObject(IEnvironment env, JStringLocalRef jStrRef, String? value, Boolean isDummy) : base(
		env, jStrRef.Value, isDummy, env.ClassProvider.StringClassObject)
	{
		this._value = value;
		this._length = value?.Length;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JStringObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
	{
		if (this._length is not null) return;
		this._length ??= this.Environment.StringProvider.GetLength(jGlobal);
		this._utf8Length ??= this.Environment.StringProvider.GetUtf8Length(jGlobal);
	}
}