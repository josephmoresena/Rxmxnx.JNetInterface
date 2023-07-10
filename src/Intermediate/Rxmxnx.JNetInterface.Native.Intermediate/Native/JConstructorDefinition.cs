namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a constructor definition.
/// </summary>
public abstract record JConstructorDefinition : JCallDefinition
{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	internal JConstructorDefinition() : this(Array.Empty<JArgumentMetadata>()) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	internal JConstructorDefinition(params JArgumentMetadata[] metadata) 
		: base(JCallDefinition.ConstructorName, metadata) { }
	
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor on <paramref name="jClass"/> 
	/// which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">The <see cref="IDataType"/> type of created object.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/>.</returns>
	internal TObject New<TObject>(JClassObject jClass, IObject?[] args)
		where TObject : JLocalObject, IDataType<TObject>
	{
		IEnvironment env = jClass.Environment;
		return env.Accessor.CallConstructor<TObject>(jClass, this, args);
	}
}

/// <summary>
/// This class stores a constructor definition.
/// </summary>
/// <typeparam name="TObject"><see cref="IDataType"/> type of constructor.</typeparam>
public record JConstructorDefinition<TObject> : JConstructorDefinition
	where TObject : JLocalObject, IDataType<TObject>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JConstructorDefinition() : this(Array.Empty<JArgumentMetadata>()) { }
	
	/// <summary>
	/// Constructor.
	/// </summary>
	protected JConstructorDefinition(params JArgumentMetadata[] metadata) : base(metadata) { }
	
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition passing the default value for each argument.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	public TObject New(IEnvironment env) 
		=> base.New<TObject>(env.ClassProvider.GetClass<TObject>(), this.CreateArgumentsArray());

	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
	/// current definition.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/>.</returns>
	protected TObject New(IEnvironment env,IObject?[] args) 
		=> base.New<TObject>(env.ClassProvider.GetClass<TObject>(), args);
}