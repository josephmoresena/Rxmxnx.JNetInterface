namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.String</c> instance.
/// </summary>
[DebuggerDisplay(nameof(JStringObject.DisplayValue))]
public sealed partial class JStringObject : JLocalObject, IClassType<JStringObject>, IWrapper<String>,
	IInterfaceObject<JSerializableObject>, IInterfaceObject<JComparableObject>, IInterfaceObject<JCharSequenceObject>
{
	/// <summary>
	/// JNI string reference.
	/// </summary>
	public new JStringLocalRef Reference => this.As<JStringLocalRef>();

	/// <summary>
	/// Internal property to debugger display.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal String DisplayValue
		=> this._value ?? (this._length is not null ?
			$"{this.Reference} Length: {this.Length}" :
			this.Reference.ToString());

	/// <summary>
	/// UTF-16 length.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int32 Length => this._length ??= this._value?.Length ?? this.Environment.StringFeature.GetLength(this);
	/// <summary>
	/// UTF-8 length.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int32 Utf8Length
	{
		get
		{
			this._utf8Length ??= JStringObject.GetUtfLength(this.Environment, this, this.Utf8LongLength);
			return this._utf8Length ?? -1;
		}
	}
	/// <summary>
	/// UTF-8 long length.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public Int64 Utf8LongLength
	{
		get
		{
			this._utf8LongLength ??= this.Environment.StringFeature.GetUtf8LongLength(this);
			if (this._utf8LongLength.HasValue)
				return this._utf8LongLength.Value;

			return this._utf8Length ??= this.Environment.StringFeature.GetUtf8Length(this);
		}
	}

	/// <inheritdoc/>
	public Int32 CompareTo(JStringObject? other) => this.CompareTo(other?.Value);
	/// <inheritdoc/>
	public Int32 CompareTo(String? other) => String.Compare(this.Value, other, StringComparison.Ordinal);

	/// <summary>
	/// Internal string value.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public String Value => this._value ??= String.Create(this.Length, (this, 0), JStringObject.GetChars);

	/// <inheritdoc/>
	public override String ToString() => this.Value;
	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public override String ToTraceText()
		=> $"{JObject.GetObjectIdentifier(this.Class.ClassSignature, this.Reference)} length: {this.Length}";

	/// <summary>
	/// Creates an <see cref="String"/> containing a copy of the UTF-16 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </summary>
	/// <param name="startIndex">Initial index to copy to.</param>
	/// <returns>
	/// An <see cref="String"/> containing a copy of the UTF-16 chars on the current
	/// <see cref="JStringObject"/> instance.
	/// </returns>
	public String GetChars(Int32 startIndex = 0) => this.GetChars(startIndex, this.Length - startIndex);
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
		return this._value is not null ?
			this._value[startIndex..(startIndex + count)] :
			String.Create(count, (this, startIndex), JStringObject.GetChars);
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
		Memory<Byte> mem = utf8Data.AsMemory()[..^1];
		this.GetUtf8(mem.Span, startIndex);
		return utf8Data;
	}
	/// <summary>
	/// Retrieves a copy of the UTF-16 chars from the current <see cref="JStringObject"/> into
	/// <paramref name="chars"/>.
	/// </summary>
	/// <param name="chars">A <see cref="Span{Char}"/> to copy to.</param>
	/// <param name="startIndex">Initial UTF-16 char to copy from.</param>
	public void Get(Span<Char> chars, Int32 startIndex = 0)
	{
		IEnvironment env = this.Environment;
		env.StringFeature.GetCopy(this, chars, startIndex);
	}
	/// <summary>
	/// Retrieves a copy of the UTF-8 chars from the current <see cref="JStringObject"/> into
	/// <paramref name="utf8Units"/>.
	/// </summary>
	/// <param name="utf8Units">A <see cref="Span{Byte}"/> to copy to.</param>
	/// <param name="startIndex">Initial UTF-8 char to copy from.</param>
	public void GetUtf8(Span<Byte> utf8Units, Int32 startIndex = 0)
	{
		IEnvironment env = this.Environment;
		env.StringFeature.GetUtf8Copy(this, utf8Units, startIndex);
	}
	/// <inheritdoc cref="String.GetEnumerator()"/>
	public CharEnumerator GetEnumerator() => this.Value.GetEnumerator();

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Value.GetHashCode();

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
	{
		Int32? utf8Length = JStringObject.GetUtfLength(this.Environment, this, this.Utf8LongLength);
		return new StringObjectMetadata(base.CreateMetadata())
		{
			Length = this.Length,
			Utf8Length = utf8Length,
			Utf8LongLength = this._utf8LongLength,
			Value = this._value,
		};
	}
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not StringObjectMetadata stringMetadata)
			return;
		this._value = stringMetadata.Value;
		this._length = stringMetadata.Length;
		this._utf8Length = stringMetadata.Utf8Length;
		this._utf8LongLength = stringMetadata.Utf8LongLength;
	}

	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="data"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="data">UTF-16 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	[return: NotNullIfNotNull(nameof(data))]
	public static JStringObject? Create(IEnvironment env, String? data)
		=> data is not null ? env.StringFeature.Create(data, data) : default;
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