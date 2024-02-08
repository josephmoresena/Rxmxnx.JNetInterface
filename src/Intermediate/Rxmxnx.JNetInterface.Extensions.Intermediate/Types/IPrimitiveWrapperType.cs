namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper class type instance.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
public interface IPrimitiveWrapperType<TWrapper> : IClassType<TWrapper>
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JPrimitiveWrapperTypeMetadata<TWrapper> Metadata { get; }

	/// <summary>
	/// Primitive metadata.
	/// </summary>
	[ReadOnly(true)]
	internal static abstract JPrimitiveTypeMetadata PrimitiveMetadata { get; }

	static JClassTypeMetadata<TWrapper> IClassType<TWrapper>.Metadata => TWrapper.Metadata;

	/// <summary>
	/// Creates a <typeparamref name="TWrapper"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <typeparamref name="TWrapper"/> instance from <paramref name="jLocal"/>.</returns>
	internal static virtual TWrapper Create(JLocalObject jLocal)
		=> TWrapper.Create(new ObjectInitializer(jLocal)
		{
			Class = jLocal.Environment.ClassFeature.GetClass<TWrapper>(),
		});
	/// <summary>
	/// Creates a <typeparamref name="TWrapper"/> instance from <paramref name="localRef"/> using
	/// <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <typeparamref name="TWrapper"/> instance from <paramref name="localRef"/> and <paramref name="jClass"/>.</returns>
	internal static virtual TWrapper Create(JClassObject jClass, JObjectLocalRef localRef)
		=> TWrapper.Create(new ClassInitializer { Class = jClass, LocalReference = localRef, RealClass = true, });
	/// <summary>
	/// Creates a <typeparamref name="TWrapper"/> instance from <paramref name="jGlobal"/> using
	/// <paramref name="env"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <typeparamref name="TWrapper"/> instance from <paramref name="jGlobal"/> and <paramref name="env"/>.</returns>
	internal static virtual TWrapper Create(IEnvironment env, JGlobalBase jGlobal)
		=> TWrapper.Create(new GlobalInitializer { Global = jGlobal, Environment = env, });
}

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper class type instance.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
/// <typeparam name="TValue"><see cref="IPrimitiveType"/> type.</typeparam>
public interface IPrimitiveWrapperType<TWrapper, TValue> : IPrimitiveWrapperType<TWrapper>, IWrapper<TValue>,
	IInterfaceObject<JSerializableObject>, IInterfaceObject<JComparableObject>
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper> where TValue : unmanaged, IPrimitiveType<TValue>
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType<TWrapper>.PrimitiveMetadata
		=> IPrimitiveType.GetMetadata<TValue>();

	/// <summary>
	/// Creates a <typeparamref name="TWrapper"/> instance initialized with <typeparamref name="TValue"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><typeparamref name="TValue"/> value.</param>
	/// <returns>A new <typeparamref name="TWrapper"/> instance.</returns>
	[return: NotNullIfNotNull(nameof(value))]
	static abstract TWrapper? Create(IEnvironment env, TValue? value);
}