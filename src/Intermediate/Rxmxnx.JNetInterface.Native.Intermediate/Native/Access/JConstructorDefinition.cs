namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a constructor definition.
/// </summary>
public partial class JConstructorDefinition : JCallDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	// ReSharper disable once MemberCanBePrivate.Global
	protected JConstructorDefinition(
#if NET9_0_OR_GREATER
		params ReadOnlySpan<JArgumentMetadata> metadata
#else
		ReadOnlySpan<JArgumentMetadata> metadata
#endif
	) : base(CommonNames.Constructor, metadata) { }

	/// <inheritdoc/>
	private protected JConstructorDefinition(AccessibleInfoSequence info, Int32 callSize, Int32[] sizes,
		Int32 referenceCount) : base(info, callSize, sizes, referenceCount) { }

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
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	private TObject New<TObject>(JClassObject jClass, ReadOnlySpan<IObject?> args)
		where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jClass.Environment;
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TObject>();
		if (typeMetadata.ClassName.AsSpan().SequenceEqual(jClass.Name.AsSpan()))
			NativeValidationUtilities.ThrowIfAbstractClass(typeMetadata);
		return env.AccessFeature.CallConstructor<TObject>(jClass, this, args);
	}
	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with current definition.
	/// </summary>
	/// <typeparam name="TObject">The <see cref="IDataType"/> type of the created object.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	/// <returns>A new <typeparamref name="TObject"/>.</returns>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	private TObject New<TObject, TArgs>(JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
		where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jClass.Environment;
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TObject>();
		if (typeMetadata.ClassName.AsSpan().SequenceEqual(jClass.Name.AsSpan()))
			NativeValidationUtilities.ThrowIfAbstractClass(typeMetadata);
		return env.AccessFeature.CallConstructor<TObject, TArgs>(jClass, this, in args);
	}

	/// <summary>
	/// Creates a new <typeparamref name="TObject"/> instance using a constructor on <paramref name="jClass"/>
	/// which matches with <paramref name="definition"/>.
	/// </summary>
	/// <typeparam name="TObject">The <see cref="IDataType"/> type of created object.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	internal static TObject New<TObject>(JConstructorDefinition definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args = default) where TObject : JLocalObject, IClassType<TObject>
		=> definition.New<TObject>(jClass, args);

	/// <summary>
	/// Create a <see cref="JConstructorDefinition"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JConstructorDefinition"/> instance.</returns>
	internal static JConstructorDefinition Create(ReadOnlySpan<JArgumentMetadata> metadata)
		=> metadata.Length > 0 ? new JConstructorDefinition(metadata) : new Parameterless();
}