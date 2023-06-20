namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JCharArrayLocalRef : IWrapper<JArrayLocalRef>
{
    JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}