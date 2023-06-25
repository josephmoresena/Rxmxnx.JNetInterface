namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a <c>java.lang.Object</c> instance.
/// </summary>
public abstract class JObject : IObject, IEquatable<JObject>
{
	/// <summary>
	/// <c>java.lang.Object</c> class name.
	/// </summary>
	public static readonly CString ClassName = UnicodeClassNames.JObjectClassName;
	/// <summary>
	/// <c>java.lang.Object</c> signature.
	/// </summary>
	public static readonly CString Signature = UnicodeObjectSignatures.JObjectSignature;

	/// <summary>
	/// Internal <see cref="JValue"/> instance.
	/// </summary>
	private readonly IMutableReference<JValue> _value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	protected JObject() : this(JValue.Empty) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Object reference.</param>
	protected JObject(JObjectLocalRef value) : this(JValue.Create(value)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	protected JObject(JObject jObject) => this._value = jObject._value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jValue">Internal <see cref="JValue"/> instance.</param>
	internal JObject(JValue jValue) => this._value = IMutableReference.Create(jValue);

	/// <summary>
	/// Object signature.
	/// </summary>
	public abstract CString ObjectClassName { get; }
	/// <summary>
	/// Object signature.
	/// </summary>
	public abstract CString ObjectSignature { get; }

	/// <summary>
	/// Internal <see cref="JValue"/> value.
	/// </summary>
	internal virtual JValue Value
	{
		get => this._value.Value;
	}

	/// <inheritdoc/>
	public virtual Boolean Equals(JObject? other) => this._value.Equals(other?._value);

	CString IObject.ObjectClassName
	{
		get => this.ObjectClassName;
	}
	CString IObject.ObjectSignature
	{
		get => this.ObjectSignature;
	}
	Boolean IObject.IsDefault
	{
		get => this._value.Value.IsDefault;
	}

	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		ReadOnlySpan<Byte> bytes = NativeUtilities.AsBytes(this.As<JObjectLocalRef>());
		bytes.CopyTo(span[offset..]);
		offset += JValue.PointerSize;
	}
	void IObject.CopyTo(Span<JValue> span, Int32 index) { span[index] = this.Value; }

	/// <inheritdoc/>
	public override Int32 GetHashCode() => HashCode.Combine(this._value.Value);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => obj is JObject jObj && this.Equals(jObj);

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="jValue"><see cref="JValue"/> that is set as the value of current instance.</param>
	internal void SetValue(JValue jValue) { this._value.Value = jValue; }
	/// <summary>
	/// Sets <see cref="JValue.Empty"/> as the current instance value.
	/// </summary>
	internal void ClearValue() { this._value.Value = JValue.Empty; }

	/// <summary>
	/// Interprets current instance a <typeparamref name="TValue"/> value.
	/// </summary>
	/// <typeparam name="TValue">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TValue"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual ref readonly TValue As<TValue>() where TValue : unmanaged
		=> ref JValue.As<TValue>(ref this._value.Reference);
	/// <summary>
	/// Interprets current instance a <typeparamref name="TValue"/> value.
	/// </summary>
	/// <typeparam name="TValue">Type of value.</typeparam>
	/// <returns>A <typeparamref name="TValue"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual TValue To<TValue>() where TValue : unmanaged => JValue.As<TValue>(ref this._value.Reference);
}