namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a generic argument used in a method or function call.
/// This struct is designed to encapsulate a single argument, its associated metadata, and its intended use within a
/// generic invocation.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
#pragma warning disable CS8500
internal readonly unsafe struct GenericCallArgument<TArgs>(TArgs* ptr) : ICallArgument where TArgs : ICallArgument
#pragma warning restore CS8500
#if NET9_0_OR_GREATER
	, allows ref struct
#endif
{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot
	{
		Debug.Assert(typeof(TSlot).IsValueType);
		ptr[0].Configure(slot);
	}
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public String ToTraceText() => ptr[0].ToTraceText() ?? String.Empty;
}