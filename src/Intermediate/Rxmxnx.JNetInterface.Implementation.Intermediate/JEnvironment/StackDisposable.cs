namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Release used bytes from stack.
		/// </summary>
		/// <param name="usedBytes">Amount of used bytes.</param>
		private void FreeStack(Int32 usedBytes) { this._usedStackBytes -= usedBytes; }

		/// <summary>
		/// Disposable object to free stack bytes.
		/// </summary>
		private sealed record StackDisposable : IDisposable
		{
			/// <summary>
			/// A <see cref="EnvironmentCache"/> cache.
			/// </summary>
			private readonly EnvironmentCache _cache;
			/// <summary>
			/// Amount of used bytes.
			/// </summary>
			private readonly Int32 _usedBytes;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="cache">A <see cref="EnvironmentCache"/> cache.</param>
			/// <param name="usedBytes">Amount of used bytes.</param>
			public StackDisposable(EnvironmentCache cache, Int32 usedBytes)
			{
				this._usedBytes = usedBytes;
				this._cache = cache;
			}

			/// <inheritdoc/>
			public void Dispose() => this._cache.FreeStack(this._usedBytes);
		}
	}
}