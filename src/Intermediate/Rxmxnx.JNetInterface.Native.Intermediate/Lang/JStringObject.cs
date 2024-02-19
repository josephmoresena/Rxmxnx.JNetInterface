namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.String</c> instance.
/// </summary>
[DebuggerDisplay(nameof(JStringObject.DisplayValue))]
public sealed partial class JStringObject : JLocalObject, IClassType<JStringObject>, IWrapper<String>,
	IInterfaceObject<JSerializableObject>, IInterfaceObject<JComparableObject>, IInterfaceObject<JCharSequenceObject>
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(StringObjectMetadata);

	/// <inheritdoc cref="JLocalObject.InternalReference"/>
	internal JStringLocalRef Reference => this.As<JStringLocalRef>();

	/// <summary>
	/// Internal property to debugger display.
	/// </summary>
	internal String DisplayValue
		=> this._value ?? (this._length is not null ?
			$"{this.Reference} Length: {this.Length}" :
			this.Reference.ToString());
	/// <summary>
	/// UTF-16 length.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int32 Length => this._length ?? this._value?.Length ?? this.Environment.StringFeature.GetLength(this);
	/// <summary>
	/// UTF-8 length.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int32 Utf8Length => this._utf8Length ?? this.Environment.StringFeature.GetUtf8Length(this);

	/// <summary>
	/// Internal string value.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String Value => this._value ??= String.Create(this.Length, (this, 0), JStringObject.GetChars);

	/// <inheritdoc/>
	public override String ToString() => this.Value;

	/// <summary>
	/// Creates an <see cref="String"/> containing a copy of the UTF-16 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </summary>
	/// <param name="startIndex">Initial index to copy to.</param>
	/// <returns>
	/// An <see cref="String"/> containing a copy of the UTF-16 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </returns>
	public String GetChars(Int32 startIndex = 0) => this.GetChars(startIndex, this.Length);
	/// <summary>
	/// Creates an <see cref="String"/> containing a copy of the UTF-16 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </summary>
	/// <param name="startIndex">Initial index to copy to.</param>
	/// <param name="count">The number of chars to copy to.</param>
	/// <returns>
	/// An <see cref="String"/> containing a copy of the UTF-16 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </returns>
	public String GetChars(Int32 startIndex, Int32 count)
	{
		if (startIndex == 0 && count == this.Length)
			return this.Value;
		if (this._value is not null)
			return this._value[startIndex..count];
		Int32 length = count - startIndex;
		return String.Create(length, (this, startIndex), JStringObject.GetChars);
	}
	/// <summary>
	/// Creates an <see cref="CString"/> containing a copy of the UTF-8 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </summary>
	/// <param name="startIndex">Initial index to copy to.</param>
	/// <returns>
	/// An <see cref="CString"/> containing a copy of the UTF-8 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </returns>
	public CString GetUtf8Chars(Int32 startIndex = 0) => this.GetUtf8Chars(startIndex, this.Utf8Length);
	/// <summary>
	/// Creates an <see cref="CString"/> containing a copy of the UTF-8 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </summary>
	/// <param name="startIndex">Initial index to copy to.</param>
	/// <param name="count">The number of chars to copy to.</param>
	/// <returns>
	/// An <see cref="CString"/> containing a copy of the UTF-8 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </returns>
	public CString GetUtf8Chars(Int32 startIndex, Int32 count)
	{
		Int32 length = count - startIndex;
		Byte[] utf8Data = new Byte[length + 1];
		IEnvironment env = this.Environment;
		env.StringFeature.GetCopyUtf8(this, utf8Data.AsMemory()[..^1], startIndex);
		return utf8Data;
	}

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.GetStringHashCode() ?? base.GetHashCode();

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new StringObjectMetadata(base.CreateMetadata())
		{
			Length = this.Length, Utf8Length = this.Utf8Length, Value = this._value,
		};
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not StringObjectMetadata stringMetadata)
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
	[return: NotNullIfNotNull(nameof(data))]
	public static JStringObject? Create(IEnvironment env, String? data)
		=> data is not null ? env.StringFeature.Create(data) : default;
	/// <inheritdoc cref="JStringObject.Create(IEnvironment, String)"/>
	public static JStringObject Create(IEnvironment env, ReadOnlySpan<Char> data) => env.StringFeature.Create(data);
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="utf8Data"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="utf8Data">UTF-8 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	[return: NotNullIfNotNull(nameof(utf8Data))]
	public static JStringObject? Create(IEnvironment env, CString? utf8Data)
		=> utf8Data is not null ? env.StringFeature.Create(utf8Data) : default;
	/// <inheritdoc cref="JStringObject.Create(IEnvironment, CString)"/>
	public static JStringObject Create(IEnvironment env, ReadOnlySpan<Byte> utf8Data)
		=> env.StringFeature.Create(utf8Data);
}