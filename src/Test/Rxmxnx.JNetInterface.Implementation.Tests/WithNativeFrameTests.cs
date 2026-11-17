// ReSharper disable RedundantExplicitParamsArrayCreation

namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Covers <see cref="NativeCallExtensions"/> through <see cref="NativeInterfaceProxy"/>.
/// Primitive results are returned directly. Object results follow the reference type of each signature.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class WithNativeFrameTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void SignaturesMaterializeTheTokenAndReturnTheCallbackValue()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JClassTypeMetadata metadata = IClassType.GetMetadata<NativeFrameObject>();
		using IFixedPointer.IDisposable info =
			metadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		JObjectLocalRef localRef = WithNativeFrameTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = WithNativeFrameTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef nameRef = WithNativeFrameTests.fixture.Create<JStringLocalRef>();
		try
		{
			proxyEnv.GetObjectClass(localRef).Returns(classRef);
			proxyEnv.GetObjectRefType(Arg.Any<JObjectLocalRef>()).Returns(JReferenceType.LocalRefType);
			proxyEnv.GetStringUtfLength(nameRef).Returns(metadata.ClassName.Length);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValue>.Zero).Returns(nameRef.Value);
			proxyEnv.GetStringUtfChars(nameRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			JEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			JReferenceTypeToken token = JReferenceTypeToken.Create<NativeFrameObject>(localRef);
			Frame frame = new(proxyEnv, env, vm, token, localRef, metadata, classRef, nameRef);

			using JStringObject jString = TestUtilities.CreateString(proxyEnv, "native-frame");
			using JThrowableObject jThrowable = TestUtilities.CreateThrowable(proxyEnv);
			using JArrayObject<JBoolean> booleanArray = TestUtilities.CreateBooleanArray(proxyEnv, 1);
			using JArrayObject<JByte> byteArray = TestUtilities.CreateByteArray(proxyEnv, 1);
			using JArrayObject<JChar> charArray = TestUtilities.CreateCharArray(proxyEnv, 1);
			using JArrayObject<JShort> shortArray = TestUtilities.CreateShortArray(proxyEnv, 1);
			using JArrayObject<JInt> intArray = TestUtilities.CreateIntArray(proxyEnv, 1);
			using JArrayObject<JLong> longArray = TestUtilities.CreateLongArray(proxyEnv, 1);
			using JArrayObject<JFloat> floatArray = TestUtilities.CreateFloatArray(proxyEnv, 1);
			using JArrayObject<JDouble> doubleArray = TestUtilities.CreateDoubleArray(proxyEnv, 1);
			using JArrayObject<JClassObject> objectArray = TestUtilities.CreateClassArray(proxyEnv, 1);

			frame.Action();
			frame.Primitive((JBoolean)true);
			frame.Primitive((JByte)2);
			frame.Primitive((JChar)'A');
			frame.Primitive((JShort)(Int16)3);
			frame.Primitive((JInt)4);
			frame.Primitive((JLong)5);
			frame.Primitive((JFloat)6);
			frame.Primitive((JDouble)7);
			frame.Object();
			frame.String(jString);
			frame.Class(env.ClassObject);
			frame.Throwable(jThrowable);
			frame.Array(intArray);
			frame.ObjectArray(objectArray);
			frame.BooleanArray(booleanArray);
			frame.ByteArray(byteArray);
			frame.CharArray(charArray);
			frame.ShortArray(shortArray);
			frame.IntArray(intArray);
			frame.LongArray(longArray);
			frame.FloatArray(floatArray);
			frame.DoubleArray(doubleArray);
		}
		finally
		{
			nameCtx.Dispose();
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Boolean removeResult = JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
			if (Environment.Is64BitProcess)
				Assert.True(removeResult);
			proxyEnv.FinalizeProxy(true);
		}
	}

	private static void Check(ReadOnlySpan<JReferenceObject?> objects, JObjectLocalRef localRef,
		JClassTypeMetadata metadata)
	{
		NativeFrameObject obj = Assert.IsType<NativeFrameObject>(objects[0]);
		Assert.Equal(localRef, obj.Reference);
		Assert.Equal(metadata.ClassName, obj.Class.Name);
		Assert.Equal(metadata.GetClass(obj.Environment), obj.Class);
	}

	private sealed class Frame(
		NativeInterfaceProxy proxyEnv,
		JEnvironment env,
		IVirtualMachine vm,
		JReferenceTypeToken token,
		JObjectLocalRef localRef,
		JClassTypeMetadata metadata,
		JClassLocalRef classRef,
		JStringLocalRef nameRef)
	{
		public void Action()
			=> this.Run(5, () =>
			{
				StructAction action = new(localRef, metadata);
				env.Reference.WithNativeFrame(action, [token,]);
				action.WithNativeFrame(env.Reference, [token,]);
				vm.WithNativeFrame(env.Reference, action, [token,]);
				action.WithNativeFrame(vm, env.Reference, [token,]);
				env.Reference.WithNativeFrame(new ClassAction(localRef, metadata), [token,]);
			});
		public void Primitive<TPrimitive>(TPrimitive value) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
			=> this.Run(5, () =>
			{
				StructFunction<TPrimitive> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out TPrimitive result, [token,]);
				Assert.Equal(value, result);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value, result);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value, result);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value, result);
				env.Reference.WithNativeFrame(new ClassFunction<TPrimitive>(localRef, metadata, value), out result,
				                              [token,]);
				Assert.Equal(value, result);
			});
		public void Object()
			=> this.Run(5, () =>
			{
				StructFunction<JReferenceObject?> func = new(localRef, metadata, null);
				env.Reference.WithNativeFrame(func, out JObjectLocalRef result, [token,]);
				Assert.Equal(localRef, result);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(localRef, result);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(localRef, result);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(localRef, result);
				env.Reference.WithNativeFrame(new ClassFunction<JReferenceObject?>(localRef, metadata, null),
				                              out result, [token,]);
				Assert.Equal(localRef, result);
			});
		public void String(JStringObject value)
			=> this.Run(5, () =>
			{
				StructFunction<JStringObject?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JStringLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JStringObject?>(localRef, metadata, value), out result,
				                              [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void Class(JClassObject value)
			=> this.Run(5, () =>
			{
				StructFunction<JClassObject?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JClassLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JClassObject?>(localRef, metadata, value), out result,
				                              [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void Throwable(JThrowableObject value)
			=> this.Run(5, () =>
			{
				StructFunction<JThrowableObject?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JThrowableLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JThrowableObject?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void Array(JArrayObject<JInt> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject?>(localRef, metadata, value), out result,
				                              [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void ObjectArray(JArrayObject<JClassObject> value)
			=> this.Run(5, () =>
			{
				StructFunction<IArrayObject<ILocalObject>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JObjectArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<IArrayObject<ILocalObject>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void BooleanArray(JArrayObject<JBoolean> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JBoolean>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JBooleanArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JBoolean>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void ByteArray(JArrayObject<JByte> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JByte>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JByteArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JByte>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void CharArray(JArrayObject<JChar> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JChar>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JCharArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JChar>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void ShortArray(JArrayObject<JShort> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JShort>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JShortArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JShort>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void IntArray(JArrayObject<JInt> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JInt>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JIntArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JInt>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void LongArray(JArrayObject<JLong> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JLong>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JLongArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JLong>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void FloatArray(JArrayObject<JFloat> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JFloat>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JFloatArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JFloat>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		public void DoubleArray(JArrayObject<JDouble> value)
			=> this.Run(5, () =>
			{
				StructFunction<JArrayObject<JDouble>?> func = new(localRef, metadata, value);
				env.Reference.WithNativeFrame(func, out JDoubleArrayLocalRef result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				vm.WithNativeFrame(env.Reference, func, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				func.WithNativeFrame(vm, env.Reference, out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
				env.Reference.WithNativeFrame(new ClassFunction<JArrayObject<JDouble>?>(localRef, metadata, value),
				                              out result, [token,]);
				Assert.Equal(value.Reference.Value, result.Value);
			});

		private void Run(Int32 calls, Action body)
		{
			proxyEnv.ClearReceivedCalls();
			body();
			proxyEnv.Received(calls).GetObjectRefType(localRef);
			proxyEnv.Received(calls).GetObjectClass(localRef);
			proxyEnv.Received(calls).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                          ReadOnlyValPtr<JValue>.Zero);
			proxyEnv.Received(calls).GetStringUtfLength(nameRef);
			proxyEnv.Received(calls).GetStringUtfChars(nameRef, Arg.Any<ValPtr<JBoolean>>());
		}
	}

	private readonly struct StructAction(JObjectLocalRef localRef, JClassTypeMetadata metadata) : IFrameAction
	{
		public void Accept(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
			=> WithNativeFrameTests.Check(objects, localRef, metadata);
	}

	private sealed class ClassAction(JObjectLocalRef localRef, JClassTypeMetadata metadata) : IFrameAction
	{
		public void Accept(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
			=> WithNativeFrameTests.Check(objects, localRef, metadata);
	}

	private readonly struct StructFunction<TResult>(
		JObjectLocalRef localRef,
		JClassTypeMetadata metadata,
		TResult value) : IFrameFunction<TResult>
	{
		public TResult Apply(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
		{
			WithNativeFrameTests.Check(objects, localRef, metadata);
			return value is not null ? value : (TResult)(Object)objects[0]!;
		}
	}

	private sealed class ClassFunction<TResult>(JObjectLocalRef localRef, JClassTypeMetadata metadata, TResult value)
		: IFrameFunction<TResult>
	{
		public TResult Apply(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
		{
			WithNativeFrameTests.Check(objects, localRef, metadata);
			return value is not null ? value : (TResult)(Object)objects[0]!;
		}
	}
}

public sealed class NativeFrameObject : JLocalObject, IClassType<NativeFrameObject>
{
	private static JClassTypeMetadata<NativeFrameObject>? metadata;

	static JClassTypeMetadata<NativeFrameObject> IClassType<NativeFrameObject>.Metadata
		=> NativeFrameObject.metadata ??= TypeMetadataBuilder<NativeFrameObject>.Create(
			(CString)$"rxmxnx/jnetinterface/test/Frame{Guid.NewGuid():N}").Build();

	private NativeFrameObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	private NativeFrameObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	private NativeFrameObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static NativeFrameObject IClassType<NativeFrameObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static NativeFrameObject IClassType<NativeFrameObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static NativeFrameObject IClassType<NativeFrameObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}