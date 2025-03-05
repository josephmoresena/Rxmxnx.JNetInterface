namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jStringClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="stringRef">Local string reference.</param>
	/// <param name="value">Internal value.</param>
	/// <param name="length">Internal value length.</param>
	internal JStringObject(JClassObject jStringClass, JStringLocalRef stringRef, String? value = default,
		Int32? length = default) : base(jStringClass, stringRef.Value)
	{
		this._value = value;
		this._length = value?.Length ?? length;
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
}