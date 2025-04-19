namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JClassNotFoundExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ClassNotFoundException</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JClassNotFoundExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JClassNotFoundExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ClassNotFoundExceptionHash, 32);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JClassNotFoundExceptionObject>(JClassNotFoundExceptionObject.typeInfo,
		                                                                      IClassType
			                                                                      .GetMetadata<
				                                                                      JReflectiveOperationExceptionObject>(),
		                                                                      JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JClassNotFoundExceptionObject>.Metadata
		=> JClassNotFoundExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassNotFoundExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassNotFoundExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassNotFoundExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassNotFoundExceptionObject IClassType<JClassNotFoundExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassNotFoundExceptionObject IClassType<JClassNotFoundExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassNotFoundExceptionObject IClassType<JClassNotFoundExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}