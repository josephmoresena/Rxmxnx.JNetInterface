namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.NoSuchMethodError</c> instance.
/// </summary>
public class JNoSuchMethodErrorObject : JIncompatibleClassChangeErrorObject, IThrowableType<JNoSuchMethodErrorObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata typeMetadata =
		JTypeMetadataBuilder<JIncompatibleClassChangeErrorObject>
			.Create<JNoSuchMethodErrorObject>(UnicodeClassNames.NoSuchMethodErrorObject()).Build();

	static JDataTypeMetadata IDataType.Metadata => JNoSuchMethodErrorObject.typeMetadata;

	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNoSuchMethodErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNoSuchMethodErrorObject IReferenceType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNoSuchMethodErrorObject IReferenceType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNoSuchMethodErrorObject IReferenceType<JNoSuchMethodErrorObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}