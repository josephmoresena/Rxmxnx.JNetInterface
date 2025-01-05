namespace Rxmxnx.JNetInterface.Tests;

public partial class PrimitiveClassesTests
{
	[Fact]
	internal void GetPrimitiveClassTest()
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

			Dictionary<JClassLocalRef, JStringLocalRef> primitiveClassNameRefs =
				PrimitiveClassesTests.GetPrimitiveClassNameRefs(proxyEnv);
			CStringSequence primitivesClassNames =
				CStringSequence.Create("void\0boolean\0byte\0char\0double\0float\0int\0long\0short\0\0"u8
					                       .AsValues<Byte, Char>());
			using IFixedPointer.IDisposable fixedClassNames = primitivesClassNames.GetFixedPointer();
			Dictionary<JStringLocalRef, CString> primitiveClassNames = new()
			{
				{ primitiveClassNameRefs[proxyEnv.VoidPrimitiveLocalRef], primitivesClassNames[0] },
				{ primitiveClassNameRefs[proxyEnv.BooleanPrimitiveLocalRef], primitivesClassNames[1] },
				{ primitiveClassNameRefs[proxyEnv.BytePrimitiveLocalRef], primitivesClassNames[2] },
				{ primitiveClassNameRefs[proxyEnv.CharPrimitiveLocalRef], primitivesClassNames[3] },
				{ primitiveClassNameRefs[proxyEnv.DoublePrimitiveLocalRef], primitivesClassNames[4] },
				{ primitiveClassNameRefs[proxyEnv.FloatPrimitiveLocalRef], primitivesClassNames[5] },
				{ primitiveClassNameRefs[proxyEnv.IntPrimitiveLocalRef], primitivesClassNames[6] },
				{ primitiveClassNameRefs[proxyEnv.LongPrimitiveLocalRef], primitivesClassNames[7] },
				{ primitiveClassNameRefs[proxyEnv.ShortPrimitiveLocalRef], primitivesClassNames[8] },
			};

			proxyEnv.CallObjectMethod(Arg.Any<JObjectLocalRef>(), proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(c =>
			{
				JClassLocalRef classRef = JClassLocalRef.FromReference((JObjectLocalRef)c[0]);
				return primitiveClassNameRefs.GetValueOrDefault(classRef).Value;
			});
			proxyEnv.GetStringUtfLength(Arg.Any<JStringLocalRef>()).Returns(c =>
			{
				JStringLocalRef strRef = (JStringLocalRef)c[0];
				return primitiveClassNames.GetValueOrDefault(strRef)?.Length ?? default;
			});
			proxyEnv.GetStringUtfChars(Arg.Any<JStringLocalRef>(), Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				JStringLocalRef strRef = (JStringLocalRef)c[0];
				ReadOnlySpan<Byte> span = primitiveClassNames.GetValueOrDefault(strRef);
				return span.GetUnsafeValPtr();
			});
			proxyEnv.CallBooleanMethod(Arg.Any<JObjectLocalRef>(), proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                           ReadOnlyValPtr<JValueWrapper>.Zero).Returns(c =>
			{
				JClassLocalRef classRef = JClassLocalRef.FromReference((JObjectLocalRef)c[0]);
				return primitiveClassNameRefs.ContainsKey(classRef);
			});

			using JClassObject voidPrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(voidObjectClass)!;
			using JClassObject booleanPrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(booleanObjectClass)!;
			using JClassObject bytePrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(byteObjectClass)!;
			using JClassObject charPrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(characterObjectClass)!;
			using JClassObject doublePrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(doubleObjectClass)!;
			using JClassObject floatPrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(floatObjectClass)!;
			using JClassObject intPrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(integerObjectClass)!;
			using JClassObject longPrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(longObjectClass)!;
			using JClassObject shortPrimitiveClass =
				NativeFunctionSetImpl.PrimitiveTypeDefinition.StaticGet(shortObjectClass)!;

			Assert.Equal(proxyEnv.VirtualMachine.VoidPGlobalRef.Value, voidPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.BooleanPGlobalRef.Value, booleanPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.BytePGlobalRef.Value, bytePrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.CharPGlobalRef.Value, charPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.DoublePGlobalRef.Value, doublePrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.FloatPGlobalRef.Value, floatPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.IntPGlobalRef.Value, intPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.LongPGlobalRef.Value, longPrimitiveClass.Reference.Value);
			Assert.Equal(proxyEnv.VirtualMachine.ShortPGlobalRef.Value, shortPrimitiveClass.Reference.Value);

			Assert.Equal(default, voidPrimitiveClass.LocalReference);
			Assert.Equal(default, booleanPrimitiveClass.LocalReference);
			Assert.Equal(default, bytePrimitiveClass.LocalReference);
			Assert.Equal(default, charPrimitiveClass.LocalReference);
			Assert.Equal(default, doublePrimitiveClass.LocalReference);
			Assert.Equal(default, floatPrimitiveClass.LocalReference);
			Assert.Equal(default, intPrimitiveClass.LocalReference);
			Assert.Equal(default, longPrimitiveClass.LocalReference);
			Assert.Equal(default, shortPrimitiveClass.LocalReference);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}