namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IAccessFeature
{
	/// <summary>
	/// Invokes a primitive static function on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="bytes">Binary span to hold the result.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The <see cref="IObject"/> list with call arguments.</param>
	internal void CallStaticPrimitiveFunction(Span<Byte> bytes, JClassObject jClass, JFunctionDefinition definition,
		ReadOnlySpan<IObject?> args = default);
	/// <summary>
	/// Invokes a primitive function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="bytes">Binary span to hold the result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> list with call arguments.</param>
	internal void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args = default);
}