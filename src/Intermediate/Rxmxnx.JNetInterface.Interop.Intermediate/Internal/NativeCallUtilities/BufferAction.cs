using JObject = Rxmxnx.JNetInterface.Native.JReferenceObject;

namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NativeCallUtilities
{
	/// <summary>
	/// Represents an action to be performed on a <see cref="JReferenceObject"/> buffer.
	/// </summary>
#if !NET9_0_OR_GREATER
	private readonly struct BufferAction<TAction> : IScopedBufferAction<JObject?> where TAction : IFrameAction
#else
	private readonly ref struct BufferAction<TAction> : IScopedBufferAction<JObject?>
		where TAction : IFrameAction, allows ref struct
#endif
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
		/// Pointer to action instance.
		/// </summary>
		private readonly ValPtr<TAction> _actionPtr;
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
		/// <param name="actionPtr">Pointer to <typeparamref name="TAction"/> instance.</param>
		public BufferAction(INativeThread env, AlienLocalCache cache, ReadOnlySpan<JReferenceTypeToken> tokens,
			ValPtr<TAction> actionPtr)
		{
			this._env = env;
			this._cache = cache;
			this._actionPtr = actionPtr;
#if !NET9_0_OR_GREATER
			// Token span should be fixed.
			this._tokenPtr = tokens.GetUnsafeValPtr();
			this.Count = (UInt16)tokens.Length;
#else
			this.Tokens = tokens;
#endif
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Accept(scoped ScopedBuffer<JObject?> buffer)
		{
			Span<JObject?> span = NativeCallUtilities.GetObjects(this._env, this._cache, this.Tokens, buffer.Span);
			this._actionPtr.Reference.Accept(this._env, span);
		}
	}
}