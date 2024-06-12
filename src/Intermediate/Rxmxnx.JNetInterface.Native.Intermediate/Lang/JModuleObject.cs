namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JModuleObject>;

/// <summary>
/// This class represents a local <c>java.lang.Module</c> instance.
/// </summary>
public sealed class JModuleObject : JLocalObject, IClassType<JModuleObject>, IInterfaceObject<JAnnotatedElementObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JModuleObject>
	                                                    .Create("java/lang/Module"u8, JTypeModifier.Final)
	                                                    .Implements<JAnnotatedElementObject>().Build();

	static TypeMetadata IClassType<JModuleObject>.Metadata => JModuleObject.typeMetadata;

	/// <inheritdoc/>
	private JModuleObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JModuleObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JModuleObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	/// <inheritdoc/>
	internal JModuleObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	static JModuleObject IClassType<JModuleObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JModuleObject IClassType<JModuleObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JModuleObject IClassType<JModuleObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}