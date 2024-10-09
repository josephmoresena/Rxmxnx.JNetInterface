namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JSecurityExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.SecurityException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JSecurityExceptionObject : JRuntimeExceptionObject, IThrowableType<JSecurityExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.SecurityExceptionHash, 27);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JSecurityExceptionObject>(
			    JSecurityExceptionObject.typeInfo, IClassType.GetMetadata<JRuntimeExceptionObject>(),
			    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JSecurityExceptionObject>.Metadata => JSecurityExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JSecurityExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JSecurityExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JSecurityExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JSecurityExceptionObject IClassType<JSecurityExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JSecurityExceptionObject IClassType<JSecurityExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JSecurityExceptionObject IClassType<JSecurityExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}