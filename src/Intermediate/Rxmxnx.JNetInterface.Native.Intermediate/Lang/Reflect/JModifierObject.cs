namespace Rxmxnx.JNetInterface.Lang.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Modifier</c> instance.
/// </summary>
public class JModifierObject : JLocalObject, IClassType<JModifierObject>
{
	/// <summary>
	/// Enum for <c>java.lang.reflect.Modifier</c> constants.
	/// </summary>
	[Flags]
	public enum Modifier
	{
		/// <summary>
		/// <c>public</c> modifier.
		/// </summary>
		Public = 0x1,
		/// <summary>
		/// <c>private</c> modifier.
		/// </summary>
		Private = 0x2,
		/// <summary>
		/// <c>protected</c> modifier.
		/// </summary>
		Protected = 0x4,
		/// <summary>
		/// <c>static</c> modifier.
		/// </summary>
		Static = 0x8,
		/// <summary>
		/// <c>final</c> modifier.
		/// </summary>
		Final = 0x10,
		/// <summary>
		/// <c>synchronized</c> modifier.
		/// </summary>
		Synchronized = 0x20,
		/// <summary>
		/// <c>volatile</c> modifier.
		/// </summary>
		Volatile = 0x40,
		/// <summary>
		/// <c>transient</c> modifier.
		/// </summary>
		Transient = 0x80,
		/// <summary>
		/// <c>native</c> modifier.
		/// </summary>
		Native = 0x100,
		/// <summary>
		/// <c>interface</c> modifier.
		/// </summary>
		Interface = 0x200,
		/// <summary>
		/// <c>abstract</c> modifier.
		/// </summary>
		Abstract = 0x400,
		/// <summary>
		/// <c>strictfp</c> modifier.
		/// </summary>
		Strict = 0x800,
		/// <summary>
		/// <c>synthetic</c> modifier.
		/// </summary>
		Synthetic = 0x1000,
		/// <summary>
		/// <c>annotation</c> modifier.
		/// </summary>
		Annotation = 0x2000,
		/// <summary>
		/// <c>enum</c> modifier.
		/// </summary>
		Enum = 0x4000,
	}

	/// <summary>
	/// Modifiers for any primitive type.
	/// </summary>
	public const Modifier PrimitiveModifiers = Modifier.Abstract | Modifier.Final | Modifier.Public;

	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JModifierObject> metadata = TypeMetadataBuilder<JModifierObject>
	                                                                       .Create(UnicodeClassNames.ModifierObject())
	                                                                       .Build();

	static JClassTypeMetadata<JModifierObject> IClassType<JModifierObject>.Metadata => JModifierObject.metadata;

	/// <inheritdoc/>
	protected JModifierObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JModifierObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JModifierObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JModifierObject IClassType<JModifierObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JModifierObject IClassType<JModifierObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JModifierObject IClassType<JModifierObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}