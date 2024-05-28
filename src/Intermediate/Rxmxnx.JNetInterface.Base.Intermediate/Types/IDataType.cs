namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java data type.
/// </summary>
public interface IDataType
{
	/// <summary>
	/// Datatype kind.
	/// </summary>
	internal static abstract JTypeKind Kind { get; }
	/// <summary>
	/// Datatype family type.
	/// </summary>
	internal static abstract Type? FamilyType { get; }

	/// <summary>
	/// Retrieves the metadata for given type.
	/// </summary>
	/// <typeparam name="TDataType">Type of the current java datatype.</typeparam>
	/// <returns>The <see cref="JDataTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JDataTypeMetadata GetMetadata<TDataType>() where TDataType : IDataType<TDataType>
		=> TDataType.Metadata;
	/// <summary>
	/// Retrieves the hash for given type.
	/// </summary>
	/// <typeparam name="TDataType">Type of the current java datatype.</typeparam>
	/// <returns>The hash string for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static String GetHash<TDataType>() where TDataType : IDataType<TDataType>
		=> IDataType.GetMetadata<TDataType>().Hash;
}

/// <summary>
/// This interface exposes a java data type.
/// </summary>
/// <typeparam name="TDataType">Type of the current Java datatype.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2743,
                 Justification = CommonConstants.StaticAbstractPropertyUseJustification)]
public interface IDataType<out TDataType> : IDataType where TDataType : IDataType<TDataType>
{
	/// <inheritdoc cref="JDataTypeMetadata.ArgumentMetadata"/>
	internal static virtual JArgumentMetadata Argument { get; } = JArgumentMetadata.Create<TDataType>();

	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	internal static abstract JDataTypeMetadata Metadata { get; }
}