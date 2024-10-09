namespace Rxmxnx.JNetInterface.Lang.Annotation;

using TypeMetadata = JEnumTypeMetadata<JElementTypeObject>;

/// <summary>
/// This class represents a local <c>java.lang.annotation.ElementType</c> instance.
/// </summary>
public sealed class JElementTypeObject : JEnumObject<JElementTypeObject>, IEnumType<JElementTypeObject>
{
	/// <summary>
	/// <c>java.lang.annotation.ElementType</c> ordinals.
	/// </summary>
	public enum ElementType
	{
		/// <summary>
		/// Class, interface (including annotation type), or enum declaration
		/// </summary>
		Type = 0x0,
		/// <summary>
		/// Field declaration (includes enum constants)
		/// </summary>
		Field = 0x1,
		/// <summary>
		/// Method declaration
		/// </summary>
		Method = 0x2,
		/// <summary>
		/// Parameter declaration
		/// </summary>
		Parameter = 0x3,
		/// <summary>
		/// Constructor declaration
		/// </summary>
		Constructor = 0x4,
		/// <summary>
		/// Local variable declaration
		/// </summary>
		LocalVariable = 0x5,
		/// <summary>
		/// Annotation type declaration
		/// </summary>
		AnnotationType = 0x6,
		/// <summary>
		/// Package declaration
		/// </summary>
		Package = 0x7,
	}

	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.ElementTypeHash, 32);
	/// <summary>
	/// Datatype values.
	/// </summary>
	private static readonly IEnumFieldList typeValues = JEnumObject.CreateFieldList([
		new(() => "TYPE"u8),
		new(() => "FIELD"u8),
		new(() => "METHOD"u8),
		new(() => "PARAMETER"u8),
		new(() => "CONSTRUCTOR"u8),
		new(() => "LOCAL_VARIABLE"u8),
		new(() => "ANNOTATION_TYPE"u8),
		new(() => "PACKAGE"u8),
	]);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JEnumObject.CreateBuiltInMetadata<JElementTypeObject>(JElementTypeObject.typeInfo,
		                                                      JElementTypeObject.typeValues);

	static TypeMetadata IEnumType<JElementTypeObject>.Metadata => JElementTypeObject.typeMetadata;

	/// <inheritdoc/>
	private JElementTypeObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JElementTypeObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JElementTypeObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }

	static JElementTypeObject IEnumType<JElementTypeObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JElementTypeObject IEnumType<JElementTypeObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JElementTypeObject IEnumType<JElementTypeObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}