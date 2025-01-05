namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult FunctionCall(JLocalObject jLocal, ReadOnlySpan<IObject?> args)
		=> this.FunctionCall(jLocal, jLocal.Class, false, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring call class.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult FunctionCall(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual,
		ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JMethodDefinition methodDefinition:
				IndeterminateCall.MethodCall(methodDefinition, jLocal, jClass, nonVirtual, args);
				break;
			case JFunctionDefinition definition:
				result = this.FunctionCall(definition, jLocal, jClass, nonVirtual, args);
				break;
		}
		return result;
	}
	/// <summary>
	/// Invokes a static function on given <see cref="JClassObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult StaticFunctionCall(JClassObject jClass, ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JConstructorDefinition constructorDefinition:
				JLocalObject newObject = IndeterminateCall.NewCall<JLocalObject>(constructorDefinition, jClass, args);
				result = new(newObject, jClass.ClassSignature);
				break;
			case JMethodDefinition methodDefinition:
				IndeterminateCall.StaticMethodCall(methodDefinition, jClass, args);
				break;
			case JFunctionDefinition definition:
				result = this.StaticFunctionCall(definition, jClass, args);
				break;
		}
		return result;
	}

	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static IndeterminateResult ReflectedFunctionCall(JMethodObject jMethod, JLocalObject jLocal,
		ReadOnlySpan<IObject?> args)
		=> IndeterminateCall.ReflectedFunctionCall(jMethod, jLocal, false, args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static IndeterminateResult ReflectedFunctionCall(JMethodObject jMethod, JLocalObject jLocal,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (jMethod.Definition)
		{
			case JMethodDefinition methodDefinition:
				IndeterminateCall.ReflectedMethodCall(methodDefinition, jMethod, jLocal, nonVirtual, args);
				break;
			case JFunctionDefinition definition:
				result = IndeterminateCall.ReflectedFunctionCall(definition, jMethod, jLocal, nonVirtual, args);
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
		ReadOnlySpan<IObject?> args)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (jExecutable.Definition)
		{
			case JConstructorDefinition constructorDefinition:
				JConstructorObject jConstructor = jExecutable.CastTo<JConstructorObject>();
				JLocalObject newObject =
					IndeterminateCall.ReflectedNewCall<JLocalObject>(constructorDefinition, jConstructor, args);
				result = new(newObject, jConstructor.DeclaringClass.ClassSignature);
				break;
			case JMethodDefinition methodDefinition:
				JMethodObject jMethod = jExecutable.CastTo<JMethodObject>();
				IndeterminateCall.ReflectedStaticMethodCall(methodDefinition, jMethod, args);
				break;
			case JFunctionDefinition definition:
				JMethodObject jFunction = jExecutable.CastTo<JMethodObject>();
				result = IndeterminateCall.ReflectedStaticFunctionCall(definition, jFunction, args);
				break;
		}
		return result;
	}
}