namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JArrayIndexOutOfBoundsExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ArrayIndexOutOfBoundsException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JArrayIndexOutOfBoundsExceptionObject : JIndexOutOfBoundsExceptionObject,
	IThrowableType<JArrayIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ArrayIndexOutOfBoundsExceptionHash, 40);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JArrayIndexOutOfBoundsExceptionObject>(
			    JArrayIndexOutOfBoundsExceptionObject.typeInfo,
			    IClassType.GetMetadata<JIndexOutOfBoundsExceptionObject>(), JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JArrayIndexOutOfBoundsExceptionObject>.Metadata
		=> JArrayIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) :
		base(initializer) { }
	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer) { }

	static JArrayIndexOutOfBoundsExceptionObject IClassType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JArrayIndexOutOfBoundsExceptionObject IClassType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JArrayIndexOutOfBoundsExceptionObject IClassType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}