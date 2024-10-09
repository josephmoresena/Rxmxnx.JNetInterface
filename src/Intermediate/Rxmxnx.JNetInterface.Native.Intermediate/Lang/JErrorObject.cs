namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.Error</c> instance.
/// </summary>
public class JErrorObject : JThrowableObject, IThrowableType<JErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ErrorHash, 15);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JErrorObject>(JErrorObject.typeInfo,
		                                                     IClassType.GetMetadata<JThrowableObject>(),
		                                                     JTypeModifier.Extensible));
	static TypeMetadata IThrowableType<JErrorObject>.Metadata => JErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JErrorObject IClassType<JErrorObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JErrorObject IClassType<JErrorObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JErrorObject IClassType<JErrorObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}