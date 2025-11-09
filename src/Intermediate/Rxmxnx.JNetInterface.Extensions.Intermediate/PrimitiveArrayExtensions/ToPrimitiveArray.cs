namespace Rxmxnx.JNetInterface;

public static partial class PrimitiveArrayExtensions
{
	/// <summary>
	/// Creates a multi-dimensional java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="values">
	/// A read-only span of <typeparamref name="TPrimitive"/> values used to initialize the java array.
	/// </param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="lengths">An array specifying the length of each dimension.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	/// <remarks>The resulting array is considered primitive only if it has a single dimension.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
#if !NET9_0_OR_GREATER
	public static JArrayObject ToPrimitiveArray<TPrimitive>(this ReadOnlySpan<TPrimitive> values, IEnvironment env,
		params Int32[] lengths)
#else
	public static JArrayObject ToPrimitiveArray<TPrimitive>(this ReadOnlySpan<TPrimitive> values, IEnvironment env,
		Int32[] lengths)
#endif
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> values.ToPrimitiveArray(env, lengths.AsReadOnlySpan());
	/// <summary>
	/// Creates a multi-dimensional java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="values">
	/// A read-only span of <typeparamref name="TPrimitive"/> values used to initialize the java array.
	/// </param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="lengths">A read-only span specifying the length of each dimension.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	/// <remarks>The resulting array is considered primitive only if it has a single dimension.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
#if NET9_0_OR_GREATER
	public static JArrayObject ToPrimitiveArray<TPrimitive>(this ReadOnlySpan<TPrimitive> values, IEnvironment env,
		params ReadOnlySpan<Int32> lengths)
#else
	public static JArrayObject ToPrimitiveArray<TPrimitive>(this ReadOnlySpan<TPrimitive> values, IEnvironment env,
		ReadOnlySpan<Int32> lengths)
#endif
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		JArrayTypeMetadata arrayTypeMetadata = JArrayObject<TPrimitive>.Metadata.GetArrayTypeMetadata(lengths.Length);
		return PrimitiveArrayExtensions.CreateInitialArray(values, arrayTypeMetadata, env, lengths);
	}
}