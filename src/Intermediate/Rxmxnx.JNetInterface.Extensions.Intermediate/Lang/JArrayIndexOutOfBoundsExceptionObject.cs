namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ArrayIndexOutOfBoundsException</c> instance.
/// </summary>
public class JArrayIndexOutOfBoundsExceptionObject : JIndexOutOfBoundsExceptionObject,
	IThrowableType<JArrayIndexOutOfBoundsExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JIndexOutOfBoundsExceptionObject>
	                                                              .Create<JArrayIndexOutOfBoundsExceptionObject>(
		                                                              UnicodeClassNames
			                                                              .ArrayIndexOutOfBoundsExceptionObject())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JArrayIndexOutOfBoundsExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.GlobalInitializer initializer) :
		base(initializer) { }
	/// <inheritdoc/>
	protected JArrayIndexOutOfBoundsExceptionObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer) { }

	static JArrayIndexOutOfBoundsExceptionObject IReferenceType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JArrayIndexOutOfBoundsExceptionObject IReferenceType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JArrayIndexOutOfBoundsExceptionObject IReferenceType<JArrayIndexOutOfBoundsExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}