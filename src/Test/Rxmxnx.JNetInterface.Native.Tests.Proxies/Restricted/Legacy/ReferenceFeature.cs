namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class ReferenceFeatureProxy
{
	ObjectLifetime IReferenceFeature.GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
		=> this.GetLifetime(jLocal, initializer.LocalReference, initializer.Class, initializer.OverrideClass).Value;
}