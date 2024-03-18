namespace Rxmxnx.JNetInterface.Tests.Lang.Reflect;

[ExcludeFromCodeCoverage]
public class JFieldObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.FieldObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JFieldObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JFieldObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JFieldObjectTests.className, JFieldObjectTests.classSignature,
	                                                   JFieldObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ConstructorClassTest(Boolean initDefinition)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JFieldObject>();
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JFieldObjectTests.fixture.Create<JObjectLocalRef>();
		JFieldId fieldId = JFieldObjectTests.fixture.Create<JFieldId>();
		JNonTypedFieldDefinition fieldDefinition = new("fieldName"u8, stringTypeMetadata.Signature);
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, stringTypeMetadata);
		using JClassObject jFieldClass = new(jClass, typeMetadata);
		using JStringObject jStringFieldName = new(jStringClass, default, fieldDefinition.Information[0].ToString());
		using JFieldObject jField = initDefinition ?
			new(jFieldClass, localRef, fieldDefinition, jClass) :
			Assert.IsType<JFieldObject>(typeMetadata.CreateInstance(jFieldClass, localRef));

		env.FunctionSet.GetDeclaringClass(jField).Returns(jClass);
		env.FunctionSet.GetName(jField).Returns(jStringFieldName);
		env.FunctionSet.GetFieldType(jField).Returns(jStringClass);
		env.ClassFeature.GetClass(jClass.Hash).Returns(jClass);
		env.AccessFeature.GetDefinition(jStringFieldName, jStringClass).Returns(fieldDefinition);
		env.AccessFeature.GetFieldId(jField).Returns(fieldId.Pointer);

		Assert.Equal(fieldDefinition, jField.Definition);
		Assert.Equal(jClass, jField.DeclaringClass);
		Assert.Equal(fieldId, jField.FieldId);
		Assert.Equal(fieldId.Pointer, jField.FieldId.Pointer);

		env.FunctionSet.Received(initDefinition ? 0 : 1).GetDeclaringClass(jField);
		env.FunctionSet.Received(initDefinition ? 0 : 1).GetName(jField);
		env.FunctionSet.Received(initDefinition ? 0 : 1).GetFieldType(jField);
		env.ClassFeature.Received(initDefinition ? 1 : 0).GetClass(jClass.Hash);
		env.AccessFeature.Received(initDefinition ? 0 : 1).GetDefinition(jStringFieldName, jStringClass);
		env.AccessFeature.Received(1).GetFieldId(jField);

		jField.ClearValue();
		Assert.Null(Assert.IsType<FieldObjectMetadata>(ILocalObject.CreateMetadata(jField)).FieldId);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void CreateMetadataTest(Boolean useMetadata)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JFieldObject>();
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JFieldObjectTests.fixture.Create<JObjectLocalRef>();
		JNonTypedFieldDefinition fieldDefinition = new("fieldName"u8, stringTypeMetadata.Signature);
		JFieldId fieldId = JFieldObjectTests.fixture.Create<JFieldId>();
		using JClassObject jClass = new(env);
		using JClassObject jFieldClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringFieldName = new(jStringClass, default, fieldDefinition.Information[0].ToString());
		using JFieldObject jField =
			Assert.IsType<JFieldObject>(typeMetadata.CreateInstance(jFieldClass, localRef, true));

		env.FunctionSet.GetDeclaringClass(jField).Returns(jClass);
		env.FunctionSet.GetName(jField).Returns(jStringFieldName);
		env.FunctionSet.GetFieldType(jField).Returns(jStringClass);
		env.ClassFeature.GetClass(jClass.Hash).Returns(jClass);
		env.AccessFeature.GetDefinition(jStringFieldName, jStringClass).Returns(fieldDefinition);
		env.AccessFeature.GetFieldId(jField).Returns(fieldId.Pointer);

		ILocalObject.ProcessMetadata(
			jField,
			useMetadata ?
				new FieldObjectMetadata(new(jFieldClass))
				{
					Definition = fieldDefinition, FieldId = fieldId, ClassHash = jClass.Hash,
				} :
				new ObjectMetadata(jFieldClass));

		FieldObjectMetadata objectMetadata = Assert.IsType<FieldObjectMetadata>(ILocalObject.CreateMetadata(jField));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(fieldDefinition, objectMetadata.Definition);
		Assert.Equal(jClass.Hash, objectMetadata.ClassHash);
		Assert.Equal(useMetadata ? fieldId : null, objectMetadata.FieldId);
		Assert.Equal(objectMetadata, new(objectMetadata));

		JAnnotatedElementObject jAnnotatedElement = jField.CastTo<JAnnotatedElementObject>();
		JMemberObject jMember = jField.CastTo<JMemberObject>();
		Assert.Equal(jField.Id, jAnnotatedElement.Id);
		Assert.Equal(jField.Id, jMember.Id);
		Assert.Equal(jField, jAnnotatedElement.Object);
		Assert.Equal(jField, jMember.Object);

		Assert.True(Object.ReferenceEquals(jField, jField.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jField, jField.CastTo<JAccessibleObject>()));

		env.FunctionSet.Received(useMetadata ? 0 : 1).GetDeclaringClass(jField);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetName(jField);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetFieldType(jField);
		env.ClassFeature.Received(0).GetClass(jClass.Hash);
		env.AccessFeature.Received(useMetadata ? 0 : 1).GetDefinition(jStringFieldName, jStringClass);
		env.AccessFeature.Received(0).GetFieldId(jField);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JFieldObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JFieldObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JFieldObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JFieldObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JFieldObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JFieldObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JFieldObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JFieldObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JFieldObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JFieldObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JFieldObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JFieldObjectTests.hash.ToString(), IDataType.GetHash<JFieldObject>());
		Assert.Equal(IDataType.GetMetadata<JAccessibleObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JFieldObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, Array.Empty<JArgumentMetadata>()));
		Assert.IsType<JFieldDefinition<JFieldObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JFieldObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JFieldObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JMemberObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JFieldObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JFieldObject jField0 =
			Assert.IsType<JFieldObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JFieldObject jField1 = Assert.IsType<JFieldObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JFieldObject jField2 = Assert.IsType<JFieldObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JFieldObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}