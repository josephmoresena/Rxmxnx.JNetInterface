namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult FunctionCall(JLocalObject jLocal,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
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
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
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
	public IndeterminateResult StaticFunctionCall(JClassObject jClass,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args = default
#endif
	)
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
}