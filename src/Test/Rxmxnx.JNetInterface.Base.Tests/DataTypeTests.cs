namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class DataTypeTests
{
	private static readonly IFixture fixture = new Fixture();
	private static readonly IReadOnlySet<JNativeType> nativeTypes =
		(Enum.GetValuesAsUnderlyingType<JNativeType>() as JNativeType[])!.ToHashSet();

	[Fact]
	internal void Test() { DataTypeTests.PrimitiveTest<PrimitiveProxy>(); }
	[Fact]
	internal void NativeTest()
	{
		DataTypeTests.NativeTypeTest<JFieldId>();
		DataTypeTests.NativeTypeTest<JMethodId>();
		DataTypeTests.NativeTypeTest<JArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JBooleanArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JByteArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JCharArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JClassLocalRef>();
		DataTypeTests.NativeTypeTest<JDoubleArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JEnvironmentRef>();
		DataTypeTests.NativeTypeTest<JFloatArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JGlobalRef>();
		DataTypeTests.NativeTypeTest<JIntArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JLongArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JObjectArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JObjectLocalRef>();
		DataTypeTests.NativeTypeTest<JShortArrayLocalRef>();
		DataTypeTests.NativeTypeTest<JStringLocalRef>();
		DataTypeTests.NativeTypeTest<JThrowableLocalRef>();
		DataTypeTests.NativeTypeTest<JVirtualMachineRef>();
		DataTypeTests.NativeTypeTest<JWeakRef>();

		DataTypeTests.NativeTypeTest<JEnvironmentValue>();
		DataTypeTests.NativeTypeTest<JInvokeInterface>();
		DataTypeTests.NativeTypeTest<JNativeInterface>();
		DataTypeTests.NativeTypeTest<JValue>();
		DataTypeTests.NativeTypeTest<JVirtualMachineValue>();

		NativeProxy.Type = (JNativeType)Enumerable.Range(Byte.MinValue, Byte.MaxValue)
		                                          .First(x => !DataTypeTests.nativeTypes.Contains((JNativeType)x));
		Assert.Throws<InvalidEnumArgumentException>(DataTypeTests.NativeTypeTest<NativeProxy>);
	}
	[Fact]
	internal void VoidMetadataTest()
	{
		JPrimitiveTypeMetadata voidMetadata = JPrimitiveTypeMetadata.VoidMetadata;
		CStringSequence wrapperInformation = JDataTypeMetadata.CreateInformationSequence(voidMetadata.WrapperClassName);
		Assert.Equal(typeof(void), voidMetadata.Type);
		Assert.Equal(typeof(void), voidMetadata.UnderlineType);
		Assert.Equal(default, voidMetadata.NativeType);
		Assert.True(ReadOnlySpan<Byte>.Empty.SequenceEqual(voidMetadata.ArraySignature));
		Assert.Equal(0, voidMetadata.SizeOf);
		Assert.Equal(CommonNames.VoidSignatureChar, voidMetadata.Signature[0]);
		Assert.Equal(JTypeModifier.Final, voidMetadata.Modifier);
		Assert.True(wrapperInformation[0].SequenceEqual(voidMetadata.WrapperClassName));
		Assert.True(wrapperInformation[1].SequenceEqual(voidMetadata.WrapperClassSignature));
		Assert.Equal(wrapperInformation, voidMetadata.WrapperInformation);

		Assert.Throws<InvalidOperationException>(() => voidMetadata.ArgumentMetadata);
		Assert.Throws<InvalidOperationException>(() => voidMetadata.CreateInstance(Array.Empty<Byte>()));
		String dataTypeString = $"{{ {nameof(JDataTypeMetadata.ClassName)} = {voidMetadata.ClassName}, " +
			$"{nameof(JDataTypeMetadata.Type)} = {voidMetadata.Type}, " +
			$"{nameof(JDataTypeMetadata.Kind)} = {voidMetadata.Kind}, " +
			$"{nameof(JPrimitiveTypeMetadata.UnderlineType)} = {voidMetadata.UnderlineType}, " +
			$"{nameof(JPrimitiveTypeMetadata.WrapperClassName)} = {voidMetadata.WrapperClassName}, " +
			$"{nameof(JDataTypeMetadata.Hash)} = {ITypeInformation.GetPrintableHash(voidMetadata.Hash, out String lastChar)}{lastChar} }}";
		Assert.Equal(dataTypeString, voidMetadata.ToString());
	}

	private static void PrimitiveTest<TPrimitive>() where TPrimitive : IPrimitiveType
	{
		Assert.Equal(JNativeType.JObject, TPrimitive.JniType);
	}
	private static void NativeTypeTest<TNative>() where TNative : unmanaged, INativeType
	{
		Byte[] bytes = DataTypeTests.fixture.CreateMany<Byte>(NativeUtilities.SizeOf<TNative>()).ToArray();
		ref TNative value = ref bytes.AsSpan().AsValue<TNative>();
		String prefix = $"{TNative.Type.GetTypeName()}: ";
		String suffix = value switch
		{
			IFixedPointer ptr => $"0x{ptr.Pointer:x8}",
			JValue or JNativeInterface or JInvokeInterface => Convert.ToHexString(NativeUtilities.AsBytes(value)),
			_ => value.GetType().ToString(),
		};
		Assert.Equal(prefix + suffix, value.ToString());
	}

	private abstract record PrimitiveProxy : ObjectProxy, IPrimitiveType
	{
		public abstract Int32 CompareTo(Object? obj);
		public abstract TypeCode GetTypeCode();
		public abstract Boolean ToBoolean(IFormatProvider? provider);
		public abstract Byte ToByte(IFormatProvider? provider);
		public abstract Char ToChar(IFormatProvider? provider);
		public abstract DateTime ToDateTime(IFormatProvider? provider);
		public abstract Decimal ToDecimal(IFormatProvider? provider);
		public abstract Double ToDouble(IFormatProvider? provider);
		public abstract Int16 ToInt16(IFormatProvider? provider);
		public abstract Int32 ToInt32(IFormatProvider? provider);
		public abstract Int64 ToInt64(IFormatProvider? provider);
		public abstract SByte ToSByte(IFormatProvider? provider);
		public abstract Single ToSingle(IFormatProvider? provider);
		public abstract String ToString(IFormatProvider? provider);
		public abstract Object ToType(Type conversionType, IFormatProvider? provider);
		public abstract UInt16 ToUInt16(IFormatProvider? provider);
		public abstract UInt32 ToUInt32(IFormatProvider? provider);
		public abstract UInt64 ToUInt64(IFormatProvider? provider);
	}

	private readonly struct NativeProxy : INativeType
	{
		public static JNativeType Type;
		static JNativeType INativeType.Type => NativeProxy.Type;
	}
}