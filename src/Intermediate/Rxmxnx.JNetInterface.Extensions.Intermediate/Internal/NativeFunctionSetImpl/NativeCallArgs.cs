namespace Rxmxnx.JNetInterface.Internal;

// ReSharper disable once ClassCannotBeInstantiated
internal partial class NativeFunctionSetImpl
{
	/// <summary>
	/// Single object call args.
	/// </summary>
	/// <typeparam name="T0">Type of the argument.</typeparam>
	/// <param name="a0">Argument value.</param>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct NativeCallArgs<T0>(T0? a0) : ICallArgument where T0 : IObject
	{
		void ICallArgument.Configure<TSlot>(TSlot slot, JCallDefinition callDefinition)
		{
			slot.SetParameterValue(0, a0);
		}
	}

	/// <summary>
	/// Single object call args.
	/// </summary>
	/// <typeparam name="T0">Type of the argument.</typeparam>
	/// <typeparam name="T1">Type of the argument.</typeparam>
	/// <param name="a0">Argument value.</param>
	/// <param name="a1">Argument value.</param>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct NativeCallArgs<T0, T1>(T0? a0, T1? a1) : ICallArgument where T0 : IObject where T1 : IObject
	{
		void ICallArgument.Configure<TSlot>(TSlot slot, JCallDefinition callDefinition)
		{
			slot.SetParameterValue(0, a0);
			slot.SetParameterValue(1, a1);
		}
	}
}