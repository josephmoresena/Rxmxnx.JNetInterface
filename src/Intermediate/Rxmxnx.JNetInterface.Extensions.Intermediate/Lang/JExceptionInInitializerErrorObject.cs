namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JExceptionInInitializerErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.ExceptionInInitializerError</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JExceptionInInitializerErrorObject : JLinkageErrorObject,
	IThrowableType<JExceptionInInitializerErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ExceptionInInitializerErrorHash, 37);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JExceptionInInitializerErrorObject>(
			    JExceptionInInitializerErrorObject.typeInfo, IClassType.GetMetadata<JLinkageErrorObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JExceptionInInitializerErrorObject>.Metadata
		=> JExceptionInInitializerErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JExceptionInInitializerErrorObject IClassType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JExceptionInInitializerErrorObject IClassType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JExceptionInInitializerErrorObject IClassType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}