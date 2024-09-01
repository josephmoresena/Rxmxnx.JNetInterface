namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
internal static class TestUtilities
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	public static TPointer InvertPointer<TPointer>(in TPointer ptr) where TPointer : unmanaged, IFixedPointer
	{
		IntPtr value = ~ptr.Pointer;
		return NativeUtilities.Transform<IntPtr, TPointer>(in value);
	}
	public static JStringObject CreateString(NativeInterfaceProxy proxyEnv, String text)
	{
		IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
		JStringLocalRef stringRef = TestUtilities.fixture.Create<JStringLocalRef>();
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		using IReadOnlyFixedMemory<Char>.IDisposable ctx = text.AsMemory().GetFixedContext();
		proxyEnv.NewString(ctx.ValuePointer, text.Length).Returns(stringRef);
		JStringObject jString = JStringObject.Create(vm.GetEnvironment()!, text);
		proxyEnv.Received(1).NewString(ctx.ValuePointer, text.Length);

		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		return jString;
	}
	public static JArrayObject<JClassObject> CreateClassArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JObjectLocalRef localRef = JClassObject.GetClass<JClassObject>(env).Global.Reference.Value;
		JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
		JObjectArrayLocalRef arrayRef = TestUtilities.fixture.Create<JObjectArrayLocalRef>();
		proxyEnv.NewObjectArray(length, classRef, Arg.Any<JObjectLocalRef>()).Returns(arrayRef);
		JArrayObject<JClassObject> result = JArrayObject<JClassObject>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JBoolean> CreateBooleanArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JBooleanArrayLocalRef arrayRef = TestUtilities.fixture.Create<JBooleanArrayLocalRef>();
		proxyEnv.NewBooleanArray(length).Returns(arrayRef);
		JArrayObject<JBoolean> result = JArrayObject<JBoolean>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JByte> CreateByteArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JByteArrayLocalRef arrayRef = TestUtilities.fixture.Create<JByteArrayLocalRef>();
		proxyEnv.NewByteArray(length).Returns(arrayRef);
		JArrayObject<JByte> result = JArrayObject<JByte>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JChar> CreateCharArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JCharArrayLocalRef arrayRef = TestUtilities.fixture.Create<JCharArrayLocalRef>();
		proxyEnv.NewCharArray(length).Returns(arrayRef);
		JArrayObject<JChar> result = JArrayObject<JChar>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JDouble> CreateDoubleArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JDoubleArrayLocalRef arrayRef = TestUtilities.fixture.Create<JDoubleArrayLocalRef>();
		proxyEnv.NewDoubleArray(length).Returns(arrayRef);
		JArrayObject<JDouble> result = JArrayObject<JDouble>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JFloat> CreateFloatArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JFloatArrayLocalRef arrayRef = TestUtilities.fixture.Create<JFloatArrayLocalRef>();
		proxyEnv.NewFloatArray(length).Returns(arrayRef);
		JArrayObject<JFloat> result = JArrayObject<JFloat>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JInt> CreateIntArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JIntArrayLocalRef arrayRef = TestUtilities.fixture.Create<JIntArrayLocalRef>();
		proxyEnv.NewIntArray(length).Returns(arrayRef);
		JArrayObject<JInt> result = JArrayObject<JInt>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JLong> CreateLongArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JLongArrayLocalRef arrayRef = TestUtilities.fixture.Create<JLongArrayLocalRef>();
		proxyEnv.NewLongArray(length).Returns(arrayRef);
		JArrayObject<JLong> result = JArrayObject<JLong>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JArrayObject<JShort> CreateShortArray(NativeInterfaceProxy proxyEnv, Int32 length)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JShortArrayLocalRef arrayRef = TestUtilities.fixture.Create<JShortArrayLocalRef>();
		proxyEnv.NewShortArray(length).Returns(arrayRef);
		JArrayObject<JShort> result = JArrayObject<JShort>.Create(env, length);
		Assert.Equal(arrayRef.ArrayValue, result.Reference);
		return result;
	}
	public static JLocalObject CreateObject(NativeInterfaceProxy proxyEnv)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JConstructorDefinition.Parameterless constructor = new();
		JObjectLocalRef localRef = TestUtilities.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = TestUtilities.fixture.Create<JClassLocalRef>();
		JMethodId methodId = TestUtilities.fixture.Create<JMethodId>();
		using IFixedPointer.IDisposable ctx = constructor.Information.GetFixedPointer();
		using IFixedPointer.IDisposable ctx2 = IDataType.GetMetadata<JLocalObject>().Information.GetFixedPointer();
		proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer).Returns(classRef);
		proxyEnv.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>())
		        .Returns(methodId);
		proxyEnv.NewObject(classRef, methodId, ReadOnlyValPtr<JValueWrapper>.Zero).Returns(localRef);

		JLocalObject jLocal = constructor.New<JLocalObject>(env);
		try
		{
			Assert.Equal(localRef, jLocal.Reference);
			proxyEnv.Received(1).FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer);
			proxyEnv.Received(1)
			        .GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).NewObject(classRef, methodId, ReadOnlyValPtr<JValueWrapper>.Zero);
			return jLocal;
		}
		finally
		{
			JClassObject.GetClass<JLocalObject>(env).Dispose(); // Avoid GC disposing.
		}
	}
	public static JThrowableObject CreateThrowable(NativeInterfaceProxy proxyEnv)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		JConstructorDefinition constructor =
			JConstructorDefinition.Create([JArgumentMetadata.Create<JStringObject>(),]);
		JThrowableLocalRef throwableRef = TestUtilities.fixture.Create<JThrowableLocalRef>();
		JClassLocalRef classRef = TestUtilities.fixture.Create<JClassLocalRef>();
		JMethodId methodId = TestUtilities.fixture.Create<JMethodId>();
		using IFixedPointer.IDisposable ctx = constructor.Information.GetFixedPointer();
		using IFixedPointer.IDisposable ctx2 = IDataType.GetMetadata<JErrorObject>().Information.GetFixedPointer();
		proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer).Returns(classRef);
		proxyEnv.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>())
		        .Returns(methodId);
		proxyEnv.NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>()).Returns(throwableRef.Value);

		using JStringObject jString = TestUtilities.CreateString(proxyEnv, "Error message");
		JStringLocalRef stringRef = jString.Reference;

		proxyEnv.When(e => e.NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>())).Do(c =>
		{
			JObjectLocalRef localRef = stringRef.Value;
			ReadOnlyValPtr<JValueWrapper> args = (ReadOnlyValPtr<JValueWrapper>)c[2];
			Assert.True(NativeUtilities.AsBytes(in args.Reference)[..IntPtr.Size]
			                           .SequenceEqual(NativeUtilities.AsBytes(in localRef)));
		});

		JThrowableObject jThrowable =
			JConstructorDefinition.New<JErrorObject>(constructor, JClassObject.GetClass<JErrorObject>(env), [jString,]);
		try
		{
			Assert.Equal(throwableRef, jThrowable.Reference);
			proxyEnv.Received(1).FindClass((ReadOnlyValPtr<Byte>)ctx2.Pointer);
			proxyEnv.Received(1)
			        .GetMethodId(classRef, (ReadOnlyValPtr<Byte>)ctx.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>());
			return jThrowable;
		}
		finally
		{
			JClassObject.GetClass<JErrorObject>(env).Dispose(); // Avoid GC disposing.
		}
	}
	public static JGlobal CreateGlobal(NativeInterfaceProxy proxyEnv, JLocalObject jLocal)
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JGlobalRef globalRef = TestUtilities.fixture.Create<JGlobalRef>();
		proxyEnv.NewGlobalRef(jLocal.Reference).Returns(globalRef);
		proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

		JGlobal jGlobal = jLocal.Global;

		Assert.Equal(globalRef, jGlobal.Reference);
		proxyEnv.Received(1).NewGlobalRef(jLocal.LocalReference);
		return jGlobal;
	}
	public static JLocalObject CreateWrapper<TPrimitive>(NativeInterfaceProxy proxyEnv, TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		proxyEnv.ClearReceivedCalls();
		proxyEnv.VirtualMachine.ClearReceivedCalls();

		JPrimitiveTypeMetadata primitiveMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		IClassFeature classFeature = env.ClassFeature;
		JObjectLocalRef localRef = TestUtilities.fixture.Create<JObjectLocalRef>();
		JMethodId methodId = TestUtilities.fixture.Create<JMethodId>();

		proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
		                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
		proxyEnv.NewObject(Arg.Any<JClassLocalRef>(), methodId, Arg.Any<ReadOnlyValPtr<JValueWrapper>>())
		        .Returns(localRef);

		JLocalObject? result = ClassNameHelper.GetClassName(primitiveMetadata.ClassName) switch
		{
			CommonNames.BooleanPrimitive => JBooleanObject.Create(env, (JBoolean)(Object)primitive),
			CommonNames.BytePrimitive => JNumberObject<JByte, JByteObject>.Create(env, (JByte)(Object)primitive),
			CommonNames.CharPrimitive => JCharacterObject.Create(env, (JChar)(Object)primitive),
			CommonNames.DoublePrimitive =>
				JNumberObject<JDouble, JDoubleObject>.Create(env, (JDouble)(Object)primitive),
			CommonNames.FloatPrimitive => JNumberObject<JFloat, JFloatObject>.Create(env, (JFloat)(Object)primitive),
			CommonNames.IntPrimitive => JNumberObject<JInt, JIntegerObject>.Create(env, (JInt)(Object)primitive),
			CommonNames.LongPrimitive => JNumberObject<JLong, JLongObject>.Create(env, (JLong)(Object)primitive),
			CommonNames.ShortPrimitive => JNumberObject<JShort, JShortObject>.Create(env, (JShort)(Object)primitive),
			_ => default,
		};
		try
		{
			Assert.Equal(localRef, result?.LocalReference);
			return result!;
		}
		finally
		{
			result?.Class.Dispose();
		}
	}
	public static JNativeCallEntry GetInstanceEntry(JMethodDefinition.Parameterless definition,
		out ObjectTracker tracker)
	{
		NativeVoidParameterless<JObjectLocalRef> instanceMethod = new();
		tracker = new() { WeakReference = new(instanceMethod), FinalizerFlag = instanceMethod.IsDisposed, };
		return JNativeCallEntry.CreateParameterless(definition, instanceMethod.VoidCall);
	}
	public static JNativeCallEntry GetStaticEntry(JMethodDefinition.Parameterless definition, out ObjectTracker tracker)
	{
		NativeVoidParameterless<JClassLocalRef> instanceMethod = new();
		tracker = new() { WeakReference = new(instanceMethod), FinalizerFlag = instanceMethod.IsDisposed, };
		return JNativeCallEntry.CreateParameterless(definition, instanceMethod.VoidCall);
	}
	public static JArgumentMetadata[] GetArgumentsMetadata(this CallType callType)
		=> callType switch
		{
			CallType.SingleValue => [JArgumentMetadata.Get<JInt>(),],
			CallType.SingleObject => [JArgumentMetadata.Get<JStringObject>(),],
			CallType.Values =>
			[
				JArgumentMetadata.Get<JInt>(),
				JArgumentMetadata.Get<JDouble>(),
				JArgumentMetadata.Get<JBoolean>(),
			],
			CallType.Objects =>
			[
				JArgumentMetadata.Get<JStringObject>(),
				JArgumentMetadata.Get<JClassObject>(),
				JArgumentMetadata.Get<JThrowableObject>(),
			],
			CallType.Mixed =>
			[
				JArgumentMetadata.Get<JInt>(),
				JArgumentMetadata.Get<JStringObject>(),
				JArgumentMetadata.Get<JDouble>(),
				JArgumentMetadata.Get<JClassObject>(),
				JArgumentMetadata.Get<JBoolean>(),
				JArgumentMetadata.Get<JThrowableObject>(),
			],
			_ => [],
		};
	public static IObject[] GetArgumentsValues(this CallType callType, NativeInterfaceProxy proxyEnv)
		=> callType switch
		{
			CallType.SingleValue => [(JInt)TestUtilities.fixture.Create<Int32>(),],
			CallType.SingleObject => [TestUtilities.CreateString(proxyEnv, TestUtilities.fixture.Create<String>()),],
			CallType.Values =>
			[
				(JInt)TestUtilities.fixture.Create<Int32>(), (JDouble)TestUtilities.fixture.Create<Double>(),
				(JBoolean)TestUtilities.fixture.Create<Boolean>(),
			],
			CallType.Objects =>
			[
				TestUtilities.CreateString(proxyEnv, TestUtilities.fixture.Create<String>()),
				(JEnvironment.GetEnvironment(proxyEnv.Reference) as IEnvironment)!.ClassFeature.ClassObject,
				TestUtilities.CreateThrowable(proxyEnv),
			],
			CallType.Mixed =>
			[
				(JInt)TestUtilities.fixture.Create<Int32>(),
				TestUtilities.CreateString(proxyEnv, TestUtilities.fixture.Create<String>()),
				(JDouble)TestUtilities.fixture.Create<Double>(),
				(JEnvironment.GetEnvironment(proxyEnv.Reference) as IEnvironment).ClassFeature.ClassObject,
				(JBoolean)TestUtilities.fixture.Create<Boolean>(), TestUtilities.CreateThrowable(proxyEnv),
			],
			_ => [],
		};
	public static Expression<Predicate<ReadOnlyValPtr<JValueWrapper>>> GetArgsPtr(IObject[] args)
	{
		Func<ReadOnlyValPtr<JValueWrapper>, Boolean> f = ptr =>
		{
			using IReadOnlyFixedContext<JValueWrapper>.IDisposable ctx = ptr.GetUnsafeFixedContext(args.Length);
			Span<Byte> byteSpan = stackalloc Byte[JValue.Size];
			ReadOnlySpan<Byte> blank = stackalloc Byte[JValue.Size];
			for (Int32 i = 0; i < args.Length; i++)
			{
				blank.CopyTo(byteSpan);
				args[i].CopyTo(byteSpan);
				if (!NativeUtilities.AsBytes(in ctx.Values[i]).SequenceEqual(byteSpan))
					return false;
			}
			return true;
		};
		ParameterExpression parameter = Expression.Parameter(typeof(ReadOnlyValPtr<JValueWrapper>), "ptr");
		Expression body = Expression.Invoke(Expression.Constant(f), parameter);
		return Expression.Lambda<Predicate<ReadOnlyValPtr<JValueWrapper>>>(body, parameter);
	}
}