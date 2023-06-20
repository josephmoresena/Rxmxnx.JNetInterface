namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JLongArrayLocalRef
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
	private JLongArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
	private JLongArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}