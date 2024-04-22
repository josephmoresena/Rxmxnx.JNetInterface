namespace Rxmxnx.JNetInterface.Tests.Nio;

[ExcludeFromCodeCoverage]
public class JBufferObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.BufferObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JBufferObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JBufferObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JBufferObjectTests.className, JBufferObjectTests.classSignature,
	                                                   JBufferObjectTests.arraySignature);

	[Theory]
	[InlineData(true, true)]
	[InlineData(false, false)]
	internal void CreateMetadataTest(Boolean isDirect, Boolean useMetadata)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JBufferObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JBufferObjectTests.fixture.Create<JObjectLocalRef>();
		IntPtr? address = isDirect ? JBufferObjectTests.fixture.Create<JObjectLocalRef>().Pointer : null;
		Int64 capacity = JBufferObjectTests.fixture.Create<Int32>();
		using JClassObject jClass = new(env);
		using JClassObject jBufferClass = new(jClass, typeMetadata);
		using JBufferObject jBuffer =
			Assert.IsType<JBufferObject>(typeMetadata.CreateInstance(jBufferClass, localRef, true));

		env.FunctionSet.IsDirectBuffer(jBuffer).Returns(isDirect);
		env.FunctionSet.BufferCapacity(jBuffer).Returns(capacity);
		env.NioFeature.GetDirectCapacity(jBuffer).Returns(capacity);
		env.NioFeature.GetDirectAddress(jBuffer).Returns(address ?? default);

		ILocalObject.ProcessMetadata(jBuffer,
		                             useMetadata ?
			                             new BufferObjectMetadata(new(jBufferClass))
			                             {
				                             Capacity = capacity, IsDirect = isDirect, Address = address,
			                             } :
			                             new ObjectMetadata(jBufferClass));

		BufferObjectMetadata objectMetadata = Assert.IsType<BufferObjectMetadata>(ILocalObject.CreateMetadata(jBuffer));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(isDirect, objectMetadata.IsDirect);
		Assert.Equal(address, objectMetadata.Address);
		Assert.Equal(capacity, objectMetadata.Capacity);
		Assert.Equal(objectMetadata, new(objectMetadata));

		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JLocalObject>()));

		env.FunctionSet.Received(useMetadata ? 0 : 1).IsDirectBuffer(jBuffer);
		env.FunctionSet.Received(useMetadata || isDirect ? 0 : 1).BufferCapacity(jBuffer);
		env.NioFeature.Received(useMetadata || !isDirect ? 0 : 1).GetDirectCapacity(jBuffer);
		env.NioFeature.Received(useMetadata || !isDirect ? 0 : 1).GetDirectAddress(jBuffer);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JBufferObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JBufferObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JBufferObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JBufferObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jBufferClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jBufferClass);
		using JGlobal jGlobal = new(vm, new(jBufferClass, IClassType.GetMetadata<JBufferObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Abstract, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JBufferObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JBufferObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JBufferObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JBufferObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JBufferObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JBufferObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JBufferObjectTests.hash.ToString(), IDataType.GetHash<JBufferObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JBufferObject>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JBufferObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JBufferObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JBufferObject>());
		Assert.Empty(typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JBufferObject>().Returns(jBufferClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jBufferClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jBufferClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JBufferObject jBuffer0 =
			Assert.IsType<JBufferObject>(typeMetadata.CreateInstance(jBufferClass, localRef, true));
		using JBufferObject jBuffer1 = Assert.IsType<JBufferObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JBufferObject jBuffer2 = Assert.IsType<JBufferObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JBufferObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}