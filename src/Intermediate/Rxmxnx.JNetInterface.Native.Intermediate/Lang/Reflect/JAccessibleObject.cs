namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JAccessibleObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.AccessibleObject</c> instance.
/// </summary>
public class JAccessibleObject : JLocalObject, IClassType<JAccessibleObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.AccessibleObjectHash, 34);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JAccessibleObject>(
		JAccessibleObject.typeInfo, JTypeModifier.Extensible, InterfaceSet.AnnotatedElementSet);

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