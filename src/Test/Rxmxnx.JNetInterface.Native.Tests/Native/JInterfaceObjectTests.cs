namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JInterfaceObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void InterfaceSetTest()
	{
		InterfaceSet emptyInterfaces = InterfaceSet.Empty;
		IInterfaceSet arrayInterfaces = InterfaceSet.ArraySet;
		IInterfaceSet annotationInterfaces = InterfaceSet.AnnotationSet;
		IInterfaceSet primitiveWrapperInterfaces = InterfaceSet.PrimitiveWrapperSet;

		Assert.Contains(IInterfaceType.GetMetadata<JCloneableObject>(), arrayInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), arrayInterfaces.ToArray());
		Assert.Empty(emptyInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotationObject>(), annotationInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), primitiveWrapperInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), primitiveWrapperInterfaces.ToArray());

		Assert.True(arrayInterfaces.Contains(IInterfaceType.GetMetadata<JCloneableObject>()));
		Assert.True(arrayInterfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(arrayInterfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.False(arrayInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));
		Assert.False(emptyInterfaces.Contains(IInterfaceType.GetMetadata<JCloneableObject>()));
		Assert.False(emptyInterfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(emptyInterfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.False(emptyInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));
		Assert.True(annotationInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));
		Assert.False(annotationInterfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.False(annotationInterfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.True(primitiveWrapperInterfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.True(primitiveWrapperInterfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(primitiveWrapperInterfaces.Contains(IInterfaceType.GetMetadata<JCloneableObject>()));
		Assert.False(primitiveWrapperInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));

		List<JInterfaceTypeMetadata> interfaces = [];
		arrayInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));
		emptyInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));
		annotationInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));
		primitiveWrapperInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));

		Assert.Equal(IInterfaceType.GetMetadata<JSerializableObject>(), interfaces[0]);
		Assert.Equal(IInterfaceType.GetMetadata<JCloneableObject>(), interfaces[1]);
		Assert.Equal(IInterfaceType.GetMetadata<JAnnotationObject>(), interfaces[2]);
		Assert.Equal(IInterfaceType.GetMetadata<JSerializableObject>(), interfaces[3]);
		Assert.Equal(IInterfaceType.GetMetadata<JComparableObject>(), interfaces[4]);

		Assert.False(arrayInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
		Assert.False(emptyInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
		Assert.False(annotationInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
		Assert.False(primitiveWrapperInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
	}

	[Fact]
	internal void DirectBufferTest() => JInterfaceObjectTests.InterfaceObjectTest<JDirectBufferObject>();
	[Fact]
	internal void CharSequenceTest() => JInterfaceObjectTests.InterfaceObjectTest<JCharSequenceObject>();
	[Fact]
	internal void CloneableTest() => JInterfaceObjectTests.InterfaceObjectTest<JCloneableObject>();
	[Fact]
	internal void ComparableTest() => JInterfaceObjectTests.InterfaceObjectTest<JComparableObject>();
	[Fact]
	internal void AnnotatedElementTest() => JInterfaceObjectTests.InterfaceObjectTest<JAnnotatedElementObject>();
	[Fact]
	internal void GenericDeclarationTest() => JInterfaceObjectTests.InterfaceObjectTest<JGenericDeclarationObject>();
	[Fact]
	internal void MemberTest() => JInterfaceObjectTests.InterfaceObjectTest<JMemberObject>();
	[Fact]
	internal void TypeTest() => JInterfaceObjectTests.InterfaceObjectTest<JTypeObject>();
	[Fact]
	internal void AnnotationTest() => JInterfaceObjectTests.InterfaceObjectTest<JAnnotationObject>();
	[Fact]
	internal void SerializableTest() => JInterfaceObjectTests.InterfaceObjectTest<JSerializableObject>();
	[Fact]
	internal void AppendableTest() => JInterfaceObjectTests.InterfaceObjectTest<JAppendableObject>();
	[Fact]
	internal void ReadableTest() => JInterfaceObjectTests.InterfaceObjectTest<JReadableObject>();
	[Fact]
	internal void RunnableTest() => JInterfaceObjectTests.InterfaceObjectTest<JRunnableObject>();

	private static void InterfaceObjectTest<TInterface>()
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		JInterfaceObjectTests.MetadataTest<TInterface>();

		JInterfaceTypeMetadata interfaceTypeMetadata = IInterfaceType.GetMetadata<TInterface>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JObjectLocalRef localRef = JInterfaceObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JInterfaceObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClass = new(env);
		using JClassObject interfaceClass = new(jClass, interfaceTypeMetadata);
		using JLocalObject jLocal = new(interfaceClass, localRef);
		using JGlobal jGlobal = new(env.VirtualMachine, new(interfaceClass), globalRef);

		env.VirtualMachine.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>())
		   .ReturnsForAnyArgs(thread);

		Assert.Null(interfaceTypeMetadata.ParseInstance(default));
		Assert.Null(interfaceTypeMetadata.ProxyMetadata.ParseInstance(default));
		Assert.Null(interfaceTypeMetadata.ParseInstance(env, default));
		Assert.Null(interfaceTypeMetadata.ProxyMetadata.ParseInstance(env, default));
		Assert.Null(interfaceTypeMetadata.CreateException(jGlobal));
		Assert.Null(interfaceTypeMetadata.ProxyMetadata.CreateException(jGlobal));

		env.ClassFeature.GetClass(interfaceTypeMetadata.ClassName).Returns(interfaceClass);
		env.GetReferenceType(jGlobal).Returns(JReferenceType.GlobalRefType);

		using JLocalObject jLocal0 = interfaceTypeMetadata.CreateInstance(interfaceClass, localRef);
		using JLocalObject jLocal1 = interfaceTypeMetadata.ProxyMetadata.CreateInstance(interfaceClass, localRef);
		using JLocalObject jLocal2 = interfaceTypeMetadata.ParseInstance(env, jGlobal);
		using JLocalObject jLocal3 = interfaceTypeMetadata.ProxyMetadata.ParseInstance(env, jGlobal);
		using TInterface instance = Assert.IsType<TInterface>(interfaceTypeMetadata.ParseInstance(jLocal));
		using JLocalObject instanceProxy = (JLocalObject)interfaceTypeMetadata.ProxyMetadata.ParseInstance(jLocal);
		using JLocalObject instanceProxyD =
			(JLocalObject)interfaceTypeMetadata.ProxyMetadata.ParseInstance(jLocal, true);

		Assert.Equal(jLocal0.GetType(), jLocal1.GetType());
		Assert.Equal(jLocal0.GetType(), jLocal2.GetType());
		Assert.Equal(jLocal0.GetType(), jLocal3.GetType());
		Assert.Equal(jLocal, instance.Object);
		Assert.Equal(jLocal0.GetType(), instanceProxy.GetType());
		Assert.Equal(localRef, jLocal0.LocalReference);
		Assert.Equal(localRef, (jLocal1 as ILocalObject).LocalReference);
		Assert.Equal(default, jLocal2.LocalReference);
		Assert.Equal(default, (jLocal3 as ILocalObject).LocalReference);
		Assert.Equal(jGlobal.Reference, jLocal2.As<JGlobalRef>());
		Assert.Equal(jGlobal.Reference, jLocal3.As<JGlobalRef>());
		Assert.Equal(jLocal.LocalReference, instanceProxy.LocalReference);
		Assert.Equal(jLocal.LocalReference, (instanceProxyD as ILocalObject).LocalReference);
		Assert.Equal(jLocal.LocalReference, (instance as ILocalObject).LocalReference);

		Assert.Equal(instance.Object.Reference, instance.Reference);

		Assert.False(interfaceTypeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		HashSet<JInterfaceTypeMetadata> interfaceList = [];
		foreach (JInterfaceTypeMetadata interfaceMetadata in interfaceTypeMetadata.Interfaces)
		{
			JReferenceObject jObject0 = interfaceMetadata.ParseInstance(jLocal1);
			JReferenceObject jObject1 = interfaceMetadata.ParseInstance(jLocal1, true);
			Assert.Equal(interfaceMetadata.Type, jObject0.GetType());
			Assert.Equal(jLocal1, (jObject0 as IWrapper<JLocalObject>)!.Value);
			Assert.Equal(interfaceMetadata.Type, jObject1.GetType());
			Assert.Equal(jLocal1, (jObject1 as IWrapper<JLocalObject>)!.Value);

			Assert.True(interfaceTypeMetadata.Interfaces.Contains(interfaceMetadata));
			interfaceList.Add(interfaceMetadata);
		}
		Assert.True(interfaceList.SetEquals(interfaceTypeMetadata.Interfaces));
		interfaceTypeMetadata.Interfaces.ForEach(default(Object), (_, i) => Assert.True(interfaceList.Remove(i)));
		Assert.Empty(interfaceList);
	}
	private static void MetadataTest<TInterface>()
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		JInterfaceTypeMetadata interfaceTypeMetadata = IInterfaceType.GetMetadata<TInterface>();
		JClassTypeMetadata proxyTypeMetadata = IClassType.GetMetadata<JProxyObject>();
		String textValue = interfaceTypeMetadata.ToString();

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(interfaceTypeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {interfaceTypeMetadata.Hash} }}", textValue);
		Assert.Equal(proxyTypeMetadata.ToString(), interfaceTypeMetadata.ProxyMetadata.ToString());

		Assert.Equal(typeof(JLocalObject.InterfaceView), EnvironmentProxy.GetFamilyType<JLocalObject.InterfaceView>());
		Assert.Equal(JTypeKind.Interface, EnvironmentProxy.GetKind<JLocalObject.InterfaceView>());
		Assert.Equal(typeof(JLocalObject.InterfaceView), EnvironmentProxy.GetFamilyType<TInterface>());
		Assert.Equal(JTypeKind.Interface, EnvironmentProxy.GetKind<JLocalObject.InterfaceView>());
		Assert.IsType<JFunctionDefinition<TInterface>>(
			interfaceTypeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<TInterface>>(interfaceTypeMetadata.CreateFieldDefinition("fieldName"u8));

		Assert.Equal(proxyTypeMetadata.ClassName, interfaceTypeMetadata.ProxyMetadata.ClassName);
		Assert.Equal(proxyTypeMetadata.Signature, interfaceTypeMetadata.ProxyMetadata.Signature);
	}
}