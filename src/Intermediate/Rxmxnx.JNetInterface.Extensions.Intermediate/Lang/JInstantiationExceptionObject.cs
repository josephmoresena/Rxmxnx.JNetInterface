namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JInstantiationExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.InstantiationException</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JInstantiationExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JInstantiationExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.InstantiationExceptionHash, 32);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JInstantiationExceptionObject>(JInstantiationExceptionObject.typeInfo,
		                                                                      IClassType
			                                                                      .GetMetadata<
				                                                                      JReflectiveOperationExceptionObject>(),
		                                                                      JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JInstantiationExceptionObject>.Metadata
		=> JInstantiationExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInstantiationExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JInstantiationExceptionObject IClassType<JInstantiationExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JInstantiationExceptionObject IClassType<JInstantiationExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JInstantiationExceptionObject IClassType<JInstantiationExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}