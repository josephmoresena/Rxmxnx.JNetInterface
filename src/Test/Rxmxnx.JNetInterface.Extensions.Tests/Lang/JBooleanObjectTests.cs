namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public sealed class JBooleanObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new("java/lang/Boolean"u8);
	private static readonly CString classSignature = CString.Concat("L"u8, JBooleanObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JBooleanObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JBooleanObjectTests.className,
	                                                   JBooleanObjectTests.classSignature,
	                                                   JBooleanObjectTests.arraySignature);
	[Fact]
	internal void ConstructorDefinitionTest()
	{
		JAccessibleObjectDefinition definition = JConstructorDefinition.Create([JArgumentMetadata.Get<JBoolean>(),]);
		Assert.Equal(NativeFunctionSetImpl.BooleanConstructor, definition);
		Assert.Equal(NativeFunctionSetImpl.BooleanConstructor.Name, definition.Name);
		Assert.Equal(NativeFunctionSetImpl.BooleanConstructor.Descriptor, definition.Descriptor);
		Assert.Equal(NativeFunctionSetImpl.BooleanConstructor.Hash, definition.Hash);
		Assert.Equal(NativeFunctionSetImpl.BooleanConstructor.Name, "<init>"u8);
		Assert.Equal(NativeFunctionSetImpl.BooleanConstructor.Descriptor, "(Z)V"u8);
	}
	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JBooleanObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JBooleanObjectTests.fixture.Create<JObjectLocalRef>();
		JBoolean value = JBooleanObjectTests.fixture.Create<Boolean>();
		using JClassObject jClass = new(env);
		using JClassObject jBooleanObjectClass = new(jClass, typeMetadata);
		using JBooleanObject jBooleanObject = new(jBooleanObjectClass, localRef, value);

		env.ClassFeature.GetClass<JBooleanObject>().Returns(jBooleanObjectClass);
		env.ClassFeature.BooleanObject.Returns(_ => env.ClassFeature.GetClass<JBooleanObject>());
		env.AccessFeature.CallFunction<JBoolean>(jBooleanObject, jBooleanObjectClass,
		                                         NativeFunctionSetImpl.BooleanValueDefinition, false, [])
		   .Returns(value);

		Assert.Equal(value, jBooleanObject.Value);

		PrimitiveWrapperObjectMetadata<JBoolean> objectMetadata =
			Assert.IsType<PrimitiveWrapperObjectMetadata<JBoolean>>(ILocalObject.CreateMetadata(jBooleanObject));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(objectMetadata, new(ILocalObject.CreateMetadata(jBooleanObject)));
		Assert.Equal(objectMetadata.Value.ToString(), jBooleanObject.ToString());
		Assert.True(jBooleanObject.Equals(objectMetadata.Value.GetValueOrDefault()));
		Assert.True(jBooleanObject.Equals(objectMetadata.Value));
		Assert.True(jBooleanObject.Equals((Object?)objectMetadata.Value));
		Assert.True(jBooleanObject.Equals((Object?)objectMetadata.Value.Value));
		Assert.True(jBooleanObject.Equals((Object?)(JObject?)objectMetadata.Value));

		env.ClassFeature.Received(0).GetClass<JBooleanObject>();
		env.AccessFeature.Received(0).CallFunction<JBoolean>(jBooleanObject, jBooleanObjectClass,
		                                                     NativeFunctionSetImpl.BooleanValueDefinition, false, []);

		JSerializableObject jSerializable = jBooleanObject.CastTo<JSerializableObject>();
		Assert.Equal(jBooleanObject.Id, jSerializable.Id);
		Assert.Equal(jBooleanObject, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jBooleanObject, jBooleanObject.CastTo<JLocalObject>()));
	}
	[Theory]
	[InlineData]
	[InlineData(true)]
	internal void MetadataTest(Boolean disposeParse = false)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JBooleanObject>();
		String? textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JBooleanObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JBooleanObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JBooleanObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jBooleanObjectClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jBooleanObjectClass);
		using JGlobal jGlobal = new(vm, new(jBooleanObjectClass), globalRef);

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JBooleanObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JBooleanObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JBooleanObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JBooleanObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JBooleanObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JBooleanObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JBooleanObjectTests.hash.ToString(), IDataType.GetHash<JBooleanObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JBooleanObject>.Parameterless>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<JBooleanObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, [JArgumentMetadata.Get<JStringObject>(),]));
		Assert.IsType<JFieldDefinition<JBooleanObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JBooleanObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JBooleanObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JBooleanObject>().Returns(jBooleanObjectClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jBooleanObjectClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jBooleanObjectClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JBooleanObject jBooleanObject0 =
			Assert.IsType<JBooleanObject>(typeMetadata.CreateInstance(jBooleanObjectClass, localRef, true));
		using JBooleanObject jBooleanObject1 =
			Assert.IsType<JBooleanObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JBooleanObject jBooleanObject2 = Assert.IsType<JBooleanObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JBooleanObject>(Arg.Any<JReferenceObject>());

		Assert.True(typeMetadata.IsInstance(jBooleanObject0));
		Assert.True(typeMetadata.IsInstance(jBooleanObject1));
		Assert.True(typeMetadata.IsInstance(jBooleanObject2));
	}
	[Theory]
	[InlineData]
	[InlineData(true)]
	[InlineData(false)]
	internal void ValueTest(Boolean? useMetadata = default)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JBooleanObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JBooleanObjectTests.fixture.Create<JObjectLocalRef>();
		JBoolean value = JBooleanObjectTests.fixture.Create<Boolean>();
		using JClassObject jClass = new(env);
		using JClassObject jBooleanObjectClass = new(jClass, typeMetadata);
		using JBooleanObject jBooleanObject = useMetadata.HasValue ?
			Assert.IsType<JBooleanObject>(typeMetadata.CreateInstance(jBooleanObjectClass, localRef, true)) :
			new(jBooleanObjectClass, localRef, value);

		env.AccessFeature.CallFunction<JBoolean>(jBooleanObject, jBooleanObjectClass,
		                                         NativeFunctionSetImpl.BooleanValueDefinition, false, [])
		   .Returns(value);
		if (useMetadata.GetValueOrDefault())
			ILocalObject.ProcessMetadata(jBooleanObject,
			                             new PrimitiveWrapperObjectMetadata<JBoolean>(new(jBooleanObjectClass))
			                             {
				                             Value = value,
			                             });

		Assert.Equal(value.ToString(), jBooleanObject.ToString());
		Assert.Equal(value.GetHashCode(), jBooleanObject.GetHashCode());
		Assert.Equal(value, jBooleanObject.Value);

		env.AccessFeature.Received(useMetadata.HasValue && !useMetadata.Value ? 1 : 0)
		   .CallFunction<JBoolean>(jBooleanObject, jBooleanObjectClass, NativeFunctionSetImpl.BooleanValueDefinition,
		                           false, []);

		JBooleanObjectTests.PrimitiveEqualityTest<JBoolean>(JBooleanObjectTests.fixture.Create<Boolean>(), value,
		                                                    jBooleanObject);
		JBooleanObjectTests.PrimitiveEqualityTest<JChar>(JBooleanObjectTests.fixture.Create<Char>(), value,
		                                                 jBooleanObject);
		JBooleanObjectTests.PrimitiveEqualityTest<JDouble>(JBooleanObjectTests.fixture.Create<Double>(), value,
		                                                   jBooleanObject);
		JBooleanObjectTests.PrimitiveEqualityTest<JFloat>(JBooleanObjectTests.fixture.Create<Single>(), value,
		                                                  jBooleanObject);
		JBooleanObjectTests.PrimitiveEqualityTest<JInt>(JBooleanObjectTests.fixture.Create<Int32>(), value,
		                                                jBooleanObject);
		JBooleanObjectTests.PrimitiveEqualityTest<JLong>(JBooleanObjectTests.fixture.Create<Int64>(), value,
		                                                 jBooleanObject);
		JBooleanObjectTests.PrimitiveEqualityTest<JShort>(JBooleanObjectTests.fixture.Create<Int16>(), value,
		                                                  jBooleanObject);

		JBooleanObjectTests.IndeterminateValueTest(jBooleanObject);
		JBooleanObjectTests.IndeterminateValueTest(default);
	}
	[Fact]
	internal void ToObjectTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JBooleanObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JBooleanObjectTests.fixture.Create<JObjectLocalRef>();
		JBoolean value = JBooleanObjectTests.fixture.Create<Boolean>();
		using JClassObject jClass = new(env);
		using JClassObject jBooleanObjectClass = new(jClass, typeMetadata);
		using JBooleanObject jBooleanObject = new(jBooleanObjectClass, localRef, value);

		env.ReferenceFeature.CreateWrapper(value).Returns(jBooleanObject);

		Assert.Null(JBooleanObject.Create(env, default));
		Assert.Equal(jBooleanObject, value.ToObject(env));

		env.ReferenceFeature.Received(1).CreateWrapper(value);
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
	private static void IndeterminateValueTest(JBooleanObject? jBooleanObject)
	{
		IndeterminateResult result = new(jBooleanObject, IDataType.GetMetadata<JBooleanObject>().Signature);
		Assert.Equal(jBooleanObject, result.Object);
		Assert.Equal(jBooleanObject?.Value ?? default, result.BooleanValue);
		Assert.Equal(jBooleanObject?.Value.Value ?? false ? JByte.One : 0, result.ByteValue);
		Assert.Equal(jBooleanObject?.Value.Value ?? false ? (JChar)JShort.One : '\0', result.CharValue);
		Assert.Equal(jBooleanObject?.Value.Value ?? false ? JDouble.One : 0, result.DoubleValue);
		Assert.Equal(jBooleanObject?.Value.Value ?? false ? JFloat.One : 0, result.FloatValue);
		Assert.Equal(jBooleanObject?.Value.Value ?? false ? JInt.One : 0, result.IntValue);
		Assert.Equal(jBooleanObject?.Value.Value ?? false ? JLong.One : 0, result.LongValue);
		Assert.Equal(jBooleanObject?.Value.Value ?? false ? JShort.One : 0, result.ShortValue);
	}
}