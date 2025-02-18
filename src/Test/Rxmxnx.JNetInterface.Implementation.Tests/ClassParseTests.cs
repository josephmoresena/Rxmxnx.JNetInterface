namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ClassParseTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData]
	[InlineData(true)]
	internal void ClassTest(Boolean lowerVersion = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		if (lowerVersion) proxyEnv.GetVersion().Returns(NativeInterface4.RequiredVersion);
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
	[InlineData(false, true)]
	[InlineData(true, true)]
	internal void GlobalTest(Boolean useNew, Boolean lowerVersion = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		if (lowerVersion) proxyEnv.GetVersion().Returns(NativeInterface4.RequiredVersion);
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
	[InlineData(false, true)]
	[InlineData(true, true)]
	internal void WeakTest(Boolean useNew, Boolean lowerVersion = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		if (lowerVersion) proxyEnv.GetVersion().Returns(NativeInterface4.RequiredVersion);
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
	[InlineData(false, true)]
	[InlineData(true, true)]
	internal void LocalTest(Boolean sameRef, Boolean lowerVersion = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JVoidObject>();
		using IFixedPointer.IDisposable clsCtx =
			typeMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		if (lowerVersion) proxyEnv.GetVersion().Returns(NativeInterface4.RequiredVersion);
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

			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			IClassFeature classFeature = env.ClassFeature;
			proxyEnv.CallObjectMethod(localRef, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(stringRef.Value);
			proxyEnv.GetStringUtfLength(stringRef).Returns(typeMetadata.ClassName.Length);
			proxyEnv.GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)nameCtx.Pointer);

			if (sameRef)
				Assert.Throws<InvalidOperationException>(
					() => new JConstructorDefinition.Parameterless().New<JVoidObject>(env));

			using JClassObject voidClass = classFeature.VoidObject;
			using JLocalObject localObject = IClassType.GetMetadata<JLocalObject>()
			                                           .CreateInstance(env.ClassFeature.ClassObject, localRef, true);
			using JClassObject jClass = classFeature.AsClassObject(localObject);
			Assert.Equal(voidClass, jClass);

			if (sameRef)
			{
				using JWeak jWeak = localObject.Weak;
				using JGlobal jGlobal = voidClass.Global;

				Assert.Equal(jWeak, voidClass.Weak);
				Assert.Equal(jGlobal, localObject.Global);
			}

			proxyEnv.Received(0).IsInstanceOf(globalRef.Value,
			                                  JClassLocalRef.FromReference(proxyEnv.VirtualMachine.ClassGlobalRef));
		}
		finally
		{
			nameCtx.Dispose();
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void ErrorTest()
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
	[Fact]
	internal void InfoTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			JClassLocalRef classRef = ClassParseTests.fixture.Create<JClassLocalRef>();
			JStringLocalRef stringRef = ClassParseTests.fixture.Create<JStringLocalRef>();
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			JDataTypeMetadata typeMetadata = IDataType.GetMetadata<JTestObject>();
			using JClassObject jClass = new(env.ClassFeature.ClassObject, classRef);
			using IFixedPointer.IDisposable fCtx = IClassType.GetMetadata<JTestObject>().Information.GetFixedPointer();

			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(stringRef.Value);
			proxyEnv.GetStringUtfLength(stringRef).Returns(typeMetadata.ClassName.Length);
			proxyEnv.GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)fCtx.Pointer);

			Assert.Equal(typeMetadata.ClassName, jClass.Name);
			Assert.Equal(typeMetadata.Hash, jClass.Hash);

			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).CallBooleanMethod(classRef.Value, proxyEnv.VirtualMachine.ClassIsPrimitiveMethodId,
			                                       ReadOnlyValPtr<JValueWrapper>.Zero);
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