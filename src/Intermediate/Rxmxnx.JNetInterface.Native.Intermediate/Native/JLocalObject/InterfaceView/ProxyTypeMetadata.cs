namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	public abstract partial class InterfaceView
	{
		private protected sealed partial class Proxy<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>
		{
			/// <summary>
			/// Proxy metadata.
			/// </summary>
			private new sealed record ProxyTypeMetadata()
				: JClassTypeMetadata<JProxyObject>.View(JProxyObject.ProxyTypeMetadata)
			{
				/// <inheritdoc/>
				public override String ToString() => JProxyObject.ProxyTypeMetadata.ToString();

				/// <inheritdoc/>
				internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
					Boolean realClass = false)
					=> IReferenceType.GetMetadata<TInterface>().CreateInstance(jClass, localRef, realClass);
				/// <inheritdoc/>
				internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
				{
					if (jLocal is null) return default;
					try
					{
						return new Proxy<TInterface>(jLocal);
					}
					finally
					{
						if (dispose) jLocal.Dispose();
					}
				}
				/// <inheritdoc/>
				internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
					=> IReferenceType.GetMetadata<TInterface>().ParseInstance(env, jGlobal);
			}
		}
	}
}