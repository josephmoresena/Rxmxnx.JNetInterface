namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JStringObject>;

public partial class JStringObject
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.StringHash, 16);
	/// <summary>
	/// Datatype interfaces.
	/// </summary>
	private static readonly ImmutableHashSet<JInterfaceTypeMetadata> typeInterfaces =
	[
		IInterfaceType.GetMetadata<JSerializableObject>(),
		IInterfaceType.GetMetadata<JComparableObject>(),
		IInterfaceType.GetMetadata<JCharSequenceObject>(),
	];
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JStringObject>(
		JStringObject.typeInfo, JTypeModifier.Final, (InterfaceSet)JStringObject.typeInterfaces);

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
	/// UTF-8 string long length.
	/// </summary>
	private Int64? _utf8LongLength;

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
		this._utf8LongLength = jString._utf8LongLength;
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
		this._utf8LongLength ??= this.Environment.StringFeature.GetUtf8LongLength(jGlobal);
		this._utf8Length ??= JStringObject.GetUtfLength(env, jGlobal, this._utf8LongLength);
	}

	/// <summary>
	/// Retrieves UTF-16 chars.
	/// </summary>
	/// <param name="chars">Span to copy chars to.</param>
	/// <param name="arg">Operation arguments.</param>
	private static void GetChars(Span<Char> chars, (JStringObject jString, Int32 startIndex) arg)
		=> arg.jString.Get(chars, arg.startIndex);

	/// <summary>
	/// Retrieves the UTF-8 length of <paramref name="jObject"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="longValue">UTF-8 string long length.</param>
	/// <returns>UTF-8 string length.</returns>
	private static Int32? GetUtfLength(IEnvironment env, JReferenceObject jObject, Int64? longValue)
	{
		if (longValue.HasValue)
			return longValue.Value <= Int32.MaxValue ? (Int32)longValue.Value : null;
		return env.StringFeature.GetUtf8Length(jObject);
	}

	static JStringObject IClassType<JStringObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer.WithClass<JStringObject>());
	static JStringObject IClassType<JStringObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer.Instance);
	static JStringObject IClassType<JStringObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer.Environment, initializer.Global);
}