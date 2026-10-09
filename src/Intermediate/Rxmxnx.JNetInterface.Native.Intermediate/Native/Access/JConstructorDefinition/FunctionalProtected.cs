// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JConstructorDefinition
{
	/// <summary>
	/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jClass">An <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject New<TArgs>(JClassObject jClass, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.New(jClass, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
			return this.New<JLocalObject, TArgs>(jClass, ref args);
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.New<JLocalObject, NonGenericCallArgument>(jClass, ref nonGeneric);
	}
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	protected TObject New<TObject, TArgs>(IEnvironment env, TArgs? args)
		where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		JClassObject jClass = env.ClassFeature.GetClass<TObject>();
		if (args is null)
			return this.New<TObject, ICallArgument.Empty>(jClass, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
			return this.New<TObject, TArgs>(jClass, ref args);
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.New<TObject, NonGenericCallArgument>(jClass, ref nonGeneric);
	}
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject NewReflected<TArgs>(JConstructorObject jConstructor, TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.NewReflected<JLocalObject, ICallArgument.Empty>(jConstructor, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jConstructor.Environment;
			return env.AccessFeature.CallConstructor<JLocalObject, TArgs>(jConstructor, this, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.NewReflected<JLocalObject, NonGenericCallArgument>(jConstructor, ref nonGeneric);
	}
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	protected TObject NewReflected<TObject, TArgs>(JConstructorObject jConstructor, TArgs? args)
		where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		if (args is null)
			return this.NewReflected<TObject, ICallArgument.Empty>(jConstructor, ref ICallArgument.Default);
		if (typeof(TArgs).IsValueType)
		{
			IEnvironment env = jConstructor.Environment;
			return env.AccessFeature.CallConstructor<TObject, TArgs>(jConstructor, this, ref args);
		}
		NonGenericCallArgument nonGeneric = NonGenericCallArgument.Create(args);
		return this.NewReflected<TObject, NonGenericCallArgument>(jConstructor, ref nonGeneric);
	}
}