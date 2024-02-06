namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.ExceptionInInitializerError</c> instance.
/// </summary>
public class JExceptionInInitializerErrorObject : JLinkageErrorObject,
	IThrowableType<JExceptionInInitializerErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JLinkageErrorObject>
	                                                              .Create<JExceptionInInitializerErrorObject>(
		                                                              UnicodeClassNames
			                                                              .ExceptionInInitializerErrorObject()).Build();

	static JDataTypeMetadata IDataType.Metadata => JExceptionInInitializerErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExceptionInInitializerErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JExceptionInInitializerErrorObject IReferenceType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JExceptionInInitializerErrorObject IReferenceType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JExceptionInInitializerErrorObject IReferenceType<JExceptionInInitializerErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}