namespace Rxmxnx.JNetInterface.Native.Access;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public sealed unsafe partial class JNonTypedFunctionDefinition
{
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke{TArgs}(JLocalObject, TArgs)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new JLocalObject? Invoke<TArgs>(JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return base.Invoke(jLocal, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return base.Invoke(jLocal, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return base.Invoke(jLocal, ref generic);
		}
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.Invoke{TArgs}(JLocalObject, JClassObject, TArgs)"/>
	public new JLocalObject? Invoke<TArgs>(JLocalObject jLocal, JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return base.Invoke(jLocal, jClass, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return base.Invoke(jLocal, jClass, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return base.Invoke(jLocal, jClass, ref generic);
		}
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeNonVirtual{TArgs}(JLocalObject, JClassObject, TArgs)"/>
	public new JLocalObject? InvokeNonVirtual<TArgs>(JLocalObject jLocal, JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return base.InvokeNonVirtual(jLocal, jClass, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return base.InvokeNonVirtual(jLocal, jClass, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return base.InvokeNonVirtual(jLocal, jClass, ref generic);
		}
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.StaticInvoke{TArgs}(JClassObject, TArgs)"/>
	public new JLocalObject? StaticInvoke<TArgs>(JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return base.StaticInvoke(jClass, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return base.StaticInvoke(jClass, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return base.StaticInvoke(jClass, ref generic);
		}
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeReflected{TArgs}(JMethodObject, JLocalObject, TArgs)"/>
	public new JLocalObject? InvokeReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return base.InvokeReflected(jMethod, jLocal, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return base.InvokeReflected(jMethod, jLocal, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return base.InvokeReflected(jMethod, jLocal, ref generic);
		}
	}
	/// <inheritdoc
	///     cref="JFunctionDefinition{TResult}.InvokeNonVirtualReflected{TArgs}(JMethodObject, JLocalObject, ref TArgs)"/>
	public new JLocalObject? InvokeNonVirtualReflected<TArgs>(JMethodObject jMethod, JLocalObject jLocal, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return base.InvokeNonVirtualReflected(jMethod, jLocal, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return base.InvokeNonVirtualReflected(jMethod, jLocal, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return base.InvokeNonVirtualReflected(jMethod, jLocal, ref generic);
		}
	}
	/// <inheritdoc cref="JFunctionDefinition{TResult}.InvokeStaticReflected{TArgs}(JMethodObject, ref TArgs)"/>
	public new JLocalObject? InvokeStaticReflected<TArgs>(JMethodObject jMethod, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return base.InvokeStaticReflected(jMethod, ref ICallArgument.Default);
		if (!typeof(TArgs).IsValueType)
		{
			NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
			return base.InvokeStaticReflected(jMethod, ref nonGeneric);
		}
#pragma warning disable CS8500
		fixed (TArgs* ptr = &Unsafe.AsRef(in args))
#pragma warning restore CS8500
		{
			GenericCallArgument<TArgs> generic = new(ptr);
			return base.InvokeStaticReflected(jMethod, ref generic);
		}
	}
}