namespace Rxmxnx.JNetInterface.Tests.Lang.Annotation;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public class JElementTypeObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ElementTypeEnum);
	private static readonly CString classSignature = CString.Concat("L"u8, JElementTypeObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JElementTypeObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JElementTypeObjectTests.className,
	                                                   JElementTypeObjectTests.classSignature,
	                                                   JElementTypeObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void CreateMetadataTest(Boolean definedOrdinal)
	{
		JEnumTypeMetadata typeMetadata = IEnumType.GetMetadata<JElementTypeObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JElementTypeObjectTests.fixture.Create<JObjectLocalRef>();
		Int32 ordinal = definedOrdinal ?
			(Int32)JElementTypeObjectTests.fixture.Create<JElementTypeObject.ElementType>() :
			Random.Shared.Next(-10, -1);
		String name = definedOrdinal ?
			typeMetadata.Fields[ordinal].ToString() :
			JElementTypeObjectTests.fixture.Create<String>();
		using JClassObject jClass = new(env);
		using JClassObject jElementTypeClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());

		env.ClassFeature.GetClass<JElementTypeObject>().Returns(jElementTypeClass);

		using JStringObject jStringName = new(jStringClass, default, name);
		using JElementTypeObject jElementType =
			Assert.IsType<JElementTypeObject>(typeMetadata.CreateInstance(jElementTypeClass, localRef, true));

		env.ClassFeature.Received(1).GetClass<JElementTypeObject>();

		env.FunctionSet.GetOrdinal(jElementType).Returns(ordinal);
		env.FunctionSet.GetName(jElementType).Returns(jStringName);

		EnumObjectMetadata objectMetadata =
			Assert.IsType<EnumObjectMetadata>(ILocalObject.CreateMetadata(jElementType));

		Assert.True(Object.ReferenceEquals(jElementType, jElementType.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jElementType, (jElementType as ILocalObject).CastTo<JLocalObject>()));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(ordinal, objectMetadata.Ordinal);
		Assert.Equal(name, objectMetadata.Name);

		env.FunctionSet.Received(1).GetOrdinal(jElementType);
		env.FunctionSet.Received(definedOrdinal ? 0 : 1).GetName(jElementType);

		JSerializableObject jSerializable = jElementType.CastTo<JSerializableObject>();
		JComparableObject jComparable = jElementType.CastTo<JComparableObject>();

		Assert.Equal(jElementType.Id, jSerializable.Id);
		Assert.Equal(jElementType.Id, jComparable.Id);

		Assert.Equal(jElementType, jSerializable.Object);
		Assert.Equal(jElementType, jComparable.Object);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JEnumTypeMetadata typeMetadata = IEnumType.GetMetadata<JElementTypeObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JElementTypeObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JElementTypeObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JElementTypeObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jElementTypeClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jElementTypeClass);
		using JGlobal jGlobal = new(vm, new(jElementTypeClass, IEnumType.GetMetadata<JElementTypeObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JElementTypeObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JElementTypeObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Enum, typeMetadata.Kind);
		Assert.Equal(JElementTypeObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JElementTypeObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JElementTypeObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JElementTypeObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JElementTypeObjectTests.hash.ToString(), IDataType.GetHash<JElementTypeObject>());
		Assert.Equal(IDataType.GetMetadata<JEnumObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JElementTypeObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JElementTypeObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JEnumObject), EnvironmentProxy.GetFamilyType<JElementTypeObject>());
		Assert.Equal(JTypeKind.Enum, EnvironmentProxy.GetKind<JElementTypeObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), typeMetadata.Interfaces);

		Assert.Equal(8, typeMetadata.Fields.Count);
		Assert.True("TYPE"u8.SequenceEqual(typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.Type]));
		Assert.True("FIELD"u8.SequenceEqual(typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.Field]));
		Assert.True("METHOD"u8.SequenceEqual(typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.Method]));
		Assert.True("PARAMETER"u8.SequenceEqual(typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.Parameter]));
		Assert.True(
			"CONSTRUCTOR"u8.SequenceEqual(typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.Constructor]));
		Assert.True(
			"LOCAL_VARIABLE"u8.SequenceEqual(typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.LocalVariable]));
		Assert.True(
			"ANNOTATION_TYPE"u8.SequenceEqual(
				typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.AnnotationType]));
		Assert.True("PACKAGE"u8.SequenceEqual(typeMetadata.Fields[(Int32)JElementTypeObject.ElementType.Package]));

		Assert.Equal(JElementTypeObject.ElementType.Type,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["TYPE"u8]);
		Assert.Equal(JElementTypeObject.ElementType.Field,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["FIELD"u8]);
		Assert.Equal(JElementTypeObject.ElementType.Method,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["METHOD"u8]);
		Assert.Equal(JElementTypeObject.ElementType.Parameter,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["PARAMETER"u8]);
		Assert.Equal(JElementTypeObject.ElementType.Constructor,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["CONSTRUCTOR"u8]);
		Assert.Equal(JElementTypeObject.ElementType.LocalVariable,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["LOCAL_VARIABLE"u8]);
		Assert.Equal(JElementTypeObject.ElementType.AnnotationType,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["ANNOTATION_TYPE"u8]);
		Assert.Equal(JElementTypeObject.ElementType.Package,
		             (JElementTypeObject.ElementType)typeMetadata.Fields["PACKAGE"u8]);

		env.ClassFeature.GetClass<JElementTypeObject>().Returns(jElementTypeClass);
		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JElementTypeObject>().Returns(jElementTypeClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jElementTypeClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jElementTypeClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JElementTypeObject jElementType0 =
			Assert.IsType<JElementTypeObject>(typeMetadata.CreateInstance(jElementTypeClass, localRef, true));
		using JElementTypeObject jElementType1 =
			Assert.IsType<JElementTypeObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JElementTypeObject jElementType2 =
			Assert.IsType<JElementTypeObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JElementTypeObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}