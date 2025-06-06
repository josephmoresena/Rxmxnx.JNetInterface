namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JIndexOutOfBoundsExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.IndexOutOfBoundsException</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JIndexOutOfBoundsExceptionObject : JRuntimeExceptionObject,
	IThrowableType<JIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.IndexOutOfBoundsExceptionHash, 35);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JIndexOutOfBoundsExceptionObject>(
			    JIndexOutOfBoundsExceptionObject.typeInfo, IClassType.GetMetadata<JRuntimeExceptionObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JIndexOutOfBoundsExceptionObject>.Metadata
		=> JIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIndexOutOfBoundsExceptionObject IClassType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIndexOutOfBoundsExceptionObject IClassType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIndexOutOfBoundsExceptionObject IClassType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}