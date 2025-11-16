namespace Rxmxnx.JNetInterface.Internal;

internal static partial class IndeterminateHelper
{
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jConstructor">Reflected constructor instance.</param>
	/// <param name="args">Method arguments.</param>
	public static JLocalObject ReflectedNewCall(JConstructorObject jConstructor,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
	{
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(jConstructor.Definition);
		return IndeterminateHelper.ReflectedNewCall<JLocalObject>(definition, jConstructor, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="jConstructor">Reflected constructor instance.</param>
	/// <param name="args">Method arguments.</param>
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public static TObject ReflectedNewCall<TObject>(JConstructorObject jConstructor,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	) where TObject : JLocalObject, IClassType<TObject>
	{
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(jConstructor.Definition);
		return IndeterminateHelper.ReflectedNewCall<TObject>(definition, jConstructor, args);
	}
}