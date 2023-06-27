namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a java data type.
/// </summary>
public interface IDataType
{
	/// <summary>
	/// Java datatype class name.
	/// </summary>
	static virtual CString ClassName => UnicodeClassNames.JObjectClassName;
	/// <summary>
	/// Java datatype signature name.
	/// </summary>
	static virtual CString Signature => UnicodeObjectSignatures.JObjectSignature;

	/// <summary>
	/// Primitive metadata.
	/// </summary>
	internal static abstract JPrimitiveMetadata? PrimitiveMetadata { get; }
}

/// <summary>
/// This interface exposes a java data type.
/// </summary>
/// <typeparam name="TDataType">Type of current Java datatype.</typeparam>
public interface IDataType<out TDataType> : IDataType where TDataType : IDataType<TDataType>
{
	/// <summary>
	/// Creates a <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>A <typeparamref name="TDataType"/> instance from <paramref name="jObject"/>.</returns>
	static abstract TDataType? Create(JObject? jObject);
}