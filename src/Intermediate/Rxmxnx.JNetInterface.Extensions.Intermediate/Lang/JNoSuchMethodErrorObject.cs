namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JNoSuchMethodErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.NoSuchMethodError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNoSuchMethodErrorObject : JIncompatibleClassChangeErrorObject, IThrowableType<JNoSuchMethodErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.NoSuchMethodErrorHash, 27);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JNoSuchMethodErrorObject>(JNoSuchMethodErrorObject.typeInfo,
		                                                                 IClassType
			                                                                 .GetMetadata<
				                                                                 JIncompatibleClassChangeErrorObject>(),
		                                                                 JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JNoSuchMethodErrorObject>.Metadata => JNoSuchMethodErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNoSuchMethodErrorObject IClassType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNoSuchMethodErrorObject IClassType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNoSuchMethodErrorObject IClassType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}