// ReSharper disable MemberCanBePrivate.Global

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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void MethodCall<TArgs>(JLocalObject jLocal, scoped ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> this.MethodCall(jLocal, jLocal.Class, false, ref args);
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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void MethodCall<TArgs>(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual, scoped ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JFunctionDefinition functionDefinition:
				result = this.FunctionCall(functionDefinition, jLocal, jClass, nonVirtual, ref args);
				break;
			case JMethodDefinition definition:
				IndeterminateCall.MethodCall(definition, jLocal, jClass, nonVirtual, ref args);
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
	public void StaticMethodCall<TArgs>(JClassObject jClass, scoped ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JConstructorDefinition constructorDefinition:
				JLocalObject newObject =
					IndeterminateCall.NewCall<JLocalObject, TArgs>(constructorDefinition, jClass, ref args);
				result = new(newObject, jClass.ClassSignature);
				break;
			case JFunctionDefinition functionDefinition:
				result = this.StaticFunctionCall(functionDefinition, jClass, ref args);
				break;
			case JMethodDefinition definition:
				IndeterminateCall.StaticMethodCall(definition, jClass, ref args);
				break;
		}
		result.Object?.Dispose();
	}
}