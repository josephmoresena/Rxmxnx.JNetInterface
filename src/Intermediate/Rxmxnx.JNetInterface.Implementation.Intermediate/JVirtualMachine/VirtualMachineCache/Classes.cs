namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private sealed partial class VirtualMachineCache
	{
		/// <inheritdoc/>
		public override void LoadMainClasses(JEnvironment env)
		{
			base.LoadMainClasses(env);
			base.LoadMainClasses(this.GlobalClassCache);
		}
	}
}