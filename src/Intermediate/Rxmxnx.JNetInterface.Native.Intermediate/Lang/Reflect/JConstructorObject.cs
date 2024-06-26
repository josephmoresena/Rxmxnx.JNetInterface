namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JConstructorObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Constructor</c> instance.
/// </summary>
public sealed class JConstructorObject : JExecutableObject, IClassType<JConstructorObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JExecutableObject>
	                                                    .Create<JConstructorObject>(
		                                                    "java/lang/reflect/Constructor"u8, JTypeModifier.Final)
	                                                    .Build();

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