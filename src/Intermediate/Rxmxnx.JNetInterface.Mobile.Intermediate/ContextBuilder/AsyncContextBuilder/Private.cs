namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <inheritdoc cref="AndroidJniContext.Booleans"/>
	private Array? _booleans = default;
	/// <inheritdoc cref="AndroidJniContext.Bytes"/>
	private Array? _bytes = default;
	/// <inheritdoc cref="AndroidJniContext.Chars"/>
	private Array? _chars = default;
	/// <inheritdoc cref="AndroidJniContext.Doubles"/>
	private Array? _doubles = default;
	/// <inheritdoc cref="AndroidJniContext.Floats"/>
	private Array? _floats = default;
	/// <inheritdoc cref="AndroidJniContext.Ints"/>
	private Array? _ints = default;
	/// <inheritdoc cref="AndroidJniContext.Longs"/>
	private Array? _longs = default;
	/// <inheritdoc cref="AndroidJniContext.Shorts"/>
	private Array? _shorts = default;
	/// <inheritdoc cref="AndroidJniContext.Objects"/>
	private IJavaPeerable?[] _objects = [];
	/// <summary>
	/// Thread name.
	/// </summary>
	private readonly CString? _threadName;
	/// <summary>
	/// Thread group instance.
	/// </summary>
	private readonly JGlobalBase? _threadGroup;
}