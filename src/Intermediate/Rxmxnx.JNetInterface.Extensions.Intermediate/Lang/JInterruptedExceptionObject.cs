namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JInterruptedExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.InterruptedException</c> instance.
/// </summary>
public class JInterruptedExceptionObject : JExceptionObject, IThrowableType<JInterruptedExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.InterruptedExceptionHash, 30);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JInterruptedExceptionObject>(JInterruptedExceptionObject.typeInfo,
		                                                                    IClassType.GetMetadata<JExceptionObject>(),
		                                                                    JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JInterruptedExceptionObject>.Metadata
		=> JInterruptedExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JInterruptedExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInterruptedExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInterruptedExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JInterruptedExceptionObject IClassType<JInterruptedExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JInterruptedExceptionObject IClassType<JInterruptedExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JInterruptedExceptionObject IClassType<JInterruptedExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}