namespace Rxmxnx.JNetInterface;

/// <summary>
/// This struct represents a JNI context hosted by Android OS.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
public readonly ref struct AndroidJniContext
{
	/// <summary>
	/// The <see cref="IEnvironment"/> instance for current context.
	/// </summary>
	public IEnvironment Environment { get; }
	/// <summary>
	/// <c>boolean</c> parameters.
	/// </summary>
	public ReadOnlySpan<JBoolean> Booleans { get; internal init; }
	/// <summary>
	/// <c>byte</c> parameters.
	/// </summary>
	public ReadOnlySpan<JByte> Bytes { get; internal init; }
	/// <summary>
	/// <c>char</c> parameters.
	/// </summary>
	public ReadOnlySpan<JChar> Chars { get; internal init; }
	/// <summary>
	/// <c>double</c> parameters.
	/// </summary>
	public ReadOnlySpan<JDouble> Doubles { get; internal init; }
	/// <summary>
	/// <c>float</c> parameters.
	/// </summary>
	public ReadOnlySpan<JFloat> Floats { get; internal init; }
	/// <summary>
	/// <c>int</c> parameters.
	/// </summary>
	public ReadOnlySpan<JInt> Ints { get; internal init; }
	/// <summary>
	/// <c>long</c> parameters.
	/// </summary>
	public ReadOnlySpan<JLong> Longs { get; internal init; }
	/// <summary>
	/// <c>short</c> parameters.
	/// </summary>
	public ReadOnlySpan<JShort> Shorts { get; internal init; }
	/// <summary>
	/// <c>java.lang.Object</c> parameters.
	/// </summary>
	public ReadOnlySpan<JLocalObject?> Objects { get; internal init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	internal AndroidJniContext(IEnvironment environment) => this.Environment = environment;
}