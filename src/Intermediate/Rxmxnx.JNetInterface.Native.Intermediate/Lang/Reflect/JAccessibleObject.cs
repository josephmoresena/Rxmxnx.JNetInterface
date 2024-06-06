namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JAccessibleObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.AccessibleObject</c> instance.
/// </summary>
public class JAccessibleObject : JLocalObject, IClassType<JAccessibleObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JAccessibleObject>
	                                                    .Create("java/lang/reflect/AccessibleObject"u8)
	                                                    .Implements<JAnnotatedElementObject>().Build();

	static TypeMetadata IClassType<JAccessibleObject>.Metadata => JAccessibleObject.typeMetadata;

	/// <inheritdoc/>
	private protected JAccessibleObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JAccessibleObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JAccessibleObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JAccessibleObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAccessibleObject IClassType<JAccessibleObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JAccessibleObject IClassType<JAccessibleObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JAccessibleObject IClassType<JAccessibleObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}