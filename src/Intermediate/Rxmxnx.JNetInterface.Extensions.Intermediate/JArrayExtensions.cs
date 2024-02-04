/// <summary>
/// Set of primitive memory extensions.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CA1050
public static class JArrayExtensions
#pragma warning restore CA1050
{
	/// <summary>
	/// Retrieves the native memory of array elements.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> array element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	public static JPrimitiveMemory<TPrimitive> GetElements<TPrimitive>(this JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind = JMemoryReferenceKind.Local)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		INativeMemoryAdapter adapter = jArray.Environment.ArrayFeature.GetSequence(jArray, referenceKind);
		return new(adapter);
	}
	/// <summary>
	/// Retrieves the critical native memory of array elements.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> array element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	public static JPrimitiveMemory<TPrimitive> GetCriticalElements<TPrimitive>(this JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind = JMemoryReferenceKind.ThreadUnrestricted)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		INativeMemoryAdapter adapter = jArray.Environment.ArrayFeature.GetCriticalSequence(jArray, referenceKind);
		return new(adapter);
	}
	/// <summary>
	/// Creates an array of <typeparamref name="TPrimitive"/> elements containing a copy of
	/// the elements on the current <see cref="JArrayObject{TPrimitive}"/> instance.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> array element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="startIndex">Initial index to copy to.</param>
	/// <returns>
	/// An array of <typeparamref name="TPrimitive"/> elements containing a copy of the elements on
	/// the current <see cref="JArrayObject{TPrimitive}"/> instance.
	/// </returns>
	public static TPrimitive[] ToArray<TPrimitive>(this JArrayObject<TPrimitive> jArray, Int32 startIndex = 0)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> jArray.ToArray(startIndex, jArray.Length - startIndex);
	/// <summary>
	/// Creates an array of <typeparamref name="TPrimitive"/> elements containing a copy of
	/// the elements on the current <see cref="JArrayObject{TPrimitive}"/> instance.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> array element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="startIndex">Initial <paramref name="jArray"/> index to copy to.</param>
	/// <param name="count">The number of elements to copy to.</param>
	/// <returns>
	/// An array of <typeparamref name="TPrimitive"/> elements containing a copy of the elements on
	/// the current <see cref="JArrayObject{TPrimitive}"/> instance.
	/// </returns>
	public static TPrimitive[] ToArray<TPrimitive>(this JArrayObject<TPrimitive> jArray, Int32 startIndex, Int32 count)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		TPrimitive[] arr = new TPrimitive[count];
		IEnvironment env = jArray.Environment;
		env.ArrayFeature.GetCopy(jArray, startIndex, arr.AsMemory());
		return arr;
	}
	/// <summary>
	/// Sets the elements on the current <see cref="JArrayObject{TPrimitive}"/> instance according to
	/// <paramref name="mem"/> content.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> array element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="mem">A <see cref="ReadOnlyMemory{TPrimitive}"/> containing elements to copy from.</param>
	/// <param name="startIndex">Initial <paramref name="jArray"/> index to copy from.</param>
	public static void Set<TPrimitive>(this JArrayObject<TPrimitive> jArray, ReadOnlyMemory<TPrimitive> mem,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jArray.Environment;
		env.ArrayFeature.SetCopy(jArray, mem, startIndex);
	}
}