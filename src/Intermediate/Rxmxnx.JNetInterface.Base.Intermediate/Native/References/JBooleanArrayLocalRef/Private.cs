namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JBooleanArrayLocalRef
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
	private JBooleanArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
	private JBooleanArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}