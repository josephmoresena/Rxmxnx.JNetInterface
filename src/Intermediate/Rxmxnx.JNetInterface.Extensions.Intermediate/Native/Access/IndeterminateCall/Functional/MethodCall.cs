namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public abstract unsafe partial class IndeterminateCall
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
	public void MethodCall<TArgs>(JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
		{
			this.MethodCall(jLocal, ref ICallArgument.Default);
			return;
		}
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			this.MethodCall(jLocal, ref nonGeneric);
			return;
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			this.MethodCall(jLocal, ref generic);
		}
	}
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
	public void MethodCall<TArgs>(JLocalObject jLocal, JClassObject jClass, Boolean nonVirtual, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
		{
			this.MethodCall(jLocal, jClass, nonVirtual, ref ICallArgument.Default);
			return;
		}
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			this.MethodCall(jLocal, jClass, nonVirtual, ref nonGeneric);
			return;
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			this.MethodCall(jLocal, jClass, nonVirtual, ref generic);
		}
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
	public void StaticMethodCall<TArgs>(JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
		{
			this.StaticMethodCall(jClass, ref ICallArgument.Default);
			return;
		}
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			this.StaticMethodCall(jClass, ref nonGeneric);
			return;
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			this.StaticMethodCall(jClass, ref generic);
		}
	}
}