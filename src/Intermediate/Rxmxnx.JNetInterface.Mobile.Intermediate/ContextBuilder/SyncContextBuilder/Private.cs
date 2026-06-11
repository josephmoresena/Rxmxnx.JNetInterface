namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
	/// <inheritdoc cref="AndroidJniContext.Booleans"/>
	private ReadOnlySpan<JBoolean> _booleans = [];
	/// <inheritdoc cref="AndroidJniContext.Bytes"/>
	private ReadOnlySpan<JByte> _bytes = [];
	/// <inheritdoc cref="AndroidJniContext.Chars"/>
	private ReadOnlySpan<JChar> _chars = [];
	/// <inheritdoc cref="AndroidJniContext.Doubles"/>
	private ReadOnlySpan<JDouble> _doubles = [];
	/// <inheritdoc cref="AndroidJniContext.Floats"/>
	private ReadOnlySpan<JFloat> _floats = [];
	/// <inheritdoc cref="AndroidJniContext.Ints"/>
	private ReadOnlySpan<JInt> _ints = [];
	/// <inheritdoc cref="AndroidJniContext.Longs"/>
	private ReadOnlySpan<JLong> _longs = [];
	/// <inheritdoc cref="AndroidJniContext.Shorts"/>
	private ReadOnlySpan<JShort> _shorts = [];
	/// <inheritdoc cref="AndroidJniContext.Objects"/>
	private ReadOnlySpan<IJavaPeerable?> _objects = [];
	/// <summary>
	/// Current thread id.
	/// </summary>
	private Int32 _threadId = Environment.CurrentManagedThreadId;

	/// <summary>
	/// Finalize JNI context.
	/// </summary>
	/// <param name="throwable">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <param name="interopCache">A <see cref="JavaInteropCache"/> instance.</param>
	private static void FinalizeJniInvocation(JGlobalBase? throwable, JavaInteropCache? interopCache)
	{
		throwable?.Dispose();
		interopCache?.Dispose();
	}
}