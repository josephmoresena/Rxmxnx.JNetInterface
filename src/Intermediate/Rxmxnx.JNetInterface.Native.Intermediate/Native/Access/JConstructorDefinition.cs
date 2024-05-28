namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a constructor definition.
/// </summary>
public partial class JConstructorDefinition : JCallDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JConstructorDefinition() : this([]) { }

	/// <summary>
	/// Retrieves a <see cref="JConstructorObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JConstructorObject"/> instance.</returns>
	public JConstructorObject GetReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedConstructor(this, declaringClass);
	}

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
		NativeValidationUtilities.ThrowIfAbstractClass(IClassType.GetMetadata<TObject>());
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
		=> definition.New<TObject>(jClass, args ?? definition.CreateArgumentsArray());

	/// <summary>
	/// Create a <see cref="JConstructorDefinition"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JConstructorDefinition"/> instance.</returns>
	internal static JConstructorDefinition Create(JArgumentMetadata[] metadata) => new(metadata);
}