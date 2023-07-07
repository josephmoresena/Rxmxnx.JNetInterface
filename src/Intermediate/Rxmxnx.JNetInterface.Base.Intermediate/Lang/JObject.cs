namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a <c>java.lang.Object</c> instance.
/// </summary>
public abstract class JObject : IObject, IEquatable<JObject>
{
	/// <summary>
	/// JNI name of <c>java.lang.Object</c> class.
	/// </summary>
	public static readonly CString JObjectClassName = UnicodeClassNames.JObjectClassName;
	/// <summary>
	/// JNI signature for <c>java.lang.Object</c> object.
	/// </summary>
	public static readonly CString JObjectSignature = UnicodeObjectSignatures.JObjectSignature;

	/// <summary>
	/// Internal <see cref="JValue"/> instance.
	/// </summary>
	private readonly IMutableReference<JValue> _value;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	internal JObject() : this(JValue.Empty) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JObject(JObject jObject) => this._value = jObject._value;
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
	internal virtual JValue Value => this._value.Value;

	/// <inheritdoc/>
	public virtual Boolean Equals(JObject? other) => this._value.Equals(other?._value);

	/// <inheritdoc/>
	public Boolean IsDefault => this._value.Value.IsDefault;

	CString IObject.ObjectClassName => this.ObjectClassName;
	CString IObject.ObjectSignature => this.ObjectSignature;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset) => this.CopyTo(span, ref offset);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<JValue> span, Int32 index) => this.CopyTo(span, index);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => HashCode.Combine(this._value.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals(Object? obj) => obj is JObject jObj && this.Equals(jObj);

	/// <inheritdoc cref="IObject.CopyTo(Span{Byte}, ref Int32)"/>
	internal abstract void CopyTo(Span<Byte> span, ref Int32 offset);

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="unmanaged"/></typeparam>
	/// <param name="value"><typeparamref name="TValue"/> that is set as the value of current instance.</param>
	protected internal void SetValue<TValue>(in TValue value) where TValue : unmanaged
		=> JValue.As<TValue>(ref this._value.Reference) = value;
	/// <summary>
	/// Sets <see cref="JValue.Empty"/> as the current instance value.
	/// </summary>
	internal void ClearValue() { this._value.Value = JValue.Empty; }
	/// <summary>
	/// Retrieves a <typeparamref name="TValue"/> value from current instance.
	/// </summary>
	/// <typeparam name="TPrimitive"><see cref="IPrimitive"/> type.</typeparam>
	/// <typeparam name="TValue"><see cref="ValueType"/> type.</typeparam>
	/// <returns>
	/// The equivalent <typeparamref name="TValue"/> value to current instance.
	/// </returns>
	/// <exception cref="InvalidCastException"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal TPrimitive AsPrimitive<TPrimitive, TValue>()
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
		where TPrimitive : unmanaged, IPrimitive<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
		=> this is IWrapper<TPrimitive> pw ? pw.Value.Value :
			this is IWrapper<TValue> vw ? vw.Value :
			ValidationUtilities.ThrowIfInvalidCast<TValue>(this as IConvertible);

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

	/// <inheritdoc cref="IObject.CopyTo(Span{JValue}, Int32)"/>
	private void CopyTo(Span<JValue> span, Int32 index) => span[index] = this._value.Value;
}