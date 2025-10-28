namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Map structure for multidimensional array creation.
/// </summary>
/// <typeparam name="TElement">The type of array element.</typeparam>
#if NET9_0_OR_GREATER
internal ref struct ArrayFillMap<TElement>(ArrayFillState<TElement> state)
#else
internal struct ArrayFillMap<TElement>(ArrayFillState<TElement> state)
#endif
{
	/// <summary>
	/// Array fill state.
	/// </summary>
	public ArrayFillState<TElement> State = state;
	/// <summary>
	/// Array type metadata.
	/// </summary>
	public JArrayTypeMetadata ArrayTypeMetadata { get; init; }
	/// <summary>
	/// A <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment { get; init; }
}