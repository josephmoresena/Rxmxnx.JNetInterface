namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NativeCallUtilities
{
	/// <summary>
	/// Represents an object function to be executed on a <see cref="JLocalObject"/> buffer.
	/// </summary>
#if !NET9_0_OR_GREATER
	private readonly struct
		BufferArrayObjectFunction<TObjectReference, TFunction> : IScopedBufferFunction<JLocalObject?, TObjectReference>
		where TFunction : IFrameFunction<IArrayObject<ILocalObject>?>
#else
	private readonly ref struct
		BufferArrayObjectFunction<TObjectReference, TFunction> : IScopedBufferFunction<JLocalObject?, TObjectReference>
		where TFunction : IFrameFunction<IArrayObject<ILocalObject>?>, allows ref struct
#endif
		where TObjectReference : unmanaged, INativePointerType<TObjectReference>
	{
		/// <summary>
		/// Current <see cref="INativeThread"/> instance.
		/// </summary>
		private readonly INativeThread _env;
		/// <summary>
		/// Current <see cref="AlienLocalCache"/> instance.
		/// </summary>
		private readonly AlienLocalCache _cache;
		/// <summary>
		/// Pointer to function instance.
		/// </summary>
		private readonly ValPtr<TFunction> _funcPtr;
#if !NET9_0_OR_GREATER
		private readonly ReadOnlyValPtr<JReferenceTypeToken> _tokenPtr;
#endif

		/// <inheritdoc/>
#if !NET9_0_OR_GREATER
		public UInt16 Count { get; }
#else
		public UInt16 Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => (UInt16)this.Tokens.Length;
		}
#endif
		/// <inheritdoc/>
		public Boolean IsMinimalCount => true;
		/// <summary>
		/// A read-only span of <see cref="JReferenceTypeToken"/> instances.
		/// </summary>
#if !NET9_0_OR_GREATER
		private ReadOnlySpan<JReferenceTypeToken> Tokens
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => this._tokenPtr.Pointer.GetUnsafeReadOnlySpan<JReferenceTypeToken>(this.Count);
		}
#else
		private ReadOnlySpan<JReferenceTypeToken> Tokens { get; }
#endif

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">Current <see cref="INativeThread"/> instance.</param>
		/// <param name="cache">Current <see cref="AlienLocalCache"/> instance.</param>
		/// <param name="tokens">A read-only span of <see cref="JReferenceTypeToken"/> instances.</param>
		/// <param name="funcPtr">Pointer to <typeparamref name="TFunction"/> instance.</param>
		public BufferArrayObjectFunction(INativeThread env, AlienLocalCache cache,
			ReadOnlySpan<JReferenceTypeToken> tokens, ValPtr<TFunction> funcPtr)
		{
			this._env = env;
			this._cache = cache;
			this._funcPtr = funcPtr;
#if !NET9_0_OR_GREATER
			// Token span should be fixed.
			this._tokenPtr = tokens.GetUnsafeValPtr();
			this.Count = (UInt16)tokens.Length;
#else
			this.Tokens = tokens;
#endif
		}

		/// <inheritdoc/>
		public TObjectReference Apply(scoped ScopedBuffer<JLocalObject?> buffer)
		{
			Span<JLocalObject?> span = NativeCallUtilities.GetObjects(this._env, this._cache, this.Tokens, buffer.Span);
			JArrayObject? result = this._funcPtr.Reference.Apply(this._env, span)?.Value;
			return !JObject.IsNullOrDefault(result) ? result.As<TObjectReference>() : default;
		}
	}
}