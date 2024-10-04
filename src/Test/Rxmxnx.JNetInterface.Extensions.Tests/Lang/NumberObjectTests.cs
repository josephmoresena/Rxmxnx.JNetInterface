namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class NumberObjectTests
{
	private static readonly Dictionary<Type, CString> classNames = new()
	{
		{ typeof(JByteObject), new("java/lang/Byte"u8) },
		{ typeof(JDoubleObject), new("java/lang/Double"u8) },
		{ typeof(JFloatObject), new("java/lang/Float"u8) },
		{ typeof(JIntegerObject), new("java/lang/Integer"u8) },
		{ typeof(JLongObject), new("java/lang/Long"u8) },
		{ typeof(JShortObject), new("java/lang/Short"u8) },
	};
	private static readonly Dictionary<Type, CString> classSignatures =
		NumberObjectTests.classNames.ToDictionary(p => p.Key, p => CString.Concat("L"u8, p.Value, ";"u8));
	private static readonly Dictionary<Type, CString> arraySignatures =
		NumberObjectTests.classSignatures.ToDictionary(p => p.Key, p => CString.Concat("["u8, p.Value));
	private static readonly Dictionary<Type, CStringSequence> hashes =
		NumberObjectTests.classSignatures.Keys.ToDictionary(
			t => t,
			t => new CStringSequence(NumberObjectTests.classNames[t], NumberObjectTests.classSignatures[t],
			                         NumberObjectTests.arraySignatures[t]));
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void ByteTest()
	{
		Func<JClassObject, JObjectLocalRef, JByte, JByteObject> creator = (e, c, v) => new(e, c, v);
		NumberObjectTests.CreateMetadataTest(creator);
		NumberObjectTests.MetadataTest<JByte, JByteObject>();
		NumberObjectTests.MetadataTest<JByte, JByteObject>(true);
		NumberObjectTests.ValueTest(creator);
		NumberObjectTests.ValueTest(creator, false);
		NumberObjectTests.ValueTest(creator, true);
		NumberObjectTests.ToObjectTest(creator, (e, v) => v.ToObject(e));
	}
	[Fact]
	internal void DoubleTest()
	{
		Func<JClassObject, JObjectLocalRef, JDouble, JDoubleObject> creator = (e, c, v) => new(e, c, v);
		NumberObjectTests.CreateMetadataTest(creator);
		NumberObjectTests.MetadataTest<JDouble, JDoubleObject>();
		NumberObjectTests.MetadataTest<JDouble, JDoubleObject>(true);
		NumberObjectTests.ValueTest(creator);
		NumberObjectTests.ValueTest(creator, false);
		NumberObjectTests.ValueTest(creator, true);
		NumberObjectTests.ToObjectTest(creator, (e, v) => v.ToObject(e));
	}
	[Fact]
	internal void FloatTest()
	{
		Func<JClassObject, JObjectLocalRef, JFloat, JFloatObject> creator = (e, c, v) => new(e, c, v);
		NumberObjectTests.CreateMetadataTest(creator);
		NumberObjectTests.MetadataTest<JFloat, JFloatObject>();
		NumberObjectTests.MetadataTest<JFloat, JFloatObject>(true);
		NumberObjectTests.ValueTest(creator);
		NumberObjectTests.ValueTest(creator, false);
		NumberObjectTests.ValueTest(creator, true);
		NumberObjectTests.ToObjectTest(creator, (e, v) => v.ToObject(e));
	}
	[Fact]
	internal void IntegerTest()
	{
		Func<JClassObject, JObjectLocalRef, JInt, JIntegerObject> creator = (e, c, v) => new(e, c, v);
		NumberObjectTests.CreateMetadataTest(creator);
		NumberObjectTests.MetadataTest<JInt, JIntegerObject>();
		NumberObjectTests.MetadataTest<JInt, JIntegerObject>(true);
		NumberObjectTests.ValueTest(creator);
		NumberObjectTests.ValueTest(creator, false);
		NumberObjectTests.ValueTest(creator, true);
		NumberObjectTests.ToObjectTest(creator, (e, v) => v.ToObject(e));
	}
	[Fact]
	internal void LongTest()
	{
		Func<JClassObject, JObjectLocalRef, JLong, JLongObject> creator = (e, c, v) => new(e, c, v);
		NumberObjectTests.CreateMetadataTest(creator);
		NumberObjectTests.MetadataTest<JLong, JLongObject>();
		NumberObjectTests.MetadataTest<JLong, JLongObject>(true);
		NumberObjectTests.ValueTest(creator);
		NumberObjectTests.ValueTest(creator, false);
		NumberObjectTests.ValueTest(creator, true);
		NumberObjectTests.ToObjectTest(creator, (e, v) => v.ToObject(e));
	}
	[Fact]
	internal void ShortTest()
	{
		Func<JClassObject, JObjectLocalRef, JShort, JShortObject> creator = (e, c, v) => new(e, c, v);
		NumberObjectTests.CreateMetadataTest(creator);
		NumberObjectTests.MetadataTest<JShort, JShortObject>();
		NumberObjectTests.MetadataTest<JShort, JShortObject>(true);
		NumberObjectTests.ValueTest(creator);
		NumberObjectTests.ValueTest(creator, false);
		NumberObjectTests.ValueTest(creator, true);
		NumberObjectTests.ToObjectTest(creator, (e, v) => v.ToObject(e));
	}

	private static void
		CreateMetadataTest<TPrimitive, TNumber>(Func<JClassObject, JObjectLocalRef, TPrimitive, TNumber> creator)
		where TNumber : JNumberObject<TPrimitive>, IPrimitiveWrapperType<TNumber>, IPrimitiveEquatable
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NumberObjectTests.fixture.Create<JObjectLocalRef>();
		TPrimitive value = (TPrimitive)NumberObjectTests.fixture.Create<Double>();
		using JClassObject jClass = new(env);
		using JClassObject jNumberClass = new(jClass, IClassType.GetMetadata<JNumberObject>());
		using JClassObject jPrimitiveWrapperClass =
			NumberObjectTests.GetPrimitiveWrapperClass<TPrimitive, TNumber>(
				env, jClass, out JPrimitiveWrapperTypeMetadata<TNumber> typeMetadata);
		using TNumber jNumberObject = creator(jPrimitiveWrapperClass, localRef, value);

		env.ClassFeature.GetClass<JNumberObject>().Returns(jNumberClass);
		Assert.Equal(value, jNumberObject.Value);

		PrimitiveWrapperObjectMetadata<TPrimitive> objectMetadata =
			Assert.IsType<PrimitiveWrapperObjectMetadata<TPrimitive>>(ILocalObject.CreateMetadata(jNumberObject));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(objectMetadata, new(ILocalObject.CreateMetadata(jNumberObject)));
		Assert.Equal(objectMetadata.Value.ToString(), jNumberObject.ToString());
		Assert.True(jNumberObject.Equals(objectMetadata.Value.GetValueOrDefault()));
		Assert.True(jNumberObject.Equals(objectMetadata.Value));
		Assert.True(jNumberObject.Equals((Object?)objectMetadata.Value));
		Assert.True(jNumberObject.Equals((Object?)objectMetadata.Value.Value));
		Assert.True(jNumberObject.Equals((Object?)(JObject?)objectMetadata.Value));

		env.ClassFeature.Received(0).GetClass<JNumberObject>();
		env.AccessFeature.Received(0).CallPrimitiveFunction(Arg.Any<IFixedMemory>(), Arg.Any<TNumber>(),
		                                                    Arg.Any<JClassObject>(),
		                                                    Arg.Any<JFunctionDefinition<TPrimitive>>(), false, []);

		JSerializableObject jSerializable = jNumberObject.CastTo<JSerializableObject>();
		JComparableObject jComparable = jNumberObject.CastTo<JComparableObject>();
		Assert.Equal(jNumberObject.Id, jSerializable.Id);
		Assert.Equal(jNumberObject.Id, jComparable.Id);
		Assert.Equal(jNumberObject, jSerializable.Object);
		Assert.Equal(jNumberObject, jComparable.Object);

		Assert.True(Object.ReferenceEquals(jNumberObject, jNumberObject.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jNumberObject, jNumberObject.CastTo<JNumberObject>()));
	}
	private static void MetadataTest<TPrimitive, TNumber>(Boolean disposeParse = false)
		where TNumber : JNumberObject<TPrimitive>, IPrimitiveWrapperType<TNumber>, IPrimitiveEquatable
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		JPrimitiveTypeMetadata primitiveTypeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JObjectLocalRef localRef = NumberObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = NumberObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jNumberClass = new(jClass, IClassType.GetMetadata<JNumberObject>());
		using JClassObject jPrimitiveWrapperClass =
			NumberObjectTests.GetPrimitiveWrapperClass<TPrimitive, TNumber>(
				env, jClass, out JPrimitiveWrapperTypeMetadata<TNumber> typeMetadata);
		using JLocalObject jLocal = new(env, localRef, jPrimitiveWrapperClass);
		using JGlobal jGlobal = new(vm, new(jPrimitiveWrapperClass), globalRef);
		String? textValue = typeMetadata.ToString();

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<TNumber>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(TNumber), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(IDataType.GetMetadata<JNumberObject>(), typeMetadata.BaseMetadata);
		Assert.Equal(primitiveTypeMetadata.WrapperClassName, typeMetadata.ClassName);
		Assert.Equal(primitiveTypeMetadata.WrapperClassSignature, typeMetadata.Signature);
		Assert.Equal(primitiveTypeMetadata, typeMetadata.PrimitiveMetadata);
		Assert.Equal(primitiveTypeMetadata.ArgumentMetadata, typeMetadata.PrimitiveArgumentMetadata);
		Assert.Equal(NumberObjectTests.classNames[typeof(TNumber)], typeMetadata.ClassName);
		Assert.Equal(NumberObjectTests.classSignatures[typeof(TNumber)], typeMetadata.Signature);
		Assert.Equal(NumberObjectTests.arraySignatures[typeof(TNumber)], typeMetadata.ArraySignature);
		Assert.Equal(NumberObjectTests.hashes[typeof(TNumber)].ToString(), typeMetadata.Hash);
		Assert.IsType<JFunctionDefinition<TNumber>.Parameterless>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<TNumber>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, [JArgumentMetadata.Get<JStringObject>(),]));
		Assert.IsType<JFieldDefinition<TNumber>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<TNumber>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<TNumber>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), typeMetadata.Interfaces);
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<TPrimitive>().Returns(jPrimitiveWrapperClass);
		env.ClassFeature.GetClass<JNumberObject>().Returns(jNumberClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jNumberClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jPrimitiveWrapperClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using TNumber jNumberObject0 =
			Assert.IsType<TNumber>(typeMetadata.CreateInstance(jPrimitiveWrapperClass, localRef, true));
		using TNumber jNumberObject1 = Assert.IsType<TNumber>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using TNumber jNumberObject2 = Assert.IsType<TNumber>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JCharacterObject>(Arg.Any<JReferenceObject>());

		Assert.True(typeMetadata.IsInstance(jNumberObject0));
		Assert.True(typeMetadata.IsInstance(jNumberObject0));
		Assert.True(typeMetadata.IsInstance(jNumberObject0));
	}
	private static void ValueTest<TPrimitive, TNumber>(Func<JClassObject, JObjectLocalRef, TPrimitive, TNumber> creator,
		Boolean? useMetadata = default)
		where TNumber : JNumberObject<TPrimitive>, IPrimitiveWrapperType<TNumber>, IPrimitiveEquatable
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NumberObjectTests.fixture.Create<JObjectLocalRef>();
		TPrimitive value = (TPrimitive)NumberObjectTests.fixture.Create<Double>();
		using JClassObject jClass = new(env);
		using JClassObject jNumberClass = new(jClass, IClassType.GetMetadata<JNumberObject>());
		using JClassObject jPrimitiveWrapperClass =
			NumberObjectTests.GetPrimitiveWrapperClass<TPrimitive, TNumber>(
				env, jClass, out JPrimitiveWrapperTypeMetadata<TNumber> typeMetadata);
		using TNumber jNumberObject = useMetadata.HasValue ?
			Assert.IsType<TNumber>(typeMetadata.CreateInstance(jPrimitiveWrapperClass, localRef, true)) :
			creator(jPrimitiveWrapperClass, localRef, value);

		env.ClassFeature.GetClass<JNumberObject>().Returns(jNumberClass);
		env.FunctionSet.GetPrimitiveValue<TPrimitive>(jNumberObject).Returns(value);
		if (typeof(TPrimitive) != typeof(JByte))
			env.FunctionSet.GetPrimitiveValue<JByte>(jNumberObject).Returns((JByte)value);
		if (typeof(TPrimitive) != typeof(JDouble))
			env.FunctionSet.GetPrimitiveValue<JDouble>(jNumberObject).Returns((JDouble)value);
		if (typeof(TPrimitive) != typeof(JFloat))
			env.FunctionSet.GetPrimitiveValue<JFloat>(jNumberObject).Returns((JFloat)value);
		if (typeof(TPrimitive) != typeof(JInt))
			env.FunctionSet.GetPrimitiveValue<JInt>(jNumberObject).Returns((JInt)value);
		if (typeof(TPrimitive) != typeof(JLong))
			env.FunctionSet.GetPrimitiveValue<JLong>(jNumberObject).Returns((JLong)value);
		if (typeof(TPrimitive) != typeof(JShort))
			env.FunctionSet.GetPrimitiveValue<JShort>(jNumberObject).Returns((JShort)value);

		if (useMetadata.GetValueOrDefault())
			ILocalObject.ProcessMetadata(jNumberObject,
			                             new PrimitiveWrapperObjectMetadata<TPrimitive>(new(jPrimitiveWrapperClass))
			                             {
				                             Value = value,
			                             });

		Assert.Equal(value.ToString(), jNumberObject.ToString());
		Assert.Equal(value.GetHashCode(), jNumberObject.GetHashCode());
		Assert.Equal(value, jNumberObject.Value);

		Int32 primitiveCount = useMetadata.HasValue && !useMetadata.Value ? 1 : 0;
		env.FunctionSet.Received(primitiveCount).GetPrimitiveValue<TPrimitive>(jNumberObject);

		Assert.Equal((JByte)value, jNumberObject.GetValue<JByte>());
		Assert.Equal((JDouble)value, jNumberObject.GetValue<JDouble>());
		Assert.Equal((JFloat)value, jNumberObject.GetValue<JFloat>());
		Assert.Equal((JInt)value, jNumberObject.GetValue<JInt>());
		Assert.Equal((JLong)value, jNumberObject.GetValue<JLong>());
		Assert.Equal((JShort)value, jNumberObject.GetValue<JShort>());

		env.FunctionSet.Received(typeof(TPrimitive) != typeof(JByte) ? 1 : primitiveCount)
		   .GetPrimitiveValue<JByte>(jNumberObject);
		env.FunctionSet.Received(typeof(TPrimitive) != typeof(JDouble) ? 1 : primitiveCount)
		   .GetPrimitiveValue<JDouble>(jNumberObject);
		env.FunctionSet.Received(typeof(TPrimitive) != typeof(JFloat) ? 1 : primitiveCount)
		   .GetPrimitiveValue<JFloat>(jNumberObject);
		env.FunctionSet.Received(typeof(TPrimitive) != typeof(JInt) ? 1 : primitiveCount)
		   .GetPrimitiveValue<JInt>(jNumberObject);
		env.FunctionSet.Received(typeof(TPrimitive) != typeof(JLong) ? 1 : primitiveCount)
		   .GetPrimitiveValue<JLong>(jNumberObject);
		env.FunctionSet.Received(typeof(TPrimitive) != typeof(JShort) ? 1 : primitiveCount)
		   .GetPrimitiveValue<JShort>(jNumberObject);

		NumberObjectTests.PrimitiveEqualityTest<JBoolean>(NumberObjectTests.fixture.Create<Boolean>(), value,
		                                                  jNumberObject);
		NumberObjectTests.PrimitiveEqualityTest<JChar>(NumberObjectTests.fixture.Create<Char>(), value, jNumberObject);
		NumberObjectTests.PrimitiveEqualityTest<JDouble>(NumberObjectTests.fixture.Create<Double>(), value,
		                                                 jNumberObject);
		NumberObjectTests.PrimitiveEqualityTest<JFloat>(NumberObjectTests.fixture.Create<Single>(), value,
		                                                jNumberObject);
		NumberObjectTests.PrimitiveEqualityTest<JInt>(NumberObjectTests.fixture.Create<Int32>(), value, jNumberObject);
		NumberObjectTests.PrimitiveEqualityTest<JLong>(NumberObjectTests.fixture.Create<Int64>(), value, jNumberObject);
		NumberObjectTests.PrimitiveEqualityTest<JShort>(NumberObjectTests.fixture.Create<Int16>(), value,
		                                                jNumberObject);
	}
	private static void ToObjectTest<TPrimitive, TNumber>(
		Func<JClassObject, JObjectLocalRef, TPrimitive, TNumber> creator0,
		Func<IEnvironment, TPrimitive, TNumber> creator1)
		where TNumber : JNumberObject<TPrimitive>, IPrimitiveWrapperType<TNumber>, IPrimitiveEquatable
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = NumberObjectTests.fixture.Create<JObjectLocalRef>();
		TPrimitive value = (TPrimitive)NumberObjectTests.fixture.Create<Double>();
		using JClassObject jClass = new(env);
		using JClassObject jNumberClass = new(jClass, IClassType.GetMetadata<JNumberObject>());
		using JClassObject jPrimitiveWrapperClass =
			NumberObjectTests.GetPrimitiveWrapperClass<TPrimitive, TNumber>(env, jClass, out _);
		using TNumber jNumberObject = creator0(jPrimitiveWrapperClass, localRef, value);

		env.ReferenceFeature.CreateWrapper(value).Returns(jNumberObject);

		Assert.Null(JNumberObject<JByte, JByteObject>.Create(env, default));
		Assert.Equal(jNumberObject, creator1(env, value));

		env.ReferenceFeature.Received(1).CreateWrapper(value);
	}

	private static JClassObject GetPrimitiveWrapperClass<TPrimitive, TNumber>(EnvironmentProxy env, JClassObject jClass,
		out JPrimitiveWrapperTypeMetadata<TNumber> typeMetadata)
		where TNumber : JNumberObject<TPrimitive>, IPrimitiveWrapperType<TNumber>
		where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive>, IPrimitiveType<TPrimitive>,
		IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		typeMetadata = Assert.IsType<JPrimitiveWrapperTypeMetadata<TNumber>>(IClassType.GetMetadata<TNumber>());
		JClassObject jPrimitiveWrapperClass = new(jClass, typeMetadata);
		env.ClassFeature.GetClass<TNumber>().Returns(jPrimitiveWrapperClass);
		switch (IDataType.GetMetadata<TPrimitive>().Signature[0])
		{
			case CommonNames.ByteSignatureChar:
				env.ClassFeature.ByteObject.Returns(_ => env.ClassFeature.GetClass<TNumber>());
				break;
			case CommonNames.DoubleSignatureChar:
				env.ClassFeature.DoubleObject.Returns(_ => env.ClassFeature.GetClass<TNumber>());
				break;
			case CommonNames.FloatSignatureChar:
				env.ClassFeature.FloatObject.Returns(_ => env.ClassFeature.GetClass<TNumber>());
				break;
			case CommonNames.IntSignatureChar:
				env.ClassFeature.IntegerObject.Returns(_ => env.ClassFeature.GetClass<TNumber>());
				break;
			case CommonNames.LongSignatureChar:
				env.ClassFeature.LongObject.Returns(_ => env.ClassFeature.GetClass<TNumber>());
				break;
			case CommonNames.ShortSignatureChar:
				env.ClassFeature.ShortObject.Returns(_ => env.ClassFeature.GetClass<TNumber>());
				break;
		}
		return jPrimitiveWrapperClass;
	}
	private static void PrimitiveEqualityTest<TPrimitive>(TPrimitive primitive, IPrimitiveType value,
		IPrimitiveEquatable equatable)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IPrimitiveEquatable, IEquatable<TPrimitive>
	{
		try
		{
			Boolean result = value.Equals(primitive);
			Assert.Equal(result, equatable.Equals(primitive));
			Assert.Equal(result, equatable.Equals((JPrimitiveObject)(JObject)primitive));
		}
		catch (Exception e)
		{
			Assert.Throws(e.GetType(), () => equatable.Equals(primitive));
			Assert.Throws(e.GetType(), () => equatable.Equals((JObject)primitive));
		}
	}
}