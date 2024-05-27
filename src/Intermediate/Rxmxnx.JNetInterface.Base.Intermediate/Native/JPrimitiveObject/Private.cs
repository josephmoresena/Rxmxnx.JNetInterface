namespace Rxmxnx.JNetInterface.Native;

internal partial class JPrimitiveObject
{
	/// <summary>
	/// Converts current instance in a read-only binary span.
	/// </summary>
	/// <returns>A <see cref="ReadOnlySpan{Byte}"/> instance.</returns>
	private protected abstract ReadOnlySpan<Byte> AsSpan();

	/// <summary>
	/// Retrieves a <typeparamref name="TValue"/> value from current instance.
	/// </summary>
	/// <typeparam name="TValue"><see cref="ValueType"/> type.</typeparam>
	/// <returns>
	/// The equivalent <typeparamref name="TValue"/> value to current instance.
	/// </returns>
	/// <exception cref="InvalidCastException"/>
	private TValue AsValue<TValue>()
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
		=> (this as IWrapper<TValue>)?.Value ??
			CommonValidationUtilities.ThrowIfInvalidCast<TValue>(this as IConvertible);

	public abstract partial class Generic<TValue>
	{
		/// <summary>
		/// Internal value.
		/// </summary>
		private readonly TValue _value;

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override ReadOnlySpan<Byte> AsSpan() => NativeUtilities.AsBytes(this._value);
	}
}

internal sealed partial class JPrimitiveObject<TPrimitive>
{
	static Type IDataType.FamilyType => typeof(TPrimitive);
	static JNativeType IPrimitiveType.JniType => IPrimitiveType.GetMetadata<TPrimitive>().NativeType;
}