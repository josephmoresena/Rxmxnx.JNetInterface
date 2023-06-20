namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JObjectArrayLocalRef : IWrapper<JArrayLocalRef>
{
	JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}