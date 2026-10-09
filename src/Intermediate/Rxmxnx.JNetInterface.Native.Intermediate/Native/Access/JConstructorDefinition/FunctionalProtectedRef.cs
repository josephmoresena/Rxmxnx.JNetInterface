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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected JLocalObject New<TArgs>(JClassObject jClass, scoped ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> this.New<JLocalObject, TArgs>(jClass, ref args);
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	protected TObject New<TObject, TArgs>(IEnvironment env, ref TArgs args)
		where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> this.New<TObject, TArgs>(env.ClassFeature.GetClass<TObject>(), ref args);
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected JLocalObject NewReflected<TArgs>(JConstructorObject jConstructor, ref TArgs args)
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
		=> this.NewReflected<JLocalObject, TArgs>(jConstructor, ref args);
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	protected TObject NewReflected<TObject, TArgs>(JConstructorObject jConstructor, ref TArgs args)
		where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : struct, ICallArgument
#else
		where TArgs : struct, ICallArgument, allows ref struct
#endif
	{
		Debug.Assert(typeof(TArgs).IsValueType);
		IEnvironment env = jConstructor.Environment;
		return env.AccessFeature.CallConstructor<TObject, TArgs>(jConstructor, this, ref args);
	}
}