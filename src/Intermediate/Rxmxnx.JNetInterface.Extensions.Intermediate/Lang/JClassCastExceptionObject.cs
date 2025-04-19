namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JClassCastExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.ClassCastException</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JClassCastExceptionObject : JRuntimeExceptionObject, IThrowableType<JClassCastExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ClassCastExceptionHash, 28);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JClassCastExceptionObject>(
			    JClassCastExceptionObject.typeInfo, IClassType.GetMetadata<JRuntimeExceptionObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JClassCastExceptionObject>.Metadata => JClassCastExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JClassCastExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCastExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JClassCastExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JClassCastExceptionObject IClassType<JClassCastExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JClassCastExceptionObject IClassType<JClassCastExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JClassCastExceptionObject IClassType<JClassCastExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}