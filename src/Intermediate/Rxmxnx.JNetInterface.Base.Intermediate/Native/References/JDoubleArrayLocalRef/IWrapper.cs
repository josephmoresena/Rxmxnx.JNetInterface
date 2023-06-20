namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JDoubleArrayLocalRef : IWrapper<JArrayLocalRef>
{
    JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}