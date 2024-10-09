namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JLinkageErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.LinkageError</c> instance.
/// </summary>
public class JLinkageErrorObject : JErrorObject, IThrowableType<JLinkageErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.LinkageErrorHash, 22);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JLinkageErrorObject>(JLinkageErrorObject.typeInfo,
		                                                            IClassType.GetMetadata<JErrorObject>(),
		                                                            JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JLinkageErrorObject>.Metadata => JLinkageErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JLinkageErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JLinkageErrorObject IClassType<JLinkageErrorObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLinkageErrorObject IClassType<JLinkageErrorObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLinkageErrorObject IClassType<JLinkageErrorObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}