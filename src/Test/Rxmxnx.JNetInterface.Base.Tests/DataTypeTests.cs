namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class DataTypeTests
{
	[Fact]
	internal void Test()
	{
		DataTypeTests.DefaultTest<DataTypeProxy>();
		DataTypeTests.GenericDefaultTest<GenericDataTypeProxy>();
		DataTypeTests.PrimitiveTest<PrimitiveProxy>();
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
}