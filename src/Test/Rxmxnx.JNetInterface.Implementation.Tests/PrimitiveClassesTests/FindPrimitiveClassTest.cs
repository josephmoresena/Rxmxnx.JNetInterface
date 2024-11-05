namespace Rxmxnx.JNetInterface.Tests;

public partial class PrimitiveClassesTests
{
	[Fact]
	internal void FindPrimitiveClassTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using JClassObject voidObjectClass = env.ClassFeature.VoidObject;
			using JClassObject byteObjectClass = env.ClassFeature.ByteObject;
			using JClassObject booleanObjectClass = env.ClassFeature.BooleanObject;
			using JClassObject characterObjectClass = env.ClassFeature.CharacterObject;
			using JClassObject doubleObjectClass = env.ClassFeature.DoubleObject;
			using JClassObject floatObjectClass = env.ClassFeature.FloatObject;
			using JClassObject integerObjectClass = env.ClassFeature.IntegerObject;
			using JClassObject longObjectClass = env.ClassFeature.LongObject;
			using JClassObject shortObjectClass = env.ClassFeature.ShortObject;

			Assert.True(JObject.IsNullOrDefault(voidObjectClass));
			Assert.False(JObject.IsNullOrDefault(byteObjectClass));
			Assert.False(JObject.IsNullOrDefault(booleanObjectClass));
			Assert.False(JObject.IsNullOrDefault(characterObjectClass));
			Assert.False(JObject.IsNullOrDefault(doubleObjectClass));
			Assert.False(JObject.IsNullOrDefault(floatObjectClass));
			Assert.False(JObject.IsNullOrDefault(integerObjectClass));
			Assert.False(JObject.IsNullOrDefault(longObjectClass));
			Assert.False(JObject.IsNullOrDefault(shortObjectClass));

			// Needed to load class.
			PrimitiveClassesTests.WrapperInstanceAssert(proxyEnv, env);

			Assert.False(JObject.IsNullOrDefault(voidObjectClass));
			Assert.False(JObject.IsNullOrDefault(byteObjectClass));
			Assert.False(JObject.IsNullOrDefault(booleanObjectClass));
			Assert.False(JObject.IsNullOrDefault(characterObjectClass));
			Assert.False(JObject.IsNullOrDefault(doubleObjectClass));
			Assert.False(JObject.IsNullOrDefault(floatObjectClass));
			Assert.False(JObject.IsNullOrDefault(integerObjectClass));
			Assert.False(JObject.IsNullOrDefault(longObjectClass));
			Assert.False(JObject.IsNullOrDefault(shortObjectClass));

			using JClassObject voidPrimitiveClass =
				new(env.ClassFeature.ClassObject, JPrimitiveTypeMetadata.VoidMetadata);
			using JClassObject booleanPrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JBoolean>());
			using JClassObject bytePrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JByte>());
			using JClassObject charPrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JChar>());
			using JClassObject doublePrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JDouble>());
			using JClassObject floatPrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JFloat>());
			using JClassObject intPrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JInt>());
			using JClassObject longPrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JLong>());
			using JClassObject shortPrimitiveClass =
				new(env.ClassFeature.ClassObject, IPrimitiveType.GetMetadata<JShort>());

			Assert.True(JObject.IsNullOrDefault(voidPrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(bytePrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(booleanPrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(charPrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(doublePrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(floatPrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(intPrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(longPrimitiveClass));
			Assert.True(JObject.IsNullOrDefault(shortPrimitiveClass));

			proxyEnv.GetStaticMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                           Arg.Any<ReadOnlyValPtr<Byte>>())
			        .Returns(_ => PrimitiveClassesTests.fixture.Create<JMethodId>());

			JMethodDefinition.Parameterless fakeMethodDefinition = new("fakeMethod"u8);

			fakeMethodDefinition.StaticInvoke(voidPrimitiveClass);
			fakeMethodDefinition.StaticInvoke(booleanPrimitiveClass);
			fakeMethodDefinition.StaticInvoke(bytePrimitiveClass);
			fakeMethodDefinition.StaticInvoke(charPrimitiveClass);
			fakeMethodDefinition.StaticInvoke(doublePrimitiveClass);
			fakeMethodDefinition.StaticInvoke(floatPrimitiveClass);
			fakeMethodDefinition.StaticInvoke(intPrimitiveClass);
			fakeMethodDefinition.StaticInvoke(longPrimitiveClass);
			fakeMethodDefinition.StaticInvoke(shortPrimitiveClass);

			Assert.Equal(proxyEnv.VirtualMachine.VoidPGlobalRef.Value, voidPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.BooleanPGlobalRef.Value, booleanPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.BytePGlobalRef.Value, bytePrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.CharPGlobalRef.Value, charPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.DoublePGlobalRef.Value, doublePrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.FloatPGlobalRef.Value, floatPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.IntPGlobalRef.Value, intPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.LongPGlobalRef.Value, longPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.ShortPGlobalRef.Value, shortPrimitiveClass.Reference.Value);

			Assert.Equal(proxyEnv.VoidPrimitiveLocalRef.Value, voidPrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.BooleanPrimitiveLocalRef.Value, booleanPrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.BytePrimitiveLocalRef.Value, bytePrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.CharPrimitiveLocalRef.Value, charPrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.DoublePrimitiveLocalRef.Value, doublePrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.FloatPrimitiveLocalRef.Value, floatPrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.IntPrimitiveLocalRef.Value, intPrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.LongPrimitiveLocalRef.Value, longPrimitiveClass.LocalReference);
			Assert.Equal(proxyEnv.ShortPrimitiveLocalRef.Value, shortPrimitiveClass.LocalReference);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}