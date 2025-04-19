namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JIllegalStateExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.IllegalStateException</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JIllegalStateExceptionObject : JRuntimeExceptionObject, IThrowableType<JIllegalStateExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.IllegalStateExceptionHash, 31);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JIllegalStateExceptionObject>(
			    JIllegalStateExceptionObject.typeInfo, IClassType.GetMetadata<JRuntimeExceptionObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JIllegalStateExceptionObject>.Metadata
		=> JIllegalStateExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIllegalStateExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalStateExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalStateExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIllegalStateExceptionObject IClassType<JIllegalStateExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIllegalStateExceptionObject IClassType<JIllegalStateExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIllegalStateExceptionObject IClassType<JIllegalStateExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}