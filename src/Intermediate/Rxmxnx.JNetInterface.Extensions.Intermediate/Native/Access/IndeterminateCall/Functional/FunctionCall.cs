namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public abstract unsafe partial class IndeterminateCall
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
	public IndeterminateResult FunctionCall<TArgs>(JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.FunctionCall(jLocal, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return this.FunctionCall(jLocal, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return this.FunctionCall(jLocal, ref generic);
		}
	}
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
	public IndeterminateResult FunctionCall<TArgs>(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual,
		TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.FunctionCall(jLocal, jClass, nonVirtual, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return this.FunctionCall(jLocal, jClass, nonVirtual, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return this.FunctionCall(jLocal, jClass, nonVirtual, ref generic);
		}
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
	public IndeterminateResult StaticFunctionCall<TArgs>(JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.StaticFunctionCall(jClass, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return this.StaticFunctionCall(jClass, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return this.StaticFunctionCall(jClass, ref generic);
		}
	}
}