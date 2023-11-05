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
	/// Array JNI signature.
	/// </summary>
	internal static abstract CString ArraySignature { get; }

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
/// <typeparam name="TWrapperClass">Type of java primitive wrapper class datatype.</typeparam>
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
public interface IPrimitiveWrapperType<out TWrapperClass> : IPrimitiveWrapperType, IClassType<TWrapperClass>
	where TWrapperClass : JReferenceObject, IClassType<TWrapperClass>
{
	/// <inheritdoc cref="IDataType{TClass}.ExcludingGenericTypes"/>
	private static readonly ImmutableHashSet<Type> excludingTypes = ImmutableHashSet.Create(
		typeof(IDataType<TWrapperClass>), typeof(IReferenceType<TWrapperClass>),
		typeof(IPrimitiveWrapperType<TWrapperClass>));

	static IImmutableSet<Type> IDataType<TWrapperClass>.ExcludingGenericTypes
		=> IPrimitiveWrapperType<TWrapperClass>.excludingTypes;
}