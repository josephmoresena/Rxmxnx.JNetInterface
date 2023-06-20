namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JCharArrayLocalRef
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
	private JCharArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
	private JCharArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}