namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JBooleanArrayLocalRef : IWrapper<JArrayLocalRef>
{
    JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}