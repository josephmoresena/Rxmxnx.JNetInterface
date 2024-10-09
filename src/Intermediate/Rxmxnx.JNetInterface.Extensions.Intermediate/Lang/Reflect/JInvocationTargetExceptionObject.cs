namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JThrowableTypeMetadata<JInvocationTargetExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.InvocationTargetException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JInvocationTargetExceptionObject : JReflectiveOperationExceptionObject,
	IThrowableType<JInvocationTargetExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.InvocationTargetExceptionHash, 43);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JInvocationTargetExceptionObject>(
			    JInvocationTargetExceptionObject.typeInfo,
			    IClassType.GetMetadata<JReflectiveOperationExceptionObject>(), JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JInvocationTargetExceptionObject>.Metadata
		=> JInvocationTargetExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JInvocationTargetExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInvocationTargetExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInvocationTargetExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JInvocationTargetExceptionObject IClassType<JInvocationTargetExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JInvocationTargetExceptionObject IClassType<JInvocationTargetExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JInvocationTargetExceptionObject IClassType<JInvocationTargetExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}