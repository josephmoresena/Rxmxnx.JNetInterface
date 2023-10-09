namespace Rxmxnx.JNetInterface.Native;

public partial record JPrimitiveReadOnlySequence
{
	/// <summary>
	/// Read-only for primitive sequence.
	/// </summary>
	/// <typeparam name="TValue"><see cref="ValueType"/> type.</typeparam>
	internal record ReadOnlyFixedContext<TValue> : IReadOnlyFixedContext<TValue> where TValue : unmanaged
	{
		/// <inheritdoc cref="ReadOnlyFixedContext{TValue}.BinaryOffset"/>
		private readonly Int32 _binaryOffset;
		/// <inheritdoc cref="ReadOnlySequence"/>
		private readonly JPrimitiveReadOnlySequence _readOnlySequence;

		/// <summary>
		/// Internal <see cref="JPrimitiveReadOnlySequence"/> instance.
		/// </summary>
		protected JPrimitiveReadOnlySequence ReadOnlySequence => this._readOnlySequence;
		/// <summary>
		/// Internal <see cref="IReadOnlyFixedMemory"/> instance.
		/// </summary>
		protected IReadOnlyFixedContext<Byte> Memory => this._readOnlySequence;
		/// <summary>
		/// Binary offset.
		/// </summary>
		protected Int32 BinaryOffset => this._binaryOffset;

		/// <summary>
		/// Constructor..
		/// </summary>
		/// <param name="sequence"><see cref="JPrimitiveReadOnlySequence"/> sequence.</param>
		/// <param name="binaryOffset">Binary offset.</param>
		internal ReadOnlyFixedContext(JPrimitiveReadOnlySequence sequence, Int32 binaryOffset = 0)
		{
			this._readOnlySequence = sequence;
			this._binaryOffset = binaryOffset;
		}

		IntPtr IFixedPointer.Pointer => this.Memory.Pointer + this._binaryOffset;
		ReadOnlySpan<Byte> IReadOnlyFixedMemory.Bytes => this.Memory.Bytes[this._binaryOffset..];
		ReadOnlySpan<TValue> IReadOnlyFixedMemory<TValue>.Values
			=> this.Memory is IReadOnlyFixedContext<TValue> ctx ?
				ctx.Values[this.GetValueOffset()..] :
				this.Memory.Bytes[this._binaryOffset..].AsValues<Byte, TValue>();

		IReadOnlyFixedContext<Byte> IReadOnlyFixedMemory.AsBinaryContext() => this.Memory;
		IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<TValue>.Transformation<TDestination>(
			out IReadOnlyFixedMemory residual)
		{
			if (this.BinaryOffset == 0)
				return this.Memory.Transformation<TDestination>(out residual);

			IReadOnlyFixedContext<TDestination> result = this.ReadOnlySequence.ReadOnly ?
				new ReadOnlyFixedContext<TDestination>(this.ReadOnlySequence, this.BinaryOffset) :
				new FixedContext<TDestination>(this.ReadOnlySequence, this.BinaryOffset);
			Int32 residualOffset = this.BinaryOffset + result.Values.Length * NativeUtilities.SizeOf<TValue>();
			residual = this.ReadOnlySequence.ReadOnly ?
				new ReadOnlyFixedContext<Byte>(this.ReadOnlySequence, residualOffset) :
				new FixedContext<Byte>(this.ReadOnlySequence, residualOffset);
			return result;
		}

		/// <summary>
		/// Calculates current index offset.
		/// </summary>
		/// <returns>Current index offset.</returns>
		protected Int32 GetValueOffset() => this._binaryOffset / NativeUtilities.SizeOf<TValue>();
	}
}