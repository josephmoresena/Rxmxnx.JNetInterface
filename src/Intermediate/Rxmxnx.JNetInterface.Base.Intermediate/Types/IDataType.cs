namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java data type.
/// </summary>
public interface IDataType
{
	/// <summary>
	/// Datatype kind.
	/// </summary>
	internal static virtual JTypeKind Kind => JTypeKind.Undefined;
	/// <summary>
	/// Datatype family type.
	/// </summary>
	internal static virtual Type? FamilyType => ValidationUtilities.ThrowInvalidInterface<Type>(nameof(IDataType));

	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected static virtual JDataTypeMetadata Metadata
		=> ValidationUtilities.ThrowInvalidInterface<JDataTypeMetadata>(nameof(IDataType));

	/// <summary>
	/// Retrieves the metadata for given type.
	/// </summary>
	/// <typeparam name="TDataType">Type of current java datatype.</typeparam>
	/// <returns>The <see cref="JDataTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JDataTypeMetadata GetMetadata<TDataType>() where TDataType : IDataType<TDataType>
		=> TDataType.Metadata;
}

/// <summary>
/// This interface exposes a java data type.
/// </summary>
/// <typeparam name="TDataType">Type of current Java datatype.</typeparam>
public interface IDataType<out TDataType> : IDataType where TDataType : IDataType<TDataType>
{
	/// <summary>
	/// Self CLR type.
	/// </summary>
	internal static virtual Type SelfType => typeof(IDataType<TDataType>);

	static JDataTypeMetadata IDataType.Metadata
		=> ValidationUtilities.ThrowInvalidInterface<JDataTypeMetadata>(nameof(IDataType));
}