namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JIllegalArgumentExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.IllegalArgumentException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JIllegalArgumentExceptionObject : JRuntimeExceptionObject, IThrowableType<JIllegalArgumentExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.IllegalArgumentExceptionHash, 32);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JIllegalArgumentExceptionObject>(
			    JIllegalArgumentExceptionObject.typeInfo, IClassType.GetMetadata<JRuntimeExceptionObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JIllegalArgumentExceptionObject>.Metadata
		=> JIllegalArgumentExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIllegalArgumentExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalArgumentExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIllegalArgumentExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIllegalArgumentExceptionObject IClassType<JIllegalArgumentExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIllegalArgumentExceptionObject IClassType<JIllegalArgumentExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIllegalArgumentExceptionObject IClassType<JIllegalArgumentExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}