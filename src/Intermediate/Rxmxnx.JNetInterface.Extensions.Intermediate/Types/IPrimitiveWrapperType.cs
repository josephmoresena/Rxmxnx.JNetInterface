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
	/// Array signature.
	/// </summary>
	internal static virtual CString ArraySignature
		=> ValidationUtilities.ThrowInvalidInterface<CString>(nameof(IPrimitiveWrapperType));

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
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
public interface IPrimitiveWrapperType<TWrapper> : IPrimitiveWrapperType, IClassType<TWrapper>,
	IInterfaceImplementation<TWrapper, JSerializableObject>, IInterfaceImplementation<TWrapper, JComparableObject>
	where TWrapper : JLocalObject, IClassType<TWrapper>, IInterfaceImplementation<TWrapper, JSerializableObject>, IInterfaceImplementation<TWrapper, JComparableObject>
{
}