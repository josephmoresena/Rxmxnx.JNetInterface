namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JArrayObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void ObjectTest() => JArrayObjectTests.ObjectArrayTest<JLocalObject>();
	[Fact]
	internal void ClassTest() => JArrayObjectTests.ObjectArrayTest<JClassObject>();

	private static void ObjectArrayTest<TElement>(Boolean useConstructor = true)
		where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JReferenceTypeMetadata elementTypeMetadata = IReferenceType.GetMetadata<TElement>();
		JArrayTypeMetadata arrayTypeMetadata = IArrayType.GetMetadata<JArrayObject<TElement>>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JArrayLocalRef arrayRef = JArrayObjectTests.fixture.Create<JArrayLocalRef>();
		Int32 length = Random.Shared.Next(0, 10);
		using JClassObject jClass = new(env);
		using JClassObject jArrayClass = new(jClass, arrayTypeMetadata);
		using JArrayObject<TElement> jArray = useConstructor ?
			new(jArrayClass, arrayRef, length) :
			Assert.IsType<JArrayObject<TElement>>(arrayTypeMetadata.CreateInstance(jArrayClass, arrayRef.Value, true));

		env.ArrayFeature.GetArrayLength(jArray).Returns(length);

		Assert.Equal(typeof(JArrayObject), EnvironmentProxy.GetFamilyType<JArrayObject>());
		Assert.Equal(JTypeKind.Array, EnvironmentProxy.GetKind<JArrayObject>());
		Assert.Equal(typeof(JArrayObject), EnvironmentProxy.GetFamilyType<JArrayObject<TElement>>());
		Assert.Equal(JTypeKind.Array, EnvironmentProxy.GetKind<JArrayObject<TElement>>());
		Assert.Equal(elementTypeMetadata is JArrayTypeMetadata, arrayTypeMetadata.Dimension > 1);
		Assert.Equal(arrayTypeMetadata, elementTypeMetadata.GetArrayMetadata());

		Assert.Equal(length, jArray.Length);
		Assert.Equal(arrayTypeMetadata.ClassName, jArray.ObjectClassName);
		Assert.Equal(arrayTypeMetadata.Signature, jArray.ObjectSignature);
		Assert.Equal(elementTypeMetadata.ArraySignature, jArray.ObjectClassName);
		Assert.Equal(elementTypeMetadata.ArraySignature, jArray.ObjectSignature);
		Assert.Equal(arrayTypeMetadata.Type, jArray.GetType());
		Assert.Equal(typeof(JArrayObject<>).MakeGenericType(elementTypeMetadata.Type), jArray.GetType());

		env.ArrayFeature.Received(useConstructor ? 0 : 1).GetArrayLength(jArray);
		JArrayObjectTests.CastTest(jArray);

		if (arrayTypeMetadata.Dimension < 10)
			JArrayObjectTests.ObjectArrayTest<JArrayObject<TElement>>(useConstructor);
	}
	private static void CastTest<TElement>(JArrayObject<TElement> jArray)
		where TElement : JReferenceObject, IReferenceType<TElement>
	{
		JArrayObject<JLocalObject> jObjectArray = jArray.CastTo<JArrayObject<JLocalObject>>();
		JSerializableObject jSerializable = jArray.CastTo<JSerializableObject>();
		JCloneableObject jCloneable = jArray.CastTo<JCloneableObject>();

		Assert.Equal(jArray.Object, jObjectArray.Object);
		Assert.Equal(jArray.Object, jSerializable.Object);
		Assert.Equal(jArray.Object, jCloneable.Object);
	}
}