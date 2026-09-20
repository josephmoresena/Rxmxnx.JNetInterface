namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IAccessFeature
{
	/// <summary>
	/// Invokes a primitive static function on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="bytes">Binary span to hold the result.</param>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The arguments to be passed to the static call.</param>
	internal void CallStaticPrimitiveFunction<TArgs>(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		this.CallStaticPrimitiveFunction(bytes, jClass, definition, slot);
	}
	/// <summary>
	/// Invokes a primitive function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="bytes">Binary span to hold the result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">The arguments to be passed to the instance call.</param>
	internal void CallPrimitiveFunction<TArgs>(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		this.CallPrimitiveFunction(bytes, jLocal, jClass, definition, nonVirtual, slot);
	}
}