namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JArrayLocalRef
{
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="objRef"><see cref="JObjectLocalRef"/> value.</param>
	internal JArrayLocalRef(JObjectLocalRef objRef) => this._value = objRef;
}