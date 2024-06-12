namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JConstructorDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	protected JConstructorDefinition(params JArgumentMetadata[] metadata) : base(CommonNames.Constructor, metadata) { }

	/// <summary>
	/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">An <see cref="JClassObject"/> instance.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject New(JClassObject jClass) => this.New<JLocalObject>(jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition passing the default value for each argument.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	protected TObject New<TObject>(IEnvironment env) where TObject : JLocalObject, IClassType<TObject>
		=> this.New<TObject>(env.ClassFeature.GetClass<TObject>(), this.CreateArgumentsArray());
	/// <summary>
	/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition.
	/// </summary>
	/// <param name="jClass">An <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject New(JClassObject jClass, IObject?[] args) => this.New<JLocalObject>(jClass, args);
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	protected TObject New<TObject>(IEnvironment env, IObject?[] args) where TObject : JLocalObject, IClassType<TObject>
		=> this.New<TObject>(env.ClassFeature.GetClass<TObject>(), args);
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject NewReflected(JConstructorObject jConstructor)
		=> this.NewReflected(jConstructor, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition
	/// passing the default value for each argument.
	/// </summary>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject NewReflected(JConstructorObject jConstructor, IObject?[] args)
		=> this.NewReflected<JLocalObject>(jConstructor, args);
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	protected TObject NewReflected<TObject>(JConstructorObject jConstructor)
		where TObject : JLocalObject, IClassType<TObject>
		=> this.NewReflected<TObject>(jConstructor, this.CreateArgumentsArray());
	/// <summary>
	/// Invokes a reflected constructor which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	protected TObject NewReflected<TObject>(JConstructorObject jConstructor, IObject?[] args)
		where TObject : JLocalObject, IClassType<TObject>
	{
		NativeValidationUtilities.ThrowIfAbstractClass(IClassType.GetMetadata<TObject>());
		IEnvironment env = jConstructor.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jConstructor, this, args);
	}
}