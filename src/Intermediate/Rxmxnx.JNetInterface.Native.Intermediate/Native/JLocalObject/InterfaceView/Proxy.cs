namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	public abstract partial class InterfaceView
	{
		/// <summary>
		/// Interface proxy.
		/// </summary>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2743,
		                 Justification = CommonConstants.StaticAbstractPropertyUseJustification)]
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
		                 Justification = CommonConstants.InternalInheritanceJustification)]
		private protected sealed partial class Proxy<TInterface> : JProxyObject, IInterfaceObject<TInterface>
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			/// <summary>
			/// Proxy metadata.
			/// </summary>
			public static readonly JClassTypeMetadata ProxyMetadata = new ProxyTypeMetadata();

			/// <inheritdoc/>
			public Proxy(IReferenceType.ClassInitializer initializer) : base(initializer)
			{
				JInterfaceTypeMetadata interfaceTypeMetadata = IInterfaceType.GetMetadata<TInterface>();
				foreach (JInterfaceTypeMetadata interfaceMetadata in interfaceTypeMetadata.Interfaces.Enumerable)
					interfaceMetadata.SetAssignable(this);
			}
			/// <inheritdoc/>
			public Proxy(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
			/// <inheritdoc/>
			public Proxy(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		}
	}
}