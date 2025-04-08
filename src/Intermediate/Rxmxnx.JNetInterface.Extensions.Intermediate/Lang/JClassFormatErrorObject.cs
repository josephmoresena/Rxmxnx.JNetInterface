namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JClassFormatErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.ClassFormatError</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JClassFormatErrorObject : JLinkageErrorObject, IThrowableType<JClassFormatErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ClassFormatErrorHash, 26);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JClassFormatErrorObject>(JClassFormatErrorObject.typeInfo,
		                                                                IClassType.GetMetadata<JLinkageErrorObject>(),
		                                                                JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JClassFormatErrorObject>.Metadata => JClassFormatErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassFormatErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassFormatErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassFormatErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassFormatErrorObject IClassType<JClassFormatErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassFormatErrorObject IClassType<JClassFormatErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassFormatErrorObject IClassType<JClassFormatErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}