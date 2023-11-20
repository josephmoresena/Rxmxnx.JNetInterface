namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// IEnvironment cache implementation.
	/// </summary>
	private sealed record ThreadCache : ReferenceHelperCache<JEnvironment, JEnvironmentRef>
	{
		protected override JEnvironment Create(JEnvironmentRef reference, Boolean isDestroyable)
			=> throw new NotImplementedException();
		protected override void Destroy(JEnvironment instance) { throw new NotImplementedException(); }
	}
}