namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a non-generic argument used in a Java method call.
/// </summary>
internal readonly struct NonGenericCallArgument : ICallArgument
{
	/// <summary>
	/// Internal value.
	/// </summary>
	private ICallArgument? Value { get; init; }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ICallArgument.Configure<TSlot>(TSlot slot)
	{
		if (this.Value is not null)
			this.Value.Configure(slot);
		else
			ICallArgument.Default.Configure(slot);
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	String ICallArgument.ToTraceText() => this.Value?.ToTraceText() ?? String.Empty;

	/// <summary>
	/// Creates a new instance of <see cref="NonGenericCallArgument"/> from the provided call argument.
	/// </summary>
	/// <typeparam name="TArgs">The type of the call argument implementing <see cref="ICallArgument"/>.</typeparam>
	/// <param name="args">The call argument to be encapsulated within the new <see cref="NonGenericCallArgument"/>.</param>
	/// <returns>A new <see cref="NonGenericCallArgument"/> encapsulating the provided call argument.</returns>
	public static NonGenericCallArgument Create<TArgs>(TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
	{
		Debug.Assert(!typeof(TArgs).IsValueType);
		return new() { Value = args, };
	}
#else
		where TArgs : ICallArgument, allows ref struct
	{
		Debug.Assert(!typeof(TArgs).IsValueType);
		return new() { Value = Unsafe.As<TArgs, ICallArgument>(ref args), };
	}
#endif
}