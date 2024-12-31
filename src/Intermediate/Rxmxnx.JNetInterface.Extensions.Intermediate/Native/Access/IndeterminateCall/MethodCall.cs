namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Method arguments.</param>
	public void MethodCall(JLocalObject jLocal, ReadOnlySpan<IObject?> args)
		=> this.MethodCall(jLocal, jLocal.Class, false, args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring call class.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	public void MethodCall(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
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
	/// Invokes a method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	public void StaticMethodCall(JClassObject jClass, ReadOnlySpan<IObject?> args)
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
}