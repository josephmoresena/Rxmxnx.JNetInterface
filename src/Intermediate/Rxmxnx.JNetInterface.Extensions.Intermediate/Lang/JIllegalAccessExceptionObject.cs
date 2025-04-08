namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JIllegalAccessExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.IllegalAccessException</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JIllegalAccessExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JIllegalAccessExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.IllegalAccessExceptionHash, 32);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JIllegalAccessExceptionObject>(
			    JIllegalAccessExceptionObject.typeInfo, IClassType.GetMetadata<JReflectiveOperationExceptionObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JIllegalAccessExceptionObject>.Metadata
		=> JIllegalAccessExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIllegalAccessExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalAccessExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalAccessExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIllegalAccessExceptionObject IClassType<JIllegalAccessExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIllegalAccessExceptionObject IClassType<JIllegalAccessExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIllegalAccessExceptionObject IClassType<JIllegalAccessExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}