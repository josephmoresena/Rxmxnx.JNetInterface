namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JArrayObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly Type typeOfArray = typeof(JArrayObject<>);
	private static readonly MethodInfo getMetadataInfo =
		typeof(IArrayType).GetMethod(nameof(IArrayType.GetMetadata), BindingFlags.Static | BindingFlags.Public)!;
	private static readonly MethodInfo elementCastTestInfo =
		typeof(JArrayObjectTests).GetMethod(nameof(JArrayObjectTests.ElementCastTest),
		                                    BindingFlags.Static | BindingFlags.NonPublic)!;

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ObjectTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JLocalObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ClassTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JClassObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void StringTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JStringObject>(initializer);

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ThrowableTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JThrowableObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void EnumTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JEnumObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void NumberTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JNumberObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void AccessibleObjectTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JAccessibleObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ExecutableTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JExceptionObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ModifierTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JModifierObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ProxyTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JProxyObject>(initializer);

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ErrorTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JErrorObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ExceptionTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JExceptionObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void RuntimeExceptionTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JRuntimeExceptionObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void BufferTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JBufferObject>(initializer);

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void FieldTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JFieldObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void MethodTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JMethodObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ConstructorTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JConstructorObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void StackTraceElementTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JStackTraceElementObject>(initializer);

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void DirectBufferTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JDirectBufferObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void CharSequenceTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JCharSequenceObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void CloneableTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JCloneableObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void ComparableTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JComparableObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void AnnotatedElementTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JAnnotatedElementObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void GenericDeclarationTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JGenericDeclarationObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void MemberTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JMemberObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void TypeTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JTypeObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void AnnotationTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JAnnotationObject>(initializer);
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void SerializableTest(Byte initializer)
		=> JArrayObjectTests.ObjectArrayTest<JSerializableObject>(initializer);

	private static void ObjectArrayTest<TElement>(Byte initializer = 0)
		where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JReferenceTypeMetadata elementTypeMetadata = IReferenceType.GetMetadata<TElement>();
		JArrayTypeMetadata arrayTypeMetadata = IArrayType.GetMetadata<JArrayObject<TElement>>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JArrayLocalRef arrayRef = JArrayObjectTests.fixture.Create<JArrayLocalRef>();
		Int32 length = Random.Shared.Next(0, 10);
		using JClassObject jClass = new(env);
		using JClassObject jArrayClass = new(jClass, arrayTypeMetadata);
		using JArrayObject<TElement> jArray = initializer == 0 ?
			new(jArrayClass, arrayRef, length) :
			(JArrayObject<TElement>)arrayTypeMetadata.CreateInstance(jArrayClass, arrayRef.Value, true);
		ObjectMetadata objectMetadata = initializer == 3 ?
			new ArrayObjectMetadata(new(jArrayClass)) { Length = length, } :
			new ObjectMetadata(jArrayClass);

		env.ArrayFeature.GetArrayLength(jArray).Returns(length);

		if (initializer > 1) ILocalObject.ProcessMetadata(jArray, objectMetadata);

		Assert.Equal(typeof(JArrayObject), EnvironmentProxy.GetFamilyType<JArrayObject>());
		Assert.Equal(JTypeKind.Array, EnvironmentProxy.GetKind<JArrayObject>());
		Assert.Equal(typeof(JArrayObject), EnvironmentProxy.GetFamilyType<JArrayObject<TElement>>());
		Assert.Equal(JTypeKind.Array, EnvironmentProxy.GetKind<JArrayObject<TElement>>());
		Assert.Equal(elementTypeMetadata is JArrayTypeMetadata, arrayTypeMetadata.Dimension > 1);
		Assert.Equal(arrayTypeMetadata, elementTypeMetadata.GetArrayMetadata());

		Assert.Equal(arrayRef, jArray.Object.Reference);
		Assert.Equal(length, jArray.Length);
		Assert.Equal(arrayTypeMetadata.ClassName, jArray.ObjectClassName);
		Assert.Equal(arrayTypeMetadata.Signature, jArray.ObjectSignature);
		Assert.Equal(elementTypeMetadata.ArraySignature, jArray.ObjectClassName);
		Assert.Equal(elementTypeMetadata.ArraySignature, jArray.ObjectSignature);
		Assert.Equal(arrayTypeMetadata.Type, jArray.GetType());
		Assert.Equal(typeof(JArrayObject<>).MakeGenericType(elementTypeMetadata.Type), jArray.GetType());
		Assert.Equal(length, Assert.IsType<ArrayObjectMetadata>(ILocalObject.CreateMetadata(jArray)).Length);
		Assert.Equal(jArray.ToString(), jArray.Object.ToString());

		env.ArrayFeature.Received(initializer is 0 or 3 ? 0 : 1).GetArrayLength(jArray);
		JArrayObjectTests.CastTest(jArray);

		if (arrayTypeMetadata.Dimension < 10)
			JArrayObjectTests.ObjectArrayTest<JArrayObject<TElement>>(initializer);
	}
	private static void CastTest<TElement>(JArrayObject<TElement> jArray)
		where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JArrayObject<JLocalObject> jObjectArray = jArray.Object.CastTo<JArrayObject<JLocalObject>>();
		JLocalObject jLocal = jArray.Object.CastTo<JLocalObject>();
		JSerializableObject jSerializable = jArray.Object.CastTo<JSerializableObject>();
		JCloneableObject jCloneable = jArray.Object.CastTo<JCloneableObject>();

		Assert.Equal(jArray.Object, jObjectArray.Object);
		Assert.Equal(jArray.Object, jLocal);
		Assert.Equal(jArray.Object, jSerializable.Object);
		Assert.Equal(jArray.Object, jCloneable.Object);

		JArrayObjectTests.ElementCastTest(jArray);
		JArrayObjectTests.ArrayCastTest(jArray);
	}
	private static void ElementCastTest<TElement>(JArrayObject<TElement> jArray)
		where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JArrayObject<JLocalObject> jObjectArray = jArray.CastTo<JArrayObject<JLocalObject>>();
		JSerializableObject jSerializable = jArray.CastTo<JSerializableObject>();
		JCloneableObject jCloneable = jArray.CastTo<JCloneableObject>();

		Assert.Equal(jArray.Object, jObjectArray.Object);
		Assert.Equal(jArray.Object, jSerializable.Object);
		Assert.Equal(jArray.Object, jCloneable.Object);

		if (IReferenceType.GetMetadata<TElement>().BaseMetadata is { } baseMetadata)
			JArrayObjectTests.ArrayCastTest(jArray, baseMetadata.Type);
		foreach (JInterfaceTypeMetadata interfaceTypeMetadata in IReferenceType.GetMetadata<TElement>().Interfaces)
			JArrayObjectTests.ArrayCastTest(jArray, interfaceTypeMetadata.Type);
	}
	private static void ArrayCastTest(JArrayObject jArray)
	{
		if (jArray.TypeMetadata.Dimension == 1) return;
		Type typeOf = typeof(JLocalObject);
		Type[] elementTypes = JArrayObjectTests.GetBasicElementTypes(jArray);
		for (Int32 i = 0; i < jArray.TypeMetadata.Dimension; i++)
		{
			if (i < jArray.TypeMetadata.Dimension - 1)
				for (Int32 j = 0; j < elementTypes.Length; j++)
					elementTypes[j] = JArrayObjectTests.typeOfArray.MakeGenericType(elementTypes[j]);
			typeOf = JArrayObjectTests.ArrayCastTest(jArray, typeOf);
		}

		foreach (Type elementType in elementTypes)
			JArrayObjectTests.ArrayCastTest(jArray, elementType);
	}
	private static Type[] GetBasicElementTypes(JArrayObject jArray)
	{
		JReferenceTypeMetadata basicElementMetadata = (JReferenceTypeMetadata)jArray.TypeMetadata.ElementMetadata;
		while (basicElementMetadata is JArrayTypeMetadata arrayTypeMetadata)
			basicElementMetadata = (JReferenceTypeMetadata)arrayTypeMetadata.ElementMetadata;
		List<Type> types = basicElementMetadata.BaseMetadata is not null ?
			[basicElementMetadata.BaseMetadata.Type,] :
			[];
		types.AddRange(basicElementMetadata.Interfaces.Select(i => i.Type));
		Type[] elementTypes = types.ToArray();
		return elementTypes;
	}
	private static Type ArrayCastTest(JArrayObject jArray, Type elementType)
	{
		Type arrayType = JArrayObjectTests.typeOfArray.MakeGenericType(elementType);
		MethodInfo generic = JArrayObjectTests.getMetadataInfo.MakeGenericMethod(arrayType);
		JArrayTypeMetadata arrayTypeMetadata = (JArrayTypeMetadata)generic.Invoke(default, Array.Empty<Object>())!;
		MethodInfo genericTest =
			JArrayObjectTests.elementCastTestInfo.MakeGenericMethod(arrayTypeMetadata.ElementMetadata.Type);
		JLocalObject.ArrayView arrayView = (JLocalObject.ArrayView)arrayTypeMetadata.ParseInstance(jArray);

		Assert.Equal(elementType, arrayTypeMetadata.ElementMetadata.Type);
		Assert.Equal(jArray, arrayView.Object);
		Assert.Equal(arrayType, arrayView.GetType());
		Assert.Equal(jArray.Id, arrayView.Id);
		genericTest.Invoke(null, [arrayView,]);

		return elementType;
	}
}