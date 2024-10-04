namespace Rxmxnx.JNetInterface.Io;

using TypeMetadata = JThrowableTypeMetadata<JFileNotFoundExceptionObject>;

/// <summary>
/// This class represents a local <c>java.io.FileNotFoundException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JFileNotFoundExceptionObject : JIoExceptionObject, IThrowableType<JFileNotFoundExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.FileNotFoundExceptionHash, 29);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JFileNotFoundExceptionObject>(JFileNotFoundExceptionObject.typeInfo,
		                                                                     IClassType
			                                                                     .GetMetadata<JIoExceptionObject>(),
		                                                                     JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JFileNotFoundExceptionObject>.Metadata
		=> JFileNotFoundExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JFileNotFoundExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JFileNotFoundExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JFileNotFoundExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JFileNotFoundExceptionObject IClassType<JFileNotFoundExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFileNotFoundExceptionObject IClassType<JFileNotFoundExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFileNotFoundExceptionObject IClassType<JFileNotFoundExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}