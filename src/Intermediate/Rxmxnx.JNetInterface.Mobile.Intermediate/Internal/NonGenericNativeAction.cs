namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a non-generic JNI context action.
/// </summary>
internal readonly struct NonGenericNativeAction : IAndroidNativeAction
{
	/// <summary>
	/// Internal value.
	/// </summary>
	private IAndroidNativeAction Value { get; init; }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IAndroidNativeAction.Accept(AndroidJniContext context) => this.Value.Accept(context);

	/// <summary>
	/// Creates a new <see cref="NonGenericNativeAction"/> instance.
	/// </summary>
	/// <typeparam name="TAction">Type of <see cref="IAndroidNativeAction"/>.</typeparam>
	/// <param name="action">Action to perform with the JNI context.</param>
	/// <returns>A new <see cref="NonGenericNativeAction"/> instance.</returns>
	public static NonGenericNativeAction Create<TAction>(TAction action)
#if !NET9_0_OR_GREATER
		where TAction : IAndroidNativeAction
	{
		Debug.Assert(!typeof(TAction).IsValueType);
		return new() { Value = action, };
	}
#else
		where TAction : IAndroidNativeAction, allows ref struct
	{
		Debug.Assert(!typeof(TAction).IsValueType);
		return new() { Value = Unsafe.As<TAction, IAndroidNativeAction>(ref action), };
	}
#endif
}