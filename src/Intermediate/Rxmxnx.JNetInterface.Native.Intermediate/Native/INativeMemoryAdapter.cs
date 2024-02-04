namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This interface exposes an adapter for a primitive sequence.
/// </summary>
public interface INativeMemoryAdapter
{
	/// <summary>
	/// Indicates whether current sequence is a copy.
	/// </summary>
	Boolean Copy { get; }
	/// <summary>
	/// Indicates whether current sequence is critical.
	/// </summary>
	Boolean Critical { get; }

	/// <summary>
	/// Retrieves a <see cref="IReadOnlyFixedContext{Byte}.IDisposable"/> instance from current adapter
	/// using <paramref name="jMemory"/>.
	/// </summary>
	/// <param name="jMemory">A <see cref="JNativeMemory"/> instance.</param>
	/// <returns>A <see cref="IReadOnlyFixedContext{Byte}.IDisposable"/> instance.</returns>
	IReadOnlyFixedContext<Byte>.IDisposable GetReadOnlyContext(JNativeMemory jMemory);
	/// <summary>
	/// Retrieves a <see cref="IFixedContext{Byte}.IDisposable"/> instance from current adapter
	/// using <paramref name="jMemory"/>.
	/// </summary>
	/// <param name="jMemory">A <see cref="JNativeMemory"/> instance.</param>
	/// <returns>A <see cref="IFixedContext{Byte}.IDisposable"/> instance.</returns>
	IFixedContext<Byte>.IDisposable GetContext(JNativeMemory jMemory);
	/// <summary>
	/// Releases the current memory.
	/// </summary>
	/// <param name="mode">Release memory mode.</param>
	void Release(JReleaseMode mode = default);
}