namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JFloatArrayLocalRef : IWrapper<JArrayLocalRef>
{
	JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;
}