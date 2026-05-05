namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class EnvironmentCore
{
	/// <summary>
	/// Release used bytes from stack.
	/// </summary>
	/// <param name="usedBytes">Amount of used bytes.</param>
	private void FreeStack(Int32 usedBytes) { this.UsedStackBytes -= usedBytes; }

	/// <summary>
	/// Disposable object to free stack bytes.
	/// </summary>
	/// <remarks>This struct should be disposed only once.</remarks>
	private readonly struct StackDisposable : IDisposable
	{
		/// <summary>
		/// A <see cref="EnvironmentCore"/> cache.
		/// </summary>
		private readonly EnvironmentCore? _core;
		/// <summary>
		/// Amount of used bytes.
		/// </summary>
		private readonly Int32 _usedBytes;

		/// <summary>
		/// Indicates whether current call is using stack memory.
		/// </summary>
		public Boolean UsingStack => this._core is not null;

		public StackDisposable() { }
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="core">A <see cref="EnvironmentCore"/> cache.</param>
		/// <param name="usedBytes">Amount of used bytes.</param>
		public StackDisposable(EnvironmentCore core, Int32 usedBytes)
		{
			this._usedBytes = usedBytes;
			this._core = core;
		}

		/// <inheritdoc/>
		public void Dispose() => this._core?.FreeStack(this._usedBytes);
	}
}