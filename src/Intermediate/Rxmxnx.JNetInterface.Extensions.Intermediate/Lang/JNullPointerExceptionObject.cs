namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JNullPointerExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.NullPointerException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JNullPointerExceptionObject : JRuntimeExceptionObject, IThrowableType<JNullPointerExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.NullPointerExceptionHash, 30);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JNullPointerExceptionObject>(JNullPointerExceptionObject.typeInfo,
		                                                                    IClassType
			                                                                    .GetMetadata<JRuntimeExceptionObject>(),
		                                                                    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JNullPointerExceptionObject>.Metadata
		=> JNullPointerExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JNullPointerExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNullPointerExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNullPointerExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNullPointerExceptionObject IClassType<JNullPointerExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNullPointerExceptionObject IClassType<JNullPointerExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNullPointerExceptionObject IClassType<JNullPointerExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}