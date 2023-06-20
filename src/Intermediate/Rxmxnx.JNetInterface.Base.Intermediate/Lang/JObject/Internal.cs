namespace Rxmxnx.JNetInterface.Lang;

public partial class JObject
{
	/// <summary>
	/// Internal <see cref="JValue"/> value.
	/// </summary>
	internal virtual JValue Value => this._value.Value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jValue">Internal <see cref="JValue"/> instance.</param>
	internal JObject(JValue jValue) => this._value = IMutableReference.Create(jValue);

	/// <summary>
	/// Interprets current instance a <typeparamref name="TValue"/> value.
	/// </summary>
	/// <typeparam name="TValue">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TValue"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual ref readonly TValue As<TValue>() where TValue : unmanaged => ref JValue.As<TValue>(ref this._value.Reference);
    /// <summary>
    /// Interprets current instance a <typeparamref name="TValue"/> value.
    /// </summary>
    /// <typeparam name="TValue">Type of value.</typeparam>
    /// <returns>A <typeparamref name="TValue"/> value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual TValue To<TValue>() where TValue : unmanaged => JValue.As<TValue>(ref this._value.Reference);

    /// <summary>
    /// Sets the current instance value.
    /// </summary>
    /// <param name="jValue"><see cref="JValue"/> that is set as the value of current instance.</param>
    internal void SetValue(JValue jValue) => this._value.Value = jValue;
	/// <summary>
	/// Sets <see cref="JValue.Empty"/> as the current instance value.
	/// </summary>
	internal void ClearValue() => this._value.Value = JValue.Empty;
}