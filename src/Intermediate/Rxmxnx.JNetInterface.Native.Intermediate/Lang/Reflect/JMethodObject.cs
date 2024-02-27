namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Method</c> instance.
/// </summary>
public sealed class JMethodObject : JExecutableObject, IClassType<JMethodObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JMethodObject> metadata = TypeMetadataBuilder<JExecutableObject>
	                                                                     .Create<JMethodObject>(
		                                                                     UnicodeClassNames.MethodObject(),
		                                                                     JTypeModifier.Final).Build();

	static JClassTypeMetadata<JMethodObject> IClassType<JMethodObject>.Metadata => JMethodObject.metadata;

	/// <inheritdoc/>
	internal JMethodObject(JClassObject jClass, JObjectLocalRef localRef, JCallDefinition definition,
		JClassObject declaringClass) : base(jClass, localRef, definition, declaringClass) { }

	/// <inheritdoc/>
	private JMethodObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JMethodObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JMethodObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMethodObject IClassType<JMethodObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JMethodObject IClassType<JMethodObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JMethodObject IClassType<JMethodObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}