namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a non-typed class constructor definition.
/// </summary>
public record JNonTypedConstructorDefinition : JConstructorDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JNonTypedConstructorDefinition() : this(Array.Empty<JArgumentMetadata>()) { }
	
	/// <summary>
	/// Constructor.
	/// </summary>
	protected JNonTypedConstructorDefinition(params JArgumentMetadata[] metadata) : base(metadata) { }
	
	/// <summary>
	/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/> 
	/// which matches with current definition passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A new <see cref="JLocalObject"/>.</returns>
	public JLocalObject New(JClassObject jClass) => base.New<JLocalObject>(jClass, this.CreateArgumentsArray());
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor on <paramref name="jClass"/> 
	/// which matches with current definition passing the default value for each argument.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
	public TObject New<TObject>(JClassObject jClass) where TObject : JLocalObject, IDataType<TObject>
		=> base.New<TObject>(jClass, this.CreateArgumentsArray());

	/// <inheritdoc cref="JConstructorDefinition.New{TObject}(JClassObject, IObject?[])"/>
	protected new TObject New<TObject>(JClassObject jClass, IObject?[] args)
		where TObject : JLocalObject, IDataType<TObject>
		=> base.New<TObject>(jClass, args);
}