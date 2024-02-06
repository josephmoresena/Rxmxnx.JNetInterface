namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.SecurityException</c> instance.
/// </summary>
public class JSecurityExceptionObject : JRuntimeExceptionObject, IThrowableType<JSecurityExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JRuntimeExceptionObject>
	                                                              .Create<JSecurityExceptionObject>(
		                                                              UnicodeClassNames.SecurityExceptionObject())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JSecurityExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JSecurityExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JSecurityExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JSecurityExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JSecurityExceptionObject IReferenceType<JSecurityExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JSecurityExceptionObject IReferenceType<JSecurityExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JSecurityExceptionObject IReferenceType<JSecurityExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}