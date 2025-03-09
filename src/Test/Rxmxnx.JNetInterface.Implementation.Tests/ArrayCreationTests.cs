namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ArrayCreationTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void StaticFailCreation()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			ArrayCreationTests.AssertStaticFail<JBoolean, JByte>(env, true);
			ArrayCreationTests.AssertStaticFail<JLong, JInt>(env, 12L);
			ArrayCreationTests.AssertStaticFail<JClassObject, JChar>(env, env.ClassFeature.ClassObject);
			ArrayCreationTests.AssertStaticFail<JDouble, JDoubleObject>(env, Math.PI);

			JObjectLocalRef localRef = ArrayCreationTests.fixture.Create<JObjectLocalRef>();
			using (JShortObject jShort = new(env.ClassFeature.GetClass<JShortObject>(), localRef, -5))
				ArrayCreationTests.AssertStaticFail<JShortObject, JShort>(env, jShort);

			proxyEnv.Received(0).IsAssignableFrom(Arg.Any<JClassLocalRef>(), Arg.Any<JClassLocalRef>());
			proxyEnv.Received(0).NewBooleanArray(Arg.Any<Int32>());
			proxyEnv.Received(0).NewLongArray(Arg.Any<Int32>());
			proxyEnv.Received(0).NewDoubleArray(Arg.Any<Int32>());
			proxyEnv.Received(0)
			        .NewObjectArray(Arg.Any<Int32>(), Arg.Any<JClassLocalRef>(), Arg.Any<JObjectLocalRef>());
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
	[Fact]
	internal void StaticCreation()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);

			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>())
			        .Returns(_ => ArrayCreationTests.fixture.Create<JClassLocalRef>());
			proxyEnv.NewBooleanArray(Arg.Any<Int32>())
			        .Returns(_ => ArrayCreationTests.fixture.Create<JBooleanArrayLocalRef>());
			proxyEnv.NewLongArray(Arg.Any<Int32>())
			        .Returns(_ => ArrayCreationTests.fixture.Create<JLongArrayLocalRef>());
			proxyEnv.NewDoubleArray(Arg.Any<Int32>())
			        .Returns(_ => ArrayCreationTests.fixture.Create<JDoubleArrayLocalRef>());
			proxyEnv.NewObjectArray(Arg.Any<Int32>(), Arg.Any<JClassLocalRef>(), Arg.Any<JObjectLocalRef>())
			        .Returns(_ => ArrayCreationTests.fixture.Create<JObjectArrayLocalRef>());

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			ArrayCreationTests.AssertStatic<JBoolean, JBoolean>(env, true);
			ArrayCreationTests.AssertStatic<JLong, JLong>(env, 10L);
			ArrayCreationTests.AssertStatic<JDouble, JDouble>(env, Math.E);
			ArrayCreationTests.AssertStatic<JClassObject, JClassObject>(env, env.ClassFeature.ClassObject);
			ArrayCreationTests.AssertStatic<JSerializableObject, JClassObject>(
				env, env.ClassFeature.ClassObject.CastTo<JSerializableObject>());

			// JObjectLocalRef localRef = ArrayCreationTests.fixture.Create<JObjectLocalRef>();
			// using (JShortObject jShort = new(env.ClassFeature.GetClass<JShortObject>(), localRef, -5))
			// 	ArrayCreationTests.AssertStatic<JNumberObject, JShortObject>(env, jShort);
			//
			proxyEnv.Received(0).IsAssignableFrom(Arg.Any<JClassLocalRef>(), Arg.Any<JClassLocalRef>());
			proxyEnv.Received().NewBooleanArray(Arg.Any<Int32>());
			proxyEnv.Received().NewLongArray(Arg.Any<Int32>());
			proxyEnv.Received().NewDoubleArray(Arg.Any<Int32>());
			proxyEnv.Received().NewObjectArray(Arg.Any<Int32>(), Arg.Any<JClassLocalRef>(), Arg.Any<JObjectLocalRef>());
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

	private static void AssertStaticFail<TElementType, TArrayElementType>(IEnvironment env, TElementType value)
		where TElementType : IDataType<TElementType> where TArrayElementType : IDataType<TArrayElementType>
	{
		using JClassObject jClass = env.ClassFeature.GetClass<TArrayElementType>();

		Assert.Throws<InvalidCastException>(() => JArrayObject<TElementType>.Create(jClass, 10, value));
		Assert.Throws<InvalidCastException>(() => JArrayObject<TElementType>.Create(jClass, 10));
		Assert.Throws<InvalidCastException>(() => JArrayObject<TElementType>.Create(jClass, 10, default!));
	}
	private static void AssertStatic<TElementType, TArrayElementType>(IEnvironment env, TElementType value)
		where TElementType : IDataType<TElementType> where TArrayElementType : IDataType<TArrayElementType>
	{
		using JClassObject jClass = env.ClassFeature.GetClass<TArrayElementType>();

		JArrayObject<TElementType>.Create(jClass, 10, value).Dispose();
		JArrayObject<TElementType>.Create(jClass, 10).Dispose();
		JArrayObject<TElementType>.Create(jClass, 10, default!).Dispose();
	}
}