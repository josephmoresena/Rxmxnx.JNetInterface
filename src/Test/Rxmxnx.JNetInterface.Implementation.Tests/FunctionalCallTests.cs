namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Covers the <see cref="ICallArgument"/> call path that the array-based call tests do not enter.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class FunctionalCallTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void VoidCallsUseCallArguments()
	{
		FunctionalCallTests.WithString((proxyEnv, jLocal, _) =>
		{
			JClassObject jClass = JClassObject.GetClass<JThrowableObject>(jLocal.Environment);
			OpenMethod method = FunctionalCallTests.OpenVoid(JArgumentMetadata.Get<JInt>());
			JInt value = 7;
			ClassArgument? classArgument = new();

			FunctionalCallTests.StubVoid(proxyEnv);
			method.InvokeValue(jLocal, value);
			method.InvokeValue(jLocal, jClass, value);
			method.InvokeNonVirtualValue(jLocal, jClass, value);
			method.StaticInvokeValue(jClass, value);
			method.InvokeByRef(jLocal, ref value);
			method.InvokeClass(jLocal, classArgument);
			method.InvokeClass(jLocal, default);

			proxyEnv.Received(5)
			        .CallVoidMethod(jLocal.Reference, Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>());
			proxyEnv.Received(1).CallNonVirtualVoidMethod(jLocal.Reference, Arg.Any<JClassLocalRef>(),
			                                              Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>());
			proxyEnv.Received(1).CallStaticVoidMethod(Arg.Any<JClassLocalRef>(), Arg.Any<JMethodId>(),
			                                          Arg.Any<ReadOnlyValPtr<JValue>>());
			jClass.Dispose();
		});
	}
	[Fact]
	internal void ReflectedVoidCallsUseCallArguments()
	{
		FunctionalCallTests.WithString((proxyEnv, jLocal, _) =>
		{
			OpenMethod method = FunctionalCallTests.OpenVoid(JArgumentMetadata.Get<JInt>());
			using JClassObject jMethodClass = new(jLocal.Class, IDataType.GetMetadata<JMethodObject>(),
			                                      FunctionalCallTests.fixture.Create<JClassLocalRef>());
			using JMethodObject jMethod = new(jMethodClass, FunctionalCallTests.fixture.Create<JObjectLocalRef>(),
			                                  method, jLocal.Class);
			JInt value = 3;

			FunctionalCallTests.StubVoid(proxyEnv);
			method.InvokeReflectedValue(jMethod, jLocal, value);
			method.InvokeNonVirtualReflectedValue(jMethod, jLocal, value);
			method.InvokeStaticReflectedValue(jMethod, value);
			method.InvokeReflectedClass(jMethod, jLocal, default);

			proxyEnv.Received(2)
			        .CallVoidMethod(jLocal.Reference, Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>());
			proxyEnv.Received(1).CallNonVirtualVoidMethod(jLocal.Reference, Arg.Any<JClassLocalRef>(),
			                                              Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>());
			proxyEnv.Received(1).CallStaticVoidMethod(Arg.Any<JClassLocalRef>(), Arg.Any<JMethodId>(),
			                                          Arg.Any<ReadOnlyValPtr<JValue>>());
		});
	}
	[Fact]
	internal void PrimitiveFunctionCallsUseCallArguments()
	{
		FunctionalCallTests.WithString((proxyEnv, jLocal, _) =>
		{
			JClassObject jClass = JClassObject.GetClass<JThrowableObject>(jLocal.Environment);
			OpenIntFunction function = FunctionalCallTests.OpenInt(JArgumentMetadata.Get<JInt>());
			JInt value = 4;
			JInt result = 11;

			FunctionalCallTests.StubInt(proxyEnv, result);
			Assert.Equal(result, function.InvokeValue(jLocal, value));
			Assert.Equal(result, function.InvokeValue(jLocal, jClass, value));
			Assert.Equal(result, function.InvokeNonVirtualValue(jLocal, jClass, value));
			Assert.Equal(result, function.StaticInvokeValue(jClass, value));
			Assert.Equal(result, function.InvokeByRef(jLocal, ref value));
			Assert.Equal(result, function.InvokeClass(jLocal, default));

			proxyEnv.Received(4)
			        .CallIntMethod(jLocal.Reference, Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>());
			proxyEnv.Received(1).CallNonVirtualIntMethod(jLocal.Reference, Arg.Any<JClassLocalRef>(),
			                                             Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>());
			proxyEnv.Received(1).CallStaticIntMethod(Arg.Any<JClassLocalRef>(), Arg.Any<JMethodId>(),
			                                         Arg.Any<ReadOnlyValPtr<JValue>>());
			jClass.Dispose();
		});
	}
	[Fact]
	internal void ObjectFunctionCallUsesCallArgument()
	{
		FunctionalCallTests.WithString((proxyEnv, jLocal, _) =>
		{
			OpenObjectFunction function = new((CString)FunctionalCallTests.fixture.Create<String>());
			JThrowableLocalRef result = FunctionalCallTests.fixture.Create<JThrowableLocalRef>();
			JStringLocalRef strRef = FunctionalCallTests.fixture.Create<JStringLocalRef>();
			JDataTypeMetadata metadata = IDataType.GetMetadata<JThrowableObject>();
			using IFixedPointer.IDisposable ctxClass =
				metadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
			JMethodId methodId = FunctionalCallTests.fixture.Create<JMethodId>();
			using IFixedPointer.IDisposable infoDef = function.Information.GetFixedPointer();

			proxyEnv.GetObjectClass(result.Value).Returns(proxyEnv.ThrowableLocalRef);
			proxyEnv.CallObjectMethod(proxyEnv.ThrowableLocalRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValue>.Zero).Returns(strRef.Value);
			proxyEnv.GetStringUtfLength(strRef).Returns(metadata.ClassName.Length);
			proxyEnv.GetStringUtfChars(strRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);
			proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), (ReadOnlyValPtr<Byte>)infoDef.Pointer,
			                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
			proxyEnv.CallObjectMethod(jLocal.Reference, methodId, Arg.Any<ReadOnlyValPtr<JValue>>())
			        .Returns(result.Value);

			using JThrowableObject? jThrowable = function.InvokeClass(jLocal, new());
			Assert.Equal(result.Value, jThrowable!.Reference.Value);
		});
	}
	[Fact]
	internal void ConstructorCallsUseCallArguments()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			OpenConstructor constructor = new(JArgumentMetadata.Get<JInt>());
			JMethodId methodId = FunctionalCallTests.fixture.Create<JMethodId>();
			JThrowableLocalRef result = FunctionalCallTests.fixture.Create<JThrowableLocalRef>();
			JClassLocalRef classRef = new(proxyEnv.VirtualMachine.ThrowableGlobalRef);
			using IFixedPointer.IDisposable infoDef = constructor.Information.GetFixedPointer();
			JInt value = 1;

			proxyEnv.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)infoDef.Pointer, Arg.Any<ReadOnlyValPtr<Byte>>())
			        .Returns(methodId);
			proxyEnv.NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValue>>()).Returns(result.Value);

			using JLocalObject created = constructor.NewValue(env.ClassFeature.ThrowableObject, value);
			Assert.Equal(result.Value, created.Reference);
			using JThrowableObject typed = constructor.NewTyped(env, value);
			Assert.Equal(result.Value, typed.Reference.Value);
			using JLocalObject createdDefault = constructor.NewClass(env.ClassFeature.ThrowableObject, default);
			Assert.Equal(result.Value, createdDefault.Reference);

			proxyEnv.Received(3).NewObject(classRef, methodId, Arg.Any<ReadOnlyValPtr<JValue>>());
		}
		finally
		{
			FunctionalCallTests.Release(proxyEnv);
		}
	}
	[Fact]
	internal void ParameterSlotWritesValuesAndRejectsInvalidIndex()
	{
		FunctionalCallTests.WithString((proxyEnv, jLocal, _) =>
		{
			OpenMethod pair =
				FunctionalCallTests.OpenVoid(JArgumentMetadata.Get<JInt>(), JArgumentMetadata.Get<JInt>());
			FunctionalCallTests.StubVoid(proxyEnv);
			pair.InvokeSpan(jLocal, default);
			proxyEnv.Received(1)
			        .CallVoidMethod(jLocal.Reference, Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>());

			OpenMethod parameterless = new((CString)FunctionalCallTests.fixture.Create<String>());
			Assert.Throws<InvalidOperationException>(() => parameterless.InvokeBad(jLocal, new()));
		});
	}

	private static void WithString(Action<NativeInterfaceProxy, JLocalObject, IEnvironment> test)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IFixedPointer.IDisposable ctx = IDataType.GetMetadata<JStringObject>().Information.GetFixedPointer();
			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)ctx.Pointer)
			        .Returns(FunctionalCallTests.fixture.Create<JClassLocalRef>());
			using JLocalObject jLocal =
				TestUtilities.CreateString(proxyEnv, FunctionalCallTests.fixture.Create<String>());
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			test(proxyEnv, jLocal, env);
			jLocal.Class.Dispose();
		}
		finally
		{
			FunctionalCallTests.Release(proxyEnv);
		}
	}
	private static void Release(NativeInterfaceProxy proxyEnv)
	{
		JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
		GC.Collect();
		GC.WaitForPendingFinalizers();
		Boolean removeResult = JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
		if (Environment.Is64BitProcess)
			Assert.True(removeResult);
		proxyEnv.FinalizeProxy(true);
	}
	private static void StubVoid(NativeInterfaceProxy proxyEnv)
	{
		JMethodId methodId = FunctionalCallTests.fixture.Create<JMethodId>();
		proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
		                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
		proxyEnv.GetStaticMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
		                           Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
	}
	private static void StubInt(NativeInterfaceProxy proxyEnv, JInt result)
	{
		JMethodId methodId = FunctionalCallTests.fixture.Create<JMethodId>();
		proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
		                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
		proxyEnv.GetStaticMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
		                           Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(methodId);
		proxyEnv.CallIntMethod(Arg.Any<JObjectLocalRef>(), Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>())
		        .Returns(result);
		proxyEnv.CallNonVirtualIntMethod(Arg.Any<JObjectLocalRef>(), Arg.Any<JClassLocalRef>(), Arg.Any<JMethodId>(),
		                                 Arg.Any<ReadOnlyValPtr<JValue>>()).Returns(result);
		proxyEnv.CallStaticIntMethod(Arg.Any<JClassLocalRef>(), Arg.Any<JMethodId>(), Arg.Any<ReadOnlyValPtr<JValue>>())
		        .Returns(result);
	}
	private static OpenMethod OpenVoid(params JArgumentMetadata[] metadata)
		=> new((CString)FunctionalCallTests.fixture.Create<String>(), metadata);
	private static OpenIntFunction OpenInt(params JArgumentMetadata[] metadata)
		=> new((CString)FunctionalCallTests.fixture.Create<String>(), metadata);

	private sealed class OpenMethod : JMethodDefinition
	{
		public OpenMethod(ReadOnlySpan<Byte> methodName, params JArgumentMetadata[] metadata) : base(
			methodName, metadata) { }

		public void InvokeValue(JLocalObject jLocal, JInt args) => this.Invoke(jLocal, args);
		public void InvokeValue(JLocalObject jLocal, JClassObject jClass, JInt args)
			=> this.Invoke(jLocal, jClass, args);
		public void InvokeNonVirtualValue(JLocalObject jLocal, JClassObject jClass, JInt args)
			=> this.InvokeNonVirtual(jLocal, jClass, args);
		public void StaticInvokeValue(JClassObject jClass, JInt args) => this.StaticInvoke(jClass, args);
		public void InvokeByRef(JLocalObject jLocal, ref JInt args) => this.Invoke(jLocal, ref args);
		public void InvokeClass(JLocalObject jLocal, ClassArgument? args) => this.Invoke(jLocal, args);
		public void InvokeBad(JLocalObject jLocal, BadIndexArgument args) => this.Invoke(jLocal, args);
		public void InvokeSpan(JLocalObject jLocal, IntSpanArgument args) => this.Invoke(jLocal, args);
		public void InvokeReflectedValue(JMethodObject jMethod, JLocalObject jLocal, JInt args)
			=> this.InvokeReflected(jMethod, jLocal, args);
		public void InvokeNonVirtualReflectedValue(JMethodObject jMethod, JLocalObject jLocal, JInt args)
			=> this.InvokeNonVirtualReflected(jMethod, jLocal, args);
		public void InvokeStaticReflectedValue(JMethodObject jMethod, JInt args)
			=> this.InvokeStaticReflected(jMethod, args);
		public void InvokeReflectedClass(JMethodObject jMethod, JLocalObject jLocal, ClassArgument? args)
			=> this.InvokeReflected(jMethod, jLocal, args);
	}

	private sealed class OpenIntFunction : JFunctionDefinition<JInt>
	{
		public OpenIntFunction(ReadOnlySpan<Byte> functionName, params JArgumentMetadata[] metadata) : base(
			functionName, metadata) { }

		public JInt? InvokeValue(JLocalObject jLocal, JInt args) => this.Invoke(jLocal, args);
		public JInt? InvokeValue(JLocalObject jLocal, JClassObject jClass, JInt args)
			=> this.Invoke(jLocal, jClass, args);
		public JInt? InvokeNonVirtualValue(JLocalObject jLocal, JClassObject jClass, JInt args)
			=> this.InvokeNonVirtual(jLocal, jClass, args);
		public JInt? StaticInvokeValue(JClassObject jClass, JInt args) => this.StaticInvoke(jClass, args);
		public JInt? InvokeByRef(JLocalObject jLocal, ref JInt args) => this.Invoke(jLocal, ref args);
		public JInt? InvokeClass(JLocalObject jLocal, ClassArgument? args) => this.Invoke(jLocal, args);
	}

	private sealed class OpenObjectFunction : JFunctionDefinition<JThrowableObject>
	{
		public OpenObjectFunction(ReadOnlySpan<Byte> functionName) : base(functionName) { }

		public JThrowableObject? InvokeClass(JLocalObject jLocal, ClassArgument? args) => this.Invoke(jLocal, args);
	}

	private sealed class OpenConstructor : JConstructorDefinition
	{
		public OpenConstructor(params JArgumentMetadata[] metadata) : base(metadata) { }

		public JLocalObject NewValue(JClassObject jClass, JInt args) => this.New(jClass, args);
		public JThrowableObject NewTyped(IEnvironment env, JInt args) => this.New<JThrowableObject, JInt>(env, args);
		public JLocalObject NewClass(JClassObject jClass, ClassArgument? args) => this.New(jClass, args);
	}

	/// <summary>Reference <see cref="ICallArgument"/> that writes nothing.</summary>
	private sealed class ClassArgument : ICallArgument
	{
		public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot { }
	}

	/// <summary>Writes two primitive values through <see cref="IParameterSlot.SetParameterValues{TObject}"/>.</summary>
	private readonly struct IntSpanArgument : ICallArgument
	{
		public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot
		{
			ReadOnlySpan<JInt> values = [1, 2,];
			slot.SetParameterValues(0, values);
		}
	}

	/// <summary>Writes past the argument buffer.</summary>
	private sealed class BadIndexArgument : ICallArgument
	{
		public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot => slot.SetParameterDefault(0);
	}
}