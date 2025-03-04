namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JConstructorDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	protected JConstructorDefinition(
#if !NET9_0_OR_GREATER
		params
#endif
			JArgumentMetadata[] metadata) : this(metadata.AsSpan()) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	protected JConstructorDefinition(
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<JArgumentMetadata> metadata) : base(CommonNames.Constructor, metadata) { }

	/// <summary>
	/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition.
	/// </summary>
	/// <param name="jClass">An <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject New(JClassObject jClass,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
		=> this.New<JLocalObject>(jClass, args);
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	protected TObject New<TObject>(IEnvironment env,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		NativeValidationUtilities.ThrowIfAbstractClass(IClassType.GetMetadata<TObject>());
		return this.New<TObject>(env.ClassFeature.GetClass<TObject>(), args);
	}
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject NewReflected(JConstructorObject jConstructor,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args)
		=> this.NewReflected<JLocalObject>(jConstructor, args);
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	protected TObject NewReflected<TObject>(JConstructorObject jConstructor,
#if NET9_0_OR_GREATER
		params
#endif
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jConstructor.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jConstructor, this, args);
	}
}