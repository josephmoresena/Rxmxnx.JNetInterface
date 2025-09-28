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
	public struct SingleObjectBuffer
	{
#pragma warning disable CS0169
		private IObject? _value;
#pragma warning restore CS0169

		public static Span<IObject?> GetSpan(ref SingleObjectBuffer buffer)
			=> MemoryMarshal.CreateSpan(ref Unsafe.As<SingleObjectBuffer, IObject?>(ref buffer), 1);
	}
}