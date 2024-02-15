namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Type</c> instance.
/// </summary>
public sealed class JTypeObject : JInterfaceObject<JTypeObject>, IInterfaceType<JTypeObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JTypeObject> typeMetadata = JTypeMetadataBuilder<JTypeObject>
	                                                                           .Create(
		                                                                           UnicodeClassNames.TypeInterface())
	                                                                           .Build();

	static JInterfaceTypeMetadata<JTypeObject> IInterfaceType<JTypeObject>.Metadata => JTypeObject.typeMetadata;

	/// <inheritdoc/>
	private JTypeObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JTypeObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JTypeObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JTypeObject IReferenceType<JTypeObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JTypeObject IReferenceType<JTypeObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JTypeObject IReferenceType<JTypeObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}