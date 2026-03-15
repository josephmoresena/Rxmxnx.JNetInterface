namespace Rxmxnx.JNetInterface.Internal;

internal partial class DeadThread : IReferenceFeature
{
	ObjectLifetime IReferenceFeature.GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
		=> this.ThrowInvalidResult<ObjectLifetime>();
	JLocalObject IReferenceFeature.CreateWrapper<TPrimitive>(TPrimitive primitive)
		=> this.ThrowInvalidResult<JLocalObject>();
	void IReferenceFeature.MonitorEnter(JObjectLocalRef localRef) => this.ThrowInvalidResult<ObjectLifetime>();
	void IReferenceFeature.MonitorExit(JObjectLocalRef localRef) => this.MonitorExitTrace(localRef);
	TGlobal IReferenceFeature.Create<TGlobal>(JLocalObject jLocal) => this.ThrowInvalidResult<TGlobal>();
	JWeak IReferenceFeature.CreateWeak(JGlobalBase jGlobal) => this.ThrowInvalidResult<JWeak>();
	void IReferenceFeature.LocalLoad(JGlobalBase jGlobal, JLocalObject jLocal) => this.LocalLoadTrace(jGlobal);
	Boolean IReferenceFeature.Unload(JLocalObject jLocal)
	{
		this.UnloadTrace(jLocal);
		return true;
	}
	Boolean IReferenceFeature.Unload(JGlobalBase jGlobal)
	{
		JGlobalRef? globalRef = (jGlobal as JGlobal)?.Reference;
		JWeakRef? weakRef = (jGlobal as JWeak)?.Reference;
		this.UnloadTrace(globalRef, weakRef);
		return true;
	}
	Boolean IReferenceFeature.IsParameter(JLocalObject jLocal)
	{
		this.IsParameterTrace(jLocal);
		return false;
	}
	IDisposable IReferenceFeature.GetSynchronizer(JReferenceObject jObject) => this.ThrowInvalidResult<IDisposable>();
}