namespace Rxmxnx.JNetInterface.Native;

public partial class JReferenceObject
{
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

	/// <inheritdoc/>
	private protected override void CopyTo(Span<JValue> span, Int32 index)
		=> this.AsSpan().CopyTo(span[index].AsBytes());
	/// <inheritdoc/>
	private protected override void CopyTo(Span<Byte> span, ref Int32 offset)
	{
		this.AsSpan().CopyTo(span[offset..]);
		offset += NativeUtilities.PointerSize;
	}

	/// <summary>
	/// Indicates whether current instance is default value.
	/// </summary>
	internal virtual Boolean IsDefaultInstance() => this.AsSpan().AsValue<IntPtr>() == IntPtr.Zero;

	/// <summary>
	/// Indicates whether current instance is an instance of <typeparamref name="TDataType"/> type class.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance is an instance of <typeparamref name="TDataType"/>
	/// type class; otherwise, <see langword="false"/>.
	/// </returns>
	internal abstract Boolean IsInstanceOf<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>;
	/// <summary>
	/// Sets <see cref="JValue.Empty"/> as the current instance value.
	/// </summary>
	internal abstract void ClearValue();
	/// <summary>
	/// Retrieves synchronizer instance for current object.
	/// </summary>
	/// <returns>A <see cref="IDisposable"/> synchronizer.</returns>
	internal abstract IDisposable GetSynchronizer();

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
}