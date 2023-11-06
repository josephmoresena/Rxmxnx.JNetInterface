namespace Rxmxnx.JNetInterface.Lang;

public partial class JNumberObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata typeMetadata = JTypeMetadataBuilder<JNumberObject>
	                                                          .Create(UnicodeClassNames.JNumberObjectClassName,
	                                                                  JTypeModifier.Abstract)
	                                                          .WithSignature(
		                                                          UnicodeObjectSignatures.JNumberObjectSignature)
	                                                          .Implements<JSerializableObject>().Build();
	/// <summary>
	/// Function name of <c>java.lang.Number.byteValue().</c>
	/// </summary>
	private static readonly CString byteValueName = new(() => "byteValue"u8);
	/// <summary>
	/// Function name of <c>java.lang.Number.shortValue().</c>
	/// </summary>
	private static readonly CString shortValueName = new(() => "shortValue"u8);
	/// <summary>
	/// Function name of <c>java.lang.Number.intValue().</c>
	/// </summary>
	private static readonly CString intValueName = new(() => "intValue"u8);
	/// <summary>
	/// Function name of <c>java.lang.Number.longValue().</c>
	/// </summary>
	private static readonly CString longValueName = new(() => "longValue"u8);
	/// <summary>
	/// Function name of <c>java.lang.Number.floatValue().</c>
	/// </summary>
	private static readonly CString floatValueName = new(() => "floatValue"u8);
	/// <summary>
	/// Function name of <c>java.lang.Number.doubleValue().</c>
	/// </summary>
	private static readonly CString doubleValueName = new(() => "doubleValue"u8);

	static JDataTypeMetadata IDataType.Metadata => JNumberObject.typeMetadata;

	/// <summary>
	/// Retrieves the <see cref="JFunctionDefinition{TResult}"/> instance in order to retrieve
	/// numeric value.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType"/> numeric type.</typeparam>
	/// <returns>A <see cref="JFunctionDefinition{TResult}"/> instance.</returns>
	private static JFunctionDefinition<TPrimitive> GetValueDefinition<TPrimitive>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		CString functionName = metadata.NativeType switch
		{
			JNativeType.JByte => JNumberObject.byteValueName,
			JNativeType.JShort => JNumberObject.shortValueName,
			JNativeType.JInt => JNumberObject.intValueName,
			JNativeType.JLong => JNumberObject.longValueName,
			JNativeType.JFloat => JNumberObject.floatValueName,
			_ => JNumberObject.doubleValueName,
		};
		return new(functionName);
	}
}