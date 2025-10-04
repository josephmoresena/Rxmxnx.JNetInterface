namespace Rxmxnx.JNetInterface.Internal;

internal static partial class IndeterminateHelper
{
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Method arguments.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReflectedMethodCall(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
		=> IndeterminateHelper.ReflectedMethodCall(jMethod, jLocal, false, args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	public static void ReflectedMethodCall(JMethodObject jMethod, JLocalObject jLocal, Boolean nonVirtual,
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
			case JFunctionDefinition functionDefinition:
				result = IndeterminateHelper.ReflectedFunctionCall(functionDefinition, jMethod, jLocal, nonVirtual,
				                                                   args);
				break;
			case JMethodDefinition definition:
				IndeterminateHelper.ReflectedMethodCall(definition, jMethod, jLocal, nonVirtual, args);
				break;
		}
		result.Object?.Dispose();
	}
	/// <summary>
	/// Invokes a static method on the declaring class of given <see cref="JExecutableObject"/> instance and
	/// returns its result.
	/// </summary>
	/// <param name="jExecutable">Reflected executable instance.</param>
	/// <param name="args">Method arguments.</param>
	public static void ReflectedStaticMethodCall(JExecutableObject jExecutable,
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
				result = new(newObject, jExecutable.DeclaringClass.ClassSignature);
				break;
			case JFunctionDefinition functionDefinition:
				JMethodObject jFunction = jExecutable.CastTo<JMethodObject>();
				result = IndeterminateHelper.ReflectedStaticFunctionCall(functionDefinition, jFunction, args);
				break;
			case JMethodDefinition definition:
				JMethodObject jMethod = jExecutable.CastTo<JMethodObject>();
				IndeterminateHelper.ReflectedStaticMethodCall(definition, jMethod, args);
				break;
		}
		result.Object?.Dispose();
	}
}