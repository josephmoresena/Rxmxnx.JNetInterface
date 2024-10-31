namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JInterfaceObjectTests
{
	private static readonly Dictionary<Type, CString> classNames = new()
	{
		{ typeof(JAnnotatedElementObject), new("java/lang/reflect/AnnotatedElement"u8) },
		{ typeof(JAnnotationObject), new("java/lang/annotation/Annotation"u8) },
		{ typeof(JAppendableObject), new("java/lang/Appendable"u8) },
		{ typeof(JCharSequenceObject), new("java/lang/CharSequence"u8) },
		{ typeof(JCloneableObject), new("java/lang/Cloneable"u8) },
		{ typeof(JComparableObject), new("java/lang/Comparable"u8) },
		{ typeof(JDirectBufferObject), new("sun/nio/ch/DirectBuffer"u8) },
		{ typeof(JGenericDeclarationObject), new("java/lang/reflect/GenericDeclaration"u8) },
		{ typeof(JMemberObject), new("java/lang/reflect/Member"u8) },
		{ typeof(JReadableObject), new("java/lang/Readable"u8) },
		{ typeof(JRunnableObject), new("java/lang/Runnable"u8) },
		{ typeof(JSerializableObject), new("java/io/Serializable"u8) },
		{ typeof(JTypeObject), new("java/lang/reflect/Type"u8) },
	};
	private static readonly Dictionary<Type, CString> classSignatures =
		JInterfaceObjectTests.classNames.ToDictionary(p => p.Key, p => CString.Concat("L"u8, p.Value, ";"u8));
	private static readonly Dictionary<Type, CString> arraySignatures =
		JInterfaceObjectTests.classSignatures.ToDictionary(p => p.Key, p => CString.Concat("["u8, p.Value));
	private static readonly Dictionary<Type, CStringSequence> hashes =
		JInterfaceObjectTests.classSignatures.Keys.ToDictionary(
			t => t,
			t => new CStringSequence(JInterfaceObjectTests.classNames[t], JInterfaceObjectTests.classSignatures[t],
			                         JInterfaceObjectTests.arraySignatures[t]));
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void InterfaceSetTest()
	{
		InterfaceSet emptyInterfaces = InterfaceSet.Empty;
		IInterfaceSet arrayInterfaces = InterfaceSet.SerializableCloneableSet;
		IInterfaceSet annotationInterfaces = InterfaceSet.AnnotationSet;
		IInterfaceSet serializableComparableInterfaces = InterfaceSet.SerializableComparableSet;
		IInterfaceSet comparableInterfaces = InterfaceSet.ComparableSet;
		IInterfaceSet serializableInterfaces = InterfaceSet.SerializableSet;
		IInterfaceSet annotatedElementInterfaces = InterfaceSet.AnnotatedElementSet;

		Assert.Contains(IInterfaceType.GetMetadata<JCloneableObject>(), arrayInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), arrayInterfaces.ToArray());
		Assert.Empty(emptyInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotationObject>(), annotationInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), serializableComparableInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), serializableComparableInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), comparableInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), serializableInterfaces.ToArray());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), annotatedElementInterfaces.ToArray());

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
		Assert.True(serializableComparableInterfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.True(serializableComparableInterfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(serializableComparableInterfaces.Contains(IInterfaceType.GetMetadata<JCloneableObject>()));
		Assert.False(serializableComparableInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));
		Assert.True(comparableInterfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.False(comparableInterfaces.Contains(IInterfaceType.GetMetadata<JCloneableObject>()));
		Assert.True(serializableInterfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(serializableInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));
		Assert.True(comparableInterfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.False(comparableInterfaces.Contains(IInterfaceType.GetMetadata<JCloneableObject>()));
		Assert.True(annotatedElementInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.False(annotatedElementInterfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));

		List<JInterfaceTypeMetadata> interfaces = [];
		arrayInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));
		emptyInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));
		annotationInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));
		serializableComparableInterfaces.ForEach(default(Object), (_, i) => interfaces.Add(i));

		Assert.Equal(IInterfaceType.GetMetadata<JSerializableObject>(), interfaces[0]);
		Assert.Equal(IInterfaceType.GetMetadata<JCloneableObject>(), interfaces[1]);
		Assert.Equal(IInterfaceType.GetMetadata<JAnnotationObject>(), interfaces[2]);
		Assert.Equal(IInterfaceType.GetMetadata<JSerializableObject>(), interfaces[3]);
		Assert.Equal(IInterfaceType.GetMetadata<JComparableObject>(), interfaces[4]);

		Assert.False(arrayInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
		Assert.False(emptyInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
		Assert.False(annotationInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
		Assert.False(serializableComparableInterfaces.Contains(JFakeInterfaceObject.TypeMetadata));
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
		Assert.Null(interfaceTypeMetadata.ParseInstance(env, default));
		Assert.Null(interfaceTypeMetadata.CreateException(jGlobal));

		env.ClassFeature.GetClass(interfaceTypeMetadata.ClassName).Returns(interfaceClass);
		env.ClassFeature.GetClass(Arg.Any<ITypeInformation>())
		   .Returns(c => env.ClassFeature.GetClass((c[0] as ITypeInformation)!.ClassName));
		env.GetReferenceType(jGlobal).Returns(JReferenceType.GlobalRefType);

		using JLocalObject jLocal0 = interfaceTypeMetadata.CreateInstance(interfaceClass, localRef);
		using JLocalObject jLocal2 = interfaceTypeMetadata.ParseInstance(env, jGlobal);
		using TInterface instance = Assert.IsType<TInterface>(interfaceTypeMetadata.ParseInstance(jLocal));

		Assert.Equal(jLocal0.GetType(), jLocal2.GetType());
		Assert.Equal(jLocal, instance.Object);
		Assert.Equal(localRef, jLocal0.LocalReference);
		Assert.Equal(default, jLocal2.LocalReference);
		Assert.Equal(jGlobal.Reference, jLocal2.As<JGlobalRef>());
		Assert.Equal(jLocal.LocalReference, (instance as ILocalObject).LocalReference);

		Assert.Equal(instance.Object.Reference, instance.Reference);

		Assert.False(interfaceTypeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		HashSet<JInterfaceTypeMetadata> interfaceList = [];
		foreach (JInterfaceTypeMetadata interfaceMetadata in interfaceTypeMetadata.Interfaces)
		{
			JReferenceObject jObject0 = interfaceMetadata.ParseInstance(jLocal0);
			JReferenceObject jObject1 = interfaceMetadata.ParseInstance(jLocal0, true);
			Assert.Equal(interfaceMetadata.Type, jObject0.GetType());
			Assert.Equal(jLocal0, (jObject0 as IWrapper<JLocalObject>)!.Value);
			Assert.Equal(interfaceMetadata.Type, jObject1.GetType());
			Assert.Equal(jLocal0, (jObject1 as IWrapper<JLocalObject>)!.Value);

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
		String? textValue = interfaceTypeMetadata.ToString();

		Assert.StartsWith("{", textValue);
		Assert.Contains(interfaceTypeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {interfaceTypeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(typeof(JLocalObject.InterfaceView), EnvironmentProxy.GetFamilyType<JLocalObject.InterfaceView>());
		Assert.Equal(JTypeKind.Interface, EnvironmentProxy.GetKind<JLocalObject.InterfaceView>());
		Assert.Equal(typeof(JLocalObject.InterfaceView), EnvironmentProxy.GetFamilyType<TInterface>());
		Assert.Equal(JTypeKind.Interface, EnvironmentProxy.GetKind<JLocalObject.InterfaceView>());
		Assert.Equal(JInterfaceObjectTests.classNames[typeof(TInterface)], interfaceTypeMetadata.ClassName);
		Assert.Equal(JInterfaceObjectTests.classSignatures[typeof(TInterface)], interfaceTypeMetadata.Signature);
		Assert.Equal(JInterfaceObjectTests.arraySignatures[typeof(TInterface)], interfaceTypeMetadata.ArraySignature);
		Assert.Equal(JInterfaceObjectTests.hashes[typeof(TInterface)].ToString(), interfaceTypeMetadata.Hash);
		Assert.IsType<JFunctionDefinition<TInterface>.Parameterless>(
			interfaceTypeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<TInterface>>(
			interfaceTypeMetadata.CreateFunctionDefinition("functionName"u8,
			                                               [JArgumentMetadata.Get<JStringObject>(),]));
		Assert.IsType<JFieldDefinition<TInterface>>(interfaceTypeMetadata.CreateFieldDefinition("fieldName"u8));
	}
}