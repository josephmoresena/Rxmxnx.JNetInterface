namespace Rxmxnx.JNetInterface.Internal;

internal partial class NativeFunctionSetImpl
{
	/// <summary>
	/// Single object buffer.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
	                 Justification = CommonConstants.BinaryStructJustification)]
#endif
	[InlineArray(1)]
	public struct SingleObjectBuffer
	{
		private IObject? _value;

		public static Span<IObject?> GetSpan(ref SingleObjectBuffer buffer)
			=> MemoryMarshal.CreateSpan(ref Unsafe.As<SingleObjectBuffer, IObject?>(ref buffer), 1);
	}
}