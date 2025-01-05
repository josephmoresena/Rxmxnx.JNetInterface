namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract partial class ReferenceFeatureProxy : IReferenceFeature
{
	public abstract JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	public abstract void MonitorEnter(JObjectLocalRef localRef);
	public abstract void MonitorExit(JObjectLocalRef localRef);
	public abstract TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase;
	public abstract JWeak CreateWeak(JGlobalBase jGlobal);
	public abstract void LocalLoad(JGlobalBase jGlobal, JLocalObject jLocal);
	public abstract Boolean Unload(JLocalObject jLocal);
	public abstract Boolean Unload(JGlobalBase jGlobal);
	public abstract Boolean IsParameter(JLocalObject jLocal);
	public abstract IDisposable GetSynchronizer(JReferenceObject jObject);
	public abstract LifetimeWrapper GetLifetime(JLocalObject jLocal, JObjectLocalRef localRef, JClassObject? jClass,
		Boolean overrideClass);
}