namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JMethodObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Method</c> instance.
/// </summary>
public sealed class JMethodObject : JExecutableObject, IClassType<JMethodObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.MethodHash, 24);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.CreateBuiltInMetadata<JMethodObject>(JMethodObject.typeInfo,
		                                                  IClassType.GetMetadata<JExecutableObject>(),
		                                                  JTypeModifier.Final);

	static TypeMetadata IClassType<JMethodObject>.Metadata => JMethodObject.typeMetadata;

	/// <inheritdoc/>
	internal JMethodObject(JClassObject jClass, JObjectLocalRef localRef, JCallDefinition? definition = default,
		JClassObject? declaringClass = default) : base(jClass, localRef, definition, declaringClass) { }

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