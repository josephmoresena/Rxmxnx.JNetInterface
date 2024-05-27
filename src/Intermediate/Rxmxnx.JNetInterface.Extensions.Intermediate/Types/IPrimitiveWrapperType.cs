namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper class type instance.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2743,
                 Justification = CommonConstants.StaticAbstractPropertyUseJustification)]
public interface
	IPrimitiveWrapperType<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TWrapper> : IClassType<TWrapper>
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
}

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper class type instance.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
/// <typeparam name="TValue"><see cref="IPrimitiveType"/> type.</typeparam>
public interface
	IPrimitiveWrapperType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TWrapper,
		TValue> : IPrimitiveWrapperType<TWrapper>, IWrapper<TValue>
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