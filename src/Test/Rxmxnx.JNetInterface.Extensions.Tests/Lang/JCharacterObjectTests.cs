namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public sealed class JCharacterObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.CharacterObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JCharacterObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JCharacterObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JCharacterObjectTests.className,
	                                                   JCharacterObjectTests.classSignature,
	                                                   JCharacterObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JCharacterObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JCharacterObjectTests.fixture.Create<JObjectLocalRef>();
		JChar value = JCharacterObjectTests.fixture.Create<Char>();
		using JClassObject jClass = new(env);
		using JClassObject jCharacterObjectClass = new(jClass, typeMetadata);
		using JCharacterObject jCharacterObject = new(jCharacterObjectClass, localRef, value);

		env.ClassFeature.GetClass<JCharacterObject>().Returns(jCharacterObjectClass);
		env.ClassFeature.CharacterObject.Returns(_ => env.ClassFeature.GetClass<JCharacterObject>());
		env.AccessFeature.CallFunction<JChar>(jCharacterObject, jCharacterObjectClass,
		                                      NativeFunctionSetImpl.CharValueDefinition, false, []).Returns(value);

		Assert.Equal(value, jCharacterObject.Value);

		PrimitiveWrapperObjectMetadata<JChar> objectMetadata =
			Assert.IsType<PrimitiveWrapperObjectMetadata<JChar>>(ILocalObject.CreateMetadata(jCharacterObject));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(objectMetadata, new(ILocalObject.CreateMetadata(jCharacterObject)));
		Assert.Equal(objectMetadata.Value.ToString(), jCharacterObject.ToString());
		Assert.True(jCharacterObject.Equals(objectMetadata.Value.GetValueOrDefault()));
		Assert.True(jCharacterObject.Equals(objectMetadata.Value));
		Assert.True(jCharacterObject.Equals((Object?)objectMetadata.Value));
		Assert.True(jCharacterObject.Equals((Object?)objectMetadata.Value.Value));
		Assert.True(jCharacterObject.Equals((Object?)(JObject?)objectMetadata.Value));

		env.ClassFeature.Received(0).GetClass<JCharacterObject>();
		env.AccessFeature.Received(0).CallFunction<JChar>(jCharacterObject, jCharacterObjectClass,
		                                                  NativeFunctionSetImpl.CharValueDefinition, false, []);

		JSerializableObject jSerializable = jCharacterObject.CastTo<JSerializableObject>();
		Assert.Equal(jCharacterObject.Id, jSerializable.Id);
		Assert.Equal(jCharacterObject, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jCharacterObject, jCharacterObject.CastTo<JLocalObject>()));
	}
	[Theory]
	[InlineData]
	[InlineData(true)]
	internal void MetadataTest(Boolean disposeParse = false)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JCharacterObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JCharacterObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JCharacterObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JCharacterObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jCharacterObjectClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jCharacterObjectClass);
		using JGlobal jGlobal = new(vm, new(jCharacterObjectClass, IClassType.GetMetadata<JCharacterObject>()),
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JCharacterObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JCharacterObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JCharacterObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JCharacterObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JCharacterObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JCharacterObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JCharacterObjectTests.hash.ToString(), IDataType.GetHash<JCharacterObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JCharacterObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JCharacterObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JCharacterObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JCharacterObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JCharacterObject>().Returns(jCharacterObjectClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jCharacterObjectClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jCharacterObjectClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JCharacterObject jCharacterObject0 =
			Assert.IsType<JCharacterObject>(typeMetadata.CreateInstance(jCharacterObjectClass, localRef, true));
		using JCharacterObject jCharacterObject1 =
			Assert.IsType<JCharacterObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JCharacterObject jCharacterObject2 =
			Assert.IsType<JCharacterObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JCharacterObject>(Arg.Any<JReferenceObject>());

		Assert.True(typeMetadata.IsInstance(jCharacterObject0));
		Assert.True(typeMetadata.IsInstance(jCharacterObject1));
		Assert.True(typeMetadata.IsInstance(jCharacterObject2));
	}
	[Theory]
	[InlineData]
	[InlineData(true)]
	[InlineData(false)]
	internal void ValueTest(Boolean? useMetadata = default)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JCharacterObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JCharacterObjectTests.fixture.Create<JObjectLocalRef>();
		JChar value = JCharacterObjectTests.fixture.Create<Char>();
		using JClassObject jClass = new(env);
		using JClassObject jCharacterObjectClass = new(jClass, typeMetadata);
		using JCharacterObject jCharacterObject = useMetadata.HasValue ?
			Assert.IsType<JCharacterObject>(typeMetadata.CreateInstance(jCharacterObjectClass, localRef, true)) :
			new(jCharacterObjectClass, localRef, value);

		env.AccessFeature.CallFunction<JChar>(jCharacterObject, jCharacterObjectClass,
		                                      NativeFunctionSetImpl.CharValueDefinition, false, []).Returns(value);
		if (useMetadata.GetValueOrDefault())
			ILocalObject.ProcessMetadata(jCharacterObject,
			                             new PrimitiveWrapperObjectMetadata<JChar>(new(jCharacterObjectClass))
			                             {
				                             Value = value,
			                             });

		Assert.Equal(value.ToString(), jCharacterObject.ToString());
		Assert.Equal(value.GetHashCode(), jCharacterObject.GetHashCode());
		Assert.Equal(value, jCharacterObject.Value);

		env.AccessFeature.Received(useMetadata.HasValue && !useMetadata.Value ? 1 : 0).CallFunction<JChar>(
			jCharacterObject, jCharacterObjectClass, NativeFunctionSetImpl.CharValueDefinition, false, []);

		JCharacterObjectTests.PrimitiveEqualityTest<JBoolean>(JCharacterObjectTests.fixture.Create<Boolean>(), value,
		                                                      jCharacterObject);
		JCharacterObjectTests.PrimitiveEqualityTest<JChar>(JCharacterObjectTests.fixture.Create<Char>(), value,
		                                                   jCharacterObject);
		JCharacterObjectTests.PrimitiveEqualityTest<JDouble>(JCharacterObjectTests.fixture.Create<Double>(), value,
		                                                     jCharacterObject);
		JCharacterObjectTests.PrimitiveEqualityTest<JFloat>(JCharacterObjectTests.fixture.Create<Single>(), value,
		                                                    jCharacterObject);
		JCharacterObjectTests.PrimitiveEqualityTest<JInt>(JCharacterObjectTests.fixture.Create<Int32>(), value,
		                                                  jCharacterObject);
		JCharacterObjectTests.PrimitiveEqualityTest<JLong>(JCharacterObjectTests.fixture.Create<Int64>(), value,
		                                                   jCharacterObject);
		JCharacterObjectTests.PrimitiveEqualityTest<JShort>(JCharacterObjectTests.fixture.Create<Int16>(), value,
		                                                    jCharacterObject);
	}
	[Fact]
	internal void ToObjectTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JCharacterObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JCharacterObjectTests.fixture.Create<JObjectLocalRef>();
		JChar value = JCharacterObjectTests.fixture.Create<Char>();
		using JClassObject jClass = new(env);
		using JClassObject jCharacterObjectClass = new(jClass, typeMetadata);
		using JCharacterObject jCharacterObject = new(jCharacterObjectClass, localRef, value);

		env.ReferenceFeature.CreateWrapper(value).Returns(jCharacterObject);

		Assert.Null(JCharacterObject.Create(env, default));
		Assert.Equal(jCharacterObject, value.ToObject(env));

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
}