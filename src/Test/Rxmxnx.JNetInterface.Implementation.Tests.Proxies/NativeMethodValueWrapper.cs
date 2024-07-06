namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public readonly unsafe struct NativeMethodValueWrapper
{
#pragma warning disable CS0649
	private readonly NativeMethodValue _value;
#pragma warning restore CS0649

	public IntPtr SignaturePtr => (IntPtr)this._value.Signature;
	public IntPtr Function => (IntPtr)this._value.Pointer;
	public IntPtr NamePtr => (IntPtr)this._value.Name;
}