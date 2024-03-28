namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java enum type instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IEnumType : IReferenceType
{
	[ExcludeFromCodeCoverage]
	static JTypeKind IDataType.Kind => JTypeKind.Enum;

	/// <summary>
	/// Retrieves the metadata for given enum type.
	/// </summary>
	/// <typeparam name="TEnum">Type of current java enum datatype.</typeparam>
	/// <returns>The <see cref="JEnumTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JEnumTypeMetadata GetMetadata<TEnum>() where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
		=> (JEnumTypeMetadata)IDataType.GetMetadata<TEnum>();
}

/// <summary>
/// This interface exposes an object that represents a java enum type instance.
/// </summary>
/// <typeparam name="TEnum">Type of java enum type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IEnumType<TEnum> : IEnumType, IReferenceType<TEnum> where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JEnumTypeMetadata<TEnum> Metadata { get; }

	static JDataTypeMetadata IDataType<TEnum>.Metadata => TEnum.Metadata;

	/// <summary>
	/// Creates a <typeparamref name="TEnum"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ClassInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TEnum"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TEnum Create(ClassInitializer initializer);
	/// <summary>
	/// Creates a <typeparamref name="TEnum"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ObjectInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TEnum"/> instance from <paramref name="initializer"/>.</returns>
	protected new static abstract TEnum Create(ObjectInitializer initializer);
	/// <summary>
	/// Creates a <typeparamref name="TEnum"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.GlobalInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TEnum"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TEnum Create(GlobalInitializer initializer);

	/// <summary>
	/// Creates a <typeparamref name="TEnum"/> instance from <paramref name="localRef"/> using
	/// <paramref name="env"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>
	/// A <typeparamref name="TEnum"/> instance from <paramref name="localRef"/> and <paramref name="env"/>.
	/// </returns>
	internal static TEnum Create(IEnvironment env, JObjectLocalRef localRef)
	{
		JClassObject enumClass = env.ClassFeature.GetClass<TEnum>();
		return TEnum.Create(new ClassInitializer { Class = enumClass, LocalReference = localRef, RealClass = true, });
	}
	/// <summary>
	/// Creates a <typeparamref name="TEnum"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <typeparamref name="TEnum"/> instance from <paramref name="jLocal"/>.</returns>
	internal static TEnum Create(JLocalObject jLocal) => TEnum.Create(jLocal);
	/// <summary>
	/// Creates a <typeparamref name="TEnum"/> instance from <paramref name="jGlobal"/> and
	/// <paramref name="env"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <typeparamref name="TEnum"/> instance from <paramref name="jGlobal"/>.</returns>
	internal static TEnum Create(IEnvironment env, JGlobalBase jGlobal)
		=> TEnum.Create(new GlobalInitializer { Global = jGlobal, Environment = env, });

	static TEnum IReferenceType<TEnum>.Create(ObjectInitializer initializer) => TEnum.Create(initializer);
}