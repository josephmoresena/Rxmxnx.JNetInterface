/// <summary>
/// Set of primitive memory extensions.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CA1050
public static class JArrayMemoryExtensions
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
		IVirtualMachine vm = jArray.Environment.VirtualMachine;
		JNativeMemoryHandler handler = referenceKind switch
		{
			JMemoryReferenceKind.ThreadIndependent => JNativeMemoryHandler.CreateWeakHandler(jArray, false),
			JMemoryReferenceKind.ThreadUnrestricted => JNativeMemoryHandler.CreateGlobalHandler(jArray, false),
			_ => JNativeMemoryHandler.CreateHandler(jArray, false),
		};
		return new(vm, handler);
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
		IVirtualMachine vm = jArray.Environment.VirtualMachine;
		JNativeMemoryHandler handler = referenceKind switch
		{
			JMemoryReferenceKind.ThreadIndependent => JNativeMemoryHandler.CreateWeakHandler(jArray, true),
			JMemoryReferenceKind.ThreadUnrestricted => JNativeMemoryHandler.CreateGlobalHandler(jArray, true),
			_ => JNativeMemoryHandler.CreateHandler(jArray, true),
		};
		return new(vm, handler);
	}
}