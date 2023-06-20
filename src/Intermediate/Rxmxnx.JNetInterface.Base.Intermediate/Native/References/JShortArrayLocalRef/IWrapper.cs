namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JShortArrayLocalRef : IWrapper<JArrayLocalRef>
{
	JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}