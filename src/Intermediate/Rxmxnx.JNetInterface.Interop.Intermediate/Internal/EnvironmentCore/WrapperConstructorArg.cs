namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class EnvironmentCore
{
	/// <summary>
	/// Represents a structure used as an argument for primitive wrapper constructors.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	private readonly struct WrapperConstructorArg<TPrimitive> : ICallArgument
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, INativeDataType<TPrimitive>
	{
		/// <summary>
		/// Internal value.
		/// </summary>
		private readonly TPrimitive _value;

		void ICallArgument.Configure<TSlot>(TSlot slot, JCallDefinition callDefinition)
			=> slot.SetParameterValue(0, this._value);
	}
}