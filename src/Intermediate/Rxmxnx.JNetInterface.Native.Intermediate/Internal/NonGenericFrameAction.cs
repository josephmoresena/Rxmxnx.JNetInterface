namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a non-generic frame action used within a specific JNI environment context.
/// </summary>
internal readonly struct NonGenericFrameAction : IFrameAction
{
	/// <summary>
	/// Internal value.
	/// </summary>
	private IFrameAction Value { get; init; }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IFrameAction.Accept(IEnvironment env, ReadOnlySpan<JLocalObject?> objects) => this.Value.Accept(env, objects);

	/// <summary>
	/// Creates a new <see cref="NonGenericFrameAction"/> instance.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IFrameAction"/>.</typeparam>
	/// <param name="action">Action to perform with the allocated buffer.</param>
	/// <returns>A new <see cref="NonGenericFrameAction"/> instance.</returns>
	public static NonGenericFrameAction Create<TAction>(TAction action)
#if !NET9_0_OR_GREATER
			where TAction : IFrameAction
		{
			Debug.Assert(!typeof(TAction).IsValueType);
			return new() { Value = action, };
		}
#else
		where TAction : IFrameAction, allows ref struct
	{
		Debug.Assert(!typeof(TAction).IsValueType);
		return new() { Value = Unsafe.As<TAction, IFrameAction>(ref action), };
	}
#endif
}