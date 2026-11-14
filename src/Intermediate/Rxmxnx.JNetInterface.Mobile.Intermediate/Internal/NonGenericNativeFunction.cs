namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a non-generic JNI context function.
/// </summary>
/// <typeparam name="TOutput">The type of the output produced by the function.</typeparam>
internal readonly struct NonGenericNativeFunction<TOutput> : IAndroidNativeFunction<TOutput>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	private IAndroidNativeFunction<TOutput> Value { get; init; }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	TOutput IAndroidNativeFunction<TOutput>.Apply(AndroidJniContext context) => this.Value.Apply(context);

	/// <summary>
	/// Creates a new <see cref="NonGenericNativeFunction{TOutput}"/> instance.
	/// </summary>
	/// <typeparam name="TFunction">Type of <see cref="IAndroidNativeFunction{TOutput}"/>.</typeparam>
	/// <param name="function">Function to perform with the JNI context.</param>
	/// <returns>A new <see cref="NonGenericNativeFunction{TOutput}"/> instance.</returns>
	public static NonGenericNativeFunction<TOutput> Create<TFunction>(TFunction function)
#if !NET9_0_OR_GREATER
		where TFunction : IAndroidNativeFunction<TOutput>
	{
		Debug.Assert(!typeof(TFunction).IsValueType);
		return new() { Value = function, };
	}
#else
		where TFunction : IAndroidNativeFunction<TOutput>, allows ref struct
	{
		Debug.Assert(!typeof(TFunction).IsValueType);
		return new() { Value = Unsafe.As<TFunction, IAndroidNativeFunction<TOutput>>(ref function), };
	}
#endif
}