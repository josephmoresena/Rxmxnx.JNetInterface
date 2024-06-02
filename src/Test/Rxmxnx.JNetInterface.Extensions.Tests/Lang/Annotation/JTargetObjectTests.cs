namespace Rxmxnx.JNetInterface.Tests.Lang.Annotation;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public class JTargetObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.TargetAnnotation);
	private static readonly CString classSignature = CString.Concat("L"u8, JTargetObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JTargetObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JTargetObjectTests.className, JTargetObjectTests.classSignature,
	                                                   JTargetObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JInterfaceTypeMetadata interfaceTypeMetadata = IInterfaceType.GetMetadata<JTargetObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JObjectLocalRef localRef = JTargetObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JTargetObjectTests.fixture.Create<JGlobalRef>();
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
		using JTargetObject instance = Assert.IsType<JTargetObject>(interfaceTypeMetadata.ParseInstance(jLocal));
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
	}
	[Fact]
	internal void MetadataTest()
	{
		JInterfaceTypeMetadata interfaceTypeMetadata = IInterfaceType.GetMetadata<JTargetObject>();
		JClassTypeMetadata proxyTypeMetadata = IClassType.GetMetadata<JProxyObject>();
		String textValue = interfaceTypeMetadata.ToString();

		Assert.Equal(JTargetObjectTests.hash.ToString(), interfaceTypeMetadata.Hash);
		Assert.Equal(JTargetObjectTests.hash.ToString(), IDataType.GetHash<JTargetObject>());
		Assert.True(interfaceTypeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()));

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(interfaceTypeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {interfaceTypeMetadata.Hash} }}", textValue);
		Assert.Equal(proxyTypeMetadata.ToString(), interfaceTypeMetadata.ProxyMetadata.ToString());

		Assert.Equal(typeof(JAnnotationObject), EnvironmentProxy.GetFamilyType<JTargetObject>());
		Assert.Equal(JTypeKind.Annotation, EnvironmentProxy.GetKind<JTargetObject>());
		Assert.IsType<JFunctionDefinition<JTargetObject>>(
			interfaceTypeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JTargetObject>>(interfaceTypeMetadata.CreateFieldDefinition("fieldName"u8));

		Assert.Equal(proxyTypeMetadata.ClassName, interfaceTypeMetadata.ProxyMetadata.ClassName);
		Assert.Equal(proxyTypeMetadata.Signature, interfaceTypeMetadata.ProxyMetadata.Signature);
	}
}