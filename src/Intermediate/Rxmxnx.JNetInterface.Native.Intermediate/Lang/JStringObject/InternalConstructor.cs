namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jStringClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="stringRef">Local string reference.</param>
	/// <param name="value">Internal value.</param>
	internal JStringObject(JClassObject jStringClass, JStringLocalRef stringRef, String? value = default) :
		base(jStringClass, stringRef.Value)
	{
		this._value = value;
		this._length = value?.Length;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jStringClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="stringRef">Local string reference.</param>
	/// <param name="utf8Length">Internal UTF-8 value length.</param>
	internal JStringObject(JClassObject jStringClass, JStringLocalRef stringRef, Int32 utf8Length) :
		base(jStringClass, stringRef.Value)
		=> this._utf8Length = utf8Length;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JStringObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
	{
		if (this._length is not null) return;
		this._length ??= this.Environment.StringFeature.GetLength(jGlobal);
		this._utf8Length ??= this.Environment.StringFeature.GetUtf8Length(jGlobal);
	}
}