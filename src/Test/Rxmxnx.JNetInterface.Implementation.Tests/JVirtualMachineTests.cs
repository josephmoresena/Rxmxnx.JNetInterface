namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class JVirtualMachineTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal async Task RegisterTest()
	{
		Assert.False(JVirtualMachine.Register<JClassObject>());
		Assert.True(JVirtualMachine.Register<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JClassObject>>>>>());
		Task registerTask = Task.WhenAll(JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject(),
		                                 JVirtualMachineTests.RegisterTestObject());
		await Task.WhenAny(registerTask, Task.Delay(500)); // Set timeout
	}
	[Fact]
	internal void GetVirtualMachineErrorTest()
	{
		JResult result = (JResult)Random.Shared.Next(-6, -1);
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy(false);

		try
		{
			proxyEnv.VirtualMachine.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(result);
			proxyEnv.VirtualMachine.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                            Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>())
			        .Returns(result);
			JniException ex =
				Assert.Throws<JniException>(() => JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference));
			Assert.Equal(result, ex.Result);
			Assert.Equal(Enum.GetName(result), ex.Message);

			proxyEnv.VirtualMachine.Received(1).GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>());
			proxyEnv.VirtualMachine.Received(1).AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                                        Arg.Any<ReadOnlyValPtr<
				                                                        VirtualMachineArgumentValueWrapper>>());
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.False(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void RemoveVirtualMachineFalseTest() => Assert.False(JVirtualMachine.RemoveVirtualMachine(default));
	[Fact]
	internal void FatalErrorTest()
	{
		String fatalErrorMessage = JVirtualMachineTests.fixture.Create<String>();
		CString fatalErrorUtf8Message = (CString)fatalErrorMessage;
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			proxyEnv.UseVirtualMachineRef = false;
			proxyEnv.When(e => e.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()))
			        .Do(c => ((ValPtr<JVirtualMachineRef>)c[0]).Reference = proxyEnv.VirtualMachine.Reference);
			proxyEnv.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()).Returns(JResult.Ok);

			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			proxyEnv.Received(1).GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>());
			IVirtualMachine vm = env.VirtualMachine;

			proxyEnv.When(e => e.FatalError(Arg.Any<ReadOnlyValPtr<Byte>>())).Do(c =>
			{
				CString cstr = ((ReadOnlyValPtr<Byte>)c[0]).Pointer.GetUnsafeCString(fatalErrorUtf8Message.Length);
				Assert.Equal(fatalErrorUtf8Message, cstr);
			});

			vm.FatalError(fatalErrorMessage);
			proxyEnv.Received(1).FatalError(Arg.Any<ReadOnlyValPtr<Byte>>());

			proxyEnv.ClearReceivedCalls();

			vm.FatalError(fatalErrorUtf8Message);
			proxyEnv.Received(1).FatalError(Arg.Any<ReadOnlyValPtr<Byte>>());
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	internal void InvocationTest(Boolean forceLocal)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JLocalObject? jLocal = default;
		JGlobal? jGlobal = default;
		JWeak? jWeak = default;
		try
		{
			using IInvokedVirtualMachine invoked = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference,
				proxyEnv.Reference, out IEnvironment env);
			Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
			Assert.Equal(proxyEnv.Reference, env.Reference);
			Assert.True((invoked as JVirtualMachine)!.IsAlive);
			Assert.True((env as JEnvironment)!.IsAttached);

			JGlobalRef globalRef = JVirtualMachineTests.fixture.Create<JGlobalRef>();
			JWeakRef weakRef = JVirtualMachineTests.fixture.Create<JWeakRef>();
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(weakRef);
			proxyEnv.NewWeakGlobalRef(globalRef.Value).Returns(weakRef);
			proxyEnv.NewGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(globalRef);
			proxyEnv.NewGlobalRef(weakRef.Value).Returns(globalRef);
			proxyEnv.NewObject(proxyEnv.VoidObjectLocalRef, Arg.Any<JMethodId>(), ReadOnlyValPtr<JValueWrapper>.Zero)
			        .Returns(JVirtualMachineTests.fixture.Create<JObjectLocalRef>());

			Assert.True((invoked as JVirtualMachine)!.IsAlive);

			jLocal = env.ClassFeature.VoidObject;
			jWeak = jLocal.Weak;
			jGlobal = jLocal.Global;
		}
		finally
		{
			Assert.Equal(default, (jLocal?.LocalReference).GetValueOrDefault());
			Assert.NotEqual(default, (jWeak?.Reference).GetValueOrDefault());
			Assert.NotEqual(default, (jGlobal?.Reference).GetValueOrDefault());

			Assert.Throws<InvalidOperationException>(() => Assert.True(JObject.IsNullOrDefault(jLocal)));
			Assert.False(JObject.IsNullOrDefault(jWeak));
			Assert.False(JObject.IsNullOrDefault(jGlobal));

			if (forceLocal) jLocal?.SetValue(proxyEnv.VoidObjectLocalRef);

			jLocal?.Dispose();
			jWeak?.Dispose();
			jGlobal?.Dispose();

			Assert.True(JObject.IsNullOrDefault(jLocal));
			Assert.True(JObject.IsNullOrDefault(jWeak));
			Assert.True(JObject.IsNullOrDefault(jGlobal));

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.False(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void NestedInvocationTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			using IInvokedVirtualMachine invoked = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference,
				proxyEnv.Reference, out IEnvironment env);
			Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
			Assert.Equal(proxyEnv.Reference, env.Reference);
			Assert.True((invoked as JVirtualMachine)!.IsAlive);
			Assert.True((env as JEnvironment)!.IsAttached);

			using (IInvokedVirtualMachine invoked2 =
			       JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference,
			                                         out IEnvironment env2))
			{
				Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
				Assert.Equal(proxyEnv.Reference, env.Reference);
				Assert.Same(invoked, invoked2);
				Assert.Same(env, env2);

				Assert.True((invoked2 as JVirtualMachine)!.IsAlive);
				Assert.True((env2 as JEnvironment)!.IsAttached);
			}

			Assert.False((invoked as JVirtualMachine)!.IsAlive);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.False(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void NoInvocationTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			using (IInvokedVirtualMachine invoked = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference,
				       proxyEnv.Reference, out IEnvironment env))
			{
				Assert.NotSame(vm, invoked);
				Assert.Same(vm.GetEnvironment(), env);

				Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
				Assert.Equal(proxyEnv.Reference, env.Reference);
				Assert.True((invoked as JVirtualMachine)!.IsAlive);
				Assert.True((env as JEnvironment)!.IsAttached);
			}

			Assert.True((vm as JVirtualMachine)!.IsAlive);
			Assert.True((vm.GetEnvironment() as JEnvironment)!.IsAttached);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}

	private static JNativeCallEntry GetInstanceEntry(JMethodDefinition.Parameterless definition,
		out ObjectTracker tracker)
	{
		NativeVoidParameterless<JObjectLocalRef> instanceMethod = new();
		tracker = new() { WeakReference = new(instanceMethod), FinalizerFlag = instanceMethod.IsDisposed, };
		return JNativeCallEntry.CreateParameterless(definition, instanceMethod.VoidCall);
	}
	private static JNativeCallEntry GetStaticEntry(JMethodDefinition.Parameterless definition,
		out ObjectTracker tracker)
	{
		NativeVoidParameterless<JClassLocalRef> instanceMethod = new();
		tracker = new() { WeakReference = new(instanceMethod), FinalizerFlag = instanceMethod.IsDisposed, };
		return JNativeCallEntry.CreateParameterless(definition, instanceMethod.VoidCall);
	}
	private static JGlobal CreateThreadGroup(IVirtualMachine vm, JGlobalRef globalRef)
	{
		EnvironmentProxy proxy = EnvironmentProxy.CreateEnvironment();
		JClassObject jClassClass = new(proxy);
		CStringSequence classInformation = MetadataHelper.GetClassInformation("java/lang/ThreadGroup"u8, false);
		JClassObject classLoaderClass = new(jClassClass, new TypeInformation(classInformation));
		return new(vm, new(classLoaderClass), globalRef);
	}
	private static Task RegisterTestObject()
	{
		TaskCompletionSource tcs = new();
		Thread registrationThread = new(() =>
		{
			Boolean register = JVirtualMachine.Register<JTestObject>();
			if (register)
				Assert.NotNull(JTestObject.GetThreadMetadata());
		});
		Thread wrapperThread = new(() =>
		{
			try
			{
				registrationThread.Start();
				registrationThread.Join();
				tcs.SetResult();
			}
			catch (Exception ex)
			{
				tcs.SetException(ex);
			}
		});
		wrapperThread.Start();
		return tcs.Task;
	}
	private static Dictionary<MainClass, IFixedPointer.IDisposable> GetMainNamePointer()
	{
		Dictionary<MainClass, IFixedPointer.IDisposable> mainPointer =
			JVirtualMachineTests.mainMetadata.ToDictionary(p => p.Key, p => p.Value.Information.GetFixedPointer());
		return mainPointer;
	}
	private static Dictionary<MainClass, JGlobalRef> GetMainGlobalRef(NativeInterfaceProxy proxyEnv)
	{
		Dictionary<MainClass, JGlobalRef> mainGlobalRef = [];
		mainGlobalRef.Add(MainClass.Class, proxyEnv.VirtualMachine.ClassGlobalRef);
		mainGlobalRef.Add(MainClass.Throwable, proxyEnv.VirtualMachine.ThrowableGlobalRef);
		mainGlobalRef.Add(MainClass.StackTraceElement, proxyEnv.VirtualMachine.StackTraceElementGlobalRef);

		mainGlobalRef.Add(MainClass.VoidPrimitive, proxyEnv.VirtualMachine.VoidGlobalRef);
		mainGlobalRef.Add(MainClass.BooleanPrimitive, proxyEnv.VirtualMachine.BooleanGlobalRef);
		mainGlobalRef.Add(MainClass.BytePrimitive, proxyEnv.VirtualMachine.ByteGlobalRef);
		mainGlobalRef.Add(MainClass.CharPrimitive, proxyEnv.VirtualMachine.CharGlobalRef);
		mainGlobalRef.Add(MainClass.DoublePrimitive, proxyEnv.VirtualMachine.DoubleGlobalRef);
		mainGlobalRef.Add(MainClass.FloatPrimitive, proxyEnv.VirtualMachine.FloatGlobalRef);
		mainGlobalRef.Add(MainClass.IntPrimitive, proxyEnv.VirtualMachine.IntGlobalRef);
		mainGlobalRef.Add(MainClass.LongPrimitive, proxyEnv.VirtualMachine.LongGlobalRef);
		mainGlobalRef.Add(MainClass.ShortPrimitive, proxyEnv.VirtualMachine.ShortGlobalRef);
		return mainGlobalRef;
	}
	private static Dictionary<MainClass, JClassLocalRef> GetMainLocalRef(NativeInterfaceProxy proxyEnv)
	{
		Dictionary<MainClass, JClassLocalRef> mainClassRef = [];
		mainClassRef.Add(MainClass.Class, proxyEnv.ClassLocalRef);
		mainClassRef.Add(MainClass.Throwable, proxyEnv.ThrowableLocalRef);
		mainClassRef.Add(MainClass.StackTraceElement, proxyEnv.StackTraceObjectLocalRef);

		mainClassRef.Add(MainClass.VoidObject, proxyEnv.VoidObjectLocalRef);
		mainClassRef.Add(MainClass.BooleanObject, proxyEnv.BooleanObjectLocalRef);
		mainClassRef.Add(MainClass.ByteObject, proxyEnv.ByteObjectLocalRef);
		mainClassRef.Add(MainClass.CharacterObject, proxyEnv.CharacterObjectLocalRef);
		mainClassRef.Add(MainClass.DoubleObject, proxyEnv.DoubleObjectLocalRef);
		mainClassRef.Add(MainClass.FloatObject, proxyEnv.FloatObjectLocalRef);
		mainClassRef.Add(MainClass.IntegerObject, proxyEnv.IntegerObjectLocalRef);
		mainClassRef.Add(MainClass.LongObject, proxyEnv.LongObjectLocalRef);
		mainClassRef.Add(MainClass.ShortObject, proxyEnv.ShortObjectLocalRef);

		mainClassRef.Add(MainClass.VoidPrimitive, proxyEnv.VoidPrimitiveLocalRef);
		mainClassRef.Add(MainClass.BooleanPrimitive, proxyEnv.BooleanPrimitiveLocalRef);
		mainClassRef.Add(MainClass.BytePrimitive, proxyEnv.BytePrimitiveLocalRef);
		mainClassRef.Add(MainClass.CharPrimitive, proxyEnv.CharPrimitiveLocalRef);
		mainClassRef.Add(MainClass.DoublePrimitive, proxyEnv.DoublePrimitiveLocalRef);
		mainClassRef.Add(MainClass.FloatPrimitive, proxyEnv.FloatPrimitiveLocalRef);
		mainClassRef.Add(MainClass.IntPrimitive, proxyEnv.IntPrimitiveLocalRef);
		mainClassRef.Add(MainClass.LongPrimitive, proxyEnv.LongPrimitiveLocalRef);
		mainClassRef.Add(MainClass.ShortPrimitive, proxyEnv.ShortPrimitiveLocalRef);
		return mainClassRef;
	}
	private static Dictionary<MainClass, JFieldId> GetTypeField(NativeInterfaceProxy proxyEnv)
	{
		Dictionary<MainClass, JFieldId> mainTypeField = [];
		mainTypeField.Add(MainClass.VoidObject, proxyEnv.VoidTypeFieldId);
		mainTypeField.Add(MainClass.BooleanObject, proxyEnv.BooleanTypeFieldId);
		mainTypeField.Add(MainClass.ByteObject, proxyEnv.ByteTypeFieldId);
		mainTypeField.Add(MainClass.CharacterObject, proxyEnv.CharacterTypeFieldId);
		mainTypeField.Add(MainClass.DoubleObject, proxyEnv.DoubleTypeFieldId);
		mainTypeField.Add(MainClass.FloatObject, proxyEnv.FloatTypeFieldId);
		mainTypeField.Add(MainClass.IntegerObject, proxyEnv.IntegerTypeFieldId);
		mainTypeField.Add(MainClass.LongObject, proxyEnv.LongTypeFieldId);
		mainTypeField.Add(MainClass.ShortObject, proxyEnv.ShortTypeFieldId);
		return mainTypeField;
	}
#pragma warning disable CA1859
	private static readonly IReadOnlyDictionary<MainClass, JDataTypeMetadata> mainMetadata =
		new Dictionary<MainClass, JDataTypeMetadata>
		{
			{ MainClass.Class, IDataType.GetMetadata<JClassObject>() },
			{ MainClass.Throwable, IDataType.GetMetadata<JThrowableObject>() },
			{ MainClass.StackTraceElement, IDataType.GetMetadata<JStackTraceElementObject>() },
			{ MainClass.VoidObject, IDataType.GetMetadata<JVoidObject>() },
			{ MainClass.BooleanObject, IDataType.GetMetadata<JBooleanObject>() },
			{ MainClass.ByteObject, IDataType.GetMetadata<JByteObject>() },
			{ MainClass.CharacterObject, IDataType.GetMetadata<JCharacterObject>() },
			{ MainClass.DoubleObject, IDataType.GetMetadata<JDoubleObject>() },
			{ MainClass.FloatObject, IDataType.GetMetadata<JFloatObject>() },
			{ MainClass.IntegerObject, IDataType.GetMetadata<JIntegerObject>() },
			{ MainClass.LongObject, IDataType.GetMetadata<JLongObject>() },
			{ MainClass.ShortObject, IDataType.GetMetadata<JShortObject>() },
			{ MainClass.VoidPrimitive, JPrimitiveTypeMetadata.VoidMetadata },
			{ MainClass.BooleanPrimitive, IDataType.GetMetadata<JBoolean>() },
			{ MainClass.BytePrimitive, IDataType.GetMetadata<JByte>() },
			{ MainClass.CharPrimitive, IDataType.GetMetadata<JChar>() },
			{ MainClass.DoublePrimitive, IDataType.GetMetadata<JDouble>() },
			{ MainClass.FloatPrimitive, IDataType.GetMetadata<JFloat>() },
			{ MainClass.IntPrimitive, IDataType.GetMetadata<JInt>() },
			{ MainClass.LongPrimitive, IDataType.GetMetadata<JLong>() },
			{ MainClass.ShortPrimitive, IDataType.GetMetadata<JShort>() },
		};
	private static readonly IReadOnlyDictionary<MainClass, MainClass> mainWrapper = new Dictionary<MainClass, MainClass>
	{
		{ MainClass.VoidObject, MainClass.VoidPrimitive },
		{ MainClass.BooleanObject, MainClass.BooleanPrimitive },
		{ MainClass.ByteObject, MainClass.BytePrimitive },
		{ MainClass.CharacterObject, MainClass.CharPrimitive },
		{ MainClass.DoubleObject, MainClass.DoublePrimitive },
		{ MainClass.FloatObject, MainClass.FloatPrimitive },
		{ MainClass.IntegerObject, MainClass.IntPrimitive },
		{ MainClass.LongObject, MainClass.LongPrimitive },
		{ MainClass.ShortObject, MainClass.ShortPrimitive },
		{ MainClass.VoidPrimitive, MainClass.VoidObject },
		{ MainClass.BooleanPrimitive, MainClass.BooleanObject },
		{ MainClass.BytePrimitive, MainClass.ByteObject },
		{ MainClass.CharPrimitive, MainClass.CharacterObject },
		{ MainClass.DoublePrimitive, MainClass.DoubleObject },
		{ MainClass.FloatPrimitive, MainClass.FloatObject },
		{ MainClass.IntPrimitive, MainClass.IntegerObject },
		{ MainClass.LongPrimitive, MainClass.LongObject },
		{ MainClass.ShortPrimitive, MainClass.ShortObject },
	};
#pragma warning restore CA1859
}