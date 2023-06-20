namespace Rxmxnx.JNetInterface.Lang;

public partial class JObject : IObject
{
    CString IObject.ClassName => this.ObjectClassName;
    CString IObject.Signature => this.ObjectSignature;
    Boolean IObject.IsDefault => this._value.Value.IsDefault;

    void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
    {
        ReadOnlySpan<Byte> bytes = NativeUtilities.AsBytes(this.As<JObjectLocalRef>());
        bytes.CopyTo(span[offset..]);
        offset += JValue.PointerSize;
    }
    void IObject.CopyTo(Span<JValue> span, Int32 index) => span[index] = this.Value;
}