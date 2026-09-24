namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public void MethodCall<TArgs>(JLocalObject jLocal, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		=> this.MethodCall(jLocal, jLocal.Class, false, in args);
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Call declaring class.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public void MethodCall<TArgs>(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JFunctionDefinition functionDefinition:
				result = this.FunctionCall(functionDefinition, jLocal, jClass, nonVirtual, in args);
				break;
			case JMethodDefinition definition:
				IndeterminateCall.MethodCall(definition, jLocal, jClass, nonVirtual, in args);
				break;
		}
		result.Object?.Dispose();
	}
	/// <summary>
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public void StaticMethodCall<TArgs>(JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JConstructorDefinition constructorDefinition:
				JLocalObject newObject =
					IndeterminateCall.NewCall<JLocalObject, TArgs>(constructorDefinition, jClass, in args);
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