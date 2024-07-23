namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public class DeadThreadTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(ThreadPurpose.RemoveGlobalReference)]
	[InlineData(ThreadPurpose.ReleaseSequence)]
	[InlineData(ThreadPurpose.ExceptionExecution)]
	[InlineData(ThreadPurpose.CheckAssignability)]
	[InlineData(ThreadPurpose.CreateGlobalReference)]
	[InlineData(ThreadPurpose.CheckGlobalReference)]
	[InlineData(ThreadPurpose.SynchronizeGlobalReference)]
	[InlineData(ThreadPurpose.FatalError)]
	internal void Test(ThreadPurpose purpose)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		InvokeInterfaceProxy proxyVm = proxyEnv.VirtualMachine;
		IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyVm.Reference);

		try
		{
			proxyEnv.NewWeakGlobalRef(Arg.Any<JObjectLocalRef>())
			        .Returns(c => DeadThreadTests.fixture.Create<JWeakRef>());

			using JClassObject jClass = JEnvironment.GetEnvironment(proxyEnv.Reference).ClassObject;
			using JGlobal jGlobal = jClass.Global;
			using JClassObject jObjectClass = jClass.Environment.ClassFeature.Object;
			using JClassObject jIntClass = jClass.Environment.ClassFeature.IntPrimitive;
			using JWeak jWeak = jIntClass.Weak;

			proxyEnv.FinalizeProxy(false);
			JVirtualMachine.RemoveEnvironment(vm.Reference, proxyEnv.Reference);

			proxyVm.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(JResult.DetachedThreadError);
			proxyVm.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                            Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>())
			       .Returns(JResult.DetachedThreadError);
			proxyVm.AttachCurrentThreadAsDaemon(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                                    Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>())
			       .Returns(JResult.DetachedThreadError);

			proxyVm.ClearReceivedCalls();
			proxyEnv.ClearReceivedCalls();

			if (purpose is ThreadPurpose.ExceptionExecution or ThreadPurpose.CreateGlobalReference or
			    ThreadPurpose.FatalError)
			{
				Assert.Throws<JniException>(() => vm.CreateThread(purpose));
				return;
			}

			using IThread thread = vm.CreateThread(purpose);
			DeadThread deadThread = Assert.IsType<DeadThread>(thread);
			IArrayFeature arrayFeature = thread.ArrayFeature;
			IClassFeature classFeature = thread.ClassFeature;
			IReferenceFeature referenceFeature = thread.ReferenceFeature;
			IStringFeature stringFeature = thread.StringFeature;

			Assert.Same(deadThread, arrayFeature);
			Assert.Same(deadThread, classFeature);
			Assert.Same(deadThread, referenceFeature);
			Assert.Same(deadThread, stringFeature);

			Assert.Same(vm, thread.VirtualMachine);
			Assert.Equal(CString.Empty, thread.Name);
			Assert.False(thread.Attached);
			Assert.False(thread.Daemon);
			Assert.False(thread.NoProxy);

			Assert.True(thread.JniSecure());
			Assert.True(thread.IsValidationAvoidable(jGlobal));
			Assert.True(thread.IsSameObject(jClass, jClass));
			Assert.True(thread.IsSameObject(jIntClass, jIntClass));
			Assert.True(thread.IsSameObject(jGlobal, jGlobal));
			Assert.True(thread.IsSameObject(jWeak, jWeak));
			Assert.False(thread.IsSameObject(jClass, jGlobal));
			Assert.False(thread.IsSameObject(jClass, jGlobal));
			Assert.False(thread.IsSameObject(jIntClass, jGlobal));
			Assert.False(thread.IsSameObject(jIntClass, jWeak));
			Assert.Equal(JReferenceType.InvalidRefType, thread.GetReferenceType(jIntClass));

			DeadThreadTests.EnvironmentThrowsTest(thread);

			Assert.Equal(0, arrayFeature.GetArrayLength(jClass));
			Assert.Equal(0, arrayFeature.GetArrayLength(jIntClass));
			Assert.Equal(0, arrayFeature.GetArrayLength(jGlobal));
			Assert.Equal(0, arrayFeature.GetArrayLength(jWeak));

			arrayFeature.ReleasePrimitiveSequence<JInt>(default, default, default);
			arrayFeature.ReleasePrimitiveCriticalSequence(default, default);

			DeadThreadTests.ArrayFeatureThrowTest(arrayFeature, jClass, jIntClass);

			Assert.True(classFeature.IsInstanceOf<JClassObject>(jClass));
			Assert.True(classFeature.IsInstanceOf<JClassObject>(jIntClass));
			Assert.True(classFeature.IsInstanceOf<JClassObject>(jGlobal));
			Assert.True(classFeature.IsInstanceOf<JClassObject>(jWeak));

			Assert.True(classFeature.IsInstanceOf(jClass, jClass));
			Assert.True(classFeature.IsInstanceOf(jIntClass, jClass));
			Assert.True(classFeature.IsInstanceOf(jGlobal, jClass));
			Assert.True(classFeature.IsInstanceOf(jWeak, jClass));

			Assert.True(classFeature.IsInstanceOf<JLocalObject>(jClass));
			Assert.True(classFeature.IsInstanceOf<JLocalObject>(jIntClass));
			Assert.False(classFeature.IsInstanceOf<JLocalObject>(jGlobal));
			Assert.False(classFeature.IsInstanceOf<JLocalObject>(jWeak));

			DeadThreadTests.ClassFeatureThrowTest(classFeature, jClass, jIntClass);

			referenceFeature.MonitorExit(default);
			referenceFeature.LocalLoad(jWeak, jClass);
			referenceFeature.LocalLoad(jGlobal, jIntClass);
			Assert.True(referenceFeature.Unload(jClass));
			Assert.True(referenceFeature.Unload(jIntClass));
			Assert.True(referenceFeature.Unload(jGlobal));
			Assert.True(referenceFeature.Unload(jWeak));
			Assert.False(referenceFeature.IsParameter(jClass));
			Assert.False(referenceFeature.IsParameter(jIntClass));

			DeadThreadTests.ReferenceFeatureThrowTest(referenceFeature, jClass, jIntClass, jGlobal, jWeak);

			Assert.Equal(0, stringFeature.GetLength(jClass));
			Assert.Equal(0, stringFeature.GetLength(jIntClass));
			Assert.Equal(0, stringFeature.GetLength(jGlobal));
			Assert.Equal(0, stringFeature.GetLength(jWeak));
			Assert.Equal(0, stringFeature.GetUtf8Length(jClass));
			Assert.Equal(0, stringFeature.GetUtf8Length(jIntClass));
			Assert.Equal(0, stringFeature.GetUtf8Length(jGlobal));
			Assert.Equal(0, stringFeature.GetUtf8Length(jWeak));

			stringFeature.ReleaseSequence(default, default);
			stringFeature.ReleaseUtf8Sequence(default, default);
			stringFeature.ReleaseCriticalSequence(default, default);

			DeadThreadTests.StringFeatureThrowTest(stringFeature);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
		}
	}

	private static void StringFeatureThrowTest(IStringFeature stringFeature)
	{
		Assert.Throws<InvalidOperationException>(() => stringFeature.GetCopy(default!, Span<Char>.Empty));
		Assert.Throws<InvalidOperationException>(() => stringFeature.GetUtf8Copy(default!, Span<Byte>.Empty));
		Assert.Throws<InvalidOperationException>(() => stringFeature.GetSequence(default!, default));
		Assert.Throws<InvalidOperationException>(() => stringFeature.GetUtf8Sequence(default!, default));
		Assert.Throws<InvalidOperationException>(() => stringFeature.Create(Span<Char>.Empty));
		Assert.Throws<InvalidOperationException>(() => stringFeature.Create(Span<Byte>.Empty));
		Assert.Throws<InvalidOperationException>(() => stringFeature.GetSequence(default, out _));
		Assert.Throws<InvalidOperationException>(() => stringFeature.GetUtf8Sequence(default, out _));
		Assert.Throws<InvalidOperationException>(() => stringFeature.GetCriticalSequence(default));
	}
	private static void ReferenceFeatureThrowTest(IReferenceFeature referenceFeature, JClassObject jClass,
		JClassObject jIntClass, JGlobal jGlobal, JWeak jWeak)
	{
		Assert.Throws<InvalidOperationException>(() => referenceFeature.GetLifetime(jClass, default));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.CreateWrapper<JInt>(default));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.MonitorEnter(default));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.Create<JGlobal>(jIntClass));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.Create<JWeak>(jClass));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.CreateWeak(jGlobal));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.CreateWeak(jWeak));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.GetSynchronizer(jClass));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.GetSynchronizer(jIntClass));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.GetSynchronizer(jGlobal));
		Assert.Throws<InvalidOperationException>(() => referenceFeature.GetSynchronizer(jWeak));
	}
	private static void ClassFeatureThrowTest(IClassFeature classFeature, JClassObject jClass, JClassObject jIntClass)
	{
		Assert.Throws<InvalidOperationException>(() => classFeature.VoidPrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.BooleanPrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.BytePrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.CharPrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.DoublePrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.FloatPrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.IntPrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.LongPrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.ShortPrimitive);
		Assert.Throws<InvalidOperationException>(() => classFeature.ClassObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.VoidObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.BooleanObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.ByteObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.CharacterObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.DoubleObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.FloatObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.IntegerObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.LongObject);
		Assert.Throws<InvalidOperationException>(() => classFeature.ShortObject);

		Assert.Throws<InvalidOperationException>(() => classFeature.AsClassObject(jClass.Reference));
		Assert.Throws<InvalidOperationException>(() => classFeature.AsClassObject(jClass));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetClass<JIntegerObject>());
		Assert.Throws<InvalidOperationException>(
			() => classFeature.GetObjectClass(ILocalObject.CreateMetadata(jClass)));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetClass(IDataType.GetMetadata<JLocalObject>()));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetObjectClass(jClass));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetSuperClass(jClass));
		Assert.Throws<InvalidOperationException>(() => classFeature.IsAssignableFrom(jClass, jIntClass));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetTypeMetadata(jClass));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetModule(jClass));
		Assert.Throws<InvalidOperationException>(
			() => classFeature.ThrowNew<JExceptionObject>(new CString(() => "msg"u8), false));
		Assert.Throws<InvalidOperationException>(() => classFeature.ThrowNew<JExceptionObject>("msg", false));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetClass("package/ClassName"u8));
		Assert.Throws<InvalidOperationException>(
			() => classFeature.LoadClass("package/ClassName"u8, ReadOnlySpan<Byte>.Empty));
		Assert.Throws<InvalidOperationException>(() => classFeature.LoadClass<JTestObject>(ReadOnlySpan<Byte>.Empty));
		Assert.Throws<InvalidOperationException>(() => classFeature.GetClassInfo(jClass, out _, out _, out _));
	}
	private static void ArrayFeatureThrowTest(IArrayFeature arrayFeature, JClassObject jClass, JClassObject jIntClass)
	{
		Assert.Throws<InvalidOperationException>(() => arrayFeature.CreateArray<JInt>(0));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.CreateArray(0, jClass));
		Assert.Throws<InvalidOperationException>(
			() => arrayFeature.GetElement(default(JArrayObject<JClassObject>)!, 0));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.SetElement(default!, 0, jIntClass));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.IndexOf(default!, jIntClass));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.CopyTo(default!, Array.Empty<JInt>(), default));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.GetCopy(default!, Span<JInt>.Empty));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.SetCopy(default!, ReadOnlySpan<JInt>.Empty));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.GetSequence(default(JArrayObject<JInt>)!, default));
		Assert.Throws<InvalidOperationException>(
			() => arrayFeature.GetCriticalSequence(default(JArrayObject<JInt>)!, default));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.GetPrimitiveSequence<JInt>(default, out _));
		Assert.Throws<InvalidOperationException>(() => arrayFeature.GetPrimitiveCriticalSequence(default));
	}
	private static void EnvironmentThrowsTest(IEnvironment thread)
	{
		Assert.Throws<InvalidOperationException>(() => thread.AccessFeature);
		Assert.Throws<InvalidOperationException>(() => thread.FunctionSet);
		Assert.Throws<InvalidOperationException>(() => thread.NioFeature);
		Assert.Throws<InvalidOperationException>(() => thread.UsedStackBytes);
		Assert.Throws<InvalidOperationException>(() => thread.UsableStackBytes);
		Assert.Throws<InvalidOperationException>(() => thread.UsableStackBytes = default);
		Assert.Throws<InvalidOperationException>(() => thread.PendingException);
		Assert.Throws<InvalidOperationException>(() => thread.PendingException = default);
		Assert.Throws<InvalidOperationException>(() => thread.LocalCapacity);
		Assert.Throws<InvalidOperationException>(() => thread.LocalCapacity = default);
		Assert.Throws<InvalidOperationException>(() => thread.Version);
		Assert.Throws<InvalidOperationException>(() => thread.Reference);

		Assert.Throws<InvalidOperationException>(() => thread.IsVirtual(default!));
		Assert.Throws<InvalidOperationException>(thread.DescribeException);
		Assert.Throws<InvalidOperationException>(() => thread.WithFrame(1, default!));
		Assert.Throws<InvalidOperationException>(() => thread.WithFrame<Int32>(1, default!));
		Assert.Throws<InvalidOperationException>(() => thread.WithFrame<Int32, Int32>(1, default, default!));
		Assert.Throws<InvalidOperationException>(() => thread.WithFrame<Int32>(1, default!));
		Assert.Throws<InvalidOperationException>(() => thread.WithFrame<Int32>(1, default, default!));
	}
}