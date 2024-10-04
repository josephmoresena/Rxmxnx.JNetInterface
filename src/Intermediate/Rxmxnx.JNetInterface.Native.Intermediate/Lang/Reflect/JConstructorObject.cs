namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JConstructorObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Constructor</c> instance.
/// </summary>
public sealed class JConstructorObject : JExecutableObject, IClassType<JConstructorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ConstructorHash, 29);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.CreateBuiltInMetadata<JConstructorObject>(JConstructorObject.typeInfo,
		                                                       IClassType.GetMetadata<JExecutableObject>(),
		                                                       JTypeModifier.Final);

	static TypeMetadata IClassType<JConstructorObject>.Metadata => JConstructorObject.typeMetadata;

	/// <summary>
	/// Executable JNI definition.
	/// </summary>
	public new JConstructorDefinition Definition => (JConstructorDefinition)base.Definition;

	/// <inheritdoc/>
	internal JConstructorObject(JClassObject jClass, JObjectLocalRef localRef, JCallDefinition definition,
		JClassObject declaringClass) : base(jClass, localRef, definition, declaringClass) { }

	/// <inheritdoc/>
	private JConstructorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JConstructorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JConstructorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JConstructorObject IClassType<JConstructorObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JConstructorObject IClassType<JConstructorObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JConstructorObject IClassType<JConstructorObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}