namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JUnsatisfiedLinkErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.UnsatisfiedLinkError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JUnsatisfiedLinkErrorObject : JLinkageErrorObject, IThrowableType<JUnsatisfiedLinkErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.UnsatisfiedLinkErrorHash, 30);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JUnsatisfiedLinkErrorObject>(
			    JUnsatisfiedLinkErrorObject.typeInfo, IClassType.GetMetadata<JLinkageErrorObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JUnsatisfiedLinkErrorObject>.Metadata
		=> JUnsatisfiedLinkErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JUnsatisfiedLinkErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JUnsatisfiedLinkErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JUnsatisfiedLinkErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JUnsatisfiedLinkErrorObject IClassType<JUnsatisfiedLinkErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JUnsatisfiedLinkErrorObject IClassType<JUnsatisfiedLinkErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JUnsatisfiedLinkErrorObject IClassType<JUnsatisfiedLinkErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}