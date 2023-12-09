namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents the base of any java reference type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class JReferenceObject : JObject
{
	/// <summary>
	/// Sequence lock instance.
	/// </summary>
	private static readonly Object sequenceLock = new();

	/// <summary>
	/// Current sequence value.
	/// </summary>
	private static Int64 sequenceValue = 1;
	/// <summary>
	/// Instance identifier.
	/// </summary>
	private readonly Int64 _id;

	/// <summary>
	/// Indicates whether the current instance is a dummy object (fake java object).
	/// </summary>
	private readonly Boolean _isDummy;
	
	/// <summary>
	/// Indicates whether current instance is default value.
	/// </summary>
	public Boolean IsDefault => this.ValueReference.IsDefault;

	/// <summary>
	/// Indicates whether the current instance is a dummy object (fake java object).
	/// </summary>
	/// <remarks>
	/// This property is used internally to prevent dummy objects be used in the .NET
	/// implementation of JNI.
	/// </remarks>
	internal Boolean IsDummy => this._isDummy;
	/// <summary>
	/// Reference instance identifiers.
	/// </summary>
	internal Int64 Id => this._id;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(Boolean isDummy)
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	internal JReferenceObject(JReferenceObject jObject) : base(jObject)
	{
		this._isDummy = jObject._isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Object reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JObjectLocalRef value, Boolean isDummy) : base(JValue.Create(value))
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Throwable reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JThrowableLocalRef value, Boolean isDummy) : base(JValue.Create(value))
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Class reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JClassLocalRef value, Boolean isDummy) : base(JValue.Create(value))
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Array reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JArrayLocalRef value, Boolean isDummy) : base(JValue.Create(value))
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Throwable reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JStringLocalRef value, Boolean isDummy) : base(JValue.Create(value))
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Global reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JGlobalRef value, Boolean isDummy) : base(JValue.Create(value))
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Weak global reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(JWeakRef value, Boolean isDummy) : base(JValue.Create(value))
	{
		this._isDummy = isDummy;
		this._id = JReferenceObject.CreateInstanceId();
	}

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
	{
		if (other is null or JReferenceObject { IsDefault: true, } && this.IsDefault)
			return true;
		return other is JReferenceObject jReference && Unsafe.AreSame(ref this.ValueReference, ref jReference.ValueReference);
	}
	
	/// <inheritdoc/>
	internal override void CopyTo(Span<JValue> span, Int32 index) => span[index] = this.ValueReference;
	
	/// <summary>
	/// Sets <see cref="JValue.Empty"/> as the current instance value.
	/// </summary>
	internal void ClearValue() { this.ValueReference = JValue.Empty; }

	/// <summary>
	/// Indicates whether current instance is assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance is assignable to <typeparamref name="TDataType"/> type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal abstract Boolean IsAssignableTo<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>;
	/// <summary>
	/// Sets current instance as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="isAssignable">Indicates whether current instance is assignable to <typeparamref name="TDataType"/> type.</param>
	internal abstract void SetAssignableTo<TDataType>(Boolean isAssignable)
		where TDataType : JReferenceObject, IDataType<TDataType>;

	/// <summary>
	/// Interprets current instance a <typeparamref name="TValue"/> value.
	/// </summary>
	/// <typeparam name="TValue">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TValue"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual ref readonly TValue As<TValue>() where TValue : unmanaged
		=> ref JValue.As<TValue>(ref this.ValueReference);
	/// <summary>
	/// Interprets current instance a <typeparamref name="TValue"/> value.
	/// </summary>
	/// <typeparam name="TValue">Type of value.</typeparam>
	/// <returns>A <typeparamref name="TValue"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual TValue To<TValue>() where TValue : unmanaged => JValue.As<TValue>(ref this.ValueReference);
	
	/// <inheritdoc/>
	internal override void CopyTo(Span<Byte> span, ref Int32 offset)
	{
		ReadOnlySpan<Byte> bytes = NativeUtilities.AsBytes(this.As<IntPtr>());
		bytes.CopyTo(span[offset..]);
		offset += NativeUtilities.PointerSize;
	}

	/// <summary>
	/// Creates the identifier for current instance.
	/// </summary>
	/// <returns>The Identifier for current instance.</returns>
	private static Int64 CreateInstanceId()
	{
		lock (JReferenceObject.sequenceLock)
			return JReferenceObject.sequenceValue++;
	}
}