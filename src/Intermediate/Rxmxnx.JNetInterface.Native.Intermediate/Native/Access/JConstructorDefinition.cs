namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a constructor definition.
/// </summary>
public record JConstructorDefinition : JCallDefinition
{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JConstructorDefinition() : this(Array.Empty<JArgumentMetadata>()) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	protected JConstructorDefinition(params JArgumentMetadata[] metadata) : base(
		UnicodeMethodNames.Constructor(), metadata) { }

	/// <summary>
	/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	public JLocalObject New(JClassObject jClass) => this.New<JLocalObject>(jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition passing the default value for each argument.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	public TObject New<TObject>(IEnvironment env) where TObject : JLocalObject, IClassType<TObject>
		=> this.New<TObject>(env.ClassFeature.GetClass<TObject>(), this.CreateArgumentsArray());

	/// <summary>
	/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
	protected JLocalObject New(JClassObject jClass, IObject?[] args) => this.New<JLocalObject>(jClass, args);
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	protected TObject New<TObject>(IEnvironment env, IObject?[] args) where TObject : JLocalObject, IClassType<TObject>
		=> this.New<TObject>(env.ClassFeature.GetClass<TObject>(), args);

	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">The <see cref="IDataType"/> type of created object.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/>.</returns>
	private TObject New<TObject>(JClassObject jClass, IObject?[] args) where TObject : JLocalObject, IClassType<TObject>
	{
		NativeValidationUtilities.ThrowIfAbstractClass<TObject>();
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jClass, this, args);
	}

	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with <paramref name="definition"/>.
	/// </summary>
	/// <typeparam name="TObject">The <see cref="IDataType"/> type of created object.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static TObject New<TObject>(JConstructorDefinition definition, JClassObject jClass,
		IObject?[]? args = default) where TObject : JLocalObject, IClassType<TObject>
	{
		NativeValidationUtilities.ThrowIfAbstractClass<TObject>();
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallInternalConstructor<TObject>(jClass, definition,
		                                                          args ?? definition.CreateArgumentsArray());
	}

	/// <summary>
	/// Create a <see cref="JConstructorDefinition"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JConstructorDefinition"/> instance.</returns>
	internal static JConstructorDefinition Create(JArgumentMetadata[] metadata) => new(metadata);
}