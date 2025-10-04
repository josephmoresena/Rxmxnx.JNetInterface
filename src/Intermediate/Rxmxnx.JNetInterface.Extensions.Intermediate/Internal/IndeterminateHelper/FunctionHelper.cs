namespace Rxmxnx.JNetInterface.Internal;

internal static partial class IndeterminateHelper
{
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IndeterminateResult ReflectedFunctionCall(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedFunctionCall(jMethod, jLocal, false, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static IndeterminateResult ReflectedFunctionCall(JMethodObject jMethod, JLocalObject jLocal,
		Boolean nonVirtual,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (jMethod.Definition)
		{
			case JMethodDefinition methodDefinition:
				IndeterminateHelper.ReflectedMethodCall(methodDefinition, jMethod, jLocal, nonVirtual, args);
				break;
			case JFunctionDefinition definition:
				result = IndeterminateHelper.ReflectedFunctionCall(definition, jMethod, jLocal, nonVirtual, args);
				break;
		}
		return result;
	}
	/// <summary>
	/// Invokes a static function on the declaring class of given <see cref="JExecutableObject"/> instance and
	/// returns its result.
	/// </summary>
	/// <param name="jExecutable">Reflected executable instance.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static IndeterminateResult ReflectedStaticFunctionCall(JExecutableObject jExecutable,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (jExecutable.Definition)
		{
			case JConstructorDefinition constructorDefinition:
				JConstructorObject jConstructor = jExecutable.CastTo<JConstructorObject>();
				JLocalObject newObject =
					IndeterminateHelper.ReflectedNewCall<JLocalObject>(constructorDefinition, jConstructor, args);
				result = new(newObject, jConstructor.DeclaringClass.ClassSignature);
				break;
			case JMethodDefinition methodDefinition:
				JMethodObject jMethod = jExecutable.CastTo<JMethodObject>();
				IndeterminateHelper.ReflectedStaticMethodCall(methodDefinition, jMethod, args);
				break;
			case JFunctionDefinition definition:
				JMethodObject jFunction = jExecutable.CastTo<JMethodObject>();
				result = IndeterminateHelper.ReflectedStaticFunctionCall(definition, jFunction, args);
				break;
		}
		return result;
	}
}