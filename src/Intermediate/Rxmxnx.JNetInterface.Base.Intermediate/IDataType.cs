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
	/// Primitive metadata.
	/// </summary>
	internal static virtual JPrimitiveMetadata? PrimitiveMetadata
		=> ValidationUtilities.ThrowInvalidInterface<JPrimitiveMetadata?>(nameof(IDataType));
}

/// <summary>
/// This interface exposes a java data type.
/// </summary>
/// <typeparam name="TDataType">Type of current Java datatype.</typeparam>
public interface IDataType<out TDataType> : IDataType where TDataType : IDataType<TDataType>
{
	static CString IDataType.ClassName => UnicodeClassNames.JObjectClassName;
	static CString IDataType.Signature => UnicodeObjectSignatures.JObjectSignature;
	/// <summary>
	/// Creates a <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>A <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.</returns>
	static abstract TDataType? Create(JObject? jObject);
}