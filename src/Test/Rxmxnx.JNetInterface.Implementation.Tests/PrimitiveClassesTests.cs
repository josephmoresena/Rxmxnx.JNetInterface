namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class PrimitiveClassesTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	private static Dictionary<JClassLocalRef, JStringLocalRef> GetPrimitiveClassNameRefs(NativeInterfaceProxy proxyEnv)
		=> new()
		{
			{ proxyEnv.VoidPrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.BooleanPrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.BytePrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.CharPrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.DoublePrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.FloatPrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.IntPrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.LongPrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
			{ proxyEnv.ShortPrimitiveLocalRef, PrimitiveClassesTests.fixture.Create<JStringLocalRef>() },
		};
	private static void WrapperInstanceAssert(NativeInterfaceProxy proxyEnv, IEnvironment env)
	{
		JBoolean booleanValue = PrimitiveClassesTests.fixture.Create<Boolean>();
		JByte byteValue = PrimitiveClassesTests.fixture.Create<SByte>();
		JChar charValue = PrimitiveClassesTests.fixture.Create<Char>();
		JDouble doubleValue = PrimitiveClassesTests.fixture.Create<Double>();
		JFloat floatValue = PrimitiveClassesTests.fixture.Create<Single>();
		JInt intValue = PrimitiveClassesTests.fixture.Create<Int32>();
		JLong longValue = PrimitiveClassesTests.fixture.Create<Int64>();
		JShort shortValue = PrimitiveClassesTests.fixture.Create<Int16>();

		Dictionary<JClassLocalRef, JMethodId> constructorIds = new()
		{
			{ proxyEnv.VoidObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.BooleanObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.ByteObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.CharacterObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.DoubleObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.FloatObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.IntegerObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.LongObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
			{ proxyEnv.ShortObjectLocalRef, PrimitiveClassesTests.fixture.Create<JMethodId>() },
		};
		Dictionary<JClassLocalRef, JObjectLocalRef> newRefs = new()
		{
			{ proxyEnv.VoidObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.BooleanObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.ByteObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.CharacterObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.DoubleObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.FloatObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.IntegerObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.LongObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
			{ proxyEnv.ShortObjectLocalRef, PrimitiveClassesTests.fixture.Create<JObjectLocalRef>() },
		};

		// Create Wrapper instance in order to load class.
		proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
		                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
		{
			JClassLocalRef classRef = (JClassLocalRef)c[0];
			ReadOnlyValPtr<Byte> methodName = (ReadOnlyValPtr<Byte>)c[1];
			return "<init>"u8.SequenceEqual(methodName.GetByteSpan()) ?
				constructorIds.GetValueOrDefault(classRef) :
				default;
		});
		proxyEnv.NewObject(Arg.Any<JClassLocalRef>(), Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValueWrapper>>())
		        .Returns(c =>
		        {
			        JClassLocalRef classRef = (JClassLocalRef)c[0];
			        JMethodId methodId = (JMethodId)c[1];
			        return constructorIds.GetValueOrDefault(classRef) == methodId ? newRefs[classRef] : default;
		        });

		Assert.Throws<InvalidOperationException>(
			() => new JConstructorDefinition.Parameterless().New<JVoidObject>(env));
		using JBooleanObject booleanObject = JBooleanObject.Create(env, booleanValue);
		using JByteObject byteObject = JNumberObject<JByte, JByteObject>.Create(env, byteValue);
		using JCharacterObject characterObject = JCharacterObject.Create(env, charValue);
		using JDoubleObject doubleObject = JNumberObject<JDouble, JDoubleObject>.Create(env, doubleValue);
		using JFloatObject floatObject = JNumberObject<JFloat, JFloatObject>.Create(env, floatValue);
		using JIntegerObject integerObject = JNumberObject<JInt, JIntegerObject>.Create(env, intValue);
		using JLongObject longObject = JNumberObject<JLong, JLongObject>.Create(env, longValue);
		using JShortObject shortObject = JNumberObject<JShort, JShortObject>.Create(env, shortValue);

		Assert.Equal(booleanValue, booleanObject.Value);
		Assert.Equal(byteValue, byteObject.Value);
		Assert.Equal(charValue, characterObject.Value);
		Assert.Equal(doubleValue, doubleObject.Value);
		Assert.Equal(floatValue, floatObject.Value);
		Assert.Equal(intValue, integerObject.Value);
		Assert.Equal(longValue, longObject.Value);
		Assert.Equal(shortValue, shortObject.Value);
	}
}