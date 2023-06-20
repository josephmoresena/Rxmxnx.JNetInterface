namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JByteArrayLocalRef : IWrapper<JArrayLocalRef>
{
    JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}