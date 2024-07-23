namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JStringObject>;

public partial class JStringObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JStringObject>
	                                                    .Create("java/lang/String"u8, JTypeModifier.Final)
	                                                    .Implements<JSerializableObject>()
	                                                    .Implements<JComparableObject>()
	                                                    .Implements<JCharSequenceObject>().Build();

	// ReSharper disable once UseSymbolAlias
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
	private JStringObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassFeature.StringObject)
	{
		if (jLocal is not JStringObject jString)
			return;
		this._length = jString.Length;
		this._utf8Length = jString._utf8Length;
		this._value = jString._value;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	private JStringObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
	{
		this._length ??= this.Environment.StringFeature.GetLength(jGlobal);
		this._utf8Length ??= this.Environment.StringFeature.GetUtf8Length(jGlobal);
	}

	/// <summary>
	/// Retrieves UTF-16 chars.
	/// </summary>
	/// <param name="chars">Span to copy chars to.</param>
	/// <param name="arg">Operation arguments.</param>
	private static void GetChars(Span<Char> chars, (JStringObject jString, Int32 startIndex) arg)
		=> arg.jString.Get(chars, arg.startIndex);

	static JStringObject IClassType<JStringObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer.WithClass<JStringObject>());
	static JStringObject IClassType<JStringObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.Instance);
	static JStringObject IClassType<JStringObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer.Environment, initializer.Global);
}