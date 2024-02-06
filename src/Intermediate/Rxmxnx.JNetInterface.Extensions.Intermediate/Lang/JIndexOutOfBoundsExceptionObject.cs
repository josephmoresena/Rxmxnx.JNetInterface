namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.IndexOutOfBoundsException</c> instance.
/// </summary>
public class JIndexOutOfBoundsExceptionObject : JRuntimeExceptionObject,
	IThrowableType<JIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JRuntimeExceptionObject>
	                                                              .Create<JIndexOutOfBoundsExceptionObject>(
		                                                              UnicodeClassNames
			                                                              .IndexOutOfBoundsExceptionObject()).Build();

	static JDataTypeMetadata IDataType.Metadata => JIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIndexOutOfBoundsExceptionObject IReferenceType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIndexOutOfBoundsExceptionObject IReferenceType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIndexOutOfBoundsExceptionObject IReferenceType<JIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}