namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Method arguments.</param>
	public void MethodCall(JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
		=> this.MethodCall(jLocal, jLocal.Class, false, args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring call class.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	public void MethodCall(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JFunctionDefinition functionDefinition:
				result = this.FunctionCall(functionDefinition, jLocal, jClass, nonVirtual, args);
				break;
			case JMethodDefinition definition:
				IndeterminateCall.MethodCall(definition, jLocal, jClass, nonVirtual, args);
				break;
		}
		result.Object?.Dispose();
	}
	/// <summary>
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	public void StaticMethodCall(JClassObject jClass,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JConstructorDefinition constructorDefinition:
				JLocalObject newObject = IndeterminateCall.NewCall<JLocalObject>(constructorDefinition, jClass, args);
				result = new(newObject, jClass.ClassSignature);
				break;
			case JFunctionDefinition functionDefinition:
				result = this.StaticFunctionCall(functionDefinition, jClass, args);
				break;
			case JMethodDefinition definition:
				IndeterminateCall.StaticMethodCall(definition, jClass, args);
				break;
		}
		result.Object?.Dispose();
	}

	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Method arguments.</param>
	public static void ReflectedMethodCall(JMethodObject jMethod, JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
		=> IndeterminateCall.ReflectedMethodCall(jMethod, jLocal, false, args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	public static void ReflectedMethodCall(JMethodObject jMethod, JLocalObject jLocal, Boolean nonVirtual,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (jMethod.Definition)
		{
			case JFunctionDefinition functionDefinition:
				result = IndeterminateCall.ReflectedFunctionCall(functionDefinition, jMethod, jLocal, nonVirtual, args);
				break;
			case JMethodDefinition definition:
				IndeterminateCall.ReflectedMethodCall(definition, jMethod, jLocal, nonVirtual, args);
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
		params
#endif
		ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (jExecutable.Definition)
		{
			case JConstructorDefinition constructorDefinition:
				JConstructorObject jConstructor = jExecutable.CastTo<JConstructorObject>();
				JLocalObject newObject =
					IndeterminateCall.ReflectedNewCall<JLocalObject>(constructorDefinition, jConstructor, args);
				result = new(newObject, jExecutable.DeclaringClass.ClassSignature);
				break;
			case JFunctionDefinition functionDefinition:
				JMethodObject jFunction = jExecutable.CastTo<JMethodObject>();
				result = IndeterminateCall.ReflectedStaticFunctionCall(functionDefinition, jFunction, args);
				break;
			case JMethodDefinition definition:
				JMethodObject jMethod = jExecutable.CastTo<JMethodObject>();
				IndeterminateCall.ReflectedStaticMethodCall(definition, jMethod, args);
				break;
		}
		result.Object?.Dispose();
	}
}