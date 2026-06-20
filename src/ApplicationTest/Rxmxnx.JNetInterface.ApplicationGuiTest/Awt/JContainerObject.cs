using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JContainerObject : JComponentObject, IClassType<JContainerObject>
{
	private static readonly JClassTypeMetadata<JContainerObject> typeMetadata =
		TypeMetadataBuilder<JComponentObject>.Create<JContainerObject>("java/awt/Container"u8).Build();
	private static readonly IndeterminateCall setLayoutDefinition = IndeterminateCall.CreateMethodDefinition(
		"setLayout"u8,
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JLayoutManagerObject>(),]
#else
		JArgumentMetadata.Get<JLayoutManagerObject>()
#endif
	);
	private static readonly IndeterminateCall addDefinition = IndeterminateCall.CreateMethodDefinition("add"u8,
#if !NET9_0_OR_GREATER
			[JArgumentMetadata.Get<JComponentObject>(), JArgumentMetadata.Get<JLocalObject>(),]
#else
			JArgumentMetadata.Get<JComponentObject>(), JArgumentMetadata.Get<JLocalObject>()
#endif
	);

	static JClassTypeMetadata<JContainerObject> IClassType<JContainerObject>.Metadata => JContainerObject.typeMetadata;
#if !NET8_0_OR_GREATER
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static Int32 IDataType.AndroidApiLevel => -1;
#endif

	protected JContainerObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JContainerObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JContainerObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void SetLayout(IInterfaceObject<JLayoutManagerObject> layout)
		=> JContainerObject.setLayoutDefinition.MethodCall(this,
#if !NET9_0_OR_GREATER
		                                                   [layout,]
#else
		                                                   layout
#endif
		);
	public void Add(JComponentObject component, JLocalObject constraints)
		=> JContainerObject.addDefinition.MethodCall(this,
#if !NET9_0_OR_GREATER
		                                             [component, constraints,]
#else
		                                             component, constraints
#endif
		);

	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JContainerObject IClassType<JContainerObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}