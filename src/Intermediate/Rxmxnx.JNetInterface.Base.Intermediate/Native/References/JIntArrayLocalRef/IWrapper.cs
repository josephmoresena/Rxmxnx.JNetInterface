namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JIntArrayLocalRef : IWrapper<JArrayLocalRef>
{
    JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}