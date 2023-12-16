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
	public Boolean IsDefault => this.AsSpan().AsValue<IntPtr>() == IntPtr.Zero;

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
	internal JReferenceObject(Boolean? isDummy = default)
	{
		this._isDummy = isDummy.GetValueOrDefault();
		this._id = isDummy.HasValue ? JReferenceObject.CreateInstanceId() : -1;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	internal JReferenceObject(JReferenceObject jObject)
	{
		this._isDummy = jObject._isDummy;
		this._id = jObject._id != -1 ? JReferenceObject.CreateInstanceId() : -1;
	}

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
	{
		if (other is null or JReferenceObject { IsDefault: true, } && this.IsDefault)
			return true;
		return other is JReferenceObject jReference && this.AsSpan().SequenceEqual(jReference.AsSpan());
	}

	/// <inheritdoc/>
	internal override void CopyTo(Span<JValue> span, Int32 index) => this.AsSpan().CopyTo(span[index].AsBytes());

	/// <summary>
	/// Indicates whether current instance is default value.
	/// </summary>
	internal virtual Boolean IsDefaultInstance() => this.AsSpan().AsValue<IntPtr>() == IntPtr.Zero;

	/// <summary>
	/// Sets <see cref="JValue.Empty"/> as the current instance value.
	/// </summary>
	internal abstract void ClearValue();

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
	/// Retrieves current value as a read-only binary span.
	/// </summary>
	/// <returns>A read-only binary span.</returns>
	internal abstract ReadOnlySpan<Byte> AsSpan();
	/// <summary>
	/// Interprets current instance as a <typeparamref name="TReference"/> value.
	/// </summary>
	/// <typeparam name="TReference">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TReference"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual ref readonly TReference As<TReference>() where TReference : unmanaged, INativeType<TReference>
		=> ref this.AsSpan().AsValue<TReference>();
	/// <summary>
	/// Interprets current instance a <typeparamref name="TReference"/> value.
	/// </summary>
	/// <typeparam name="TReference">Type of value.</typeparam>
	/// <returns>A <typeparamref name="TReference"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual TReference To<TReference>() where TReference : unmanaged, INativeType<TReference>
		=> this.AsSpan().AsValue<TReference>();

	/// <inheritdoc/>
	internal override void CopyTo(Span<Byte> span, ref Int32 offset)
	{
		this.AsSpan().CopyTo(span[offset..]);
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