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
	private static readonly MethodInfo objectArrayTestInfo =
		typeof(JArrayObjectTests).GetMethod(nameof(JArrayObjectTests.ObjectArrayTest),
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
	internal void ModuleTest(Byte initializer) => JArrayObjectTests.ObjectArrayTest<JModuleObject>(initializer);

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
		JArrayObjectTests.MetadataTest<TElement>();
		JArrayTypeMetadata arrayTypeMetadata = IArrayType.GetMetadata<JArrayObject<TElement>>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JArrayLocalRef arrayRef = JArrayObjectTests.fixture.Create<JArrayLocalRef>();
		Int32 length = Random.Shared.Next(0, 10);
		using JClassObject jClass = new(env);
		using JClassObject jElementClass = new(jClass, arrayTypeMetadata.ElementMetadata);
		using JClassObject jArrayClass = new(jClass, arrayTypeMetadata);
		using JArrayObject<TElement> jArray = initializer == 0 ?
			new(jArrayClass, arrayRef, length) :
			(JArrayObject<TElement>)arrayTypeMetadata.CreateInstance(jArrayClass, arrayRef.Value, true);
		ObjectMetadata objectMetadata = initializer == 3 ?
			new ArrayObjectMetadata(new(jArrayClass)) { Length = length, } :
			new ObjectMetadata(jArrayClass);
		ILocalViewObject localView = jArray;

		env.ArrayFeature.GetArrayLength(jArray).Returns(length);

		Assert.Equal(jArray.Object.Reference, jArray.Reference);

		if (initializer > 1) ILocalObject.ProcessMetadata(jArray, objectMetadata);

		Assert.Equal(arrayRef, jArray.Object.Reference);
		Assert.Equal(length, jArray.Length);
		Assert.Equal(arrayTypeMetadata.ClassName, jArray.ObjectClassName);
		Assert.Equal(arrayTypeMetadata.Signature, jArray.ObjectSignature);
		Assert.Equal(arrayTypeMetadata.ElementMetadata.ArraySignature, jArray.ObjectClassName);
		Assert.Equal(arrayTypeMetadata.ElementMetadata.ArraySignature, jArray.ObjectSignature);
		Assert.Equal(arrayTypeMetadata.Type, jArray.GetType());
		Assert.Equal(typeof(JArrayObject<>).MakeGenericType(arrayTypeMetadata.ElementMetadata.Type), jArray.GetType());
		Assert.Equal(length, Assert.IsType<ArrayObjectMetadata>(ILocalObject.CreateMetadata(jArray)).Length);
		Assert.Equal(jArray.ToString(), jArray.Object.ToString());
		Assert.Equal($"{jArray.Object.Class.Name} {jArray.As<JObjectArrayLocalRef>()} length: {jArray.Length}",
		             jArray.ToTraceText());

		Assert.Equal(jArray.Object, localView.Object);
		Assert.Equal(jArray.Object, (localView as IViewObject).Object);
		Assert.Equal(jArray.Object, ILocalViewObject.GetObject(jArray));
		Assert.Equal(jArray.Object.Lifetime, localView.Lifetime);
		Assert.Equal(jArray.Object.IsProxy, localView.IsProxy);
		Assert.Equal(jArray.Object.Environment.VirtualMachine, localView.VirtualMachine);

		env.ArrayFeature.Received(initializer is 0 or 3 ? 0 : 1).GetArrayLength(jArray);
		JArrayObjectTests.CastTest(jArray);
		JArrayObjectTests.CollectionTest(jArray);

		if (arrayTypeMetadata.Dimension < 5)
		{
			MethodInfo generic =
				JArrayObjectTests.objectArrayTestInfo.MakeGenericMethod(
					arrayTypeMetadata.GetArrayMetadata()!.ElementMetadata.Type);
			generic.Invoke(null, [initializer,]);
		}

		if (length <= 0 || JLocalObject.IsClassType<TElement>() || typeof(TElement) == typeof(JStringObject)) return;
		JArrayObjectTests.ElementTest(jElementClass, env, jArray, length);

		env.ArrayFeature.CreateArray<TElement>(jArray.Length).Returns(jArray);
		Assert.Equal(jArray, JArrayObject<TElement>.Create(env, jArray.Length));
		env.ArrayFeature.Received(1).CreateArray<TElement>(jArray.Length);

		Assert.Equal(ILocalObject.CreateMetadata(jArray), new ArrayObjectMetadata(ILocalObject.CreateMetadata(jArray)));
	}
	private static void MetadataTest<TElement>() where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JReferenceTypeMetadata elementTypeMetadata = IReferenceType.GetMetadata<TElement>();
		JArrayTypeMetadata arrayTypeMetadata = IArrayType.GetMetadata<JArrayObject<TElement>>();
		String textValue = arrayTypeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JArrayLocalRef arrayRef = JArrayObjectTests.fixture.Create<JArrayLocalRef>();
		JGlobalRef globalRef = JArrayObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jArrayClass = new(jClass, arrayTypeMetadata);
		using JLocalObject jLocal = new(jArrayClass, arrayRef.Value);
		using JGlobal jGlobal = new(env.VirtualMachine, new(jArrayClass), globalRef);

		env.VirtualMachine.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>())
		   .ReturnsForAnyArgs(thread);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(arrayTypeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {arrayTypeMetadata.Hash} }}", textValue);

		Assert.Equal(typeof(JArrayObject), EnvironmentProxy.GetFamilyType<JArrayObject>());
		Assert.Equal(JTypeKind.Array, EnvironmentProxy.GetKind<JArrayObject>());
		Assert.Equal(typeof(JArrayObject), EnvironmentProxy.GetFamilyType<JArrayObject<TElement>>());
		Assert.Equal(JTypeKind.Array, EnvironmentProxy.GetKind<JArrayObject<TElement>>());
		Assert.Equal(elementTypeMetadata is JArrayTypeMetadata, arrayTypeMetadata.Dimension > 1);
		Assert.Equal(arrayTypeMetadata, elementTypeMetadata.GetArrayMetadata());
		Assert.IsType<JFunctionDefinition<JArrayObject<TElement>>>(
			arrayTypeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JArrayObject<TElement>>>(arrayTypeMetadata.CreateFieldDefinition("fieldName"u8));

		env.GetReferenceType(jGlobal).Returns(JReferenceType.GlobalRefType);

		using JArrayObject<TElement> jArray0 =
			Assert.IsType<JArrayObject<TElement>>(arrayTypeMetadata.ParseInstance(jLocal));
		JArrayObject<TElement> jArray1 =
			Assert.IsType<JArrayObject<TElement>>(arrayTypeMetadata.ParseInstance(jArray0));
		using JArrayObject jArray2 =
			Assert.IsAssignableFrom<JArrayObject>(arrayTypeMetadata.ParseInstance(env, jGlobal));
		JArrayObject<JLocalObject> jArray3 = (JArrayObject<JLocalObject>)(JArrayObject)jArray1;
		using JArrayObject<TElement> jArray4 = jLocal.CastTo<JArrayObject<TElement>>();
		using JArrayObject<TElement> jArray5 =
			Assert.IsType<JArrayObject<TElement>>(arrayTypeMetadata.ParseInstance(jLocal, true));
		JArrayObject<TElement> jArray6 =
			Assert.IsType<JArrayObject<TElement>>(arrayTypeMetadata.ParseInstance(jArray0, true));

		Assert.Equal(jArray1.Object, jArray0.Object);
		Assert.Equal(jArray1.Object, jArray6.Object);
		Assert.Equal(JArrayLocalRef.FromReference(globalRef.Value), jArray2.Reference);
		Assert.Equal(arrayRef, jArray5.Object.Reference);
		Assert.Equal(arrayRef, jArray3.Object.Reference);
		Assert.Equal(arrayRef, jArray4.Object.Reference);
		Assert.False(Object.ReferenceEquals(jLocal, jArray4));
		Assert.True(Object.ReferenceEquals(jArray4.Object, jArray4.CastTo<JArrayObject<JLocalObject>>().Object));
		Assert.Null(arrayTypeMetadata.ParseInstance(default));
		Assert.Null(arrayTypeMetadata.ParseInstance(env, default));

		JLocalObject? nullObject = default;
		JArrayObject<TElement>? nullArrayView = (JArrayObject<TElement>?)nullObject;
		Assert.Null(nullArrayView);
	}
	private static void CollectionTest<TElement>(JArrayObject<TElement> jArray)
		where TElement : JReferenceObject, IReferenceType<TElement>
	{
		ICollection<TElement?> collection = jArray;
		Assert.Equal(jArray.Length, collection.Count);
		Assert.True(collection.IsReadOnly);

		jArray.Environment.ArrayFeature.ClearReceivedCalls();
	}
	private static void CastTest<TElement>(JArrayObject<TElement> jArray)
		where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JArrayObject<JLocalObject> jObjectArray = jArray.Object.CastTo<JArrayObject<JLocalObject>>();
		JLocalObject jLocal = (jArray.Object as ILocalObject).CastTo<JLocalObject>();
		JSerializableObject jSerializable = jArray.Object.CastTo<JSerializableObject>();
		JCloneableObject jCloneable = (jArray.Object as ILocalObject).CastTo<JCloneableObject>();

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
		basicElementMetadata.Interfaces.ForEach(default(Object), (_, i) => types.Add(i.Type));
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
	private static void ElementTest<TElement>(JClassObject jElementClass, EnvironmentProxy env,
		JArrayObject<TElement> jArray, Int32 length) where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JReferenceTypeMetadata elementTypeMetadata = IReferenceType.GetMetadata<TElement>();
		JObjectLocalRef localRef = JArrayObjectTests.fixture.Create<JObjectLocalRef>();
		using JLocalObject jLocal = elementTypeMetadata.CreateInstance(jElementClass, localRef, true);
		TElement element = jLocal.CastTo<TElement>();
		Int32 i = 0;
		TElement?[] arr = new TElement[jArray.Length];

		Assert.True(jArray.Object.TypeMetadata.TypeOf(elementTypeMetadata.GetArrayMetadata()!));

		env.ArrayFeature.GetElement(jArray, 0).Returns(element);

		Assert.Equal(element, jArray[0]);
		jArray[^1] = element;

		env.ArrayFeature.Received(1).GetElement(jArray, 0);
		env.ArrayFeature.Received(1).SetElement(jArray, length - 1, element);

		env.ArrayFeature.ClearReceivedCalls();
		env.ArrayFeature.GetElement(jArray, Arg.Any<Int32>()).Returns(default(TElement?));

		foreach (TElement? nullElement in jArray)
		{
			Assert.Null(nullElement);
			env.ArrayFeature.Received(1).GetElement(jArray, i);
			i++;
		}
		Assert.Throws<NotSupportedException>(() => (jArray as ICollection<TElement?>).Add(default));
		Assert.Throws<NotSupportedException>(() => (jArray as ICollection<TElement?>).Clear());
		Assert.Throws<NotSupportedException>(() => (jArray as ICollection<TElement?>).Remove(default));
		Assert.Throws<NotSupportedException>(() => (jArray as IList<TElement?>).Insert(0, default));
		Assert.Throws<NotSupportedException>(() => (jArray as IList<TElement?>).RemoveAt(0));

		env.ArrayFeature.IndexOf(jArray, element).Returns(-1);

		(jArray as IList<TElement?>).CopyTo(arr, 0);
		Assert.Equal(-1, (jArray as IList<TElement?>).IndexOf(element));
		Assert.False((jArray as IList<TElement?>).Contains(element));

		env.ArrayFeature.Received(2).IndexOf(jArray, element);
		env.ArrayFeature.Received(1).CopyTo(jArray, arr, 0);

		env.ArrayFeature.CreateArray(jArray.Length, element).Returns(jArray);
		Assert.Equal(jArray, JArrayObject<TElement>.Create(env, jArray.Length, element));
		env.ArrayFeature.Received(1).CreateArray(jArray.Length, element);

		if (jArray.Object.TypeMetadata.Equals(elementTypeMetadata.GetArrayMetadata()))
			jArray.Object.ValidateObjectElement(element);
		else
			Assert.Throws<InvalidCastException>(() => jArray.Object.ValidateObjectElement(element));
	}
}