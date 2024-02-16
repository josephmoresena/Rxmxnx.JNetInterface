namespace Rxmxnx.JNetInterface.Native;

internal partial class JPrimitiveObject
{
	/// <summary>
	/// Internal <see cref="JPrimitiveObject"/> implementation.
	/// </summary>
	/// <typeparam name="TValue">A <see langword="unmanaged"/> type.</typeparam>
	public abstract partial class Generic<TValue> : JPrimitiveObject, IPrimitiveType, IPrimitiveValue<TValue>
		where TValue : unmanaged, IEquatable<TValue>, IComparable, IConvertible
	{
		/// <summary>
		/// Size of current type in bytes.
		/// </summary>
		protected override Int32 SizeOf => NativeUtilities.SizeOf<TValue>();

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="value">Internal wrapper.</param>
		protected Generic(TValue value) => this._value = value;

		/// <summary>
		/// Internal primitive value.
		/// </summary>
		public TValue Value => this._value;

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override Int32 GetHashCode() => this._value.GetHashCode();
		/// <inheritdoc/>
		public override String? ToString() => this._value.ToString();
		/// <inheritdoc/>
		public override Byte ToByte() => NativeUtilities.AsBytes(in this._value)[0];

		/// <inheritdoc cref="IObject.CopyTo(Span{JValue}, Int32)"/>
		private protected override void CopyTo(Span<JValue> span, Int32 index)
			=> this.AsSpan().CopyTo(span[index].AsBytes());
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override void CopyTo(Span<Byte> span, ref Int32 offset)
		{
			this.AsSpan().CopyTo(span[offset..]);
			offset += this.SizeOf;
		}
	}
}