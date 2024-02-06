namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.IncompatibleClassChangeError</c> instance.
/// </summary>
public class JIncompatibleClassChangeErrorObject : JLinkageErrorObject,
	IThrowableType<JIncompatibleClassChangeErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata = JTypeMetadataBuilder<JLinkageErrorObject>
	                                                              .Create<JIncompatibleClassChangeErrorObject>(
		                                                              UnicodeClassNames
			                                                              .IncompatibleClassChangeErrorObject())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JIncompatibleClassChangeErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JIncompatibleClassChangeErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIncompatibleClassChangeErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JIncompatibleClassChangeErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JIncompatibleClassChangeErrorObject IReferenceType<JIncompatibleClassChangeErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIncompatibleClassChangeErrorObject IReferenceType<JIncompatibleClassChangeErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIncompatibleClassChangeErrorObject IReferenceType<JIncompatibleClassChangeErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}