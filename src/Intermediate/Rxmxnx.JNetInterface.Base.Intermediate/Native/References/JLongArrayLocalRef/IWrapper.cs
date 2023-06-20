namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JLongArrayLocalRef : IWrapper<JArrayLocalRef>
{
    JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}