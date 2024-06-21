namespace Rxmxnx.JNetInterface.Text;

using TypeMetadata = JThrowableTypeMetadata<JParseExceptionObject>;

/// <summary>
/// This class represents a local <c>java.text.ParseException</c> instance.
/// </summary>
public class JParseExceptionObject : JExceptionObject, IThrowableType<JParseExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JExceptionObject>
	                                                    .Create<JParseExceptionObject>("java/text/ParseException"u8)
	                                                    .Build();

	static TypeMetadata IThrowableType<JParseExceptionObject>.Metadata => JParseExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JParseExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JParseExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JParseExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JParseExceptionObject IClassType<JParseExceptionObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JParseExceptionObject IClassType<JParseExceptionObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JParseExceptionObject IClassType<JParseExceptionObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}