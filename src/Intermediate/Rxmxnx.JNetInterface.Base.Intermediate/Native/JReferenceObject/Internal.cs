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
	internal Boolean IsProxy => this._isProxy;
	/// <summary>
	/// Reference instance identifiers.
	/// </summary>
	internal Int64 Id => this._id;

	/// <inheritdoc/>
	private protected override void CopyTo(Span<JValue> span, Int32 index)
	{
		JObjectLocalRef localRef = this.To<JObjectLocalRef>();
		NativeUtilities.AsBytes(in localRef).CopyTo(span[index..].AsBytes());
	}
	/// <inheritdoc/>
	private protected override void CopyTo(Span<Byte> span, ref Int32 offset)
	{
		JObjectLocalRef localRef = this.To<JObjectLocalRef>();
		NativeUtilities.AsBytes(in localRef).CopyTo(span[offset..]);
		offset += NativeUtilities.PointerSize;
	}
	/// <summary>
	/// Indicates whether the current instance is default value.
	/// </summary>
	internal virtual Boolean IsDefaultInstance() => this.IsBlankSpan();

	/// <summary>
	/// Indicates whether the current instance is an instance of <typeparamref name="TDataType"/> type class.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if the current instance is an instance of <typeparamref name="TDataType"/>
	/// type class; otherwise, <see langword="false"/>.
	/// </returns>
	private protected abstract Boolean IsInstanceOf<TDataType>()
		where TDataType : JReferenceObject, IDataType<TDataType>;
	/// <summary>
	/// Sets <see cref="JValue.Empty"/> as the current instance value.
	/// </summary>
	internal abstract void ClearValue();
	/// <summary>
	/// Retrieves synchronizer instance for the current object.
	/// </summary>
	/// <returns>A <see cref="IDisposable"/> synchronizer.</returns>
	private protected abstract IDisposable GetSynchronizer();
	/// <summary>
	/// Indicates whether the current instance is same of <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the current instance is <paramref name="jObject"/>; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	private protected virtual Boolean Same(JReferenceObject jObject)
		=> this.IsProxy == jObject.IsProxy && this.IsDefault == jObject.IsDefault &&
			this.AsSpan().SequenceEqual(jObject.AsSpan());

	/// <summary>
	/// Sets current instance as assignable to <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="isAssignable">
	/// Indicates whether the current instance is assignable to <typeparamref name="TDataType"/>
	/// type.
	/// </param>
	internal abstract void SetAssignableTo<TDataType>(Boolean isAssignable)
		where TDataType : JReferenceObject, IDataType<TDataType>;
	/// <summary>
	/// Retrieves the current value as a read-only binary span.
	/// </summary>
	/// <returns>A read-only binary span.</returns>
	private protected abstract ReadOnlySpan<Byte> AsSpan();
	/// <summary>
	/// Interprets the current instance as a <typeparamref name="TReference"/> value.
	/// </summary>
	/// <typeparam name="TReference">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TReference"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual ref readonly TReference As<TReference>() where TReference : unmanaged, INativeType
		=> ref this.AsSpan().AsValue<TReference>();
	/// <summary>
	/// Interprets the current instance a <typeparamref name="TReference"/> value.
	/// </summary>
	/// <typeparam name="TReference">Type of value.</typeparam>
	/// <returns>A <typeparamref name="TReference"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal virtual TReference To<TReference>() where TReference : unmanaged, INativeType
		=> this.AsSpan().AsValue<TReference>();
}