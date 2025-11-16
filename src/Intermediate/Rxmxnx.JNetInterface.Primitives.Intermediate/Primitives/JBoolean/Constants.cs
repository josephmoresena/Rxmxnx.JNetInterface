namespace Rxmxnx.JNetInterface.Primitives;

public partial struct JBoolean : INativeDataType<JBoolean>
{
	/// <summary>
	/// Unsigned byte value for <see langword="true"/> value.
	/// </summary>
	public const Byte TrueValue = 0x01;
	/// <summary>
	/// Unsigned byte value for <see langword="false"/> value.
	/// </summary>
	public const Byte FalseValue = 0x00;

	static explicit INativeDataType<JBoolean>.operator Byte(JBoolean value) => value._value;
	static implicit INativeDataType<JBoolean>.operator JBoolean(Byte value) => value == 0x01;
#if !NET8_0_OR_GREATER
	// For unknown reasons, in .NET 7.0 these static abstract members must be implemented explicitly.
	static JDataTypeMetadata IDataType<JBoolean>.Metadata => JBoolean.typeMetadata;
	static JTypeKind IDataType.Kind => JTypeKind.Primitive;
	static Type? IDataType.FamilyType => default;
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd1;
#endif

	static JBoolean IPrimitiveType<JBoolean>.CreateFrom<TSource>(TSource value) => value != default;
}