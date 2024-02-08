namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.AccessibleObject</c> instance.
/// </summary>
public class JAccessibleObject : JLocalObject, IClassType<JAccessibleObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JAccessibleObject> metadata = JTypeMetadataBuilder<JAccessibleObject>
	                                                                         .Create(
		                                                                         UnicodeClassNames.AccessibleObject())
	                                                                         .Implements<JAnnotatedElementObject>()
	                                                                         .Build();

	static JClassTypeMetadata<JAccessibleObject> IClassType<JAccessibleObject>.Metadata => JAccessibleObject.metadata;

	/// <inheritdoc/>
	internal JAccessibleObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JAccessibleObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JAccessibleObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JAccessibleObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAccessibleObject IReferenceType<JAccessibleObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JAccessibleObject IReferenceType<JAccessibleObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JAccessibleObject IReferenceType<JAccessibleObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}