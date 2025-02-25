using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using NSubstitute;

using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Proxies;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ManagedTest;

public class DefaultTest
{
	[Fact]
	internal void GetHelloStringTest()
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		TextWriter writer = Substitute.For<TextWriter>();
		JObjectLocalRef localRef = DefaultTest.CreateNative<JObjectLocalRef>();
		JStringLocalRef stringRef = DefaultTest.CreateNative<JStringLocalRef>();
		IMutableWrapper<JStringObject?> jStringW = IMutableWrapper<JStringObject>.Create();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject jStrClass = EnvironmentProxy.CreateClassObject<JStringObject>(jClass); // java.lang.String
		using JStringObject jString = EnvironmentProxy.CreateString(jStrClass, stringRef);

		envProxy.VirtualMachine.Returns(Substitute.For<VirtualMachineProxy>());
		envProxy.ClassObject.Returns(jClass);

		using JHelloDotnetObject jLocal = EnvironmentProxy.CreateObject(jObjClass, localRef)
		                                                  .CastTo<JHelloDotnetObject>(true);

		IManagedCallback callback = new IManagedCallback.Default(envProxy.VirtualMachine, writer);

		envProxy.GetObjectClass(jLocal).Returns(jObjClass);
		jStringW.Value = jString;
		envProxy.Create(Arg.Any<String>()).Returns(jString);
		envProxy.When(e => e.Create(Arg.Any<String>()))
		        .Do(c => EnvironmentProxy.SetText(jStringW.Value!, (String)c[0]));

		Assert.Equal(jString, callback.GetHelloString(jLocal));
		envProxy.Received(1).Create(jString.Value);
	}
	[Fact]
	internal void GetThreadIdTest()
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		TextWriter writer = Substitute.For<TextWriter>();
		JObjectLocalRef localRef = DefaultTest.CreateNative<JObjectLocalRef>();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet

		envProxy.VirtualMachine.Returns(Substitute.For<VirtualMachineProxy>());
		envProxy.ClassObject.Returns(jClass);

		using JHelloDotnetObject jLocal = EnvironmentProxy.CreateObject(jObjClass, localRef)
		                                                  .CastTo<JHelloDotnetObject>(true);

		IManagedCallback callback = new IManagedCallback.Default(envProxy.VirtualMachine, writer);

		envProxy.GetObjectClass(jLocal).Returns(jObjClass);

		Assert.Equal(Environment.CurrentManagedThreadId, callback.GetThreadId(jLocal));
		writer.Received(1).WriteLine(jObjClass);
	}
	[Fact]
	internal void PrintRuntimeInformationTest()
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		TextWriter writer = Substitute.For<TextWriter>();
		JObjectLocalRef localRef = DefaultTest.CreateNative<JObjectLocalRef>();
		JStringLocalRef stringRef = DefaultTest.CreateNative<JStringLocalRef>();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject jStrClass = EnvironmentProxy.CreateClassObject<JStringObject>(jClass); // java.lang.String

		envProxy.VirtualMachine.Returns(Substitute.For<VirtualMachineProxy>());
		envProxy.ClassObject.Returns(jClass);

		using JHelloDotnetObject jLocal = EnvironmentProxy.CreateObject(jObjClass, localRef)
		                                                  .CastTo<JHelloDotnetObject>(true);
		using JStringObject jString = EnvironmentProxy.CreateString(jStrClass, stringRef, Guid.NewGuid().ToString());

		IManagedCallback callback = new IManagedCallback.Default(envProxy.VirtualMachine, writer);

		envProxy.GetObjectClass(jLocal).Returns(jObjClass);
		callback.PrintRuntimeInformation(jLocal, jString);

		writer.Received(1).WriteLine(jLocal);
		writer.Received(1).WriteLine(jString.Value);
	}
	[Fact]
	internal void ProcessFieldTest()
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		TextWriter writer = Substitute.For<TextWriter>();
		INativeMemoryAdapter adapter = Substitute.For<INativeMemoryAdapter>();
		JObjectLocalRef localRef = DefaultTest.CreateNative<JObjectLocalRef>();
		JStringLocalRef stringRef1 = DefaultTest.CreateNative<JStringLocalRef>();
		JStringLocalRef stringRef2 = DefaultTest.CreateNative<JStringLocalRef>();
		JFieldDefinition<JStringObject> fieldDefinition = new("s_field"u8);
		String strVal1 = Guid.NewGuid().ToString();
		Byte[] strUtf8Val1 = Encoding.UTF8.GetBytes(strVal1);
		String strVal2 = Convert.ToHexString(strUtf8Val1);

		using IFixedContext<Byte>.IDisposable ctx = strUtf8Val1.AsMemory().GetFixedContext();
		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject jStrClass = EnvironmentProxy.CreateClassObject<JStringObject>(jClass); // java.lang.String

		envProxy.VirtualMachine.Returns(Substitute.For<VirtualMachineProxy>());
		envProxy.ClassObject.Returns(jClass);

		using JHelloDotnetObject jLocal = EnvironmentProxy.CreateObject(jObjClass, localRef)
		                                                  .CastTo<JHelloDotnetObject>(true);
		using JStringObject jString1 = EnvironmentProxy.CreateString(jStrClass, stringRef1, strVal1);
		using JStringObject jString2 = EnvironmentProxy.CreateString(jStrClass, stringRef2, strVal2);

		IManagedCallback callback = new IManagedCallback.Default(envProxy.VirtualMachine, writer);

		envProxy.GetObjectClass(jLocal).Returns(jObjClass);
		envProxy.GetField<JStringObject>(jLocal, jLocal.Class, fieldDefinition).Returns(jString1);
		adapter.Copy.Returns(true);
		adapter.Critical.Returns(false);
		adapter.GetReadOnlyContext(Arg.Any<JNativeMemory>()).Returns(ctx);
		adapter.GetContext(Arg.Any<JNativeMemory>()).Returns(ctx);

		envProxy.GetUtf8Sequence(jString1, Arg.Any<JMemoryReferenceKind>()).Returns(adapter);
		envProxy.Create(strVal2).Returns(jString2);

		callback.ProcessField(jLocal);

		envProxy.Received(1).GetField<JStringObject>(jLocal, jLocal.Class, fieldDefinition);
		envProxy.Received(1).GetUtf8Sequence(jString1, Arg.Any<JMemoryReferenceKind>());
		envProxy.Received(1).SetField(jLocal, jLocal.Class, fieldDefinition, jString2);
	}
	[Fact]
	internal void ThrowTest()
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		ThreadProxy threadProxy = Substitute.For<ThreadProxy>();
		TextWriter writer = Substitute.For<TextWriter>();
		JObjectLocalRef localRef = DefaultTest.CreateNative<JObjectLocalRef>();
		JGlobalRef globalRef = DefaultTest.CreateNative<JGlobalRef>();
		JWeakRef weakRef = DefaultTest.CreateNative<JWeakRef>();

		JMethodDefinition definition = new JMethodDefinition.Parameterless("throwException"u8);
		String message = Guid.NewGuid().ToString();
		StackTraceInfo info = new()
		{
			ClassName = "com.rxmxnx.dotnet.test.HelloDotnet",
			LineNumber = 10,
			FileName = "HelloDotnet.java",
			NativeMethod = false,
			MethodName = "throwException",
		};

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject
			jThrowableClass = EnvironmentProxy
				.CreateClassObject<JNullPointerExceptionObject>(jClass); // java.lang.NullPointerException

		envProxy.Reference.Returns(DefaultTest.CreateNative<JEnvironmentRef>());
		envProxy.VirtualMachine.Returns(Substitute.For<VirtualMachineProxy>());
		envProxy.ClassObject.Returns(jClass);
		envProxy.VirtualMachine.GetEnvironment().Returns(envProxy);

		using JHelloDotnetObject jLocal = EnvironmentProxy.CreateObject(jObjClass, localRef)
		                                                  .CastTo<JHelloDotnetObject>(true);

		ThrowableException exception =
			EnvironmentProxy.CreateException<JNullPointerExceptionObject>(
				envProxy.VirtualMachine, globalRef, out JGlobalBase throwableGlobal, message, info);

		using JWeak throwableWeak = EnvironmentProxy.CreateGlobal<JWeak>(throwableGlobal, weakRef.Value);

		envProxy.GetObjectClass(jLocal).Returns(jObjClass);
		envProxy.VirtualMachine.CreateThread(ThreadPurpose.ExceptionExecution).Returns(threadProxy);
		envProxy.When(e => e.CallMethod(Arg.Any<JLocalObject>(), Arg.Any<JClassObject>(), definition, false,
		                                Arg.Any<IObject?[]>())).Throws(exception);

		threadProxy.VirtualMachine.Returns(envProxy.VirtualMachine);
		threadProxy.CreateWeak(throwableGlobal).Returns(throwableWeak);
		threadProxy.GetObjectClass(throwableGlobal.ObjectMetadata).Returns(jThrowableClass);
		threadProxy.GetTypeMetadata(jThrowableClass).Returns(IDataType.GetMetadata<JNullPointerExceptionObject>());
		try
		{
			IManagedCallback callback = new IManagedCallback.Default(envProxy.VirtualMachine, writer);
			String throwableString;

			using (JNullPointerExceptionObject jThrowable =
			       throwableWeak.AsLocal<JNullPointerExceptionObject>(threadProxy))
			{
				EnvironmentProxy.SetThreadId(jThrowable, exception.ThreadId);
				throwableString = jThrowable.ToString();
			}

			threadProxy.ClearReceivedCalls();
			envProxy.ClearReceivedCalls();
			envProxy.VirtualMachine.ClearReceivedCalls();

			callback.Throw(jLocal);

			envProxy.Received(1).CallMethod(jLocal, jLocal.Class, definition, false, Arg.Any<IObject?[]>());
			threadProxy.Received(1).CreateWeak(throwableGlobal);
			threadProxy.Received().GetObjectClass(throwableWeak.ObjectMetadata);
			threadProxy.Received(1).GetTypeMetadata(jThrowableClass);

			writer.Received(1).WriteLine(throwableString);

			Assert.Equal(message, exception.Message);
			Assert.Equal(globalRef.Value, exception.ThrowableRef.Value);
			Assert.Equal(envProxy.Reference, exception.EnvironmentRef);
			Assert.Equal(Environment.CurrentManagedThreadId, exception.ThreadId);
		}
		finally
		{
			throwableGlobal.Dispose();
		}
	}
	[Fact]
	internal void SumArrayTest() => DefaultTest.SumArrayDefaultTest<IManagedCallback.Default>();
	[Fact]
	internal void GetIntArrayArrayTest() => DefaultTest.GetIntArrayArrayDefaultTest<IManagedCallback.Default>();
	[Fact]
	internal void PrintClassTest() => DefaultTest.PrintClassDefaultTest<IManagedCallback.Default>();
	[Fact]
	internal void GetVoidClassTest() => DefaultTest.GetVoidClassDefaultTest<IManagedCallback.Default>();
	[Fact]
	internal void GetPrimitiveClassesTest() => DefaultTest.GetPrimitiveClassesDefaultTest<IManagedCallback.Default>();

	private static TNative CreateNative<TNative>() where TNative : unmanaged
	{
		IntPtr result;
		if (RuntimeInformation.ProcessArchitecture is Architecture.Arm64 or Architecture.X64 or
		    Architecture.LoongArch64 or Architecture.RiscV64)
#pragma warning disable
			result = (IntPtr)Random.Shared.NextInt64();
#pragma warning restore
		else
			result = Random.Shared.Next();
		return Unsafe.As<IntPtr, TNative>(ref result);
	}
	private static void SumArrayDefaultTest<TCallback>() where TCallback : IManagedCallback
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		INativeMemoryAdapter adapter = Substitute.For<INativeMemoryAdapter>();
		JIntArrayLocalRef arrayRef = DefaultTest.CreateNative<JIntArrayLocalRef>();
		JObjectLocalRef localRef = DefaultTest.CreateNative<JObjectLocalRef>();
		JInt[] values = Enumerable.Range(0, Random.Shared.Next(0, 100)).Select(i => (JInt)i).ToArray();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject
			integerClass = EnvironmentProxy.CreateClassObject<JIntegerObject>(jClass); // java.lang.Integer
		using JClassObject jArrClass = EnvironmentProxy.CreateClassObject<JArrayObject<JInt>>(jClass); // int[]

		envProxy.Reference.Returns(DefaultTest.CreateNative<JEnvironmentRef>());
		envProxy.VirtualMachine.Returns(Substitute.For<VirtualMachineProxy>());
		envProxy.ClassObject.Returns(jClass);
		envProxy.VirtualMachine.GetEnvironment().Returns(envProxy);

		Assert.Null(TCallback.SumArray(jObjClass, null));

		JInt sum = values.Sum(v => v.Value);
		using IFixedContext<JInt>.IDisposable ctx = values.AsMemory().GetFixedContext();
		using JArrayObject<JInt> jArray =
			EnvironmentProxy.CreateArrayObject<JInt>(jArrClass, arrayRef.ArrayValue, values.Length);
		using JIntegerObject integerObject =
			EnvironmentProxy.CreteWrapperObject<JIntegerObject>(integerClass, localRef, sum);

		envProxy.CreateWrapper(sum).Returns(integerObject);
		envProxy.GetSequence(jArray, Arg.Any<JMemoryReferenceKind>()).Returns(adapter);
		adapter.Copy.Returns(true);
		adapter.Critical.Returns(false);
		adapter.GetReadOnlyContext(Arg.Any<JNativeMemory>())
		       .Returns<IReadOnlyFixedContext<Byte>>(ctx.AsBinaryContext());
		adapter.GetContext(Arg.Any<JNativeMemory>()).Returns(ctx.AsBinaryContext());

		Assert.Equal(integerObject, TCallback.SumArray(jObjClass, jArray));

		envProxy.Received(0).GetLength(jArray);
		envProxy.Received(1).GetSequence(jArray, Arg.Any<JMemoryReferenceKind>());
		envProxy.Received(1).CreateWrapper(sum);
		envProxy.Received(0).GetPrimitiveValue<JInt>(Arg.Any<JIntegerObject>());
	}
	private static void GetIntArrayArrayDefaultTest<TCallback>() where TCallback : IManagedCallback
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		JObjectArrayLocalRef arrayArrayRef = DefaultTest.CreateNative<JObjectArrayLocalRef>();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject jArrArrClass =
			EnvironmentProxy.CreateClassObject<JArrayObject<JArrayObject<JInt>>>(jClass); // int[][]
		using JClassObject jArrClass = EnvironmentProxy.CreateClassObject<JArrayObject<JInt>>(jClass); // int[]

		envProxy.Reference.Returns(DefaultTest.CreateNative<JEnvironmentRef>());
		envProxy.VirtualMachine.Returns(Substitute.For<VirtualMachineProxy>());
		envProxy.ClassObject.Returns(jClass);
		envProxy.VirtualMachine.GetEnvironment().Returns(envProxy);
		envProxy.GetClass<JArrayObject<JInt>>().Returns(jArrClass);

		Int32 length = Random.Shared.Next(0, 10);
		using JArrayObject<JArrayObject<JInt>> jArrArr =
			EnvironmentProxy.CreateArrayObject<JArrayObject<JInt>>(jArrArrClass, arrayArrayRef.ArrayValue, length);
		List<JArrayObject<JInt>> arrays = [];
		List<IFixedContext<JInt>.IDisposable> ctxs = [];
		Dictionary<Int64, INativeMemoryAdapter> adapters = [];

		envProxy.GetArrayLength(jArrArr).Returns(length);
		envProxy.CreateArray<JArrayObject<JInt>>(length).Returns(jArrArr);
		envProxy.GetCriticalSequence(Arg.Any<JArrayObject<JInt>>(), Arg.Any<JMemoryReferenceKind>())
		        .Returns(c => adapters[EnvironmentProxy.GetProxyId(c[0] as JArrayObject<JInt>)]);
		envProxy.CreateArray<JInt>(Arg.Any<Int32>()).Returns(c =>
		{
			Int32 arrLength = (Int32)c[0];
			JIntArrayLocalRef arrayRef = DefaultTest.CreateNative<JIntArrayLocalRef>();
			JInt[] val = new JInt[arrLength];
			IFixedContext<JInt>.IDisposable ctx = val.AsMemory().GetFixedContext();
			INativeMemoryAdapter adapter = Substitute.For<INativeMemoryAdapter>();
			JClassObject jArrC = envProxy.GetClass<JArrayObject<JInt>>();
			JArrayObject<JInt> jArr = EnvironmentProxy.CreateArrayObject<JInt>(jArrC, arrayRef.ArrayValue, arrLength);

			adapter.Copy.Returns(false);
			adapter.Critical.Returns(true);
			adapter.GetReadOnlyContext(Arg.Any<JNativeMemory>())
			       .Returns<IReadOnlyFixedContext<Byte>>(ctx.AsBinaryContext());
			adapter.GetContext(Arg.Any<JNativeMemory>()).Returns(ctx.AsBinaryContext());

			adapters.Add(EnvironmentProxy.GetProxyId(jArr), adapter);
			arrays.Add(jArr);
			ctxs.Add(ctx);

			return jArr;
		});

		TCallback.GetIntArrayArray(jObjClass, length);

		for (Int32 index = 0; index < arrays.Count; index++)
		{
			JArrayObject<JInt> jArray = arrays[index];
			INativeMemoryAdapter adapter = adapters[EnvironmentProxy.GetProxyId(jArray)];

			envProxy.Received(1).SetElement(jArrArr, index, jArray);
			envProxy.Received(1).GetCriticalSequence(jArray, Arg.Any<JMemoryReferenceKind>());
			adapter.Received(1).GetContext(Arg.Any<JNativeMemory>());
		}

		arrays.ForEach(a => a.Dispose());
		ctxs.ForEach(c => c.Dispose());
	}
	private static void PrintClassDefaultTest<TCallback>() where TCallback : IManagedCallback
	{
		TextWriter writer = Substitute.For<TextWriter>();
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		JClassLocalRef classRef = DefaultTest.CreateNative<JClassLocalRef>();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass, classRef); // com.rxmxnx.dotnet.test.HelloDotnet

		TCallback.PrintClass(jObjClass, writer);

		writer.Received(1).WriteLine(jObjClass.ToString());
	}
	private static void GetVoidClassDefaultTest<TCallback>() where TCallback : IManagedCallback
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		JClassLocalRef classRef = DefaultTest.CreateNative<JClassLocalRef>();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject voidClass = EnvironmentProxy.CreateVoidClassObject(jClass, classRef); // void

		envProxy.VoidPrimitive.Returns(voidClass);
		Assert.Equal(voidClass, TCallback.GetVoidClass(jClass));
	}
	private static void GetPrimitiveClassesDefaultTest<TCallback>() where TCallback : IManagedCallback
	{
		EnvironmentProxy envProxy = Substitute.For<EnvironmentProxy>();
		JArrayLocalRef arrayRef = DefaultTest.CreateNative<JArrayLocalRef>();

		using JClassObject jClass = EnvironmentProxy.CreateClassObject(envProxy); // java.lang.Class<?>
		using JClassObject
			jObjClass = EnvironmentProxy
				.CreateClassObject<JHelloDotnetObject>(jClass); // com.rxmxnx.dotnet.test.HelloDotnet
		using JClassObject
			arrClass = EnvironmentProxy.CreateClassObject<JArrayObject<JClassObject>>(jClass); // java.lang.Class<?>[]

		using JClassObject booleanClass =
			EnvironmentProxy.CreateClassObject<JBoolean>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // boolean
		using JClassObject byteClass =
			EnvironmentProxy.CreateClassObject<JByte>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // byte
		using JClassObject charClass =
			EnvironmentProxy.CreateClassObject<JChar>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // char
		using JClassObject doubleClass =
			EnvironmentProxy.CreateClassObject<JDouble>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // double
		using JClassObject floatClass =
			EnvironmentProxy.CreateClassObject<JFloat>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // float
		using JClassObject intClass =
			EnvironmentProxy.CreateClassObject<JInt>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // int
		using JClassObject longClass =
			EnvironmentProxy.CreateClassObject<JLong>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // long
		using JClassObject shortClass =
			EnvironmentProxy.CreateClassObject<JShort>(jClass, DefaultTest.CreateNative<JClassLocalRef>()); // short
		using JArrayObject<JClassObject> jClasses =
			EnvironmentProxy.CreateArrayObject<JClassObject>(arrClass, arrayRef, 8);

		envProxy.CreateArray<JClassObject>(8).Returns(jClasses);
		envProxy.GetClass<JBoolean>().Returns(booleanClass);
		envProxy.GetClass<JByte>().Returns(byteClass);
		envProxy.GetClass<JChar>().Returns(charClass);
		envProxy.GetClass<JDouble>().Returns(doubleClass);
		envProxy.GetClass<JFloat>().Returns(floatClass);
		envProxy.GetClass<JInt>().Returns(intClass);
		envProxy.GetClass<JLong>().Returns(longClass);
		envProxy.GetClass<JShort>().Returns(shortClass);

		Assert.Equal(jClasses, TCallback.GetPrimitiveClasses(jClass));

		envProxy.Received(1).GetClass<JBoolean>();
		envProxy.Received(1).GetClass<JByte>();
		envProxy.Received(1).GetClass<JChar>();
		envProxy.Received(1).GetClass<JDouble>();
		envProxy.Received(1).GetClass<JFloat>();
		envProxy.Received(1).GetClass<JInt>();
		envProxy.Received(1).GetClass<JLong>();
		envProxy.Received(1).GetClass<JShort>();

		envProxy.SetElement(jClasses, 0, booleanClass);
		envProxy.SetElement(jClasses, 1, byteClass);
		envProxy.SetElement(jClasses, 2, charClass);
		envProxy.SetElement(jClasses, 3, doubleClass);
		envProxy.SetElement(jClasses, 4, floatClass);
		envProxy.SetElement(jClasses, 5, intClass);
		envProxy.SetElement(jClasses, 6, longClass);
		envProxy.SetElement(jClasses, 7, shortClass);
	}
}