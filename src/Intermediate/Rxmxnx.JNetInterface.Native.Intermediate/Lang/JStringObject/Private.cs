namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JStringObject> typeMetadata = JTypeMetadataBuilder<JStringObject>
	                                                                         .Create(UnicodeClassNames.StringObject(),
		                                                                         JTypeModifier.Final)
	                                                                         .Implements<JSerializableObject>()
	                                                                         .Implements<JComparableObject>()
	                                                                         .Implements<JCharSequenceObject>().Build();

	static JClassTypeMetadata<JStringObject> IClassType<JStringObject>.Metadata => JStringObject.typeMetadata;

	/// <summary>
	/// String length.
	/// </summary>
	private Int32? _length;
	/// <summary>
	/// UTF-8 string length.
	/// </summary>
	private Int32? _utf8Length;

	/// <summary>
	/// Instance value.
	/// </summary>
	private String? _value;

	/// <inheritdoc/>
	private JStringObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JStringObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassFeature.StringClassObject)
	{
		if (jLocal is not JStringObject jString)
			return;
		this._length = jString.Length;
		this._utf8Length = jString._utf8Length;
		this._value = jString._value;
	}

	/// <summary>
	/// Retrieves hash code of current text.
	/// </summary>
	/// <returns>A <see cref="Nullable{Int32}"/> hash code.</returns>
	private Int32? GetStringHashCode() => this._value?.GetHashCode();

	/// <summary>
	/// Retrieves UTF-16 chars.
	/// </summary>
	/// <param name="chars">Span to copy chars to.</param>
	/// <param name="arg">Operation arguments.</param>
	private static void GetChars(Span<Char> chars, (JStringObject jStr, Int32 startIndex) arg)
	{
		IEnvironment env = arg.jStr.Environment;
		env.StringFeature.GetCopy(arg.jStr, chars, arg.startIndex);
	}

	static JStringObject IReferenceType<JStringObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer.WithClass<JStringObject>());
	static JStringObject IReferenceType<JStringObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.Instance);
	static JStringObject IReferenceType<JStringObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer.Environment, initializer.Global);
}