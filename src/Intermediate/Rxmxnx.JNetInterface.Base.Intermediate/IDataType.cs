namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a java data type.
/// </summary>
public interface IDataType
{
	/// <summary>
	/// Java datatype class name.
	/// </summary>
	static virtual CString ClassName => ValidationUtilities.ThrowInvalidInterface<CString>(nameof(IDataType));
	/// <summary>
	/// Java datatype signature name.
	/// </summary>
	static virtual CString Signature => ValidationUtilities.ThrowInvalidInterface<CString>(nameof(IDataType));
	
	/// <summary>
	/// Indicates whether the current type is final.
	/// </summary>
	internal static virtual Boolean Final 
		=> ValidationUtilities.ThrowInvalidInterface<Boolean>(nameof(IDataType));
	/// <summary>
	/// Primitive metadata.
	/// </summary>
	internal static virtual JPrimitiveMetadata? PrimitiveMetadata
		=> ValidationUtilities.ThrowInvalidInterface<JPrimitiveMetadata?>(nameof(IDataType));
	/// <summary>
	/// List of compatible types.
	/// </summary>
	internal static virtual IEnumerable<Type> CompatibleTypes 
		=> ValidationUtilities.ThrowInvalidInterface<IEnumerable<Type>>(nameof(IDataType));
}

/// <summary>
/// This interface exposes a java data type.
/// </summary>
/// <typeparam name="TDataType">Type of current Java datatype.</typeparam>
public interface IDataType<out TDataType> : IDataType where TDataType : IDataType<TDataType>
{
	/// <summary>
	/// Internal datatype information.
	/// </summary>
	[UnconditionalSuppressMessage("ReflectionAnalysis","IL2091")]
	private static readonly DataTypeInfo<TDataType> info = new();

	static CString IDataType.ClassName => JObject.JObjectClassName;
	static CString IDataType.Signature => JObject.JObjectSignature;
	static Boolean IDataType.Final => IDataType<TDataType>.info.IsFinal;

	/// <summary>
	/// Creates a <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>A <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.</returns>
	static abstract TDataType? Create(JObject? jObject);
}