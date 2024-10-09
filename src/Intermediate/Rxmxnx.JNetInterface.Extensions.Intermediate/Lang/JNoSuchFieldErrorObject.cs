namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JNoSuchFieldErrorObject>;

/// <summary>
/// This class represents a local <c>java.lang.NoSuchFieldError</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNoSuchFieldErrorObject : JIncompatibleClassChangeErrorObject, IThrowableType<JNoSuchFieldErrorObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.NoSuchFieldErrorHash, 26);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JNoSuchFieldErrorObject>(JNoSuchFieldErrorObject.typeInfo,
		                                                                IClassType
			                                                                .GetMetadata<
				                                                                JIncompatibleClassChangeErrorObject>(),
		                                                                JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JNoSuchFieldErrorObject>.Metadata => JNoSuchFieldErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JNoSuchFieldErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchFieldErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchFieldErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNoSuchFieldErrorObject IClassType<JNoSuchFieldErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNoSuchFieldErrorObject IClassType<JNoSuchFieldErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNoSuchFieldErrorObject IClassType<JNoSuchFieldErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}