namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ClassParseTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	private static void ClassTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			IClassFeature classFeature = env.ClassFeature;
			Assert.Equal(classFeature.VoidPrimitive, classFeature.AsClassObject(classFeature.VoidPrimitive));
			Assert.Equal(classFeature.VoidObject, classFeature.AsClassObject(classFeature.VoidObject));
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
	private static void GlobalTest(Boolean useNew)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JGlobalRef globalRef = ClassParseTests.fixture.Create<JGlobalRef>();
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);
			proxyEnv.NewGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(globalRef);

			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			IClassFeature classFeature = env.ClassFeature;
			JGlobal jGlobal = useNew ? classFeature.VoidObject.Global : new(classFeature.VoidObject, globalRef);
			Assert.Equal(classFeature.VoidObject, classFeature.AsClassObject(jGlobal));
			if (useNew) Assert.Equal(jGlobal, classFeature.VoidObject.Global);

			proxyEnv.Received(0).IsInstanceOf(globalRef.Value,
			                                  JClassLocalRef.FromReference(proxyEnv.VirtualMachine.ClassGlobalRef));
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
	private static void WeakTest(Boolean useNew)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JWeakRef weakRef = ClassParseTests.fixture.Create<JWeakRef>();
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(weakRef);

			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			IClassFeature classFeature = env.ClassFeature;
			JWeak jWeak = useNew ? classFeature.VoidObject.Weak : new(classFeature.VoidObject, weakRef);
			Assert.Equal(classFeature.VoidObject, classFeature.AsClassObject(jWeak));
			if (useNew) Assert.Equal(jWeak, classFeature.VoidObject.Weak);
			proxyEnv.Received(0).IsInstanceOf(weakRef.Value,
			                                  JClassLocalRef.FromReference(proxyEnv.VirtualMachine.ClassGlobalRef));
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
	private static void LocalTest(Boolean sameRef)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JGlobalRef globalRef = ClassParseTests.fixture.Create<JGlobalRef>();
			JWeakRef weakRef = ClassParseTests.fixture.Create<JWeakRef>();
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.NewWeakGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(weakRef);
			proxyEnv.NewWeakGlobalRef(globalRef.Value).Returns(weakRef);
			proxyEnv.NewGlobalRef(proxyEnv.VoidObjectLocalRef.Value).Returns(globalRef);
			proxyEnv.NewGlobalRef(weakRef.Value).Returns(globalRef);
			proxyEnv.NewObject(proxyEnv.VoidObjectLocalRef, Arg.Any<JMethodId>(), ReadOnlyValPtr<JValueWrapper>.Zero)
			        .Returns(ClassParseTests.fixture.Create<JObjectLocalRef>());
			JObjectLocalRef localRef = sameRef ?
				proxyEnv.VoidObjectLocalRef.Value :
				ClassParseTests.fixture.Create<JObjectLocalRef>();
			JStringLocalRef stringRef = ClassParseTests.fixture.Create<JStringLocalRef>();
			JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JVoidObject>();
			using IFixedPointer.IDisposable clsCtx = typeMetadata.Information.GetFixedPointer();

			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			IClassFeature classFeature = env.ClassFeature;
			proxyEnv.CallObjectMethod(localRef, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(stringRef.Value);
			proxyEnv.GetStringUtfLength(stringRef).Returns(typeMetadata.ClassName.Length);
			proxyEnv.GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)clsCtx.Pointer);

			if (sameRef)
				Assert.Throws<InvalidOperationException>(
					() => new JConstructorDefinition.Parameterless().New<JVoidObject>(env));

			using JLocalObject localObject = IClassType.GetMetadata<JLocalObject>()
			                                           .CreateInstance(env.ClassFeature.ClassObject, localRef, true);
			Assert.Equal(classFeature.VoidObject, classFeature.AsClassObject(localObject));

			if (sameRef)
			{
				using JWeak jWeak = localObject.Weak;
				using JGlobal jGlobal = classFeature.VoidObject.Global;

				Assert.Equal(jWeak, classFeature.VoidObject.Weak);
				Assert.Equal(jGlobal, localObject.Global);
			}

			proxyEnv.Received(0).IsInstanceOf(globalRef.Value,
			                                  JClassLocalRef.FromReference(proxyEnv.VirtualMachine.ClassGlobalRef));
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
	private static void ErrorTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JInt value = ClassParseTests.fixture.Create<Int32>();
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			IClassFeature classFeature = env.ClassFeature;
			using JLocalObject wrapperValue = TestUtilities.CreateWrapper(proxyEnv, value);
			proxyEnv.IsInstanceOf(wrapperValue.Reference,
			                      JClassLocalRef.FromReference(proxyEnv.VirtualMachine.ClassGlobalRef)).Returns(false);
			Assert.Throws<ArgumentException>(() => classFeature.AsClassObject(wrapperValue));

			proxyEnv.Received(1).IsInstanceOf(wrapperValue.Reference,
			                                  JClassLocalRef.FromReference(
				                                  proxyEnv.VirtualMachine.ClassGlobalRef.Value));
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
}