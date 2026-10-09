using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal readonly struct GenericArgument<T0, T1>(T0? a0, T1? a1) : ICallArgument where T0 : IObject where T1 : IObject
{
	public void Configure<TSlot>(TSlot slot, JCallDefinition _) where TSlot : IParameterSlot
	{
		slot.SetParameterValue(0, a0);
		slot.SetParameterValue(1, a1);
	}
}

internal readonly struct GenericArgument<T0, T1, T2>(T0? a0, T1? a1, T2? a2)
	: ICallArgument where T0 : IObject where T1 : IObject where T2 : IObject
{
	public void Configure<TSlot>(TSlot slot, JCallDefinition _) where TSlot : IParameterSlot
	{
		slot.SetParameterValue(0, a0);
		slot.SetParameterValue(1, a1);
		slot.SetParameterValue(2, a2);
	}
}