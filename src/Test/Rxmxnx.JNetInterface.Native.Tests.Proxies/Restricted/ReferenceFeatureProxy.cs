namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract class ReferenceFeatureProxy : IReferenceFeature
{
	public abstract JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract void MonitorEnter(JObjectLocalRef localRef);
	public abstract void MonitorExit(JObjectLocalRef localRef);
	public abstract TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase;
	public abstract Boolean Unload(JLocalObject jLocal);
	public abstract Boolean Unload(JGlobalBase jGlobal);
	public abstract Boolean IsParameter(JLocalObject jLocal);
	public abstract IDisposable GetSynchronizer(JReferenceObject jObject);

	ObjectLifetime IReferenceFeature.GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
		=> this.GetLifetime(jLocal, initializer);

	internal abstract ObjectLifetime GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer);
}