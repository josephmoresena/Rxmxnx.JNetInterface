namespace Rxmxnx.JNetInterface.Text;

using TypeMetadata = JThrowableTypeMetadata<JParseExceptionObject>;

/// <summary>
/// This class represents a local <c>java.text.ParseException</c> instance.
/// </summary>
public class JParseExceptionObject : JExceptionObject, IThrowableType<JParseExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ParseExceptionHash, 24);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JParseExceptionObject>(JParseExceptionObject.typeInfo,
		                                                              IClassType.GetMetadata<JExceptionObject>(),
		                                                              JTypeModifier.Extensible));

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