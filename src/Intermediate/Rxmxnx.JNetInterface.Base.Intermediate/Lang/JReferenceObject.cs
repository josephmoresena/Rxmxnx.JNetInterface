namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents the base of any java reference type instance.
/// </summary>
public abstract class JReferenceObject : JObject
{
	/// <summary>
	/// Indicates whether the current instance is a dummy object (fake java object).
	/// </summary>
	private readonly Boolean _isDummy;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(Boolean isDummy) => this._isDummy = isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	internal JReferenceObject(JReferenceObject jObject) : base(jObject) => this._isDummy = jObject._isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Object reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JObjectLocalRef value, Boolean isDummy) : base(JValue.Create(value))
		=> this._isDummy = isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Throwable reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JThrowableLocalRef value, Boolean isDummy) : base(JValue.Create(value))
		=> this._isDummy = isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Class reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JClassLocalRef value, Boolean isDummy) : base(JValue.Create(value))
		=> this._isDummy = isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Array reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JArrayLocalRef value, Boolean isDummy) : base(JValue.Create(value))
		=> this._isDummy = isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Throwable reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JStringLocalRef value, Boolean isDummy) : base(JValue.Create(value))
		=> this._isDummy = isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Global reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JGlobalRef value, Boolean isDummy) : base(JValue.Create(value))
		=> this._isDummy = isDummy;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Weak global reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JWeakRef value, Boolean isDummy) : base(JValue.Create(value)) => this._isDummy = isDummy;

	/// <summary>
	/// Indicates whether the current instance is a dummy object (fake java object).
	/// </summary>
	/// <remarks>
	/// This property is used internally to prevent dummy objects be used in the .NET
	/// implementation of JNI.
	/// </remarks>
	internal Boolean IsDummy => this._isDummy;

	/// <inheritdoc/>
	protected internal override void CopyTo(Span<Byte> span, ref Int32 offset)
	{
		ReadOnlySpan<Byte> bytes = NativeUtilities.AsBytes(this.As<IntPtr>());
		bytes.CopyTo(span[offset..]);
		offset += NativeUtilities.PointerSize;
	}
}