namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JShortArrayLocalRef
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
	private JShortArrayLocalRef(JObjectLocalRef objRef) => this._value = new(objRef);
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="arrayRef"><see cref="JArrayLocalRef"/> value.</param>
	private JShortArrayLocalRef(JArrayLocalRef arrayRef) => this._value = arrayRef;
}