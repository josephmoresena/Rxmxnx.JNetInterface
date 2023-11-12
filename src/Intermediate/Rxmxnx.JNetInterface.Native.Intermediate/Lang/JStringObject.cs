namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.String</c> instance.
/// </summary>
public sealed partial class JStringObject : JLocalObject, IClassType<JStringObject>, IWrapper<String>,
	IInterfaceImplementation<JStringObject, JSerializableObject>,
	IInterfaceImplementation<JStringObject, JComparableObject>,
	IInterfaceImplementation<JStringObject, JCharSequenceObject>
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(JStringObjectMetadata);

	/// <inheritdoc cref="JLocalObject.InternalReference"/>
	internal JStringLocalRef Reference => this.As<JStringLocalRef>();
	/// <summary>
	/// UTF-16 length.
	/// </summary>
	public Int32 Length => this._length ?? this._value?.Length ?? this.Environment.StringProvider.GetLength(this);
	/// <summary>
	/// UTF-8 length.
	/// </summary>
	public Int32 Utf8Length => this._utf8Length ?? this.Environment.StringProvider.GetUtf8Length(this);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JStringObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
	{
		if (this._length is not null) return;
		this._value ??= env.StringProvider.ToString(jGlobal);
		this._length ??= this._value.Length;
	}

	/// <summary>
	/// Internal string value.
	/// </summary>
	public new String Value => this._value ??= this.Environment.StringProvider.ToString(this);

	/// <inheritdoc/>
	public override String ToString() => this.Value;
	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JStringObjectMetadata(base.CreateMetadata())
		{
			Length = this.Length, Utf8Length = this.Utf8Length, Value = this._value,
		};
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not JStringObjectMetadata stringMetadata)
			return;
		this._value = stringMetadata.Value;
		this._length = stringMetadata.Length;
		this._utf8Length = stringMetadata.Utf8Length;
	}
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="data"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="data">UTF-16 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	public static JStringObject? Create(IEnvironment env, String? data)
		=> data is not null ? env.StringProvider.Create(data) : default;
	/// <inheritdoc cref="JStringObject.Create(IEnvironment, String)"/>
	public static JStringObject Create(IEnvironment env, ReadOnlySpan<Char> data) => env.StringProvider.Create(data);
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="utf8Data"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="utf8Data">UTF-8 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	public static JStringObject? Create(IEnvironment env, CString? utf8Data)
		=> utf8Data is not null ? env.StringProvider.Create(utf8Data) : default;
	/// <inheritdoc cref="JStringObject.Create(IEnvironment, CString)"/>
	public static JStringObject Create(IEnvironment env, ReadOnlySpan<Byte> utf8Data)
		=> env.StringProvider.Create(utf8Data);

	/// <inheritdoc/>
	public static JStringObject? Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JStringObject>(jLocal)) : default;
}