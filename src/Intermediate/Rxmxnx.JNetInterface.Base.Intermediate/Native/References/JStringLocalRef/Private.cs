namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JStringLocalRef
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
	private JStringLocalRef(JObjectLocalRef objRef) => this._value = objRef;
}