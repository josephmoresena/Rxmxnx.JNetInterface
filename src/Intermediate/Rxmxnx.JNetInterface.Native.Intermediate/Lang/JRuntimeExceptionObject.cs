namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JRuntimeExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.RuntimeException</c> instance.
/// </summary>
public class JRuntimeExceptionObject : JExceptionObject, IThrowableType<JRuntimeExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.RuntimeExceptionHash, 26);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JRuntimeExceptionObject>(JRuntimeExceptionObject.typeInfo,
		                                                                IClassType.GetMetadata<JExceptionObject>(),
		                                                                JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JRuntimeExceptionObject>.Metadata => JRuntimeExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JRuntimeExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JRuntimeExceptionObject IClassType<JRuntimeExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JRuntimeExceptionObject IClassType<JRuntimeExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JRuntimeExceptionObject IClassType<JRuntimeExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}