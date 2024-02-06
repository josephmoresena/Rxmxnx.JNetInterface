namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.StringIndexOutOfBoundsException</c> instance.
/// </summary>
public class JStringIndexOutOfBoundsExceptionObject : JIndexOutOfBoundsExceptionObject,
	IThrowableType<JStringIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JIndexOutOfBoundsExceptionObject>
	                                                              .Create<JStringIndexOutOfBoundsExceptionObject>(
		                                                              UnicodeClassNames
			                                                              .StringIndexOutOfBoundsExceptionObject())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JStringIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JStringIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) :
		base(initializer) { }
	/// <inheritdoc/>
	protected JStringIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) :
		base(initializer) { }
	/// <inheritdoc/>
	protected JStringIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer) { }

	static JStringIndexOutOfBoundsExceptionObject IReferenceType<JStringIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JStringIndexOutOfBoundsExceptionObject IReferenceType<JStringIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JStringIndexOutOfBoundsExceptionObject IReferenceType<JStringIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}