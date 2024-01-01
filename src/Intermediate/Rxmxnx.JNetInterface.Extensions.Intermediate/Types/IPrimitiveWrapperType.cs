namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper class type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IPrimitiveWrapperType : IClassType
{
	/// <summary>
	/// Primitive metadata.
	/// </summary>
	internal static abstract JPrimitiveTypeMetadata PrimitiveMetadata { get; }

	/// <summary>
	/// Retrieves the metadata for given class type.
	/// </summary>
	/// <typeparam name="TWrapperClass">Type of current java primitive wrapper class datatype.</typeparam>
	/// <returns>The <see cref="JClassTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JPrimitiveWrapperTypeMetadata GetMetadata<TWrapperClass>()
		where TWrapperClass : JReferenceObject, IClassType<TWrapperClass>
		=> (JPrimitiveWrapperTypeMetadata)IDataType.GetMetadata<TWrapperClass>();
}

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper class type instance.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
public interface IPrimitiveWrapperType<TWrapper> : IPrimitiveWrapperType, IClassType<TWrapper>,
	IInterfaceImplementation<TWrapper, JSerializableObject>, IInterfaceImplementation<TWrapper, JComparableObject>
	where TWrapper : JLocalObject, IClassType<TWrapper>, IInterfaceImplementation<TWrapper, JSerializableObject>,
	IInterfaceImplementation<TWrapper, JComparableObject> { }

/// <summary>
/// This interface exposes an object that represents a java primitive wrapper class type instance.
/// </summary>
/// <typeparam name="TWrapper">Type of java primitive wrapper class datatype.</typeparam>
/// <typeparam name="TValue"><see cref="IPrimitiveType"/> type.</typeparam>
public interface IPrimitiveWrapperType<TWrapper, TValue> : IPrimitiveWrapperType<TWrapper>, IWrapper<TValue>
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper> where TValue : unmanaged, IPrimitiveType<TValue>
{
	/// <summary>
	/// Creates a <typeparamref name="TWrapper"/> instance initialized with <typeparamref name="TValue"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><typeparamref name="TValue"/> value.</param>
	/// <returns>A new <typeparamref name="TWrapper"/> instance.</returns>
	[return: NotNullIfNotNull(nameof(value))]
	static abstract TWrapper? Create(IEnvironment env, TValue? value);
}