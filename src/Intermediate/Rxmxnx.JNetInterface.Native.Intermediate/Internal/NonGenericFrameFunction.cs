namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a non-generic frame action used within a specific JNI environment context.
/// </summary>
internal readonly struct NonGenericFrameFunction<TOutput> : IFrameFunction<TOutput>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	private IFrameFunction<TOutput> Value { get; init; }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	TOutput IFrameFunction<TOutput>.Apply(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
		=> this.Value.Apply(env, objects);

	/// <summary>
	/// Creates a new <see cref="NonGenericFrameFunction{TOutput}"/> instance.
	/// </summary>
	/// <typeparam name="TFunction">Type of <see cref="IFrameFunction{TOutput}"/>.</typeparam>
	/// <param name="func">Function to perform with the allocated buffer.</param>
	/// <returns>A new <see cref="NonGenericFrameFunction{TOutput}"/> instance.</returns>
	public static NonGenericFrameFunction<TOutput> Create<TFunction>(TFunction func)
#if !NET9_0_OR_GREATER
		where TFunction : IFrameFunction<TOutput>
	{
		Debug.Assert(!typeof(TFunction).IsValueType);
		return new() { Value = func, };
	}
#else
		where TFunction : IFrameFunction<TOutput>, allows ref struct
	{
		Debug.Assert(!typeof(TFunction).IsValueType);
		return new() { Value = Unsafe.As<TFunction, IFrameFunction<TOutput>>(ref func), };
	}
#endif
}