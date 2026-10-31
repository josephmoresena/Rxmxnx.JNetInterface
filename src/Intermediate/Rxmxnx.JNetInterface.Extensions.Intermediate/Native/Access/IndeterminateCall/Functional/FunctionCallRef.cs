// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jLocal">Target object.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IndeterminateResult FunctionCall<TArgs>(JLocalObject jLocal, scoped ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> this.FunctionCall(jLocal, jLocal.Class, false, ref args);
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Call declaring class.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IndeterminateResult FunctionCall<TArgs>(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual,
		scoped ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		switch (this.Definition)
		{
			case JMethodDefinition methodDefinition:
				IndeterminateCall.MethodCall(methodDefinition, jLocal, jClass, nonVirtual, ref args);
				break;
			case JFunctionDefinition definition:
				result = this.FunctionCall(definition, jLocal, jClass, nonVirtual, ref args);
				break;
		}
		return result;
	}
	/// <summary>
	/// Invokes a static function on given <see cref="JClassObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IndeterminateResult StaticFunctionCall<TArgs>(JClassObject jClass, scoped ref TArgs args)
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
			case JMethodDefinition methodDefinition:
				IndeterminateCall.StaticMethodCall(methodDefinition, jClass, ref args);
				break;
			case JFunctionDefinition definition:
				result = this.StaticFunctionCall(definition, jClass, ref args);
				break;
		}
		return result;
	}
}