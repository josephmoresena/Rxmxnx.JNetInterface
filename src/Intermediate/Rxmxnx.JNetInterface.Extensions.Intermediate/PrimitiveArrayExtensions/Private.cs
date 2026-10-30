namespace Rxmxnx.JNetInterface;

public static partial class PrimitiveArrayExtensions
{
	/// <summary>
	/// Creates a multidimensional java array of primitive values.
	/// </summary>
	/// <typeparam name="TArrayObject">The type of the array.</typeparam>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="values">
	/// A read-only span of <typeparamref name="TPrimitive"/> values used to initialize the java array.
	/// </param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	[return: NotNullIfNotNull(nameof(array))]
	private static TArrayObject?
		CreateInitialArray<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TArrayObject,
			TPrimitive>(Array? array, IEnvironment env, ReadOnlySpan<TPrimitive> values)
		where TArrayObject : JLocalObject.ArrayView, IArrayType, IReferenceType<TArrayObject>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (array is null) return default;
		Span<Int32> dimensions = stackalloc Int32[array.Rank];
		for (Int32 i = 0; i < dimensions.Length; i++)
			dimensions[i] = array.GetLength(i);
		return PrimitiveArrayExtensions
		       .CreateInitialArray(values, IArrayType.GetMetadata<TArrayObject>(), env, dimensions)
		       .CastTo<TArrayObject>();
	}
	/// <summary>
	/// Creates the initial multidimensional array.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
	/// <param name="values">
	/// A read-only span of <typeparamref name="TPrimitive"/> values used to initialize the array.
	/// </param>
	/// <param name="arrayTypeMetadata">Initial <see cref="JArrayTypeMetadata"/> instance.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="lengths">A read-only span specifying the length of each dimension.</param>
	/// <returns>A <see cref="JArrayObject"/> instance.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JArrayObject CreateInitialArray<TPrimitive>(ReadOnlySpan<TPrimitive> values,
		JArrayTypeMetadata arrayTypeMetadata, IEnvironment env, ReadOnlySpan<Int32> lengths)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		Int32 frameSize = lengths.Length * 2;
#if NET9_0_OR_GREATER
		InitialArrayFunction<TPrimitive> function = new(new(new(lengths, values))
		{
			ArrayTypeMetadata = arrayTypeMetadata,
		});
		function.WithFrame(env, frameSize, out JArrayObject result);
#else
		InitialArrayFunction<TPrimitive> function = new(arrayTypeMetadata, env, frameSize);
		function.WithSafeFixed(lengths, values, out JArrayObject result);
#endif
		return result;
	}

	/// <summary>
	/// Fills <paramref name="jArray"/> instance according to <paramref name="state"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> to fill.</param>
	/// <param name="dimension">Current array dimension.</param>
	/// <param name="state">A <see cref="ArrayFillState{TPrimitive}"/> instance.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static void FillArray<TPrimitive>(JArrayObject jArray, Int32 dimension,
		ref ArrayFillState<TPrimitive> state) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (jArray.TypeMetadata.ElementMetadata is JArrayTypeMetadata arrayElementMetadata)
			PrimitiveArrayExtensions.FillArrayArray(arrayElementMetadata, jArray.CastTo<JArrayObject<JLocalObject>>(),
			                                        dimension + 1, ref state);
		else
			PrimitiveArrayExtensions.FillPrimitive(jArray.CastTo<JArrayObject<TPrimitive>>(), ref state);
	}
	/// <summary>
	/// Fills <paramref name="jArray"/> with primitive data from <paramref name="state"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive>"/> instance.</param>
	/// <param name="state">Reference. A <see cref="ArrayFillState{TPrimitive}"/> instance.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void FillPrimitive<TPrimitive>(JArrayObject<TPrimitive> jArray, ref ArrayFillState<TPrimitive> state)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		using JPrimitiveMemory<TPrimitive> sequence = jArray.GetCriticalElements(JMemoryReferenceKind.Local);
		ReadOnlySpan<TPrimitive> valuesToCopy = state.Take(jArray.Length);
		valuesToCopy.CopyTo(sequence.Values);
	}
	/// <summary>
	/// Fills <paramref name="jArray"/> with arrays according to <paramref name="state"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
	/// <param name="arrayElementMetadata">A <see cref="JArrayTypeMetadata"/> of the <paramref name="jArray"/> element type.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JLocalObject>"/> instance.</param>
	/// <param name="dimension">Current dimension.</param>
	/// <param name="state">Reference. A <see cref="ArrayFillState{TPrimitive}"/> instance.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void FillArrayArray<TPrimitive>(JArrayTypeMetadata arrayElementMetadata,
		JArrayObject<JLocalObject> jArray, Int32 dimension, ref ArrayFillState<TPrimitive> state)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jArray.Environment;
		IArrayFeature arrayFeature = env.ArrayFeature;
		for (Int32 i = 0; i < jArray.Length; i++)
		{
			using JArrayObject jArrayElement = arrayElementMetadata.CreateInstance(env, state.GetLength(dimension));
			PrimitiveArrayExtensions.FillArray(jArrayElement, dimension, ref state);
			arrayFeature.SetElement(jArray, i, jArrayElement);
		}
	}
}