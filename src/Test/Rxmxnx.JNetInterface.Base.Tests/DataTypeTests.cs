using System.ComponentModel;

namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class DataTypeTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void Test()
	{
		DataTypeTests.DefaultTest<DataTypeProxy>();
		DataTypeTests.GenericDefaultTest<GenericDataTypeProxy>();
		DataTypeTests.PrimitiveTest<PrimitiveProxy>();
	}
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

		NativeProxy.Type = default;
		Assert.Throws<InvalidEnumArgumentException>(DataTypeTests.NativeTypeTest<NativeProxy>);
		NativeProxy.Type = JNativeType.JBoolean;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JByte;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JChar;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JDouble;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JFloat;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JInt;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JLong;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JShort;
		DataTypeTests.NativeTypeTest<NativeProxy>();
		NativeProxy.Type = JNativeType.JVirtualMachineInitArgument;
		DataTypeTests.NativeTypeTest<NativeProxy>();
	}

	private static void DefaultTest<TDataType>() where TDataType : IDataType
	{
		Assert.Equal(JTypeKind.Undefined, TDataType.Kind);
		Assert.Equal(default, TDataType.FamilyType);
		Assert.Equal(default, TDataType.FamilyType);
	}
	private static void GenericDefaultTest<TDataType>() where TDataType : IDataType<TDataType>
	{
		DataTypeTests.DefaultTest<TDataType>();
		List<Exception> exceptions =
		[
			Assert.Throws<NotImplementedException>(IDataType.GetMetadata<TDataType>),
			Assert.Throws<NotImplementedException>(IDataType.GetHash<TDataType>),
			Assert.Throws<NotImplementedException>(() => TDataType.Metadata),
		];
		Assert.Throws<TypeInitializationException>(() => TDataType.Argument);
		exceptions.ForEach(
			e => Assert.True(e.Message == $"The {nameof(IDataType)} interface can't be implemented by itself."));
	}
	private static void PrimitiveTest<TPrimitive>() where TPrimitive : IPrimitiveType
	{
		DataTypeTests.DefaultTest<TPrimitive>();
		NotImplementedException ex = Assert.Throws<NotImplementedException>(() => TPrimitive.JniType);
		Assert.True(ex.Message ==
		            $"The {nameof(IPrimitiveType)} interface can't be implemented by itself. Please use primitive types such as JBoolean, JByte, JChar, JDouble, JFloat, JInt, JLong, JShort.");
	}
	private static void NativeTypeTest<TNative>() where TNative : unmanaged, INativeType<TNative>
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

	private abstract record DataTypeProxy : IDataType;
	private abstract record GenericDataTypeProxy : DataTypeProxy, IDataType<GenericDataTypeProxy>;

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

	private readonly struct NativeProxy : INativeType<NativeProxy>
	{
		public static JNativeType Type;
		static JNativeType INativeType.Type => NativeProxy.Type;
		public override String ToString() => INativeType.ToString(this);
	}
}