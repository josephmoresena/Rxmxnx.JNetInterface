namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JNoClassDefFoundErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.NoClassDefFoundError</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JNoClassDefFoundErrorObject : JLinkageErrorObject, IThrowableType<JNoClassDefFoundErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.NoClassDefFoundErrorHash, 30);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JNoClassDefFoundErrorObject>(JNoClassDefFoundErrorObject.typeInfo,
		                                                                    IClassType
			                                                                    .GetMetadata<JLinkageErrorObject>(),
		                                                                    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JNoClassDefFoundErrorObject>.Metadata
		=> JNoClassDefFoundErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JNoClassDefFoundErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoClassDefFoundErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoClassDefFoundErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNoClassDefFoundErrorObject IClassType<JNoClassDefFoundErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNoClassDefFoundErrorObject IClassType<JNoClassDefFoundErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNoClassDefFoundErrorObject IClassType<JNoClassDefFoundErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}