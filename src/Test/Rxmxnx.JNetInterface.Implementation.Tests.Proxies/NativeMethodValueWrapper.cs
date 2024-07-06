namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly unsafe struct NativeMethodValueWrapper
{
	private readonly NativeMethodValue _value;

	public IntPtr SignaturePtr => (IntPtr)this._value.Signature;
	public IntPtr Function => (IntPtr)this._value.Pointer;
	public IntPtr NamePtr => (IntPtr)this._value.Name;
}