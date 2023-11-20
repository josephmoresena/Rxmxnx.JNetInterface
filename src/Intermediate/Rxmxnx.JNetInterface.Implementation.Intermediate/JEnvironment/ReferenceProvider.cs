namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IReferenceProvider
	{
		public JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive) => throw new NotImplementedException();
		public Boolean ReloadObject(JLocalObject jLocal) => throw new NotImplementedException();
		public TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase
			=> throw new NotImplementedException();
		public Boolean Unload(JLocalObject jLocal) => throw new NotImplementedException();
		public Boolean Unload(JGlobalBase jGlobal) => throw new NotImplementedException();
	}
}