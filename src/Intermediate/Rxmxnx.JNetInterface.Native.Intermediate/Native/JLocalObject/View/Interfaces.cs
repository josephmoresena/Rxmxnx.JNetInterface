namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	public new abstract partial class View<TObject> : ILocalViewObject
	{
		ILocalObject ILocalViewObject.Object => this.Object;
		JObjectLocalRef ILocalObject.LocalReference => this.Object.LocalReference;
		JLocalObject IWrapper<JLocalObject>.Value => this.Object;
	}
}